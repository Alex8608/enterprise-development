using AirlineCompany.Application.Helpers;
using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing ticket entities
/// </summary>
public class TicketService(
    IRepository<Ticket> ticketRepository,
    IRepository<Flight> flightRepository,
    IRepository<Passenger> passengerRepository) : ITicketService
{
    /// <summary>
    /// Create a new ticket record
    /// </summary>
    public async Task<int> CreateTicket(TicketCreateDto dto)
    {
        var flight = await flightRepository.Read(dto.FlightId) 
            ?? throw new ArgumentException("Invalid Flight ID");
        var passenger = await passengerRepository.Read(dto.PassengerId)
            ?? throw new ArgumentException("Invalid Passenger ID");
        var existingTicket = (await ticketRepository.Read())
            .FirstOrDefault(t => t.FlightId == dto.FlightId && t.SeatNumber == dto.SeatNumber);

        if (existingTicket != null)
            throw new ArgumentException($"Seat {dto.SeatNumber} is already taken on this flight");

        var entity = MapperHelper.ToEntity(dto);
        entity.Flight = flight;
        entity.Passenger = passenger;
        return await ticketRepository.Create(entity);
    }

    /// <summary>
    /// Get all tickets
    /// </summary>
    public async Task<List<TicketDto>> GetTickets() =>
         [.. (await ticketRepository.Read()).Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get tickets by flight ID
    /// </summary>
    public async Task<List<TicketDto>> GetTicketsByFlightId(int flightId) =>
        [.. (await ticketRepository.Read())
            .Where(t => t.FlightId == flightId)
            .Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get tickets by passenger ID
    /// </summary>
    public async Task<List<TicketDto>> GetTicketsByPassengerId(int passengerId) =>
       [.. (await ticketRepository.Read())
            .Where(t => t.PassengerId == passengerId)
            .Select(MapperHelper.ToDto)];

    /// <summary>
    /// Get ticket by ID
    /// </summary>
    public async Task<TicketDto?> GetTicket(int id)
    {
        var entity = await ticketRepository.Read(id);
        return entity == null ? null : MapperHelper.ToDto(entity);
    }

    /// <summary>
    /// Update ticket by ID
    /// </summary>
    public async Task<TicketDto?> UpdateTicket(int id, TicketCreateDto dto)
    {
        var flight = await flightRepository.Read(dto.FlightId)
            ?? throw new ArgumentException("Invalid Flight ID");
        var passenger = await passengerRepository.Read(dto.PassengerId)
            ?? throw new ArgumentException("Invalid Passenger ID");
        var existingTicket = (await ticketRepository.Read())
            .FirstOrDefault(t => t.FlightId == dto.FlightId &&
                                t.SeatNumber == dto.SeatNumber &&
                                t.Id != id);

        if (existingTicket != null)
            throw new ArgumentException($"Seat {dto.SeatNumber} is already taken on this flight");

        var entity = MapperHelper.ToEntity(dto);
        entity.Flight = flight;
        entity.Passenger = passenger;
        var updated = await ticketRepository.Update(id, entity);
        return updated == null ? null : MapperHelper.ToDto(updated);
    }

    /// <summary>
    /// Delete ticket by ID
    /// </summary>
    public async Task<bool> DeleteTicket(int id) =>
        await ticketRepository.Delete(id);
}