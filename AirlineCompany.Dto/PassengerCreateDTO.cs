using System.ComponentModel.DataAnnotations;

namespace AirlineCompany.Dto;

/// <summary>
/// DTO for creating a passenger
/// </summary>
public record PassengerCreateDTO
{
    /// <summary>
    /// Passport number
    /// </summary>
    [Required(ErrorMessage = "Passport number is required")]
    [RegularExpression(@"^\d{4}-\d{6}$", ErrorMessage = "Passport number must be in format 1234-567890")]
    public string PassportNumber { get; init; }

    /// <summary>
    /// Full name
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Full name must be between 1 and 100 characters")]
    public string FullName { get; init; }

    /// <summary>
    /// Date of birth
    /// </summary>
    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1900-01-01", "2100-01-01", ErrorMessage = "Invalid date of birth")]
    public DateTime DateOfBirth { get; init; }

    /// <summary>
    /// Create DTO constructor
    /// </summary>
    public PassengerCreateDTO(string passportNumber, string fullName, DateTime dateOfBirth)
    {
        PassportNumber = passportNumber;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
    }
}