using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.Api.Controllers;

/// <summary>
/// Controller for managing tickets
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TicketsController(
    ITicketService service,
    ILogger<TicketsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all tickets
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<TicketDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TicketDTO>>> GetAll()
    {
        try
        {
            var result = await service.GetTickets();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting tickets");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about ticket by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TicketDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TicketDTO>> Get(int id)
    {
        try
        {
            var entity = await service.GetTicket(id);
            if (entity == null)
            {
                logger.LogWarning("Ticket with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting ticket with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all tickets for a specific flight
    /// </summary>
    [HttpGet("flight/{flightId:int}")]
    [ProducesResponseType(typeof(List<TicketDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TicketDTO>>> GetTicketsByFlight(int flightId)
    {
        try
        {
            var tickets = await service.GetTicketsByFlightId(flightId);
            if (tickets.Count == 0)
            {
                logger.LogWarning("No tickets found for flight with id {FlightId}", flightId);
                return NotFound();
            }
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting tickets for flight with id {FlightId}", flightId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all tickets for a specific passenger
    /// </summary>
    [HttpGet("passenger/{passengerId:int}")]
    [ProducesResponseType(typeof(List<TicketDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TicketDTO>>> GetTicketsByPassenger(int passengerId)
    {
        try
        {
            var tickets = await service.GetTicketsByPassengerId(passengerId);
            if (tickets.Count == 0)
            {
                logger.LogWarning("No tickets found for passenger with id {PassengerId}", passengerId);
                return NotFound();
            }
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting tickets for passenger with id {PassengerId}", passengerId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new ticket
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TicketDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TicketDTO>> Create([FromBody] TicketCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for ticket creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateTicket(dto);
            var createdEntity = await service.GetTicket(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for ticket creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating ticket");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update ticket by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TicketDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TicketDTO>> Update(int id, [FromBody] TicketCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for ticket update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateTicket(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Ticket with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for ticket update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating ticket with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete ticket by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteTicket(id);
            if (!deleted)
            {
                logger.LogWarning("Ticket with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting ticket with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}