namespace AirlineCompany.Dto;

/// <summary>
/// DTO for reading a passenger
/// </summary>
public record PassengerDto
{
    /// <summary>
    /// Unique ID for the primary key in the database
    /// </summary>
    public int Id { get; init; }

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
    /// DTO for read constructor
    /// </summary>
    public PassengerDto(int id, string passportNumber, string fullName, DateTime dateOfBirth)
    {
        Id = id;
        PassportNumber = passportNumber;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
    }
}