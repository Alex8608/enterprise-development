using AirlineCompany.Application.Services;
using AirlineCompany.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirlineCompany.API.Controllers;

/// <summary>
/// Controller for managing passengers
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PassengersController(PassengerService _service, TicketService _ticketService) : ControllerBase
{
    /// <summary>
    /// Returns a list of all passengers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult GetAll() =>
        Ok(_service.GetPassengers());

    /// <summary>
    /// Returns information about passenger by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Get(int id)
    {
        var entity = _service.GetPassenger(id);
        if (entity == null)
            return NotFound();
        return Ok(entity);
    }

    /// <summary>
    /// Returns all tickets for this passenger
    /// </summary>
    [HttpGet("{id}/tickets")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetTickets(int id)
    {
        var passenger = _service.GetPassenger(id);
        if (passenger == null) return NotFound();

        var tickets = _ticketService
            .GetTicketsByPassengerId(id)
            .ToList();

        return Ok(tickets);
    }

    /// <summary>
    /// Create a new passenger
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    public ActionResult Create([FromBody] PassengerCreateDTO dto)
    {
        var id = _service.CreatePassenger(dto);
        return CreatedAtAction(nameof(Get), new { id }, dto);
    }

    /// <summary>
    /// Update passenger by ID
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult Update(int id, [FromBody] PassengerCreateDTO dto)
    {
        var updated = _service.UpdatePassenger(id, dto);
        if (updated == null)
            return NotFound();
        return Ok(updated);
    }

    /// <summary>
    /// Delete passenger by ID
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public ActionResult Delete(int id)
    {
        _service.DeletePassenger(id);
        return NoContent();
    }
}