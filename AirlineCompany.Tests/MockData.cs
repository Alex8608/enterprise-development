using AirlineCompany.Core.Entities;

namespace AirlineCompany.Tests;

/// <summary>
/// Provides mock data for unit testing airline company domain logic.
/// Contains sample data for all entities with proper relationships.
/// </summary>
public static class MockData
{
    /// <summary>
    /// Gets the collection of aircraft families.
    /// </summary>
    /// <returns>List of 10 aircraft families from major manufacturers.</returns>
    public static List<AircraftFamily> GetAircraftFamilies()
    {
        return
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
    }

    /// <summary>
    /// Gets the collection of aircraft models with technical specifications.
    /// </summary>
    /// <returns>List of 10 aircraft models with complete technical data.</returns>
    public static List<AircraftModel> GetAircraftModels()
    {
        return
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
    }

    /// <summary>
    /// Gets the collection of flights with international routes.
    /// </summary>
    /// <returns>List of 10 flights covering major international routes.</returns>
    public static List<Flight> GetFlights()
    {
        return
        [
            new Flight(1, "SU100", "Moscow", "London", DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5).AddHours(4), TimeSpan.FromHours(4), 1),
            new Flight(2, "SU200", "Moscow", "Paris", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-3).AddHours(3.5), TimeSpan.FromHours(3.5), 2),
            new Flight(3, "SU300", "London", "New York", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-2).AddHours(8), TimeSpan.FromHours(8), 3),
            new Flight(4, "SU400", "Paris", "Tokyo", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddHours(12), TimeSpan.FromHours(12), 4),
            new Flight(5, "SU500", "Berlin", "Dubai", DateTime.Now, DateTime.Now.AddHours(6), TimeSpan.FromHours(6), 5),
            new Flight(6, "SU600", "Dubai", "Singapore", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(7), TimeSpan.FromHours(7), 6),
            new Flight(7, "SU700", "Singapore", "Sydney", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(8), TimeSpan.FromHours(8), 7),
            new Flight(8, "SU800", "Sydney", "Los Angeles", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(14), TimeSpan.FromHours(14), 8),
            new Flight(9, "SU900", "Los Angeles", "Tokyo", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(11), TimeSpan.FromHours(11), 9),
            new Flight(10, "SU1000", "Tokyo", "Moscow", DateTime.Now.AddDays(5), DateTime.Now.AddDays(5).AddHours(10), TimeSpan.FromHours(10), 10)
        ];
    }

    /// <summary>
    /// Gets the collection of passengers with valid passport numbers.
    /// </summary>
    /// <returns>List of 10 passengers with unique passport numbers.</returns>
    public static List<Passenger> GetPassengers()
    {
        return
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
    }

    /// <summary>
    /// Gets the collection of tickets with established relationships.
    /// Creates relationships between tickets, flights, and passengers.
    /// </summary>
    /// <returns>List of 24 tickets with proper flight and passenger relationships.</returns>
    public static List<Ticket> GetTickets()
    {
        var flights = GetFlights();
        var passengers = GetPassengers();

        var tickets = new List<Ticket>
        {
            // Flight 1: 3 passengers
            new(1, "10A", true, 15.5, 1, flights[0], 1, passengers[0]),
            new(2, "10B", false, 0, 1, flights[0], 2, passengers[1]),
            new(3, "10C", true, 10.0, 1, flights[0], 3, passengers[2]),

            // Flight 2: 3 passengers
            new(4, "15A", true, 12.0, 2, flights[1], 4, passengers[3]),
            new(5, "15B", true, 8.5, 2, flights[1], 5, passengers[4]),
            new(6, "15C", false, 0, 2, flights[1], 6, passengers[5]),

            // Flight 3: 2 passengers
            new(7, "20A", true, 20.0, 3, flights[2], 7, passengers[6]),
            new(8, "20B", true, 5.5, 3, flights[2], 8, passengers[7]),

            // Flight 4: 4 passengers
            new(9, "25A", false, 0, 4, flights[3], 9, passengers[8]),
            new(10, "25B", true, 18.0, 4, flights[3], 10, passengers[9]),
            new(11, "25C", true, 7.5, 4, flights[3], 1, passengers[0]),
            new(12, "25D", false, 0, 4, flights[3], 2, passengers[1]),

            // Flight 5: 1 passenger
            new(13, "30A", true, 9.0, 5, flights[4], 3, passengers[2]),

            // Flight 6: 3 passengers
            new(14, "35A", true, 11.0, 6, flights[5], 4, passengers[3]),
            new(15, "35B", false, 0, 6, flights[5], 5, passengers[4]),
            new(16, "35C", true, 6.5, 6, flights[5], 6, passengers[5]),

            // Flight 7: 2 passengers
            new(17, "40A", true, 14.0, 7, flights[6], 7, passengers[6]),
            new(18, "40B", false, 0, 7, flights[6], 8, passengers[7]),

            // Flight 8: 1 passenger
            new(19, "45A", true, 16.5, 8, flights[7], 9, passengers[8]),

            // Flight 9: 2 passengers
            new(20, "50A", true, 13.0, 9, flights[8], 10, passengers[9]),
            new(21, "50B", false, 0, 9, flights[8], 1, passengers[0]),

            // Flight 10: 3 passengers
            new(22, "55A", true, 8.0, 10, flights[9], 2, passengers[1]),
            new(23, "55B", true, 19.5, 10, flights[9], 3, passengers[2]),
            new(24, "55C", false, 0, 10, flights[9], 4, passengers[3])
        };

        foreach (var ticket in tickets)
        {
            ticket.Flight.Tickets.Add(ticket);
            ticket.Passenger.Tickets.Add(ticket);
        }

        return tickets;
    }
    public class MockDataFixture
    {
        public List<AircraftFamily> AircraftFamilies { get; private set; }
        public List<AircraftModel> AircraftModels { get; private set; }
        public List<Flight> Flights { get; private set; }
        public List<Passenger> Passengers { get; private set; }
        public List<Ticket> Tickets { get; private set; }

        public MockDataFixture()
        {
            AircraftFamilies = MockData.GetAircraftFamilies();
            AircraftModels = MockData.GetAircraftModels();
            Flights = MockData.GetFlights();
            Passengers = MockData.GetPassengers();
            Tickets = MockData.GetTickets();
        }
    }
}