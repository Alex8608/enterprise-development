using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirlineCompany.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Ticket entities in the database
/// </summary>
public class TicketRepository(AppDbContext dbContext) : IRepository<Ticket>
{
    /// <summary>
    /// Create a new Ticket record
    /// </summary>
    public async Task<int> Create(Ticket entity)
    {
        await dbContext.Tickets.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all Ticket records
    /// </summary>
    public async Task<List<Ticket>> Read() =>
        await dbContext.Tickets
            .Include(t => t.Flight)
                .ThenInclude(f => f!.AircraftModel)
                    .ThenInclude(m => m!.AircraftFamily)
            .Include(t => t.Passenger)
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return Ticket by ID
    /// </summary>
    public async Task<Ticket?> Read(int id) =>
       await dbContext.Tickets
           .Include(t => t.Flight)
               .ThenInclude(f => f!.AircraftModel)
                   .ThenInclude(m => m!.AircraftFamily)
           .Include(t => t.Passenger)
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update Ticket by ID
    /// </summary>
    public async Task<Ticket?> Update(int id, Ticket entity)
    {
        var existingEntity = await dbContext.Tickets.FindAsync(id);
        if (existingEntity == null) return null;

        existingEntity.SeatNumber = entity.SeatNumber;
        existingEntity.HasHandLuggage = entity.HasHandLuggage;
        existingEntity.BaggageWeight = entity.BaggageWeight;
        existingEntity.FlightId = entity.FlightId;
        existingEntity.PassengerId = entity.PassengerId;

        await dbContext.SaveChangesAsync();

        await dbContext.Entry(existingEntity)
            .Reference(t => t.Flight)
            .LoadAsync();

        if (existingEntity.Flight != null)
        {
            await dbContext.Entry(existingEntity.Flight)
                .Reference(f => f.AircraftModel)
                .LoadAsync();

            if (existingEntity.Flight.AircraftModel != null)
            {
                await dbContext.Entry(existingEntity.Flight.AircraftModel)
                    .Reference(m => m.AircraftFamily)
                    .LoadAsync();
            }
        }
        await dbContext.Entry(existingEntity)
            .Reference(t => t.Passenger)
            .LoadAsync();

        return existingEntity;
    }

    /// <summary>
    /// Delete Ticket by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.Tickets.FindAsync(id);

        if (existingEntity == null) return false;

        dbContext.Tickets.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}