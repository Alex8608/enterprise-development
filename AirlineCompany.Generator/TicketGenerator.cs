using Bogus;
using AirlineCompany.Dto;

namespace AirlineCompany.Generator;

/// <summary>
/// Provides methods for generating fake ticket data for testing or seeding purposes.
/// </summary>
public class TicketGenerator
{
    /// <summary>
    /// Generates a list of fake TicketCreateDto objects with realistic data.
    /// </summary>
    /// <param name="count">Number of ticket records to generate.</param>
    /// <returns>A list of generated TicketCreateDto objects.</returns>
    public static List<TicketCreateDto> GenerateTickets(int count) =>
        new Faker<TicketCreateDto>()
            .CustomInstantiator(f => new TicketCreateDto(
                seatNumber: GenerateSeatNumber(f),
                hasHandLuggage: f.Random.Bool(0.8f),
                baggageWeight: f.Random.Int(0, 32),
                flightId: f.Random.Int(1, 10),
                passengerId: f.Random.Int(1, 10)
            ))
            .Generate(count);

    private static string GenerateSeatNumber(Faker f)
    {
        var row = f.Random.Int(1, 40);
        var seat = f.Random.Char('A', 'F');
        return $"{row}{seat}";
    }
}