namespace AirlineCompany.Core.Entities;

/// <summary>
/// Represents a booking for a passenger on a specific flight.
/// </summary>
public class Ticket
{
    /// <summary>
    /// Initializes a new instance of the Ticket class with all required properties.
    /// </summary>
    /// <param name="id">Unique identifier for the ticket.</param>
    /// <param name="seatNumber">Assigned seat number on the aircraft.</param>
    /// <param name="hasHandLuggage">Indicates whether the passenger has hand luggage.</param>
    /// <param name="baggageWeight">Total weight of checked baggage in kilograms.</param>
    /// <param name="flightId">Foreign key to the flight.</param>
    /// <param name="flight">Navigation property to the flight. A ticket must always have a flight.</param>
    /// <param name="passengerId">Foreign key to the passenger.</param>
    /// <param name="passenger">Navigation property to the passenger. A ticket must always have a passenger.</param>
    public Ticket(int id, string seatNumber, bool hasHandLuggage, double baggageWeight,
                  int flightId, Flight flight, int passengerId, Passenger passenger)
    {
        Id = id;
        SeatNumber = seatNumber;
        HasHandLuggage = hasHandLuggage;
        BaggageWeight = baggageWeight;
        FlightId = flightId;
        Flight = flight;
        PassengerId = passengerId;
        Passenger = passenger;
    }


    /// <summary>
    /// Unique identifier for the ticket.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Assigned seat number on the aircraft.
    /// </summary>
    public string SeatNumber { get; set; }

    /// <summary>
    /// Indicates whether the passenger has hand luggage.
    /// </summary>
    public bool HasHandLuggage { get; set; }

    /// <summary>
    /// Total weight of checked baggage in kilograms.
    /// </summary>
    public double BaggageWeight { get; set; }

    /// <summary>
    /// Foreign key to the flight.
    /// </summary>
    public int FlightId { get; set; }

    /// <summary>
    /// Navigation property to the flight.
    /// </summary>
    public Flight Flight { get; set; }

    /// <summary>
    /// Foreign key to the passenger.
    /// </summary>
    public int PassengerId { get; set; }

    /// <summary>
    /// Navigation property to the passenger.
    /// </summary>
    public Passenger Passenger { get; set; }
}