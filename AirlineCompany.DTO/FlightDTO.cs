namespace AirlineCompany.Dto;

/// <summary>
/// DTO for reading a flight
/// </summary>
public record FlightDTO
{
    /// <summary>
    /// Unique ID for the primary key in the database
    /// </summary>
    public int Id { get; init; }

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
    /// Associated aircraft model
    /// </summary>
    public AircraftModelDTO AircraftModel { get; init; }

    /// <summary>
    /// DTO for read constructor
    /// </summary>
    public FlightDTO(
        int id,
        string code,
        string departureCity,
        string arrivalCity,
        DateTime departureDate,
        DateTime arrivalDate,
        TimeSpan duration,
        AircraftModelDTO aircraftModel)
    {
        Id = id;
        Code = code;
        DepartureCity = departureCity;
        ArrivalCity = arrivalCity;
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        Duration = duration;
        AircraftModel = aircraftModel;
    }
}