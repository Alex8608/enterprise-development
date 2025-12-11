using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.DTO;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Service for managing ticket entities
/// </summary>
public class TicketService(
    IRepository<Ticket> ticketRepository,
    IRepository<Flight> flightRepository,
    IRepository<Passenger> passengerRepository)
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
            //Flight = flight,
            PassengerId = passenger.Id,
            //Passenger = passenger
        };

    /// <summary>
    /// Converts entity to read DTO
    /// </summary>
    private static TicketDTO MapReadDto(Ticket entity)
    {
        // Map Flight
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

        // Map Passenger
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
    public int CreateTicket(TicketCreateDTO dto)
    {
        var flight = flightRepository.Read(dto.FlightId);
        if (flight == null)
            throw new ArgumentException("Invalid Flight ID");

        var passenger = passengerRepository.Read(dto.PassengerId);
        if (passenger == null)
            throw new ArgumentException("Invalid Passenger ID");

        // Check if seat is already taken on this flight
        var existingTicket = ticketRepository.Read()
            .FirstOrDefault(t => t.FlightId == dto.FlightId && t.SeatNumber == dto.SeatNumber);

        if (existingTicket != null)
            throw new ArgumentException($"Seat {dto.SeatNumber} is already taken on this flight");

        return ticketRepository.Create(MapDto(dto, flight, passenger));
    }

    /// <summary>
    /// Get all tickets
    /// </summary>
    public List<TicketDTO> GetTickets() =>
        ticketRepository.Read().Select(MapReadDto).ToList();

    /// <summary>
    /// Get tickets by flight ID
    /// </summary>
    public List<TicketDTO> GetTicketsByFlightId(int flightId) =>
        ticketRepository.Read()
            .Where(t => t.FlightId == flightId)
            .Select(MapReadDto)
            .ToList();

    /// <summary>
    /// Get tickets by passenger ID
    /// </summary>
    public List<TicketDTO> GetTicketsByPassengerId(int passengerId) =>
        ticketRepository.Read()
            .Where(t => t.PassengerId == passengerId)
            .Select(MapReadDto)
            .ToList();

    /// <summary>
    /// Get ticket by ID
    /// </summary>
    public TicketDTO? GetTicket(int id)
    {
        var entity = ticketRepository.Read(id);
        return entity == null ? null : MapReadDto(entity);
    }

    /// <summary>
    /// Update ticket by ID
    /// </summary>
    public TicketDTO? UpdateTicket(int id, TicketCreateDTO dto)
    {
        var flight = flightRepository.Read(dto.FlightId);
        if (flight == null)
            throw new ArgumentException("Invalid Flight ID");

        var passenger = passengerRepository.Read(dto.PassengerId);
        if (passenger == null)
            throw new ArgumentException("Invalid Passenger ID");

        // Check if seat is already taken by another ticket on this flight
        var existingTicket = ticketRepository.Read()
            .FirstOrDefault(t => t.FlightId == dto.FlightId &&
                                t.SeatNumber == dto.SeatNumber &&
                                t.Id != id);

        if (existingTicket != null)
            throw new ArgumentException($"Seat {dto.SeatNumber} is already taken on this flight");

        var entity = MapDto(dto, flight, passenger);
        var updated = ticketRepository.Update(id, entity);
        return updated == null ? null : MapReadDto(updated);
    }

    /// <summary>
    /// Delete ticket by ID
    /// </summary>
    public bool DeleteTicket(int id) =>
        ticketRepository.Delete(id);
}