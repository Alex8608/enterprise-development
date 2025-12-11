namespace AirlineCompany.DTO;

/// <summary>
/// DTO for creating a flight
/// </summary>
public record FlightCreateDTO
{
    /// <summary>
    /// Flight code
    /// </summary>
    public string Code { get; init; }

    /// <summary>
    /// Departure city
    /// </summary>
    public string DepartureCity { get; init; }

    /// <summary>
    /// Arrival city
    /// </summary>
    public string ArrivalCity { get; init; }

    /// <summary>
    /// Departure date and time
    /// </summary>
    public DateTime DepartureDate { get; init; }

    /// <summary>
    /// Arrival date and time
    /// </summary>
    public DateTime ArrivalDate { get; init; }

    /// <summary>
    /// Flight duration
    /// </summary>
    public TimeSpan Duration { get; init; }

    /// <summary>
    /// Associated aircraft model ID
    /// </summary>
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
}