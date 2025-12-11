using AirlineCompany.Application.Services;
using AirlineCompany.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.API.Controllers;

/// <summary>
/// Controller for managing tickets
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TicketsController(TicketService _service) : ControllerBase
{
    /// <summary>
    /// Returns a list of all tickets
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult GetAll() =>
        Ok(_service.GetTickets());

    /// <summary>
    /// Returns information about ticket by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Get(int id)
    {
        var entity = _service.GetTicket(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// Returns all tickets for a specific flight
    /// </summary>
    [HttpGet("flight/{flightId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetTicketsByFlight(int flightId)
    {
        var tickets = _service.GetTicketsByFlightId(flightId);
        if (!tickets.Any()) return NotFound();
        return Ok(tickets);
    }

    /// <summary>
    /// Returns all tickets for a specific passenger
    /// </summary>
    [HttpGet("passenger/{passengerId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetTicketsByPassenger(int passengerId)
    {
        var tickets = _service.GetTicketsByPassengerId(passengerId);
        if (!tickets.Any()) return NotFound();
        return Ok(tickets);
    }

    /// <summary>
    /// Create a new ticket
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    public ActionResult Create([FromBody] TicketCreateDTO dto)
    {
        try
        {
            var id = _service.CreateTicket(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update ticket by ID
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Update(int id, [FromBody] TicketCreateDTO dto)
    {
        try
        {
            var updated = _service.UpdateTicket(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete ticket by ID
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult Delete(int id)
    {
        _service.DeleteTicket(id);
        return NoContent();
    }
}