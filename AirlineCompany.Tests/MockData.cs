using AirlineCompany.Core.Entities;

namespace AirlineCompany.Tests;

/// <summary>
/// Provides mock data for unit testing airline company domain logic.
/// Contains sample data for all entities.
/// </summary>
public static class MockData
{
    /// <summary>
    /// Generates a collection of aircraft families with major manufacturers.
    /// </summary>
    /// <returns>List of 10 aircraft families from Airbus, Boeing, Bombardier, and Embraer.</returns>
    public static List<AircraftFamily> GetAircraftFamilies()
    {
        return new List<AircraftFamily>
        {
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
        };
    }

    /// <summary>
    /// Generates aircraft models with technical specifications linked to families.
    /// Includes range, capacity, and cargo information for testing.
    /// </summary>
    /// <returns>List of 10 aircraft models with complete technical data.</returns>
    public static List<AircraftModel> GetAircraftModels()
    {
        return new List<AircraftModel>
        {
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
        };
    }

    /// <summary>
    /// Generates flight schedule with international routes and varying durations.
    /// Includes both past and future flights for comprehensive testing.
    /// </summary>
    /// <returns>List of 10 flights covering major international routes.</returns>
    public static List<Flight> GetFlights()
    {
        return new List<Flight>
        {
            new() { Id = 1, Code = "SU100", DepartureCity = "Moscow", ArrivalCity = "London", DepartureDate = DateTime.Now.AddDays(-5), ArrivalDate = DateTime.Now.AddDays(-5).AddHours(4), Duration = TimeSpan.FromHours(4), AircraftModelId = 1 },
            new() { Id = 2, Code = "SU200", DepartureCity = "Moscow", ArrivalCity = "Paris", DepartureDate = DateTime.Now.AddDays(-3), ArrivalDate = DateTime.Now.AddDays(-3).AddHours(3.5), Duration = TimeSpan.FromHours(3.5), AircraftModelId = 2 },
            new() { Id = 3, Code = "SU300", DepartureCity = "London", ArrivalCity = "New York", DepartureDate = DateTime.Now.AddDays(-2), ArrivalDate = DateTime.Now.AddDays(-2).AddHours(8), Duration = TimeSpan.FromHours(8), AircraftModelId = 3 },
            new() { Id = 4, Code = "SU400", DepartureCity = "Paris", ArrivalCity = "Tokyo", DepartureDate = DateTime.Now.AddDays(-1), ArrivalDate = DateTime.Now.AddDays(-1).AddHours(12), Duration = TimeSpan.FromHours(12), AircraftModelId = 4 },
            new() { Id = 5, Code = "SU500", DepartureCity = "Berlin", ArrivalCity = "Dubai", DepartureDate = DateTime.Now, ArrivalDate = DateTime.Now.AddHours(6), Duration = TimeSpan.FromHours(6), AircraftModelId = 5 },
            new() { Id = 6, Code = "SU600", DepartureCity = "Dubai", ArrivalCity = "Singapore", DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(1).AddHours(7), Duration = TimeSpan.FromHours(7), AircraftModelId = 6 },
            new() { Id = 7, Code = "SU700", DepartureCity = "Singapore", ArrivalCity = "Sydney", DepartureDate = DateTime.Now.AddDays(2), ArrivalDate = DateTime.Now.AddDays(2).AddHours(8), Duration = TimeSpan.FromHours(8), AircraftModelId = 7 },
            new() { Id = 8, Code = "SU800", DepartureCity = "Sydney", ArrivalCity = "Los Angeles", DepartureDate = DateTime.Now.AddDays(3), ArrivalDate = DateTime.Now.AddDays(3).AddHours(14), Duration = TimeSpan.FromHours(14), AircraftModelId = 8 },
            new() { Id = 9, Code = "SU900", DepartureCity = "Los Angeles", ArrivalCity = "Tokyo", DepartureDate = DateTime.Now.AddDays(4), ArrivalDate = DateTime.Now.AddDays(4).AddHours(11), Duration = TimeSpan.FromHours(11), AircraftModelId = 9 },
            new() { Id = 10, Code = "SU1000", DepartureCity = "Tokyo", ArrivalCity = "Moscow", DepartureDate = DateTime.Now.AddDays(5), ArrivalDate = DateTime.Now.AddDays(5).AddHours(10), Duration = TimeSpan.FromHours(10), AircraftModelId = 10 }
        };
    }

    /// <summary>
    /// Generates passenger data.
    /// </summary>
    /// <returns>List of 10 passengers with unique passport numbers.</returns>
    public static List<Passenger> GetPassengers()
    {
        return new List<Passenger>
        {
            new() { PassportNumber = "3600-123456", FullName = "Ivanov Ivan Ivanovich", DateOfBirth = new DateTime(1980, 5, 15) },
            new() { PassportNumber = "3605-654321", FullName = "Petrov Petr Petrovich", DateOfBirth = new DateTime(1990, 8, 22) },
            new() { PassportNumber = "3610-987654", FullName = "Sidorova Anna Sergeevna", DateOfBirth = new DateTime(1985, 3, 10) },
            new() { PassportNumber = "3615-456789", FullName = "Kuznetsov Alexey Vladimirovich", DateOfBirth = new DateTime(1978, 11, 5) },
            new() { PassportNumber = "3620-135790", FullName = "Smirnova Elena Dmitrievna", DateOfBirth = new DateTime(1992, 7, 30) },
            new() { PassportNumber = "3624-246801", FullName = "Popov Mikhail Igorevich", DateOfBirth = new DateTime(1988, 2, 14) },
            new() { PassportNumber = "3602-112233", FullName = "Volkova Olga Nikolaevna", DateOfBirth = new DateTime(1983, 9, 18) },
            new() { PassportNumber = "3608-445566", FullName = "Novikov Dmitry Andreevich", DateOfBirth = new DateTime(1995, 12, 3) },
            new() { PassportNumber = "3612-778899", FullName = "Fedorova Maria Pavlovna", DateOfBirth = new DateTime(1987, 6, 25) },
            new() { PassportNumber = "3618-990011", FullName = "Orlov Sergey Viktorovich", DateOfBirth = new DateTime(1975, 4, 8) }
        };
    }

