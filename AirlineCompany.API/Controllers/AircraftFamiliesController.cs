using AirlineCompany.Application.Services;
using AirlineCompany.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.API.Controllers;

/// <summary>
/// Controller for managing aircraft families
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AircraftFamiliesController(AircraftFamilyService _service, AircraftModelService _modelService) : ControllerBase
{
    /// <summary>
    /// Returns a list of all aircraft families
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult GetAll() =>
        Ok(_service.GetAircraftFamilies());

    /// <summary>
    /// Returns information about aircraft family by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Get(int id)
    {
        var entity = _service.GetAircraftFamily(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// Returns aircraft models for family
    /// </summary>
    [HttpGet("{id}/models")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetModels(int id)
    {
        var family = _service.GetAircraftFamily(id);
        if (family == null) return NotFound();

        var models = _modelService
            .GetModelsByFamilyId(id)
            .ToList();

        return Ok(models);
    }

    /// <summary>
    /// Create a new aircraft family
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    public ActionResult Create([FromBody] AircraftFamilyCreateDTO dto)
    {
        var id = _service.CreateAircraftFamily(dto);
        return CreatedAtAction(nameof(Get), new { id }, dto);
    }

    /// <summary>
    /// Update aircraft family by ID
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Update(int id, [FromBody] AircraftFamilyCreateDTO dto)
    {
        var updated = _service.UpdateAircraftFamily(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    /// <summary>
    /// Delete aircraft family by ID
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult Delete(int id)
    {
        _service.DeleteAircraftFamily(id);
        return NoContent();
    }
}