using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.DTO;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing aircraft family entities
/// </summary>
public class AircraftFamilyService(IRepository<AircraftFamily> repository)
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
    public int CreateAircraftFamily(AircraftFamilyCreateDTO dto) =>
        repository.Create(MapDto(dto));

    /// <summary>
    /// Get all aircraft families
    /// </summary>
    public List<AircraftFamilyDTO> GetAircraftFamilies() =>
        repository.Read().Select(MapReadDto).ToList();

    /// <summary>
    /// Get aircraft family by ID
    /// </summary>
    public AircraftFamilyDTO? GetAircraftFamily(int id)
    {
        var entity = repository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update aircraft family by ID
    /// </summary>
    public AircraftFamilyDTO? UpdateAircraftFamily(int id, AircraftFamilyCreateDTO dto)
    {
        var entity = repository.Update(id, MapDto(dto));
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Delete aircraft family by ID
    /// </summary>
    public bool DeleteAircraftFamily(int id) =>
        repository.Delete(id);
}