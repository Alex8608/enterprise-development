namespace AirlineCompany.Dto.Services;

/// <summary>
/// Service interface for analytical operations and reports
/// </summary>
public interface IAnalyticService
{
    /// <summary>
    /// Gets top 5 flights by passenger count
    /// </summary>
    /// <returns>List of flights with passenger count, ordered by count descending</returns>
    public Task<List<FlightPassengerCountDTO>> GetTopFiveFlightsByPassengerCount();

    /// <summary>
    /// Gets flights with minimal duration
    /// </summary>
    /// <returns>List of flights having the shortest travel time</returns>
    public Task<List<FlightDurationDTO>> GetFlightsWithMinDuration();

    /// <summary>
    /// Gets passengers with zero baggage on a specific flight
    /// </summary>
    /// <param name="flightCode">Flight code</param>
    /// <returns>List of passengers without baggage, sorted by full name</returns>
    public Task<List<PassengerDTO>> GetPassengersWithZeroBaggageOnFlight(string flightCode);

    /// <summary>
    /// Gets flights of a specific aircraft model within a time period
    /// </summary>
    /// <param name="modelId">Aircraft model ID</param>
    /// <param name="fromDate">Start date of the period (optional)</param>
    /// <param name="toDate">End date of the period (optional)</param>
    /// <returns>List of flights matching the criteria</returns>
    public Task<List<FlightDTO>> GetFlightsOfModelInPeriod(int modelId, DateTime? fromDate, DateTime? toDate);

    /// <summary>
    /// Gets flights by departure and arrival cities
    /// </summary>
    /// <param name="departureCity">Departure city</param>
    /// <param name="arrivalCity">Arrival city</param>
    /// <returns>List of flights on the specified route</returns>
    public Task<List<FlightByRouteDTO>> GetFlightsByRoute(string departureCity, string arrivalCity);
}