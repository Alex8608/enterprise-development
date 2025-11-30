namespace AirlineCompany.Core.Entities;

/// <summary>
/// Represents a specific aircraft model with technical specifications.
/// </summary>
public class AircraftModel
{
    /// <summary>
    /// Unique identifier for the aircraft model.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Name of the aircraft model (e.g., "A320-200", "737-800").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Maximum flight range in kilometers.
    /// </summary>
    public required int Range { get; set; }

    /// <summary>
    /// Maximum number of passengers the aircraft can carry.
    /// </summary>
    public required int PassengerCapacity { get; set; }

    /// <summary>
    /// Cargo capacity in tons.
    /// </summary>
    public required double CargoCapacity { get; set; }

    /// <summary>
    /// Foreign key to the aircraft family.
    /// </summary>
    public required int AircraftFamilyId { get; set; }

    /// <summary>
    /// Navigation property to the aircraft family.
    /// </summary>
    public AircraftFamily? AircraftFamily { get; set; }

    /// <summary>
    /// Collection of flights operated by this aircraft model.
    /// </summary>
    public List<Flight> Flights { get; set; } = new();
}