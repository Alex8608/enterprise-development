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
    /// Converts create DTO to entity
    /// </summary>
    private static Ticket MapDto(TicketCreateDTO dto, Flight flight, Passenger passenger) =>
        new()
        {
            Id = 0,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight,
            FlightId = flight.Id,
            PassengerId = passenger.Id,
        };

    /// <summary>
    /// Converts entity to read DTO
    /// </summary>
    private static TicketDTO MapReadDto(Ticket entity)
    {
        var flightDto = new FlightDTO(
            entity.Flight!.Id,
            entity.Flight.Code,
            entity.Flight.DepartureCity,
            entity.Flight.ArrivalCity,
            entity.Flight.DepartureDate,
            entity.Flight.ArrivalDate,
            entity.Flight.Duration,
            new AircraftModelDTO(
                entity.Flight.AircraftModel!.Id,
                entity.Flight.AircraftModel.Name,
                entity.Flight.AircraftModel.Range,
                entity.Flight.AircraftModel.PassengerCapacity,
                entity.Flight.AircraftModel.CargoCapacity,
                new AircraftFamilyDTO(
                    entity.Flight.AircraftModel.AircraftFamily!.Id,
                    entity.Flight.AircraftModel.AircraftFamily.Name,
                    entity.Flight.AircraftModel.AircraftFamily.Manufacturer)));

        var passengerDto = new PassengerDTO(
            entity.Passenger!.Id,
            entity.Passenger.PassportNumber,
            entity.Passenger.FullName,
            entity.Passenger.DateOfBirth);

        return new TicketDTO(
            entity.Id,
            flightDto,
            passengerDto,
            entity.SeatNumber,
            entity.HasHandLuggage,
            entity.BaggageWeight);
    }

    /// <summary>
    /// Create a new ticket record
    /// </summary>
    public async Task<int> CreateTicket(TicketCreateDTO dto)
    {
        var flight = await flightRepository.Read(dto.FlightId) 
            ?? throw new ArgumentException("Invalid Flight ID");
        var passenger = await passengerRepository.Read(dto.PassengerId)
            ?? throw new ArgumentException("Invalid Passenger ID");
        var existingTicket = (await ticketRepository.Read())
            .FirstOrDefault(t => t.FlightId == dto.FlightId && t.SeatNumber == dto.SeatNumber);

        if (existingTicket != null)
            throw new ArgumentException($"Seat {dto.SeatNumber} is already taken on this flight");

        return await ticketRepository.Create(MapDto(dto, flight, passenger));
    }

    /// <summary>
    /// Get all tickets
    /// </summary>
    public async Task<List<TicketDTO>> GetTickets() =>
         [.. (await ticketRepository.Read()).Select(MapReadDto)];

    /// <summary>
    /// Get tickets by flight ID
    /// </summary>
    public async Task<List<TicketDTO>> GetTicketsByFlightId(int flightId) =>
        [.. (await ticketRepository.Read())
            .Where(t => t.FlightId == flightId)
            .Select(MapReadDto)];

    /// <summary>
    /// Get tickets by passenger ID
    /// </summary>
    public async Task<List<TicketDTO>> GetTicketsByPassengerId(int passengerId) =>
       [.. (await ticketRepository.Read())
            .Where(t => t.PassengerId == passengerId)
            .Select(MapReadDto)];

    /// <summary>
    /// Get ticket by ID
    /// </summary>
    public async Task<TicketDTO?> GetTicket(int id)
    {
        var entity = await ticketRepository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update ticket by ID
    /// </summary>
    public async Task<TicketDTO?> UpdateTicket(int id, TicketCreateDTO dto)
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

        var entity = MapDto(dto, flight, passenger);
        var updated = await ticketRepository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete ticket by ID
    /// </summary>
    public async Task<bool> DeleteTicket(int id) =>
        await ticketRepository.Delete(id);
}