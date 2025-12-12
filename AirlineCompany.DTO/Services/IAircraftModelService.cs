namespace AirlineCompany.Dto.Services;

/// <summary>
/// Service interface for managing aircraft model entities
/// </summary>
public interface IAircraftModelService
{
    /// <summary>
    /// Creates a new aircraft model
    /// </summary>
    /// <param name="dto">Data for creating aircraft model</param>
    /// <returns>ID of the created aircraft model</returns>
    public Task<int> CreateAircraftModel(AircraftModelCreateDTO dto);

    /// <summary>
    /// Gets all aircraft models
    /// </summary>
    /// <returns>List of all aircraft models</returns>
    public Task<List<AircraftModelDTO>> GetAircraftModels();

    /// <summary>
    /// Gets aircraft models by family ID
    /// </summary>
    /// <param name="familyId">Aircraft family ID</param>
    /// <returns>List of aircraft models belonging to the specified family</returns>
    public Task<List<AircraftModelDTO>> GetModelsByFamilyId(int familyId);

    /// <summary>
    /// Gets aircraft model by ID
    /// </summary>
    /// <param name="id">Aircraft model ID</param>
    /// <returns>Aircraft model or null if not found</returns>
    public Task<AircraftModelDTO?> GetAircraftModel(int id);

    /// <summary>
    /// Updates an existing aircraft model
    /// </summary>
    /// <param name="id">Aircraft model ID</param>
    /// <param name="dto">Updated aircraft model data</param>
    /// <returns>Updated aircraft model or null if not found</returns>
    public Task<AircraftModelDTO?> UpdateAircraftModel(int id, AircraftModelCreateDTO dto);

    /// <summary>
    /// Deletes an aircraft model by ID
    /// </summary>
    /// <param name="id">Aircraft model ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteAircraftModel(int id);
}