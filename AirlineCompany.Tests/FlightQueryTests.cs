using AirlineCompany.Core.Entities;

namespace AirlineCompany.Tests;

/// <summary>
/// Contains unit tests for airline company data queries using LINQ.
/// </summary>
public class FlightQueryTests
{
    /// <summary>
    /// Tests the query for top 5 flights by passenger count.
    /// Verifies that flights are correctly ordered by descending passenger count
    /// and only the top 5 results are returned.
    /// </summary>
    [Fact]
    public void Top5FlightsByPassengerCount()
    {
        var tickets = MockData.GetTickets();
        var flights = MockData.GetFlights();

        foreach (var ticket in tickets)
        {
            ticket.Flight = flights.First(f => f.Id == ticket.FlightId);
        }

        var topFlights = tickets
            .GroupBy(t => t.Flight)
            .Select(g => new
            {
                Flight = g.Key,
                PassengerCount = g.Count()
            })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .ToList();

        Assert.Equal(5, topFlights.Count);

        for (var i = 0; i < topFlights.Count - 1; i++)
        {
            Assert.True(topFlights[i].PassengerCount >= topFlights[i + 1].PassengerCount);
        }

        var topFlight = topFlights.First();
        Assert.Equal(4, topFlight.Flight.Id);
        Assert.Equal(4, topFlight.PassengerCount);
    }

    /// <summary>
    /// Tests the query for flights with minimal duration.
    /// Verifies that all returned flights have the same minimum duration value
    /// and that the correct flights are identified.
    /// </summary>
    [Fact]
    public void FlightsWithMinimalDuration()
    {
        var flights = MockData.GetFlights();

        var minDuration = flights.Min(f => f.Duration);
        var flightsWithMinDuration = flights
            .Where(f => f.Duration == minDuration)
            .ToList();

        Assert.True(flightsWithMinDuration.Count > 0);
        Assert.True(flightsWithMinDuration.All(f => f.Duration == minDuration));

        var shortFlight = flightsWithMinDuration.First();
        Assert.Equal(TimeSpan.FromHours(3.5), shortFlight.Duration);
        Assert.Equal(2, shortFlight.Id);
    }

    /// <summary>
    /// Tests the query for passengers with no baggage on a specific flight, ordered by full name.
    /// Verifies correct filtering by flight and baggage weight, proper ordering by name,
    /// and data integrity.
    /// </summary>
    [Fact]
    public void PassengersWithNoBaggageOrderedByName()
    {
        var tickets = MockData.GetTickets();
        var passengers = MockData.GetPassengers();
        var flights = MockData.GetFlights();
        var selectedFlightId = 1; // Moscow-London

        foreach (var ticket in tickets)
        {
            var flight = flights.First(f => f.Id == ticket.FlightId);
            ticket.Flight = flight;

            var passenger = passengers.First(p => p.PassportNumber == ticket.PassengerPassportNumber);
            ticket.Passenger = passenger;
        }

        var passengersWithNoBaggage = tickets
            .Where(t => t.Flight.Id == selectedFlightId && t.BaggageWeight == 0)
            .Select(t => t.Passenger)
            .OrderBy(p => p.FullName)
            .ToList();

        Assert.True(passengersWithNoBaggage.Count > 0);
        Assert.True(passengersWithNoBaggage.All(p => p != null));

        for (var i = 0; i < passengersWithNoBaggage.Count - 1; i++)
        {
            Assert.True(string.Compare(passengersWithNoBaggage[i].FullName, passengersWithNoBaggage[i + 1].FullName) <= 0);
        }

        var passengerPassports = passengersWithNoBaggage.Select(p => p.PassportNumber).ToList();
        var theirTickets = tickets.Where(t => t.Flight.Id == selectedFlightId && passengerPassports.Contains(t.PassengerPassportNumber));
        Assert.True(theirTickets.All(t => t.BaggageWeight == 0));
    }

    /// <summary>
    /// Tests the query for summary information about all flights of a specific aircraft model
    /// within a specified time period. Verifies correct filtering by model ID and date range.
    /// </summary>
    [Fact]
    public void FlightsByAircraftModelInPeriod()
    {
        var flights = MockData.GetFlights();
        var models = MockData.GetAircraftModels();
        var selectedModelId = 1; // A320-200
        var startDate = DateTime.Now.AddDays(-10);
        var endDate = DateTime.Now.AddDays(10);

        foreach (var flight in flights)
        {
            flight.AircraftModel = models.First(m => m.Id == flight.AircraftModelId);
        }

        var modelFlights = flights
            .Where(f => f.AircraftModelId == selectedModelId && f.DepartureDate >= startDate && f.DepartureDate <= endDate)
            .ToList();

        Assert.True(modelFlights.Count > 0);
        Assert.True(modelFlights.All(f => f.AircraftModelId == selectedModelId));
        Assert.True(modelFlights.All(f => f.DepartureDate >= startDate && f.DepartureDate <= endDate));
    }

    /// <summary>
    /// Tests the query for flights by specific departure and arrival cities.
    /// Verifies correct filtering by both departure and arrival cities.
    /// </summary>
    [Fact]
    public void FlightsByDepartureAndArrival()
    {
        var flights = MockData.GetFlights();
        var departureCity = "Moscow";
        var arrivalCity = "London";

        var routeFlights = flights
            .Where(f => f.DepartureCity == departureCity && f.ArrivalCity == arrivalCity)
            .ToList();

        Assert.True(routeFlights.Count > 0);
        Assert.True(routeFlights.All(f => f.DepartureCity == departureCity));
        Assert.True(routeFlights.All(f => f.ArrivalCity == arrivalCity));

        // Specific verification: flight SU100 Moscow-London
        var moscowLondonFlight = routeFlights.First();
        Assert.Equal("SU100", moscowLondonFlight.Code);
        Assert.Equal("Moscow", moscowLondonFlight.DepartureCity);
        Assert.Equal("London", moscowLondonFlight.ArrivalCity);
    }
}