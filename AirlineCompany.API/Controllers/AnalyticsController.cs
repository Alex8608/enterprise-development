using AirlineCompany.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.API.Controllers;

/// <summary>
/// Controller exposing analytic endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(AnalyticService _service) : ControllerBase
{
    /// <summary>
    /// Top 5 flights by number of passengers
    /// </summary>
    [HttpGet("top-five-flights")]
    [ProducesResponseType(200)]
    public ActionResult GetTopFiveFlights() =>
        Ok(_service.GetTopFiveFlightsByPassengerCount());

    /// <summary>
    /// Flights with minimum duration
    /// </summary>
    [HttpGet("min-duration-flights")]
    [ProducesResponseType(200)]
    public ActionResult GetMinDurationFlights() =>
        Ok(_service.GetFlightsWithMinDuration());

    /// <summary>
    /// Passengers on the flight with zero baggage, ordered by full name
    /// </summary>
    [HttpGet("flight/{flightCode}/zero-baggage-passengers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetPassengersWithZeroBaggage(string flightCode)
    {
        var result = _service.GetPassengersWithZeroBaggageOnFlight(flightCode);
        if (!result.Any()) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Summary of all flights of the model during period
    /// </summary>
    [HttpGet("model/{modelId}/flights")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetFlightsOfModelInPeriod(int modelId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        var result = _service.GetFlightsOfModelInPeriod(modelId, fromDate, toDate);
        if (!result.Any()) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Flights by departure and arrival cities
    /// </summary>
    [HttpGet("flights-by-route")]
    [ProducesResponseType(200)]
    public ActionResult GetFlightsByRoute([FromQuery] string departureCity, [FromQuery] string arrivalCity) =>
        Ok(_service.GetFlightsByRoute(departureCity, arrivalCity));
}