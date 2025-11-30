namespace AirlineCompany.Core.Entities;

/// <summary>
/// Represents a booking for a passenger on a specific flight.
/// </summary>
public class Ticket
{
    /// <summary>
    /// Unique identifier for the ticket.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Assigned seat number on the aircraft.
    /// </summary>
    public required string SeatNumber { get; set; }

    /// <summary>
    /// Indicates whether the passenger has hand luggage.
    /// </summary>
    public required bool HasHandLuggage { get; set; }

    /// <summary>
    /// Total weight of checked baggage in kilograms.
    /// </summary>
    public required double BaggageWeight { get; set; }

    /// <summary>
    /// Foreign key to the flight.
    /// </summary>
    public required int FlightId { get; set; }

    /// <summary>
    /// Navigation property to the flight.
    /// </summary>
    public Flight? Flight { get; set; }

    /// <summary>
    /// Foreign key to the passenger.
    /// </summary>
    public required string PassengerPassportNumber { get; set; }

    /// <summary>
    /// Navigation property to the passenger.
    /// </summary>
    public Passenger? Passenger { get; set; }
}