namespace AirlineCompany.Tests;

/// <summary>
/// Contains unit tests for airline company data queries using LINQ.
/// </summary>
public class FlightQueryTests(MockDataFixture fixture) : IClassFixture<MockDataFixture>
{
    private readonly MockDataFixture _fixture = fixture;

    /// <summary>
    /// Tests the query for top 5 flights by passenger count.
    /// Verifies that flights are correctly ordered by descending passenger count and only the top 5 results are returned.
    /// </summary>
    [Fact]
    public void Top5FlightsByPassengerCount()
    {
        var tickets = _fixture.Tickets;
        var flights = _fixture.Flights;

        var topFlights = tickets
            .Where(t => t.Flight != null)
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
        Assert.NotNull(topFlight.Flight);
        Assert.Equal(4, topFlight.Flight.Id);
        Assert.Equal(4, topFlight.PassengerCount);
    }

    /// <summary>
    /// Tests the query for flights with minimal duration.
    /// Verifies that all returned flights have the same minimum duration value and that the correct flights are identified.
    /// </summary>
    [Fact]
    public void FlightsWithMinimalDuration()
    {
        var flights = _fixture.Flights;

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
    /// Verifies correct filtering by flight and baggage weight, proper ordering by name, and data integrity.
    /// </summary>
    [Fact]
    public void PassengersWithNoBaggageOrderedByName()
    {
        var tickets = _fixture.Tickets;
        var passengers = _fixture.Passengers;
        var flights = _fixture.Flights;
        var selectedFlightId = 1; // Moscow-London

        var passengersWithNoBaggage = tickets
            .Where(t => t.Flight != null && t.Flight.Id == selectedFlightId && t.BaggageWeight == 0 && t.Passenger != null)
            .Select(t => t.Passenger)
            .OrderBy(p => p!.FullName)
            .ToList();

        Assert.True(passengersWithNoBaggage.Count > 0);
        Assert.True(passengersWithNoBaggage.All(p => p != null));

        for (var i = 0; i < passengersWithNoBaggage.Count - 1; i++)
        {
            Assert.NotNull(passengersWithNoBaggage[i]);
            Assert.NotNull(passengersWithNoBaggage[i + 1]);
            Assert.True(string.Compare(passengersWithNoBaggage[i]!.FullName, passengersWithNoBaggage[i + 1]!.FullName) <= 0);
        }

        // Additional verification: ensure selected passengers truly have zero baggage
        var passengerIds = passengersWithNoBaggage.Select(p => p!.Id).ToList();
        var theirTickets = tickets.Where(t => t.Flight != null && t.Flight.Id == selectedFlightId && passengerIds.Contains(t.PassengerId));
        Assert.True(theirTickets.All(t => t.BaggageWeight == 0));
    }

    /// <summary>
    /// Tests the query for summary information about all flights of a specific aircraft model
    /// within a specified time period. Verifies correct filtering by model ID and date range.
    /// </summary>
    [Fact]
    public void FlightsByAircraftModelInPeriod()
    {
        var flights = _fixture.Flights;
        var models = _fixture.AircraftModels;
        var selectedModelId = 1; // A320-200
        var startDate = DateTime.Now.AddDays(-10);
        var endDate = DateTime.Now.AddDays(10);

        var modelFlights = flights
            .Where(f => f.AircraftModelId == selectedModelId &&
                       f.DepartureDate >= startDate &&
                       f.DepartureDate <= endDate)
            .ToList();

        Assert.True(modelFlights.Count > 0);
        Assert.True(modelFlights.All(f => f != null));
        Assert.True(modelFlights.All(f => f.AircraftModelId == selectedModelId));
        Assert.True(modelFlights.All(f => f.DepartureDate >= startDate && f.DepartureDate <= endDate));
    }

    /// <summary>
    /// Tests the query for flights by specific departure and arrival cities.
    /// Verifies correct filtering by both departure and arrival cities and returns accurate route information.
    /// </summary>
    [Fact]
    public void FlightsByDepartureAndArrival()
    {
        var flights = _fixture.Flights;
        var departureCity = "Moscow";
        var arrivalCity = "London";

        var routeFlights = flights
            .Where(f => f.DepartureCity == departureCity && f.ArrivalCity == arrivalCity)
            .ToList();

        Assert.True(routeFlights.Count > 0);
        Assert.True(routeFlights.All(f => f != null));
        Assert.True(routeFlights.All(f => f.DepartureCity == departureCity));
        Assert.True(routeFlights.All(f => f.ArrivalCity == arrivalCity));

        var moscowLondonFlight = routeFlights.First();
        Assert.NotNull(moscowLondonFlight);
        Assert.Equal("SU100", moscowLondonFlight.Code);
        Assert.Equal("Moscow", moscowLondonFlight.DepartureCity);
        Assert.Equal("London", moscowLondonFlight.ArrivalCity);
    }
}