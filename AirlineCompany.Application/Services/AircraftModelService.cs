using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing aircraft model entities
/// </summary>
public class AircraftModelService(
    IRepository<AircraftModel> modelRepository,
    IRepository<AircraftFamily> familyRepository) : IAircraftModelService
{
    /// <summary>
    /// Converts create DTO to entity
    /// </summary>
    private static AircraftModel MapDto(AircraftModelCreateDto dto, AircraftFamily family) =>
        new()
        {
            Id = 0,
            Name = dto.Name,
            Range = dto.Range,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            AircraftFamilyId = family.Id,
        };

    /// <summary>
    /// Converts entity to read DTO
    /// </summary>
    private static AircraftModelDto MapReadDto(AircraftModel entity)
    {
        var familyDto = new AircraftFamilyDto(
            entity.AircraftFamily!.Id,
            entity.AircraftFamily.Name,
            entity.AircraftFamily.Manufacturer);

        return new AircraftModelDto(
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
    public async Task<int> CreateAircraftModel(AircraftModelCreateDto dto)
    {
        var family = await familyRepository.Read(dto.AircraftFamilyId)
            ?? throw new ArgumentException("Invalid AircraftFamily ID");

        return await modelRepository.Create(MapDto(dto, family));
    }

    /// <summary>
    /// Get all aircraft models
    /// </summary>
    public async Task<List<AircraftModelDto>> GetAircraftModels() =>
       [.. (await modelRepository.Read()).Select(MapReadDto)];

    /// <summary>
    /// Get aircraft models by family ID
    /// </summary>
    public async Task<List<AircraftModelDto>> GetModelsByFamilyId(int familyId) =>
         [.. (await modelRepository.Read())
            .Where(m => m.AircraftFamilyId == familyId)
            .Select(MapReadDto)];

    /// <summary>
    /// Get aircraft model by ID
    /// </summary>
    public async Task<AircraftModelDto?> GetAircraftModel(int id)
    {
        var entity = await modelRepository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update aircraft model by ID
    /// </summary>
    public async Task<AircraftModelDto?> UpdateAircraftModel(int id, AircraftModelCreateDto dto)
    {
        var family = await familyRepository.Read(dto.AircraftFamilyId)
            ?? throw new ArgumentException("Invalid AircraftFamily ID");

        var entity = MapDto(dto, family);
        var updated = await modelRepository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete aircraft model by ID
    /// </summary>
    public async Task<bool> DeleteAircraftModel(int id) =>
       await modelRepository.Delete(id);
}