using AirlineCompany.Api.Kafka;
using AirlineCompany.Application.Services;
using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Dto.Services;
using AirlineCompany.Infrastructure.Data;
using AirlineCompany.Infrastructure.Repositories;
using AirlineCompany.ServiceDefaults;
using Confluent.Kafka;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddScoped<IRepository<AircraftFamily>, AircraftFamilyRepository>();
builder.Services.AddScoped<IRepository<AircraftModel>, AircraftModelRepository>();
builder.Services.AddScoped<IRepository<Flight>, FlightRepository>();
builder.Services.AddScoped<IRepository<Passenger>, PassengerRepository>();
builder.Services.AddScoped<IRepository<Ticket>, TicketRepository>();

builder.Services.AddScoped<IAircraftFamilyService, AircraftFamilyService>();
builder.Services.AddScoped<IAircraftModelService, AircraftModelService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAnalyticService, AnalyticService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var kafkaConnection = builder.Configuration.GetConnectionString("KafkaConnection")
    ?? "localhost:9092";

builder.Services.AddSingleton<IConsumer<Ignore, string>>(sp =>
{
    var config = new ConsumerConfig
    {
        BootstrapServers = kafkaConnection,
        GroupId = builder.Configuration["Kafka:GroupId"] ?? "airline-company-api-group",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = false,
        EnableAutoOffsetStore = false
    };
    return new ConsumerBuilder<Ignore, string>(config).Build();
});

builder.Services.AddHostedService<KafkaConsumer>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        logger.LogError(exception, "Unhandled exception occurred");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "An internal server error has occurred",
            TraceId = context.TraceIdentifier
        });
    });
});

app.UseStatusCodePages();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        await db.Database.EnsureDeletedAsync();
        logger.LogInformation("Database deleted");

        await db.Database.MigrateAsync();
        logger.LogInformation("Database migrated");

        logger.LogInformation("Starting data seeding...");
        await DataSeeder.SeedAsync(db);
        logger.LogInformation("Data seeding completed");

        var flightCount = await db.Flights.CountAsync();
        var passengerCount = await db.Passengers.CountAsync();
        var ticketCount = await db.Tickets.CountAsync();

        logger.LogInformation("Seed data: {Flights} flights, {Passengers} passengers, {Tickets} tickets",
            flightCount, passengerCount, ticketCount);

        var flightIds = await db.Flights.Select(f => f.Id).ToListAsync();
        var passengerIds = await db.Passengers.Select(p => p.Id).ToListAsync();

        logger.LogInformation("Available Flight IDs: {FlightIds}", string.Join(", ", flightIds));
        logger.LogInformation("Available Passenger IDs: {PassengerIds}", string.Join(", ", passengerIds));
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();