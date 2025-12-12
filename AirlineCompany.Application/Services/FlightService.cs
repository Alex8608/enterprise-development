using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing flight entities
/// </summary>
public class FlightService(
    IRepository<Flight> flightRepository,
    IRepository<AircraftModel> modelRepository) : IFlightService
{
    /// <summary>
    /// Converts create DTO to entity
    /// </summary>
    private static Flight MapDto(FlightCreateDTO dto, AircraftModel model) =>
        new()
        {
            Id = 0,
            Code = dto.Code,
            DepartureCity = dto.DepartureCity,
            ArrivalCity = dto.ArrivalCity,
            DepartureDate = dto.DepartureDate,
            ArrivalDate = dto.ArrivalDate,
            Duration = dto.Duration,
            AircraftModelId = model.Id,
        };

    /// <summary>
    /// Converts entity to read DTO
    /// </summary>
    private FlightDTO MapReadDto(Flight entity)
    {
        var modelDto = new AircraftModelDTO(
            entity.AircraftModel!.Id,
            entity.AircraftModel.Name,
            entity.AircraftModel.Range,
            entity.AircraftModel.PassengerCapacity,
            entity.AircraftModel.CargoCapacity,
            new AircraftFamilyDTO(
                entity.AircraftModel.AircraftFamily!.Id,
                entity.AircraftModel.AircraftFamily.Name,
                entity.AircraftModel.AircraftFamily.Manufacturer));

        return new FlightDTO(
            entity.Id,
            entity.Code,
            entity.DepartureCity,
            entity.ArrivalCity,
            entity.DepartureDate,
            entity.ArrivalDate,
            entity.Duration,
            modelDto);
    }

    /// <summary>
    /// Create a new flight record
    /// </summary>
    public async Task<int> CreateFlight(FlightCreateDTO dto)
    {
        var model = await modelRepository.Read(dto.AircraftModelId)
            ?? throw new ArgumentException("Invalid AircraftModel ID");

        return await flightRepository.Create(MapDto(dto, model));
    }

    /// <summary>
    /// Get all flights
    /// </summary>
    public async Task<List<FlightDTO>> GetFlights() =>
        [.. (await flightRepository.Read()).Select(MapReadDto)];

    /// <summary>
    /// Get flights by aircraft model ID
    /// </summary>
    public async Task<List<FlightDTO>> GetFlightsByModelId(int modelId) =>
       [.. (await flightRepository.Read())
            .Where(f => f.AircraftModelId == modelId)
            .Select(MapReadDto)];

    /// <summary>
    /// Get flight by ID
    /// </summary>
    public async Task<FlightDTO?> GetFlight(int id)
    {
        var entity = await flightRepository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update flight by ID
    /// </summary>
    public async Task<FlightDTO?> UpdateFlight(int id, FlightCreateDTO dto)
    {
        var model = await modelRepository.Read(dto.AircraftModelId)
            ?? throw new ArgumentException("Invalid AircraftModel ID");

        var entity = MapDto(dto, model);
        var updated = await flightRepository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete flight by ID
    /// </summary>
    public async Task<bool> DeleteFlight(int id) =>
        await flightRepository.Delete(id);
}