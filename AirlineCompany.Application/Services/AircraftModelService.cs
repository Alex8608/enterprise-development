using AirlineCompany.Application.Helpers;
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
    /// Create a new aircraft model record
    /// </summary>
    public async Task<int> CreateAircraftModel(AircraftModelCreateDto dto)
    {
        var family = await familyRepository.Read(dto.AircraftFamilyId)
            ?? throw new ArgumentException("Invalid AircraftFamily ID");

        var entity = MapperHelper.ToEntity(dto);
        entity.AircraftFamily = family;
        return await modelRepository.Create(entity);
    }

    /// <summary>
    /// Get all aircraft models
    /// </summary>
    public async Task<List<AircraftModelDto>> GetAircraftModels() =>
       [.. (await modelRepository.Read()).Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get aircraft models by family ID
    /// </summary>
    public async Task<List<AircraftModelDto>> GetModelsByFamilyId(int familyId) =>
         [.. (await modelRepository.Read())
            .Where(m => m.AircraftFamilyId == familyId)
            .Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get aircraft model by ID
    /// </summary>
    public async Task<AircraftModelDto?> GetAircraftModel(int id)
    {
        var entity = await modelRepository.Read(id);
        return entity == null ? null : MapperHelper.ToDto(entity);
    }

    /// <summary>
    /// Update aircraft model by ID
    /// </summary>
    public async Task<AircraftModelDto?> UpdateAircraftModel(int id, AircraftModelCreateDto dto)
    {
        var family = await familyRepository.Read(dto.AircraftFamilyId)
            ?? throw new ArgumentException("Invalid AircraftFamily ID");

        var entity = MapperHelper.ToEntity(dto);
        entity.AircraftFamily = family;
        var updated = await modelRepository.Update(id, entity);
        return updated == null ? null : MapperHelper.ToDto(updated);
    }

    /// <summary>
    /// Delete aircraft model by ID
    /// </summary>
    public async Task<bool> DeleteAircraftModel(int id) =>
       await modelRepository.Delete(id);
}