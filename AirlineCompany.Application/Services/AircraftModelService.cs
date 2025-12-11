using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.DTO;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing aircraft model entities
/// </summary>
public class AircraftModelService(
    IRepository<AircraftModel> modelRepository,
    IRepository<AircraftFamily> familyRepository)
{
    /// <summary>
    /// Converts create DTO to entity
    /// </summary>
    private static AircraftModel MapDto(AircraftModelCreateDTO dto, AircraftFamily family) =>
        new()
        {
            Id = 0,
            Name = dto.Name,
            Range = dto.Range,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            AircraftFamilyId = family.Id,
            //AircraftFamily = family
        };

    /// <summary>
    /// Converts entity to read DTO
    /// </summary>
    private static AircraftModelDTO MapReadDto(AircraftModel entity)
    {
        var familyDto = new AircraftFamilyDTO(
            entity.AircraftFamily!.Id,
            entity.AircraftFamily.Name,
            entity.AircraftFamily.Manufacturer);

        return new AircraftModelDTO(
            entity.Id,
            entity.Name,
            entity.Range,
            entity.PassengerCapacity,
            entity.CargoCapacity,
            familyDto);
    }

    /// <summary>
    /// Create a new aircraft model record
    /// </summary>
    public int CreateAircraftModel(AircraftModelCreateDTO dto)
    {
        var family = familyRepository.Read(dto.AircraftFamilyId);
        if (family == null)
            throw new ArgumentException("Invalid AircraftFamily ID");

        return modelRepository.Create(MapDto(dto, family));
    }

    /// <summary>
    /// Get all aircraft models
    /// </summary>
    public List<AircraftModelDTO> GetAircraftModels() =>
        modelRepository.Read().Select(MapReadDto).ToList();

    /// <summary>
    /// Get aircraft models by family ID
    /// </summary>
    public List<AircraftModelDTO> GetModelsByFamilyId(int familyId) =>
        modelRepository.Read()
            .Where(m => m.AircraftFamilyId == familyId)
            .Select(MapReadDto)
            .ToList();

    /// <summary>
    /// Get aircraft model by ID
    /// </summary>
    public AircraftModelDTO? GetAircraftModel(int id)
    {
        var entity = modelRepository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update aircraft model by ID
    /// </summary>
    public AircraftModelDTO? UpdateAircraftModel(int id, AircraftModelCreateDTO dto)
    {
        var family = familyRepository.Read(dto.AircraftFamilyId);
        if (family == null)
            throw new ArgumentException("Invalid AircraftFamily ID");

        var entity = MapDto(dto, family);
        var updated = modelRepository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete aircraft model by ID
    /// </summary>
    public bool DeleteAircraftModel(int id) =>
        modelRepository.Delete(id);
}