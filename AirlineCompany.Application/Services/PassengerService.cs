using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.DTO;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing passenger entities
/// </summary>
public class PassengerService(IRepository<Passenger> repository)
{
    /// <summary>
    /// Converts create DTO to entity
    /// </summary>
    private static Passenger MapDto(PassengerCreateDTO dto) =>
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
    private static PassengerDTO MapReadDto(Passenger entity) =>
        new(entity.Id, entity.PassportNumber, entity.FullName, entity.DateOfBirth);

    /// <summary>
    /// Create a new passenger record
    /// </summary>
    public int CreatePassenger(PassengerCreateDTO dto)
    {
        // Check for duplicate passport number
        var existing = repository.Read()
            .FirstOrDefault(p => p.PassportNumber == dto.PassportNumber);

        if (existing != null)
            throw new ArgumentException($"Passenger with passport number {dto.PassportNumber} already exists");

        return repository.Create(MapDto(dto));
    }

    /// <summary>
    /// Get all passengers
    /// </summary>
    public List<PassengerDTO> GetPassengers() =>
        repository.Read().Select(MapReadDto).ToList();

    /// <summary>
    /// Get passenger by ID
    /// </summary>
    public PassengerDTO? GetPassenger(int id)
    {
        var entity = repository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update passenger by ID
    /// </summary>
    public PassengerDTO? UpdatePassenger(int id, PassengerCreateDTO dto)
    {
        var existing = repository.Read()
            .FirstOrDefault(p => p.PassportNumber == dto.PassportNumber && p.Id != id);

        if (existing != null)
            throw new ArgumentException($"Passport number {dto.PassportNumber} is already used by another passenger");

        var entity = MapDto(dto);
        var updated = repository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete passenger by ID
    /// </summary>
    public bool DeletePassenger(int id) =>
        repository.Delete(id);
}