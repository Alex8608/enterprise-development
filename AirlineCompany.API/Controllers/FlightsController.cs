using AirlineCompany.DTO;
using AirlineCompany.DTO.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.Api.Controllers;

/// <summary>
/// Controller for managing flights
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FlightsController(
    IFlightService service,
    ITicketService ticketService,
    ILogger<FlightsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all flights
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<FlightDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FlightDTO>>> GetAll()
    {
        try
        {
            var result = await service.GetFlights();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting flights");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about flight by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FlightDTO>> Get(int id)
    {
        try
        {
            var entity = await service.GetFlight(id);
            if (entity == null)
            {
                logger.LogWarning("Flight with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting flight with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all tickets for this flight
    /// </summary>
    [HttpGet("{id:int}/tickets")]
    [ProducesResponseType(typeof(List<TicketDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TicketDTO>>> GetTickets(int id)
    {
        try
        {
            var flight = await service.GetFlight(id);
            if (flight == null)
            {
                logger.LogWarning("Flight with id {Id} not found when getting tickets", id);
                return NotFound();
            }

            var tickets = await ticketService.GetTicketsByFlightId(id);
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting tickets for flight with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new flight
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(FlightDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FlightDTO>> Create([FromBody] FlightCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for flight creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateFlight(dto);
            var createdEntity = await service.GetFlight(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for flight creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating flight");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update flight by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(FlightDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FlightDTO>> Update(int id, [FromBody] FlightCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for flight update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateFlight(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Flight with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for flight update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating flight with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete flight by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteFlight(id);
            if (!deleted)
            {
                logger.LogWarning("Flight with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting flight with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}