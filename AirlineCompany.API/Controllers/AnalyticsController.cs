using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.Api.Controllers;

/// <summary>
/// Controller exposing analytic endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Top 5 flights by number of passengers
    /// </summary>
    [HttpGet("top-five-flights")]
    [ProducesResponseType(typeof(List<FlightPassengerCountDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FlightPassengerCountDTO>>> GetTopFiveFlights()
    {
        try
        {
            var result = await service.GetTopFiveFlightsByPassengerCount();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top five flights");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Flights with minimum duration
    /// </summary>
    [HttpGet("min-duration-flights")]
    [ProducesResponseType(typeof(List<FlightDurationDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FlightDurationDTO>>> GetMinDurationFlights()
    {
        try
        {
            var result = await service.GetFlightsWithMinDuration();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting min duration flights");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Passengers on the flight with zero baggage, ordered by full name
    /// </summary>
    [HttpGet("flight/{flightCode}/zero-baggage-passengers")]
    [ProducesResponseType(typeof(List<PassengerDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PassengerDTO>>> GetPassengersWithZeroBaggage(string flightCode)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(flightCode))
            {
                logger.LogWarning("Empty flight code provided for zero baggage passengers query");
                return BadRequest("Flight code is required");
            }

            var result = await service.GetPassengersWithZeroBaggageOnFlight(flightCode);

            if (result.Count == 0)
            {
                logger.LogInformation("No passengers with zero baggage found for flight code {FlightCode}", flightCode);
                return NotFound($"No passengers with zero baggage found for flight {flightCode}");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting passengers with zero baggage for flight code {FlightCode}", flightCode);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Summary of all flights of the model during period
    /// </summary>
    [HttpGet("model/{modelId:int}/flights")]
    [ProducesResponseType(typeof(List<FlightDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FlightDTO>>> GetFlightsOfModelInPeriod(
        int modelId,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate)
    {
        try
        {
            if (modelId <= 0)
            {
                logger.LogWarning("Invalid model ID {ModelId} provided", modelId);
                return BadRequest("Model ID must be greater than 0");
            }

            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
            {
                logger.LogWarning("Invalid date range: fromDate {FromDate} is after toDate {ToDate}",
                    fromDate, toDate);
                return BadRequest("From date cannot be after to date");
            }

            var result = await service.GetFlightsOfModelInPeriod(modelId, fromDate, toDate);

            if (result.Count == 0)
            {
                logger.LogInformation("No flights found for model ID {ModelId} in the specified period", modelId);
                return NotFound($"No flights found for model {modelId} in the specified period");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting flights for model ID {ModelId}", modelId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Flights by departure and arrival cities
    /// </summary>
    [HttpGet("flights-by-route")]
    [ProducesResponseType(typeof(List<FlightByRouteDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FlightByRouteDTO>>> GetFlightsByRoute(
        [FromQuery] string departureCity,
        [FromQuery] string arrivalCity)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(departureCity))
            {
                logger.LogWarning("Empty departure city provided for flights by route query");
                return BadRequest("Departure city is required");
            }

            if (string.IsNullOrWhiteSpace(arrivalCity))
            {
                logger.LogWarning("Empty arrival city provided for flights by route query");
                return BadRequest("Arrival city is required");
            }

            var result = await service.GetFlightsByRoute(departureCity, arrivalCity);

            if (result.Count == 0)
            {
                logger.LogInformation("No flights found from {DepartureCity} to {ArrivalCity}",
                    departureCity, arrivalCity);
                return NotFound($"No flights found from {departureCity} to {arrivalCity}");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting flights from {DepartureCity} to {ArrivalCity}",
                departureCity, arrivalCity);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}