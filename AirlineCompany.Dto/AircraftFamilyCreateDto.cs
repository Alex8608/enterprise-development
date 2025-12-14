using System.ComponentModel.DataAnnotations;

namespace AirlineCompany.Dto;

/// <summary>
/// DTO for creating an aircraft family
/// </summary>
public record AircraftFamilyCreateDto
{
    /// <summary>
    /// Family name
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
    public string Name { get; init; }

    /// <summary>
    /// Manufacturer name
    /// </summary>
    [Required(ErrorMessage = "Manufacturer is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Manufacturer must be between 1 and 100 characters")]
    public string Manufacturer { get; init; }

    /// <summary>
    /// Create DTO constructor
    /// </summary>
    public AircraftFamilyCreateDto(string name, string manufacturer)
    {
        Name = name;
        Manufacturer = manufacturer;
    }
}