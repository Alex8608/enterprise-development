using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.DTO;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing flight entities
/// </summary>
public class FlightService(
    IRepository<Flight> flightRepository,
    IRepository<AircraftModel> modelRepository)
{
    /// <summary>
    /// Converts create DTO to entity
    /// </summary>
    private Flight MapDto(FlightCreateDTO dto, AircraftModel model) =>
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
            //AircraftModel = model
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
    public int CreateFlight(FlightCreateDTO dto)
    {
        var model = modelRepository.Read(dto.AircraftModelId);
        if (model == null)
            throw new ArgumentException("Invalid AircraftModel ID");

        return flightRepository.Create(MapDto(dto, model));
    }

    /// <summary>
    /// Get all flights
    /// </summary>
    public List<FlightDTO> GetFlights() =>
        flightRepository.Read().Select(MapReadDto).ToList();

    /// <summary>
    /// Get flights by aircraft model ID
    /// </summary>
    public List<FlightDTO> GetFlightsByModelId(int modelId) =>
        flightRepository.Read()
            .Where(f => f.AircraftModelId == modelId)
            .Select(MapReadDto)
            .ToList();

    /// <summary>
    /// Get flight by ID
    /// </summary>
    public FlightDTO? GetFlight(int id)
    {
        var entity = flightRepository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update flight by ID
    /// </summary>
    public FlightDTO? UpdateFlight(int id, FlightCreateDTO dto)
    {
        var model = modelRepository.Read(dto.AircraftModelId);
        if (model == null)
            throw new ArgumentException("Invalid AircraftModel ID");

        var entity = MapDto(dto, model);
        var updated = flightRepository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete flight by ID
    /// </summary>
    public bool DeleteFlight(int id) =>
        flightRepository.Delete(id);
}