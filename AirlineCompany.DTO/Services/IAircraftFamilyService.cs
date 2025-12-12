namespace AirlineCompany.Dto.Services;

/// <summary>
/// Service interface for managing aircraft family entities
/// </summary>
public interface IAircraftFamilyService
{
    /// <summary>
    /// Creates a new aircraft family
    /// </summary>
    /// <param name="dto">Data for creating aircraft family</param>
    /// <returns>ID of the created aircraft family</returns>
    public Task<int> CreateAircraftFamily(AircraftFamilyCreateDTO dto);

    /// <summary>
    /// Gets all aircraft families
    /// </summary>
    /// <returns>List of all aircraft families</returns>
    public Task<List<AircraftFamilyDTO>> GetAircraftFamilies();

    /// <summary>
    /// Gets aircraft family by ID
    /// </summary>
    /// <param name="id">Aircraft family ID</param>
    /// <returns>Aircraft family or null if not found</returns>
    public Task<AircraftFamilyDTO?> GetAircraftFamily(int id);

    /// <summary>
    /// Updates an existing aircraft family
    /// </summary>
    /// <param name="id">Aircraft family ID</param>
    /// <param name="dto">Updated aircraft family data</param>
    /// <returns>Updated aircraft family or null if not found</returns>
    public Task<AircraftFamilyDTO?> UpdateAircraftFamily(int id, AircraftFamilyCreateDTO dto);

    /// <summary>
    /// Deletes an aircraft family by ID
    /// </summary>
    /// <param name="id">Aircraft family ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteAircraftFamily(int id);
}