using AirlineCompany.Dto;
using AirlineCompany.Dto.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.Api.Controllers;

/// <summary>
/// Controller for managing aircraft families
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AircraftFamiliesController(
    IAircraftFamilyService service,
    IAircraftModelService modelService,
    ILogger<AircraftFamiliesController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all aircraft families
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<AircraftFamilyDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<AircraftFamilyDTO>>> GetAll()
    {
        try
        {
            var result = await service.GetAircraftFamilies();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting aircraft families");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about aircraft family by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AircraftFamilyDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AircraftFamilyDTO>> Get(int id)
    {
        try
        {
            var entity = await service.GetAircraftFamily(id);
            if (entity == null)
            {
                logger.LogWarning("Aircraft family with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting aircraft family with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns aircraft models for family
    /// </summary>
    [HttpGet("{id:int}/models")]
    [ProducesResponseType(typeof(List<AircraftModelDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<AircraftModelDTO>>> GetModels(int id)
    {
        try
        {
            var family = await service.GetAircraftFamily(id);
            if (family == null)
            {
                logger.LogWarning("Aircraft family with id {Id} not found when getting models", id);
                return NotFound();
            }

            var models = await modelService.GetModelsByFamilyId(id);
            return Ok(models);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting models for aircraft family with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new aircraft family
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AircraftFamilyDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AircraftFamilyDTO>> Create([FromBody] AircraftFamilyCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for aircraft family creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateAircraftFamily(dto);
            var createdEntity = await service.GetAircraftFamily(id);
            
            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for aircraft family creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating aircraft family");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update aircraft family by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AircraftFamilyDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AircraftFamilyDTO>> Update(int id, [FromBody] AircraftFamilyCreateDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for aircraft family update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateAircraftFamily(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Aircraft family with id {Id} not found for update", id);
                return NotFound();
            }
            
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for aircraft family update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating aircraft family with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete aircraft family by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteAircraftFamily(id);
            if (!deleted)
            {
                logger.LogWarning("Aircraft family with id {Id} not found for deletion", id);
                return NotFound();
            }
            
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting aircraft family with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}