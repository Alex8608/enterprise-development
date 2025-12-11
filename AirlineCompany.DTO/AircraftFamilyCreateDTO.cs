namespace AirlineCompany.DTO;

/// <summary>
/// DTO for creating an aircraft family
/// </summary>
public record AircraftFamilyCreateDTO
{
    /// <summary>
    /// Family name
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Manufacturer name
    /// </summary>
    public string Manufacturer { get; init; }

    /// <summary>
    /// Create DTO constructor
    /// </summary>
    public AircraftFamilyCreateDTO(string name, string manufacturer)
    {
        Name = name;
        Manufacturer = manufacturer;
    }
}