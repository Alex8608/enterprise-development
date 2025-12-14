using System.ComponentModel.DataAnnotations;

namespace AirlineCompany.Dto;

/// <summary>
/// DTO for creating a ticket
/// </summary>
public record TicketCreateDto
{
    /// <summary>
    /// Seat number
    /// </summary>
    [Required(ErrorMessage = "Seat number is required")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Seat number must be between 1 and 10 characters")]
    [RegularExpression(@"^[0-9]{1,3}[A-Z]$", ErrorMessage = "Seat number must be like '10A', '25B', etc.")]
    public string SeatNumber { get; init; }

    /// <summary>
    /// Has hand luggage
    /// </summary>
    [Required(ErrorMessage = "Hand luggage flag is required")]
    public bool HasHandLuggage { get; init; }

    /// <summary>
    /// Baggage weight in kg
    /// </summary>
    [Required(ErrorMessage = "Baggage weight is required")]
    [Range(0, 32, ErrorMessage = "Baggage weight must be between 0 and 32 kg")]
    public double BaggageWeight { get; init; }

    /// <summary>
    /// Flight ID
    /// </summary>
    [Required(ErrorMessage = "Flight ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid flight ID")]
    public int FlightId { get; init; }

    /// <summary>
    /// Passenger ID
    /// </summary>
    [Required(ErrorMessage = "Passenger ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid passenger ID")]
    public int PassengerId { get; init; }

    /// <summary>
    /// Create DTO constructor
    /// </summary>
    public TicketCreateDto(
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