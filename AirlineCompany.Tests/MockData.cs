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
            new()
            {
                Id = 1,
                Code = "SU100",
                DepartureCity = "Moscow",
                ArrivalCity = "London",
                DepartureDate = DateTime.Now.AddDays(-5),
                ArrivalDate = DateTime.Now.AddDays(-5).AddHours(4),
                Duration = TimeSpan.FromHours(4),
                AircraftModelId = 1,
                Tickets = []
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
                AircraftModelId = 2,
                Tickets = []
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
                AircraftModelId = 3,
                Tickets = []
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
                AircraftModelId = 4,
                Tickets = []
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
                AircraftModelId = 5,
                Tickets = []
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
                AircraftModelId = 6,
                Tickets = []
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
                AircraftModelId = 7,
                Tickets = []
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
                AircraftModelId = 8,
                Tickets = []
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
                AircraftModelId = 9,
                Tickets = []
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
                AircraftModelId = 10,
                Tickets = []
            }
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
    public static List<Ticket> GetTickets(List<Flight> flights, List<Passenger> passengers)
    {
        var flightDict = flights.ToDictionary(f => f.Id);
        var passengerDict = passengers.ToDictionary(p => p.Id);

        var tickets = new List<Ticket>();

        Ticket CreateTicket(int id, string seat, bool hasLuggage, double weight, int flightId, int passengerId)
        {
            var flight = flightDict[flightId];
            var passenger = passengerDict[passengerId];

            var ticket = new Ticket
            {
                Id = id,
                SeatNumber = seat,
                HasHandLuggage = hasLuggage,
                BaggageWeight = weight,
                FlightId = flightId,
                Flight = flight,
                PassengerId = passengerId,
                Passenger = passenger
            };

            flight.Tickets.Add(ticket);
            passenger.Tickets.Add(ticket);

            return ticket;
        }

        // Flight 1: 3 passengers
        tickets.Add(CreateTicket(1, "10A", true, 15.5, 1, 1));
        tickets.Add(CreateTicket(2, "10B", false, 0, 1, 2));
        tickets.Add(CreateTicket(3, "10C", true, 10.0, 1, 3));

        // Flight 2: 3 passengers
        tickets.Add(CreateTicket(4, "15A", true, 12.0, 2, 4));
        tickets.Add(CreateTicket(5, "15B", true, 8.5, 2, 5));
        tickets.Add(CreateTicket(6, "15C", false, 0, 2, 6));

        // Flight 3: 2 passengers
        tickets.Add(CreateTicket(7, "20A", true, 20.0, 3, 7));
        tickets.Add(CreateTicket(8, "20B", true, 5.5, 3, 8));

        // Flight 4: 4 passengers
        tickets.Add(CreateTicket(9, "25A", false, 0, 4, 9));
        tickets.Add(CreateTicket(10, "25B", true, 18.0, 4, 10));
        tickets.Add(CreateTicket(11, "25C", true, 7.5, 4, 1));
        tickets.Add(CreateTicket(12, "25D", false, 0, 4, 2));

        // Flight 5: 1 passenger
        tickets.Add(CreateTicket(13, "30A", true, 9.0, 5, 3));

        // Flight 6: 3 passengers
        tickets.Add(CreateTicket(14, "35A", true, 11.0, 6, 4));
        tickets.Add(CreateTicket(15, "35B", false, 0, 6, 5));
        tickets.Add(CreateTicket(16, "35C", true, 6.5, 6, 6));

        // Flight 7: 2 passengers
        tickets.Add(CreateTicket(17, "40A", true, 14.0, 7, 7));
        tickets.Add(CreateTicket(18, "40B", false, 0, 7, 8));

        // Flight 8: 1 passenger
        tickets.Add(CreateTicket(19, "45A", true, 16.5, 8, 9));

        // Flight 9: 2 passengers
        tickets.Add(CreateTicket(20, "50A", true, 13.0, 9, 10));
        tickets.Add(CreateTicket(21, "50B", false, 0, 9, 1));

        // Flight 10: 3 passengers
        tickets.Add(CreateTicket(22, "55A", true, 8.0, 10, 2));
        tickets.Add(CreateTicket(23, "55B", true, 19.5, 10, 3));
        tickets.Add(CreateTicket(24, "55C", false, 0, 10, 4));

        return tickets;
    }
}