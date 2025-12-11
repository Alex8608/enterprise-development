using AirlineCompany.Application.Services;
using AirlineCompany.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.API.Controllers;

/// <summary>
/// Controller for managing aircraft models
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AircraftModelsController(AircraftModelService _service, FlightService _flightService) : ControllerBase
{
    /// <summary>
    /// Returns a list of all aircraft models
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult GetAll() =>
        Ok(_service.GetAircraftModels());

    /// <summary>
    /// Returns information about aircraft model by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Get(int id)
    {
        var entity = _service.GetAircraftModel(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// Returns all flights for this aircraft model
    /// </summary>
    [HttpGet("{id}/flights")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetFlights(int id)
    {
        var model = _service.GetAircraftModel(id);
        if (model == null) return NotFound();

        var flights = _flightService
            .GetFlightsByModelId(id)
            .ToList();

        return Ok(flights);
    }

    /// <summary>
    /// Create a new aircraft model
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    public ActionResult Create([FromBody] AircraftModelCreateDTO dto)
    {
        try
        {
            var id = _service.CreateAircraftModel(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update aircraft model by ID
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Update(int id, [FromBody] AircraftModelCreateDTO dto)
    {
        try
        {
            var updated = _service.UpdateAircraftModel(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete aircraft model by ID
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult Delete(int id)
    {
        _service.DeleteAircraftModel(id);
        return NoContent();
    }
}