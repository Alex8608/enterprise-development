namespace AirlineCompany.Core.Entities;

/// <summary>
/// Represents a passenger who can book flights.
/// </summary>
public class Passenger
{
    /// <summary>
    /// Unique identifier for the passenger.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Passport number - must be unique.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the passenger.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of birth of the passenger.
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Collection of tickets booked by the passenger.
    /// </summary>
    public List<Ticket> Tickets { get; set; } = [];
}