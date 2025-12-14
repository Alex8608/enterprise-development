using AirlineCompany.Application.Helpers;
using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing passenger entities
/// </summary>
public class PassengerService(IRepository<Passenger> repository) : IPassengerService
{
    /// <summary>
    /// Create a new passenger record
    /// </summary>
    public async Task<int> CreatePassenger(PassengerCreateDto dto)
    {
        var existing = (await repository.Read())
            .FirstOrDefault(p => p.PassportNumber == dto.PassportNumber);

        if (existing != null)
            throw new ArgumentException($"Passenger with passport number {dto.PassportNumber} already exists");

        return await repository.Create(MapperHelper.ToEntity(dto));
    }

    /// <summary>
    /// Get all passengers
    /// </summary>
    public async Task<List<PassengerDto>> GetPassengers() =>
        [.. (await repository.Read()).Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get passenger by ID
    /// </summary>
    public async Task<PassengerDto?> GetPassenger(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : MapperHelper.ToDto(entity);
    }

    /// <summary>
    /// Update passenger by ID
    /// </summary>
    public async Task<PassengerDto?> UpdatePassenger(int id, PassengerCreateDto dto)
    {
        var existing = (await repository.Read())
            .FirstOrDefault(p => p.PassportNumber == dto.PassportNumber && p.Id != id);

        if (existing != null)
            throw new ArgumentException($"Passport number {dto.PassportNumber} is already used by another passenger");

        var entity = MapperHelper.ToEntity(dto);
        var updated = await repository.Update(id, entity);
        return updated == null ? null : MapperHelper.ToDto(updated);
    }

    /// <summary>
    /// Delete passenger by ID
    /// </summary>
    public async Task<bool> DeletePassenger(int id) =>
       await repository.Delete(id);
}