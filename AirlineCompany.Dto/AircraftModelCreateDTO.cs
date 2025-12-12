using System.ComponentModel.DataAnnotations;

namespace AirlineCompany.Dto;

/// <summary>
/// DTO for creating an aircraft model
/// </summary>
public record AircraftModelCreateDTO
{
    /// <summary>
    /// Model name
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
    public string Name { get; init; }

    /// <summary>
    /// Flight range in kilometers
    /// </summary>
    [Range(1, 50000, ErrorMessage = "Range must be between 1 and 50000 km")]
    public int Range { get; init; }

    /// <summary>
    /// Passenger capacity
    /// </summary>
    [Range(1, 1000, ErrorMessage = "Passenger capacity must be between 1 and 1000")]
    public int PassengerCapacity { get; init; }

    /// <summary>
    /// Cargo capacity in tons
    /// </summary>
    [Range(0, 100, ErrorMessage = "Cargo capacity must be between 0 and 100 tons")]
    public double CargoCapacity { get; init; }

    /// <summary>
    /// Associated aircraft family ID
    /// </summary>
    [Required(ErrorMessage = "Aircraft family ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid aircraft family ID")]
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