using AirlineCompany.Application.Services;
using AirlineCompany.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.API.Controllers;

/// <summary>
/// Controller for managing flights
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FlightsController(FlightService _service, TicketService _ticketService) : ControllerBase
{
    /// <summary>
    /// Returns a list of all flights
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult GetAll() =>
        Ok(_service.GetFlights());

    /// <summary>
    /// Returns information about flight by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Get(int id)
    {
        var entity = _service.GetFlight(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// Returns all tickets for this flight
    /// </summary>
    [HttpGet("{id}/tickets")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetTickets(int id)
    {
        var flight = _service.GetFlight(id);
        if (flight == null) return NotFound();

        var tickets = _ticketService
            .GetTicketsByFlightId(id)
            .ToList();

        return Ok(tickets);
    }

    /// <summary>
    /// Create a new flight
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    public ActionResult Create([FromBody] FlightCreateDTO dto)
    {
        try
        {
            var id = _service.CreateFlight(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update flight by ID
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Update(int id, [FromBody] FlightCreateDTO dto)
    {
        try
        {
            var updated = _service.UpdateFlight(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete flight by ID
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult Delete(int id)
    {
        _service.DeleteFlight(id);
        return NoContent();
    }
}