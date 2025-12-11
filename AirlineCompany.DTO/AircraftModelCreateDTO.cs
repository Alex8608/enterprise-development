namespace AirlineCompany.DTO;

/// <summary>
/// DTO for creating an aircraft model
/// </summary>
public record AircraftModelCreateDTO
{
    /// <summary>
    /// Model name
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Flight range in kilometers
    /// </summary>
    public int Range { get; init; }

    /// <summary>
    /// Passenger capacity
    /// </summary>
    public int PassengerCapacity { get; init; }

    /// <summary>
    /// Cargo capacity in tons
    /// </summary>
    public double CargoCapacity { get; init; }

    /// <summary>
    /// Associated aircraft family ID
    /// </summary>
    public int AircraftFamilyId { get; init; }

    /// <summary>
    /// Create DTO constructor
    /// </summary>
    public AircraftModelCreateDTO(
        string name,
        int range,
        int passengerCapacity,
        double cargoCapacity,
        int aircraftFamilyId)
    {
        Name = name;
        Range = range;
        PassengerCapacity = passengerCapacity;
        CargoCapacity = cargoCapacity;
        AircraftFamilyId = aircraftFamilyId;
    }
}