namespace AirlineCompany.DTO;

/// <summary>
/// DTO for creating a passenger
/// </summary>
public record PassengerCreateDTO
{
    /// <summary>
    /// Passport number
    /// </summary>
    public string PassportNumber { get; init; }

    /// <summary>
    /// Full name
    /// </summary>
    public string FullName { get; init; }

    /// <summary>
    /// Date of birth
    /// </summary>
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