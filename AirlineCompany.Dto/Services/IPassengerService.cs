namespace AirlineCompany.Dto.Services;

/// <summary>
/// Service interface for managing passenger entities
/// </summary>
public interface IPassengerService
{
    /// <summary>
    /// Creates a new passenger
    /// </summary>
    /// <param name="dto">Data for creating passenger</param>
    /// <returns>ID of the created passenger</returns>
    public Task<int> CreatePassenger(PassengerCreateDto dto);

    /// <summary>
    /// Gets all passengers
    /// </summary>
    /// <returns>List of all passengers</returns>
    public Task<List<PassengerDto>> GetPassengers();

    /// <summary>
    /// Gets passenger by ID
    /// </summary>
    /// <param name="id">Passenger ID</param>
    /// <returns>Passenger or null if not found</returns>
    public Task<PassengerDto?> GetPassenger(int id);

    /// <summary>
    /// Updates an existing passenger
    /// </summary>
    /// <param name="id">Passenger ID</param>
    /// <param name="dto">Updated passenger data</param>
    /// <returns>Updated passenger or null if not found</returns>
    public Task<PassengerDto?> UpdatePassenger(int id, PassengerCreateDto dto);

    /// <summary>
    /// Deletes a passenger by ID
    /// </summary>
    /// <param name="id">Passenger ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeletePassenger(int id);
}