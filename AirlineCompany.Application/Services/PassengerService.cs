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
    /// Converts create DTO to entity
    /// </summary>
    private static Passenger MapDto(PassengerCreateDto dto) =>
        new()
        {
            Id = 0,
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth
        };

    /// <summary>
    /// Converts entity to read DTO
    /// </summary>
    private static PassengerDto MapReadDto(Passenger entity) =>
        new(entity.Id, entity.PassportNumber, entity.FullName, entity.DateOfBirth);

    /// <summary>
    /// Create a new passenger record
    /// </summary>
    public async Task<int> CreatePassenger(PassengerCreateDto dto)
    {
        var existing = (await repository.Read())
            .FirstOrDefault(p => p.PassportNumber == dto.PassportNumber);

        if (existing != null)
            throw new ArgumentException($"Passenger with passport number {dto.PassportNumber} already exists");

        return await repository.Create(MapDto(dto));
    }

    /// <summary>
    /// Get all passengers
    /// </summary>
    public async Task<List<PassengerDto>> GetPassengers() =>
        [.. (await repository.Read()).Select(MapReadDto)];

    /// <summary>
    /// Get passenger by ID
    /// </summary>
    public async Task<PassengerDto?> GetPassenger(int id)
    {
        var entity = await repository.Read(id);
        return entity == null ? null : MapReadDto(entity);
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

        var entity = MapDto(dto);
        var updated = await repository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete passenger by ID
    /// </summary>
    public async Task<bool> DeletePassenger(int id) =>
       await repository.Delete(id);
}