using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.DTO;
using AirlineCompany.DTO.Services;

namespace AirlineCompany.Application.Services;

/// <summary>
/// Analytic service for processing and analyzing airline data
/// </summary>
public class AnalyticService(
    IRepository<Flight> flightRepository,
    IRepository<Ticket> ticketRepository,
    IRepository<Passenger> passengerRepository) : IAnalyticService
{
    /// <summary>
    /// Display the top 5 flights by the number of passengers carried.
    /// </summary>
    public async Task<List<FlightPassengerCountDTO>> GetTopFiveFlightsByPassengerCount()
    {
        var flights = await flightRepository.Read();
        var tickets = await ticketRepository.Read();

        var topFive = (
            from f in flights
            let passengerCount = tickets.Count(t => t.FlightId == f.Id)
            orderby passengerCount descending
            select new FlightPassengerCountDTO(f.Code, passengerCount)
        )
        .Take(5)
        .ToList();

        return topFive;
    }

    /// <summary>
    /// Display a list of flights with the minimum travel time.
    /// </summary>
    public async Task<List<FlightDurationDTO>> GetFlightsWithMinDuration()
    {
        var flights = await flightRepository.Read();

        if (flights.Count == 0)
            return [];

        var minDuration = flights.Min(f => f.Duration);

        var result = flights
            .Where(f => f.Duration == minDuration)
            .Select(f => new FlightDurationDTO(f.Code, f.Duration))
            .ToList();

        return result;
    }

    /// <summary>
    /// Display information about all passengers flying on the selected flight whose baggage weight is zero, sorted by full name.
    /// </summary>
    public async Task<List<PassengerDTO>> GetPassengersWithZeroBaggageOnFlight(string flightCode)
    {
        var flights = await flightRepository.Read();
        var tickets = await ticketRepository.Read();
        var passengers = await passengerRepository.Read();

        var flight = flights.FirstOrDefault(f => f.Code == flightCode);
        if (flight == null)
            return [];

        var passengerIdsWithZeroBaggage = tickets
            .Where(t => t.FlightId == flight.Id && t.BaggageWeight == 0)
            .Select(t => t.PassengerId)
            .Distinct()
            .ToList();

        var result = passengers
            .Where(p => passengerIdsWithZeroBaggage.Contains(p.Id))
            .OrderBy(p => p.FullName)
            .Select(p => new PassengerDTO(p.Id, p.PassportNumber, p.FullName, p.DateOfBirth))
            .ToList();

        return result;
    }


    /// <summary>
    /// Display summary information about all flights of aircraft of the selected model during a specified period of time.
    /// </summary>
    public async Task<List<FlightDTO>> GetFlightsOfModelInPeriod(int modelId, DateTime? fromDate, DateTime? toDate)
    {
        var flights = await flightRepository.Read();

        var result = flights
            .Where(f => f.AircraftModelId == modelId)
            .Where(f => !fromDate.HasValue || f.DepartureDate >= fromDate.Value || f.ArrivalDate >= fromDate.Value)
            .Where(f => !toDate.HasValue || f.DepartureDate <= toDate.Value || f.ArrivalDate <= toDate.Value)
            .Select(f => MapToFlightDTO(f))
            .ToList();

        return result;
    }

    /// <summary>
    /// Display information about all flights departing from a specified departure point to a specified arrival point.
    /// </summary>
    public async Task<List<FlightByRouteDTO>> GetFlightsByRoute(string departureCity, string arrivalCity)
    {
        var flights = await flightRepository.Read();

        var result = flights
            .Where(f =>
                string.Equals(f.DepartureCity, departureCity, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(f.ArrivalCity, arrivalCity, StringComparison.OrdinalIgnoreCase))
            .Select(f => new FlightByRouteDTO(
                f.Code,
                f.DepartureCity,
                f.ArrivalCity,
                f.DepartureDate,
                f.Duration))
            .ToList();

        return result;
    }

    /// <summary>
    /// Helper method to convert Flight entity to FlightDTO
    /// </summary>
    private FlightDTO MapToFlightDTO(Flight entity)
    {
        var model = entity.AircraftModel;
        var family = model?.AircraftFamily;

        var familyDto = family != null
            ? new AircraftFamilyDTO(family.Id, family.Name, family.Manufacturer)
            : new AircraftFamilyDTO(0, string.Empty, string.Empty);

        var modelDto = model != null
            ? new AircraftModelDTO(
                model.Id,
                model.Name,
                model.Range,
                model.PassengerCapacity,
                model.CargoCapacity,
                familyDto)
            : new AircraftModelDTO(0, string.Empty, 0, 0, 0, familyDto);

        return new FlightDTO(
            entity.Id,
            entity.Code,
            entity.DepartureCity,
            entity.ArrivalCity,
            entity.DepartureDate,
            entity.ArrivalDate,
            entity.Duration,
            modelDto);
    }
}