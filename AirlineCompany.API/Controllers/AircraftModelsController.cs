using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.Api.Controllers;

/// <summary>
/// Controller for managing aircraft models
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AircraftModelsController(
    IAircraftModelService service,
    IFlightService flightService,
    ILogger<AircraftModelsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all aircraft models
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<AircraftModelDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<AircraftModelDTO>>> GetAll()
    {
        try
        {
            var result = await service.GetAircraftModels();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting aircraft models");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about aircraft model by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AircraftModelDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AircraftModelDTO>> Get(int id)
    {
        try
        {
            var entity = await service.GetAircraftModel(id);
            if (entity == null)
            {
                logger.LogWarning("Aircraft model with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting aircraft model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all flights for this aircraft model
    /// </summary>
    [HttpGet("{id:int}/flights")]
    [ProducesResponseType(typeof(List<FlightDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FlightDTO>>> GetFlights(int id)
    {
        try
        {
            var model = await service.GetAircraftModel(id);
            if (model == null)
            {
                logger.LogWarning("Aircraft model with id {Id} not found when getting flights", id);
                return NotFound();
            }

            var flights = await flightService.GetFlightsByModelId(id);
            return Ok(flights);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting flights for aircraft model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new aircraft model
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AircraftModelDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AircraftModelDTO>> Create([FromBody] AircraftModelCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for aircraft model creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateAircraftModel(dto);
            var createdEntity = await service.GetAircraftModel(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for aircraft model creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating aircraft model");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update aircraft model by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AircraftModelDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AircraftModelDTO>> Update(int id, [FromBody] AircraftModelCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for aircraft model update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateAircraftModel(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Aircraft model with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for aircraft model update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating aircraft model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete aircraft model by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteAircraftModel(id);
            if (!deleted)
            {
                logger.LogWarning("Aircraft model with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting aircraft model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}