namespace AirlineCompany.Core.Entities;

/// <summary>
/// Represents a scheduled flight between two destinations.
/// </summary>
public class Flight
{
    /// <summary>
    /// Initializes a new instance of the Flight class with all required properties.
    /// </summary>
    /// <param name="id">Unique identifier for the flight.</param>
    /// <param name="code">Flight code (e.g., "SU100", "BA249").</param>
    /// <param name="departureCity">City of departure.</param>
    /// <param name="arrivalCity">City of arrival.</param>
    /// <param name="departureDate">Scheduled departure date and time.</param>
    /// <param name="arrivalDate">Scheduled arrival date and time.</param>
    /// <param name="duration">Planned duration of the flight.</param>
    /// <param name="aircraftModelId">Foreign key to the aircraft model.</param>
    public Flight(int id, string code, string departureCity, string arrivalCity,
                 DateTime departureDate, DateTime arrivalDate, TimeSpan duration,
                 int aircraftModelId)
    {
        Id = id;
        Code = code;
        DepartureCity = departureCity;
        ArrivalCity = arrivalCity;
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        Duration = duration;
        AircraftModelId = aircraftModelId;
        Tickets = [];
    }

    /// <summary>
    /// Unique identifier for the flight.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Flight code (e.g., "SU100", "BA249").
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// City of departure.
    /// </summary>
    public string DepartureCity { get; set; }

    /// <summary>
    /// City of arrival.
    /// </summary>
    public string ArrivalCity { get; set; }

    /// <summary>
    /// Scheduled departure date and time.
    /// </summary>
    public DateTime DepartureDate { get; set; }

    /// <summary>
    /// Scheduled arrival date and time.
    /// </summary>
    public DateTime ArrivalDate { get; set; }

    /// <summary>
    /// Planned duration of the flight.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Foreign key to the aircraft model.
    /// </summary>
    public int AircraftModelId { get; set; }

    /// <summary>
    /// Navigation property to the aircraft model.
    /// </summary>
    public AircraftModel? AircraftModel { get; set; }

    /// <summary>
    /// Collection of tickets sold for this flight.
    /// </summary>
    public List<Ticket> Tickets { get; set; } = [];
}