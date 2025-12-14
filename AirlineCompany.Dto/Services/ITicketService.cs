namespace AirlineCompany.Dto.Services;

/// <summary>
/// Service interface for managing ticket entities
/// </summary>
public interface ITicketService
{
    /// <summary>
    /// Creates a new ticket
    /// </summary>
    /// <param name="dto">Data for creating ticket</param>
    /// <returns>ID of the created ticket</returns>
    public Task<int> CreateTicket(TicketCreateDto dto);

    /// <summary>
    /// Gets all tickets
    /// </summary>
    /// <returns>List of all tickets</returns>
    public Task<List<TicketDto>> GetTickets();

    /// <summary>
    /// Gets tickets by flight ID
    /// </summary>
    /// <param name="flightId">Flight ID</param>
    /// <returns>List of tickets for the specified flight</returns>
    public Task<List<TicketDto>> GetTicketsByFlightId(int flightId);

    /// <summary>
    /// Gets tickets by passenger ID
    /// </summary>
    /// <param name="passengerId">Passenger ID</param>
    /// <returns>List of tickets for the specified passenger</returns>
    public Task<List<TicketDto>> GetTicketsByPassengerId(int passengerId);

    /// <summary>
    /// Gets ticket by ID
    /// </summary>
    /// <param name="id">Ticket ID</param>
    /// <returns>Ticket or null if not found</returns>
    public Task<TicketDto?> GetTicket(int id);

    /// <summary>
    /// Updates an existing ticket
    /// </summary>
    /// <param name="id">Ticket ID</param>
    /// <param name="dto">Updated ticket data</param>
    /// <returns>Updated ticket or null if not found</returns>
    public Task<TicketDto?> UpdateTicket(int id, TicketCreateDto dto);

    /// <summary>
    /// Deletes a ticket by ID
    /// </summary>
    /// <param name="id">Ticket ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public Task<bool> DeleteTicket(int id);
}