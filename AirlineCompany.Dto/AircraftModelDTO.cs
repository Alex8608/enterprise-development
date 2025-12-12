namespace AirlineCompany.Dto;

/// <summary>
/// DTO for reading an aircraft model
/// </summary>
public record AircraftModelDTO
{
    /// <summary>
    /// Unique ID for the primary key in the database
    /// </summary>
    public int Id { get; init; }

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
    /// Associated aircraft family
    /// </summary>
    public AircraftFamilyDTO AircraftFamily { get; init; }

    /// <summary>
    /// DTO for read constructor
    /// </summary>
    public AircraftModelDTO(
        int id,
        string name,
        int range,
        int passengerCapacity,
        double cargoCapacity,
        AircraftFamilyDTO aircraftFamily)
    {
        Id = id;
        Name = name;
        Range = range;
        PassengerCapacity = passengerCapacity;
        CargoCapacity = cargoCapacity;
        AircraftFamily = aircraftFamily;
    }
}