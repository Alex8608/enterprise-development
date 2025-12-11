using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Infrastructure.Data;
using AirlineCompany.Infrastructure.Repositories;
using AirlineCompany.ServiceDefaults;
using AirlineCompany.Application.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddScoped<IRepository<AircraftFamily>, AircraftFamilyRepository>();
builder.Services.AddScoped<IRepository<AircraftModel>, AircraftModelRepository>();
builder.Services.AddScoped<IRepository<Flight>, FlightRepository>();
builder.Services.AddScoped<IRepository<Passenger>, PassengerRepository>();
builder.Services.AddScoped<IRepository<Ticket>, TicketRepository>();

builder.Services.AddScoped<AircraftFamilyService>();
builder.Services.AddScoped<AircraftModelService>();
builder.Services.AddScoped<PassengerService>();
builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<AnalyticService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                     ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureDeleted();
    db.Database.Migrate();
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