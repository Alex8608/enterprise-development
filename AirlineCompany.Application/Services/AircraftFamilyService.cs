using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing aircraft family entities
/// </summary>
public class AircraftFamilyService(IRepository<AircraftFamily> repository) : IAircraftFamilyService
{
    /// <summary>
    /// Converts create DTO to entity
    /// </summary>
    private static AircraftFamily MapDto(AircraftFamilyCreateDTO dto) =>
        new()
        {
            Id = 0,
            Name = dto.Name,
            Manufacturer = dto.Manufacturer
        };

    /// <summary>
    /// Converts entity to read DTO
    /// </summary>
    private static AircraftFamilyDTO MapReadDto(AircraftFamily entity) =>
        new(entity.Id, entity.Name, entity.Manufacturer);

    /// <summary>
    /// Create a new aircraft family record
    /// </summary>
    public async Task<int> CreateAircraftFamily(AircraftFamilyCreateDTO dto) =>
        await repository.Create(MapDto(dto));

    /// <summary>
    /// Get all aircraft families
    /// </summary>
    public async Task<List<AircraftFamilyDTO>> GetAircraftFamilies() =>
        [.. (await repository.Read()).Select(MapReadDto)];

    /// <summary>
    /// Get aircraft family by ID
    /// </summary>
    public async Task<AircraftFamilyDTO?> GetAircraftFamily(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update aircraft family by ID
    /// </summary>
    public async Task<AircraftFamilyDTO?> UpdateAircraftFamily(int id, AircraftFamilyCreateDTO dto)
    {
        var entity = await repository.Update(id, MapDto(dto));
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Delete aircraft family by ID
    /// </summary>
    public async Task<bool> DeleteAircraftFamily(int id) =>
        await repository.Delete(id);
}