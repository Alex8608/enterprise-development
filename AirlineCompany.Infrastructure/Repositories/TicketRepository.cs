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
    public int Create(Ticket entity)
    {
        dbContext.Tickets.Add(entity);
        dbContext.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Return all Ticket records
    /// </summary>
    public List<Ticket> Read() =>
        dbContext.Tickets
            .Include(t => t.Flight)
                .ThenInclude(f => f!.AircraftModel)
                    .ThenInclude(m => m!.AircraftFamily)
            .Include(t => t.Passenger)
            .AsNoTracking()
            .ToList();

    /// <summary>
    /// Return Ticket by ID
    /// </summary>
    public Ticket? Read(int id) =>
        dbContext.Tickets
            .Include(t => t.Flight)
                .ThenInclude(f => f!.AircraftModel)
                    .ThenInclude(m => m!.AircraftFamily)
            .Include(t => t.Passenger)
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Update Ticket by ID
    /// </summary>
    public Ticket? Update(int id, Ticket entity)
    {
        var existingEntity = dbContext.Tickets.Find(id);
        if (existingEntity == null) return null;

        existingEntity.SeatNumber = entity.SeatNumber;
        existingEntity.HasHandLuggage = entity.HasHandLuggage;
        existingEntity.BaggageWeight = entity.BaggageWeight;
        existingEntity.FlightId = entity.FlightId;
        existingEntity.PassengerId = entity.PassengerId;

        dbContext.SaveChanges();

        dbContext.Entry(existingEntity)
            .Reference(t => t.Flight)
            .Load();

        if (existingEntity.Flight != null)
        {
            dbContext.Entry(existingEntity.Flight)
                .Reference(f => f.AircraftModel)
                .Load();

            if (existingEntity.Flight.AircraftModel != null)
            {
                dbContext.Entry(existingEntity.Flight.AircraftModel)
                    .Reference(m => m.AircraftFamily)
                    .Load();
            }
        }
        dbContext.Entry(existingEntity)
            .Reference(t => t.Passenger)
            .Load();

        return existingEntity;
    }

    /// <summary>
    /// Delete Ticket by ID
    /// </summary>
    public bool Delete(int id)
    {
        var existingEntity = dbContext.Tickets.Find(id);

        if (existingEntity == null) return false;

        dbContext.Tickets.Remove(existingEntity);
        dbContext.SaveChanges();

        return true;
    }
}