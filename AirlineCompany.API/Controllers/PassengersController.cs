using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.Api.Controllers;

/// <summary>
/// Controller for managing passengers
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PassengersController(
    IPassengerService service,
    ITicketService ticketService,
    ILogger<PassengersController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all passengers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<PassengerDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<PassengerDTO>>> GetAll()
    {
        try
        {
            var result = await service.GetPassengers();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting passengers");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about passenger by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PassengerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PassengerDTO>> Get(int id)
    {
        try
        {
            var entity = await service.GetPassenger(id);
            if (entity == null)
            {
                logger.LogWarning("Passenger with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting passenger with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all tickets for this passenger
    /// </summary>
    [HttpGet("{id:int}/tickets")]
    [ProducesResponseType(typeof(List<TicketDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<TicketDTO>>> GetTickets(int id)
    {
        try
        {
            var passenger = await service.GetPassenger(id);
            if (passenger == null)
            {
                logger.LogWarning("Passenger with id {Id} not found when getting tickets", id);
                return NotFound();
            }

            var tickets = await ticketService.GetTicketsByPassengerId(id);
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting tickets for passenger with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new passenger
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PassengerDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PassengerDTO>> Create([FromBody] PassengerCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for passenger creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreatePassenger(dto);
            var createdEntity = await service.GetPassenger(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for passenger creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating passenger");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update passenger by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PassengerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PassengerDTO>> Update(int id, [FromBody] PassengerCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for passenger update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdatePassenger(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Passenger with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for passenger update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating passenger with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete passenger by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeletePassenger(id);
            if (!deleted)
            {
                logger.LogWarning("Passenger with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting passenger with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}