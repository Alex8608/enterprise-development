using System.ComponentModel.DataAnnotations;

namespace AirlineCompany.Dto;

/// <summary>
/// DTO for creating a flight
/// </summary>
public record FlightCreateDTO
{
    /// <summary>
    /// Flight code
    /// </summary>
    [Required(ErrorMessage = "Flight code is required")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Flight code must be between 1 and 10 characters")]
    [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Flight code can only contain uppercase letters and numbers")]
    public string Code { get; init; }

    /// <summary>
    /// Departure city
    /// </summary>
    [Required(ErrorMessage = "Departure city is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Departure city must be between 1 and 100 characters")]
    public string DepartureCity { get; init; }

    /// <summary>
    /// Arrival city
    /// </summary>
    [Required(ErrorMessage = "Arrival city is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Arrival city must be between 1 and 100 characters")]
    public string ArrivalCity { get; init; }

    /// <summary>
    /// Departure date and time
    /// </summary>
    [Required(ErrorMessage = "Departure date is required")]
    [DataType(DataType.DateTime)]
    public DateTime DepartureDate { get; init; }

    /// <summary>
    /// Arrival date and time
    /// </summary>
    [Required(ErrorMessage = "Arrival date is required")]
    [DataType(DataType.DateTime)]
    [CustomValidation(typeof(FlightCreateDTO), nameof(ValidateArrivalDate))]
    public DateTime ArrivalDate { get; init; }

    /// <summary>
    /// Flight duration
    /// </summary>
    [Required(ErrorMessage = "Duration is required")]
    [Range(typeof(TimeSpan), "00:01:00", "48:00:00", ErrorMessage = "Duration must be between 1 minute and 48 hours")]
    public TimeSpan Duration { get; init; }

    /// <summary>
    /// Associated aircraft model ID
    /// </summary>
    [Required(ErrorMessage = "Aircraft model ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid aircraft model ID")]
    public int AircraftModelId { get; init; }

    /// <summary>
    /// Create DTO constructor
    /// </summary>
    public FlightCreateDTO(
        string code,
        string departureCity,
        string arrivalCity,
        DateTime departureDate,
        DateTime arrivalDate,
        TimeSpan duration,
        int aircraftModelId)
    {
        Code = code;
        DepartureCity = departureCity;
        ArrivalCity = arrivalCity;
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        Duration = duration;
        AircraftModelId = aircraftModelId;
    }

    /// <summary>
    /// Method for validation
    /// </summary>
    private static ValidationResult? ValidateArrivalDate(DateTime arrivalDate, ValidationContext context)
    {
        var instance = (FlightCreateDTO)context.ObjectInstance;
        return arrivalDate > instance.DepartureDate
            ? ValidationResult.Success
            : new ValidationResult("Arrival date must be after departure date");
    }
}