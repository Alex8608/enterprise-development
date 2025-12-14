using AirlineCompany.Application.Helpers;
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
    /// Create a new flight record
    /// </summary>
    public async Task<int> CreateFlight(FlightCreateDto dto)
    {
        var model = await modelRepository.Read(dto.AircraftModelId)
            ?? throw new ArgumentException("Invalid AircraftModel ID");

        var entity = MapperHelper.ToEntity(dto);
        entity.AircraftModel = model;
        return await flightRepository.Create(entity);
    }

    /// <summary>
    /// Get all flights
    /// </summary>
    public async Task<List<FlightDto>> GetFlights() =>
        [.. (await flightRepository.Read()).Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get flights by aircraft model ID
    /// </summary>
    public async Task<List<FlightDto>> GetFlightsByModelId(int modelId) =>
       [.. (await flightRepository.Read())
            .Where(f => f.AircraftModelId == modelId)
            .Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get flight by ID
    /// </summary>
    public async Task<FlightDto?> GetFlight(int id)
    {
        var entity = await flightRepository.Read(id);
        return entity == null ? null : MapperHelper.ToDto(entity);
    }

    /// <summary>
    /// Update flight by ID
    /// </summary>
    public async Task<FlightDto?> UpdateFlight(int id, FlightCreateDto dto)
    {
        var model = await modelRepository.Read(dto.AircraftModelId)
            ?? throw new ArgumentException("Invalid AircraftModel ID");

        var entity = MapperHelper.ToEntity(dto);
        entity.AircraftModel = model;
        var updated = await flightRepository.Update(id, entity);
        return updated == null ? null : MapperHelper.ToDto(updated);
    }

    /// <summary>
    /// Delete flight by ID
    /// </summary>
    public async Task<bool> DeleteFlight(int id) =>
        await flightRepository.Delete(id);
}