namespace AirlineCompany.Core.Entities;

/// <summary>
/// Represents a family of aircraft models from a specific manufacturer.
/// </summary>
public class AircraftFamily
{
    /// <summary>
    /// Unique identifier for the aircraft family.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the aircraft family (e.g., "A320", "737").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Manufacturer of the aircraft family (e.g., "Airbus", "Boeing").
    /// </summary>
    public required string Manufacturer { get; set; }

    /// <summary>
    /// Collection of aircraft models belonging to this family.
    /// </summary>
    public List<AircraftModel> Models { get; set; } = [];
}