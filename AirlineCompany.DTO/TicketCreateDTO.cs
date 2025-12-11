namespace AirlineCompany.DTO;

/// <summary>
/// DTO for creating a ticket
/// </summary>
public record TicketCreateDTO
{
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
    /// Flight ID
    /// </summary>
    public int FlightId { get; init; }

    /// <summary>
    /// Passenger ID
    /// </summary>
    public int PassengerId { get; init; }

    /// <summary>
    /// Create DTO constructor
    /// </summary>
    public TicketCreateDTO(
        string seatNumber,
        bool hasHandLuggage,
        double baggageWeight,
        int flightId,
        int passengerId)
    {
        SeatNumber = seatNumber;
        HasHandLuggage = hasHandLuggage;
        BaggageWeight = baggageWeight;
        FlightId = flightId;
        PassengerId = passengerId;
    }
}