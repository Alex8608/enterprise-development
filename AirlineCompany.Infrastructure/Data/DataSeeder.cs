using AirlineCompany.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirlineCompany.Infrastructure.Data;

/// <summary>
/// Provides seed data for the database
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Seeds the database with initial data
    /// </summary>
    /// <param name="context">Database context</param>
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AircraftFamilies ON");
        if (!await context.AircraftFamilies.AnyAsync())
        {
            context.AircraftFamilies.AddRange(AircraftFamilies);
            await context.SaveChangesAsync();
        }
        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AircraftFamilies OFF");

        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AircraftModels ON");
        if (!await context.AircraftModels.AnyAsync())
        {
            context.AircraftModels.AddRange(AircraftModels);
            await context.SaveChangesAsync();
        }
        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT AircraftModels OFF");

        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Flights ON");
        if (!await context.Flights.AnyAsync())
        {
            context.Flights.AddRange(Flights);
            await context.SaveChangesAsync();
        }
        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Flights OFF");

        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Passengers ON");
        if (!await context.Passengers.AnyAsync())
        {
            context.Passengers.AddRange(Passengers);
            await context.SaveChangesAsync();
        }
        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Passengers OFF");

        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Tickets ON");
        if (!await context.Tickets.AnyAsync())
        {
            context.Tickets.AddRange(Tickets);
            await context.SaveChangesAsync();
        }
        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Tickets OFF");
    }
    /// <summary>
    /// Gets seed data for aircraft families
    /// </summary>
    public static List<AircraftFamily> AircraftFamilies =>
    [
        new() { Id = 1, Name = "A320", Manufacturer = "Airbus" },
        new() { Id = 2, Name = "737", Manufacturer = "Boeing" },
        new() { Id = 3, Name = "A330", Manufacturer = "Airbus" },
        new() { Id = 4, Name = "777", Manufacturer = "Boeing" },
        new() { Id = 5, Name = "A350", Manufacturer = "Airbus" },
        new() { Id = 6, Name = "787", Manufacturer = "Boeing" },
        new() { Id = 7, Name = "CRJ", Manufacturer = "Bombardier" },
        new() { Id = 8, Name = "E-Jet", Manufacturer = "Embraer" },
        new() { Id = 9, Name = "A220", Manufacturer = "Airbus" },
        new() { Id = 10, Name = "747", Manufacturer = "Boeing" }
    ];

    /// <summary>
    /// Gets seed data for aircraft models
    /// </summary>
    public static List<AircraftModel> AircraftModels =>
    [
        new() { Id = 1, Name = "A320-200", Range = 6100, PassengerCapacity = 180, CargoCapacity = 4.5, AircraftFamilyId = 1 },
        new() { Id = 2, Name = "A321neo", Range = 7400, PassengerCapacity = 240, CargoCapacity = 5.2, AircraftFamilyId = 1 },
        new() { Id = 3, Name = "737-800", Range = 5765, PassengerCapacity = 189, CargoCapacity = 4.8, AircraftFamilyId = 2 },
        new() { Id = 4, Name = "737 MAX 8", Range = 6570, PassengerCapacity = 210, CargoCapacity = 5.1, AircraftFamilyId = 2 },
        new() { Id = 5, Name = "A330-300", Range = 10800, PassengerCapacity = 440, CargoCapacity = 12.5, AircraftFamilyId = 3 },
        new() { Id = 6, Name = "777-300ER", Range = 13650, PassengerCapacity = 550, CargoCapacity = 16.5, AircraftFamilyId = 4 },
        new() { Id = 7, Name = "A350-900", Range = 15000, PassengerCapacity = 440, CargoCapacity = 14.5, AircraftFamilyId = 5 },
        new() { Id = 8, Name = "787-9", Range = 14140, PassengerCapacity = 420, CargoCapacity = 13.5, AircraftFamilyId = 6 },
        new() { Id = 9, Name = "CRJ-900", Range = 2870, PassengerCapacity = 90, CargoCapacity = 2.5, AircraftFamilyId = 7 },
        new() { Id = 10, Name = "E195-E2", Range = 4815, PassengerCapacity = 146, CargoCapacity = 3.8, AircraftFamilyId = 8 }
    ];

    /// <summary>
    /// Gets seed data for flights
    /// </summary>
    public static List<Flight> Flights =>
    [
        new()
        {
            Id = 1,
            Code = "SU100",
            DepartureCity = "Moscow",
            ArrivalCity = "London",
            DepartureDate = DateTime.Now.AddDays(-5),
            ArrivalDate = DateTime.Now.AddDays(-5).AddHours(4),
            Duration = TimeSpan.FromHours(4),
            AircraftModelId = 1
        },
        new()
        {
            Id = 2,
            Code = "SU200",
            DepartureCity = "Moscow",
            ArrivalCity = "Paris",
            DepartureDate = DateTime.Now.AddDays(-3),
            ArrivalDate = DateTime.Now.AddDays(-3).AddHours(3.5),
            Duration = TimeSpan.FromHours(3.5),
            AircraftModelId = 2
        },
        new()
        {
            Id = 3,
            Code = "SU300",
            DepartureCity = "London",
            ArrivalCity = "New York",
            DepartureDate = DateTime.Now.AddDays(-2),
            ArrivalDate = DateTime.Now.AddDays(-2).AddHours(8),
            Duration = TimeSpan.FromHours(8),
            AircraftModelId = 3
        },
        new()
        {
            Id = 4,
            Code = "SU400",
            DepartureCity = "Paris",
            ArrivalCity = "Tokyo",
            DepartureDate = DateTime.Now.AddDays(-1),
            ArrivalDate = DateTime.Now.AddDays(-1).AddHours(12),
            Duration = TimeSpan.FromHours(12),
            AircraftModelId = 4
        },
        new()
        {
            Id = 5,
            Code = "SU500",
            DepartureCity = "Berlin",
            ArrivalCity = "Dubai",
            DepartureDate = DateTime.Now,
            ArrivalDate = DateTime.Now.AddHours(6),
            Duration = TimeSpan.FromHours(6),
            AircraftModelId = 5
        },
        new()
        {
            Id = 6,
            Code = "SU600",
            DepartureCity = "Dubai",
            ArrivalCity = "Singapore",
            DepartureDate = DateTime.Now.AddDays(1),
            ArrivalDate = DateTime.Now.AddDays(1).AddHours(7),
            Duration = TimeSpan.FromHours(7),
            AircraftModelId = 6
        },
        new()
        {
            Id = 7,
            Code = "SU700",
            DepartureCity = "Singapore",
            ArrivalCity = "Sydney",
            DepartureDate = DateTime.Now.AddDays(2),
            ArrivalDate = DateTime.Now.AddDays(2).AddHours(8),
            Duration = TimeSpan.FromHours(8),
            AircraftModelId = 7
        },
        new()
        {
            Id = 8,
            Code = "SU800",
            DepartureCity = "Sydney",
            ArrivalCity = "Los Angeles",
            DepartureDate = DateTime.Now.AddDays(3),
            ArrivalDate = DateTime.Now.AddDays(3).AddHours(14),
            Duration = TimeSpan.FromHours(14),
            AircraftModelId = 8
        },
        new()
        {
            Id = 9,
            Code = "SU900",
            DepartureCity = "Los Angeles",
            ArrivalCity = "Tokyo",
            DepartureDate = DateTime.Now.AddDays(4),
            ArrivalDate = DateTime.Now.AddDays(4).AddHours(11),
            Duration = TimeSpan.FromHours(11),
            AircraftModelId = 9
        },
        new()
        {
            Id = 10,
            Code = "SU1000",
            DepartureCity = "Tokyo",
            ArrivalCity = "Moscow",
            DepartureDate = DateTime.Now.AddDays(5),
            ArrivalDate = DateTime.Now.AddDays(5).AddHours(10),
            Duration = TimeSpan.FromHours(10),
            AircraftModelId = 10
        }
    ];

    /// <summary>
    /// Gets seed data for passengers
    /// </summary>
    public static List<Passenger> Passengers =>
    [
        new() { Id = 1, PassportNumber = "3600-123456", FullName = "Ivanov Ivan Ivanovich", DateOfBirth = new DateTime(1980, 5, 15) },
        new() { Id = 2, PassportNumber = "3605-654321", FullName = "Petrov Petr Petrovich", DateOfBirth = new DateTime(1990, 8, 22) },
        new() { Id = 3, PassportNumber = "3610-987654", FullName = "Sidorova Anna Sergeevna", DateOfBirth = new DateTime(1985, 3, 10) },
        new() { Id = 4, PassportNumber = "3615-456789", FullName = "Kuznetsov Alexey Vladimirovich", DateOfBirth = new DateTime(1978, 11, 5) },
        new() { Id = 5, PassportNumber = "3620-135790", FullName = "Smirnova Elena Dmitrievna", DateOfBirth = new DateTime(1992, 7, 30) },
        new() { Id = 6, PassportNumber = "3624-246801", FullName = "Popov Mikhail Igorevich", DateOfBirth = new DateTime(1988, 2, 14) },
        new() { Id = 7, PassportNumber = "3602-112233", FullName = "Volkova Olga Nikolaevna", DateOfBirth = new DateTime(1983, 9, 18) },
        new() { Id = 8, PassportNumber = "3608-445566", FullName = "Novikov Dmitry Andreevich", DateOfBirth = new DateTime(1995, 12, 3) },
        new() { Id = 9, PassportNumber = "3612-778899", FullName = "Fedorova Maria Pavlovna", DateOfBirth = new DateTime(1987, 6, 25) },
        new() { Id = 10, PassportNumber = "3618-990011", FullName = "Orlov Sergey Viktorovich", DateOfBirth = new DateTime(1975, 4, 8) }
    ];

    /// <summary>
    /// Gets seed data for tickets
    /// </summary>
    public static List<Ticket> Tickets =>
    [
        new() { Id = 1, SeatNumber = "10A", HasHandLuggage = true, BaggageWeight = 15.5, FlightId = 1, PassengerId = 1 },
        new() { Id = 2, SeatNumber = "10B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 1, PassengerId = 2 },
        new() { Id = 3, SeatNumber = "10C", HasHandLuggage = true, BaggageWeight = 10.0, FlightId = 1, PassengerId = 3 },

        new() { Id = 4, SeatNumber = "15A", HasHandLuggage = true, BaggageWeight = 12.0, FlightId = 2, PassengerId = 4 },
        new() { Id = 5, SeatNumber = "15B", HasHandLuggage = true, BaggageWeight = 8.5, FlightId = 2, PassengerId = 5 },
        new() { Id = 6, SeatNumber = "15C", HasHandLuggage = false, BaggageWeight = 0, FlightId = 2, PassengerId = 6 },

        new() { Id = 7, SeatNumber = "20A", HasHandLuggage = true, BaggageWeight = 20.0, FlightId = 3, PassengerId = 7 },
        new() { Id = 8, SeatNumber = "20B", HasHandLuggage = true, BaggageWeight = 5.5, FlightId = 3, PassengerId = 8 },

        new() { Id = 9, SeatNumber = "25A", HasHandLuggage = false, BaggageWeight = 0, FlightId = 4, PassengerId = 9 },
        new() { Id = 10, SeatNumber = "25B", HasHandLuggage = true, BaggageWeight = 18.0, FlightId = 4, PassengerId = 10 },
        new() { Id = 11, SeatNumber = "25C", HasHandLuggage = true, BaggageWeight = 7.5, FlightId = 4, PassengerId = 1 },
        new() { Id = 12, SeatNumber = "25D", HasHandLuggage = false, BaggageWeight = 0, FlightId = 4, PassengerId = 2 },

        new() { Id = 13, SeatNumber = "30A", HasHandLuggage = true, BaggageWeight = 9.0, FlightId = 5, PassengerId = 3 },

        new() { Id = 14, SeatNumber = "35A", HasHandLuggage = true, BaggageWeight = 11.0, FlightId = 6, PassengerId = 4 },
        new() { Id = 15, SeatNumber = "35B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 6, PassengerId = 5 },
        new() { Id = 16, SeatNumber = "35C", HasHandLuggage = true, BaggageWeight = 6.5, FlightId = 6, PassengerId = 6 },

        new() { Id = 17, SeatNumber = "40A", HasHandLuggage = true, BaggageWeight = 14.0, FlightId = 7, PassengerId = 7 },
        new() { Id = 18, SeatNumber = "40B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 7, PassengerId = 8 },

        new() { Id = 19, SeatNumber = "45A", HasHandLuggage = true, BaggageWeight = 16.5, FlightId = 8, PassengerId = 9 },

        new() { Id = 20, SeatNumber = "50A", HasHandLuggage = true, BaggageWeight = 13.0, FlightId = 9, PassengerId = 10 },
        new() { Id = 21, SeatNumber = "50B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 9, PassengerId = 1 },

        new() { Id = 22, SeatNumber = "55A", HasHandLuggage = true, BaggageWeight = 8.0, FlightId = 10, PassengerId = 2 },
        new() { Id = 23, SeatNumber = "55B", HasHandLuggage = true, BaggageWeight = 19.5, FlightId = 10, PassengerId = 3 },
        new() { Id = 24, SeatNumber = "55C", HasHandLuggage = false, BaggageWeight = 0, FlightId = 10, PassengerId = 4 }
    ];
}