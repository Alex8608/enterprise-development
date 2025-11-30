using System.Net.Sockets;

namespace AirlineCompany.Core.Entities;

/// <summary>
/// Represents a scheduled flight between two destinations.
/// </summary>
public class Flight
{
    /// <summary>
    /// Unique identifier for the flight.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Flight code (e.g., "SU100", "BA249").
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// City of departure.
    /// </summary>
    public required string DepartureCity { get; set; }

    /// <summary>
    /// City of arrival.
    /// </summary>
    public required string ArrivalCity { get; set; }

    /// <summary>
    /// Scheduled departure date and time.
    /// </summary>
    public required DateTime DepartureDate { get; set; }

    /// <summary>
    /// Scheduled arrival date and time.
    /// </summary>
    public required DateTime ArrivalDate { get; set; }

    /// <summary>
    /// Planned duration of the flight.
    /// </summary>
    public required TimeSpan Duration { get; set; }

    /// <summary>
    /// Foreign key to the aircraft model.
    /// </summary>
    public required int AircraftModelId { get; set; }

    /// <summary>
    /// Navigation property to the aircraft model.
    /// </summary>
    public AircraftModel? AircraftModel { get; set; }

    /// <summary>
    /// Collection of tickets sold for this flight.
    /// </summary>
    public List<Ticket> Tickets { get; set; } = new();
}