    /// <summary>
    /// Generates ticket bookings with seat assignments and baggage weights.
    /// Creates complex relationships between passengers and flights for testing queries.
    /// </summary>
    /// <returns>List of 24 tickets with varied baggage scenarios and seat assignments.</returns>
    public static List<Ticket> GetTickets()
    {
        var flights = GetFlights();
        var passengers = GetPassengers();

        return new List<Ticket>
        {
            // Flight 1: 3 passengers
            new() { Id = 1, SeatNumber = "10A", HasHandLuggage = true, BaggageWeight = 15.5, FlightId = 1, PassengerPassportNumber = "3600-123456" },
            new() { Id = 2, SeatNumber = "10B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 1, PassengerPassportNumber = "3605-654321" },
            new() { Id = 3, SeatNumber = "10C", HasHandLuggage = true, BaggageWeight = 10.0, FlightId = 1, PassengerPassportNumber = "3610-987654" },

            // Flight 2: 3 passengers
            new() { Id = 4, SeatNumber = "15A", HasHandLuggage = true, BaggageWeight = 12.0, FlightId = 2, PassengerPassportNumber = "3615-456789" },
            new() { Id = 5, SeatNumber = "15B", HasHandLuggage = true, BaggageWeight = 8.5, FlightId = 2, PassengerPassportNumber = "3620-135790" },
            new() { Id = 6, SeatNumber = "15C", HasHandLuggage = false, BaggageWeight = 0, FlightId = 2, PassengerPassportNumber = "3624-246801" },

            // Flight 3: 2 passengers
            new() { Id = 7, SeatNumber = "20A", HasHandLuggage = true, BaggageWeight = 20.0, FlightId = 3, PassengerPassportNumber = "3602-112233" },
            new() { Id = 8, SeatNumber = "20B", HasHandLuggage = true, BaggageWeight = 5.5, FlightId = 3, PassengerPassportNumber = "3608-445566" },

            // Flight 4: 4 passengers
            new() { Id = 9, SeatNumber = "25A", HasHandLuggage = false, BaggageWeight = 0, FlightId = 4, PassengerPassportNumber = "3612-778899" },
            new() { Id = 10, SeatNumber = "25B", HasHandLuggage = true, BaggageWeight = 18.0, FlightId = 4, PassengerPassportNumber = "3618-990011" },
            new() { Id = 11, SeatNumber = "25C", HasHandLuggage = true, BaggageWeight = 7.5, FlightId = 4, PassengerPassportNumber = "3600-123456" },
            new() { Id = 12, SeatNumber = "25D", HasHandLuggage = false, BaggageWeight = 0, FlightId = 4, PassengerPassportNumber = "3605-654321" },

            // Flight 5: 1 passenger
            new() { Id = 13, SeatNumber = "30A", HasHandLuggage = true, BaggageWeight = 9.0, FlightId = 5, PassengerPassportNumber = "3610-987654" },

            // Flight 6: 3 passengers
            new() { Id = 14, SeatNumber = "35A", HasHandLuggage = true, BaggageWeight = 11.0, FlightId = 6, PassengerPassportNumber = "3615-456789" },
            new() { Id = 15, SeatNumber = "35B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 6, PassengerPassportNumber = "3620-135790" },
            new() { Id = 16, SeatNumber = "35C", HasHandLuggage = true, BaggageWeight = 6.5, FlightId = 6, PassengerPassportNumber = "3624-246801" },

            // Flight 7: 2 passengers
            new() { Id = 17, SeatNumber = "40A", HasHandLuggage = true, BaggageWeight = 14.0, FlightId = 7, PassengerPassportNumber = "3602-112233" },
            new() { Id = 18, SeatNumber = "40B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 7, PassengerPassportNumber = "3608-445566" },

            // Flight 8: 1 passenger
            new() { Id = 19, SeatNumber = "45A", HasHandLuggage = true, BaggageWeight = 16.5, FlightId = 8, PassengerPassportNumber = "3612-778899" },

            // Flight 9: 2 passengers
            new() { Id = 20, SeatNumber = "50A", HasHandLuggage = true, BaggageWeight = 13.0, FlightId = 9, PassengerPassportNumber = "3618-990011" },
            new() { Id = 21, SeatNumber = "50B", HasHandLuggage = false, BaggageWeight = 0, FlightId = 9, PassengerPassportNumber = "3600-123456" },

            // Flight 10: 3 passengers
            new() { Id = 22, SeatNumber = "55A", HasHandLuggage = true, BaggageWeight = 8.0, FlightId = 10, PassengerPassportNumber = "3605-654321" },
            new() { Id = 23, SeatNumber = "55B", HasHandLuggage = true, BaggageWeight = 19.5, FlightId = 10, PassengerPassportNumber = "3610-987654" },
            new() { Id = 24, SeatNumber = "55C", HasHandLuggage = false, BaggageWeight = 0, FlightId = 10, PassengerPassportNumber = "3615-456789" }
        };
    }
}