using AirlineCompany.Core.Entities;
using AirlineCompany.Dto;

namespace AirlineCompany.Application.Helpers;

/// <summary>
/// Helper class for converting between entities and DTOs
/// </summary>
public static class MapperHelper
{
    #region AircraftFamily

    /// <summary>
    /// Converts AircraftFamily entity to AircraftFamilyDto
    /// </summary>
    public static AircraftFamilyDto ToDto(AircraftFamily entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new AircraftFamilyDto(
            entity.Id,
            entity.Name,
            entity.Manufacturer);
    }

    /// <summary>
    /// Converts AircraftFamilyCreateDto to AircraftFamily entity
    /// </summary>
    public static AircraftFamily ToEntity(AircraftFamilyCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new AircraftFamily
        {
            Id = default,
            Name = dto.Name,
            Manufacturer = dto.Manufacturer
        };
    }

    #endregion

    #region AircraftModel

    /// <summary>
    /// Converts AircraftModel entity to AircraftModelDto
    /// </summary>
    public static AircraftModelDto ToDto(AircraftModel entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new AircraftModelDto(
            entity.Id,
            entity.Name,
            entity.Range,
            entity.PassengerCapacity,
            entity.CargoCapacity,
            ToDto(entity.AircraftFamily!));
    }

    /// <summary>
    /// Converts AircraftModelCreateDto to AircraftModel entity
    /// </summary>
    public static AircraftModel ToEntity(AircraftModelCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new AircraftModel
        {
            Id = default,
            Name = dto.Name,
            Range = dto.Range,
            PassengerCapacity = dto.PassengerCapacity,
            CargoCapacity = dto.CargoCapacity,
            AircraftFamilyId = dto.AircraftFamilyId
        };
    }

    #endregion

    #region Flight

    /// <summary>
    /// Converts Flight entity to FlightDto
    /// </summary>
    public static FlightDto ToDto(Flight entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new FlightDto(
            entity.Id,
            entity.Code,
            entity.DepartureCity,
            entity.ArrivalCity,
            entity.DepartureDate,
            entity.ArrivalDate,
            entity.Duration,
            ToDto(entity.AircraftModel!));
    }

    /// <summary>
    /// Converts FlightCreateDto to Flight entity
    /// </summary>
    public static Flight ToEntity(FlightCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new Flight
        {
            Id = default,
            Code = dto.Code,
            DepartureCity = dto.DepartureCity,
            ArrivalCity = dto.ArrivalCity,
            DepartureDate = dto.DepartureDate,
            ArrivalDate = dto.ArrivalDate,
            Duration = dto.Duration,
            AircraftModelId = dto.AircraftModelId
        };
    }

    #endregion

    #region Passenger

    /// <summary>
    /// Converts Passenger entity to PassengerDto
    /// </summary>
    public static PassengerDto ToDto(Passenger entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new PassengerDto(
            entity.Id,
            entity.PassportNumber,
            entity.FullName,
            entity.DateOfBirth);
    }

    /// <summary>
    /// Converts PassengerCreateDto to Passenger entity
    /// </summary>
    public static Passenger ToEntity(PassengerCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new Passenger
        {
            Id = default,
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth
        };
    }

    #endregion

    #region Ticket

    /// <summary>
    /// Converts Ticket entity to TicketDto
    /// </summary>
    public static TicketDto ToDto(Ticket entity)
    {
        return entity == null
            ? throw new ArgumentNullException(nameof(entity))
            : new TicketDto(
            entity.Id,
            ToDto(entity.Flight!),
            ToDto(entity.Passenger!),
            entity.SeatNumber,
            entity.HasHandLuggage,
            entity.BaggageWeight);
    }

    /// <summary>
    /// Converts TicketCreateDto to Ticket entity
    /// </summary>
    public static Ticket ToEntity(TicketCreateDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new Ticket
        {
            Id = default,
            SeatNumber = dto.SeatNumber,
            HasHandLuggage = dto.HasHandLuggage,
            BaggageWeight = dto.BaggageWeight,
            FlightId = dto.FlightId,
            PassengerId = dto.PassengerId,
            };
    }

    #endregion

    #region Analytic DTOs

    /// <summary>
    /// Converts Flight to FlightPassengerCountDto
    /// </summary>
    public static FlightPassengerCountDto ToFlightPassengerCountDto(Flight flight, int passengerCount)
    {
        return flight == null ? throw new ArgumentNullException(nameof(flight)) : new FlightPassengerCountDto(flight.Code, passengerCount);
    }

    /// <summary>
    /// Converts Flight to FlightDurationDto
    /// </summary>
    public static FlightDurationDto ToFlightDurationDto(Flight flight)
    {
        return flight == null ? throw new ArgumentNullException(nameof(flight)) : new FlightDurationDto(flight.Code, flight.Duration);
    }

    /// <summary>
    /// Converts Flight to FlightByRouteDto
    /// </summary>
    public static FlightByRouteDto ToFlightByRouteDto(Flight flight)
    {
        return flight == null
            ? throw new ArgumentNullException(nameof(flight))
            : new FlightByRouteDto(
            flight.Code,
            flight.DepartureCity,
            flight.ArrivalCity,
            flight.DepartureDate,
            flight.Duration);
    }

    #endregion
}