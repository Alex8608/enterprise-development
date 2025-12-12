namespace AirlineCompany.Dto.Services;

/// <summary>
/// Service interface for managing flight entities
/// </summary>
public interface IFlightService
{
    /// <summary>
    /// Creates a new flight
    /// </summary>
    /// <param name="dto">Data for creating flight</param>
    /// <returns>ID of the created flight</returns>
    public Task<int> CreateFlight(FlightCreateDTO dto);

    /// <summary>
    /// Gets all flights
    /// </summary>
    /// <returns>List of all flights</returns>
    public Task<List<FlightDTO>> GetFlights();

    /// <summary>
    /// Gets flights by aircraft model ID
    /// </summary>
    /// <param name="modelId">Aircraft model ID</param>
    /// <returns>List of flights using the specified aircraft model</returns>
    public Task<List<FlightDTO>> GetFlightsByModelId(int modelId);

    /// <summary>
    /// Gets flight by ID
    /// </summary>
    /// <param name="id">Flight ID</param>
    /// <returns>Flight or null if not found</returns>
    public Task<FlightDTO?> GetFlight(int id);

    /// <summary>
    /// Updates an existing flight
    /// </summary>
    /// <param name="id">Flight ID</param>
    /// <param name="dto">Updated flight data</param>
    /// <returns>Updated flight or null if not found</returns>
    public Task<FlightDTO?> UpdateFlight(int id, FlightCreateDTO dto);

    /// <summary>
    /// Deletes a flight by ID
    /// </summary>
    /// <param name="id">Flight ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteFlight(int id);
}