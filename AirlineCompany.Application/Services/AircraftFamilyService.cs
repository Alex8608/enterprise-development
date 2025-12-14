using AirlineCompany.Application.Helpers;
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
    /// Create a new aircraft family record
    /// </summary>
    public async Task<int> CreateAircraftFamily(AircraftFamilyCreateDto dto) =>
        await repository.Create(MapperHelper.ToEntity(dto));

    /// <summary>
    /// Get all aircraft families
    /// </summary>
    public async Task<List<AircraftFamilyDto>> GetAircraftFamilies() =>
        [.. (await repository.Read()).Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get aircraft family by ID
    /// </summary>
    public async Task<AircraftFamilyDto?> GetAircraftFamily(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : MapperHelper.ToDto(entity);
    }

    /// <summary>
    /// Update aircraft family by ID
    /// </summary>
    public async Task<AircraftFamilyDto?> UpdateAircraftFamily(int id, AircraftFamilyCreateDto dto)
    {
        var entity = await repository.Update(id, MapperHelper.ToEntity(dto));
        return entity == null ? null : MapperHelper.ToDto(entity);
    }

    /// <summary>
    /// Delete aircraft family by ID
    /// </summary>
    public async Task<bool> DeleteAircraftFamily(int id) =>
        await repository.Delete(id);
}