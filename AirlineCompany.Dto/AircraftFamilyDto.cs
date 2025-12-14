namespace AirlineCompany.Dto;

/// <summary>
/// DTO for reading an aircraft family
/// </summary>
public record AircraftFamilyDto
{
    /// <summary>
    /// Unique ID for the primary key in the database
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Family name
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Manufacturer name
    /// </summary>
    public string Manufacturer { get; init; }

    /// <summary>
    /// DTO for read constructor
    /// </summary>
    public AircraftFamilyDto(int id, string name, string manufacturer)
    {
        Id = id;
        Name = name;
        Manufacturer = manufacturer;
    }
}