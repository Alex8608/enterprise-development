namespace AirlineCompany.Dto;

/// <summary>
/// DTO for reading a ticket
/// </summary>
public record TicketDTO
{
    /// <summary>
    /// Unique ID for the primary key in the database
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Flight information
    /// </summary>
    public FlightDTO Flight { get; init; }

    /// <summary>
    /// Passenger information
    /// </summary>
    public PassengerDTO Passenger { get; init; }

    /// <summary>
    /// Seat number
    /// </summary>
    public string SeatNumber { get; init; }

    /// <summary>
    /// Has hand luggage
    /// </summary>
    public bool HasHandLuggage { get; init; }

    /// <summary>
    /// Baggage weight in kg
    /// </summary>
    public double BaggageWeight { get; init; }

    /// <summary>
    /// DTO for read constructor
    /// </summary>
    public TicketDTO(
        int id,
        FlightDTO flight,
        PassengerDTO passenger,
        string seatNumber,
        bool hasHandLuggage,
        double baggageWeight)
    {
        Id = id;
        Flight = flight;
        Passenger = passenger;
        SeatNumber = seatNumber;
        HasHandLuggage = hasHandLuggage;
        BaggageWeight = baggageWeight;
    }
}