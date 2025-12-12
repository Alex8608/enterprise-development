using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirlineCompany.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Flight entities in the database
/// </summary>
public class FlightRepository(AppDbContext dbContext) : IRepository<Flight>
{
    /// <summary>
    /// Create a new Flight record
    /// </summary>
    public async Task<int> Create(Flight entity)
    {
        await dbContext.Flights.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all Flight records
    /// </summary>
    public async Task<List<Flight>> Read() =>
        await dbContext.Flights
            .Include(x => x.AircraftModel)
                .ThenInclude(m => m!.AircraftFamily)
            .Include(x => x.Tickets)
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return Flight by ID
    /// </summary>
    public async Task<Flight?> Read(int id) =>
        await dbContext.Flights
            .Include(x => x.AircraftModel)
                .ThenInclude(m => m!.AircraftFamily)
            .Include(x => x.Tickets)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update Flight by ID
    /// </summary>
    public async Task<Flight?> Update(int id, Flight entity)
    {
        var existingEntity = await dbContext.Flights.FindAsync(id);
        if (existingEntity == null) return null;

        existingEntity.Code = entity.Code;
        existingEntity.DepartureCity = entity.DepartureCity;
        existingEntity.ArrivalCity = entity.ArrivalCity;
        existingEntity.DepartureDate = entity.DepartureDate;
        existingEntity.ArrivalDate = entity.ArrivalDate;
        existingEntity.Duration = entity.Duration;
        existingEntity.AircraftModelId = entity.AircraftModelId;

        await dbContext.SaveChangesAsync();

        await dbContext.Entry(existingEntity)
            .Reference(f => f.AircraftModel)
            .LoadAsync();

        if (existingEntity.AircraftModel != null)
        {
            await dbContext.Entry(existingEntity.AircraftModel)
                .Reference(m => m.AircraftFamily)
                .LoadAsync();
        }

        return existingEntity;
    }

    /// <summary>
    /// Delete Flight by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.Flights.FindAsync(id);

        if (existingEntity == null) return false;

        dbContext.Flights.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Get top N flights by passenger count
    /// </summary>
    public async Task<List<Flight>> GetTopFlightsByPassengerCount(int topN) =>
        await dbContext.Flights
            .Include(f => f.Tickets)
            .Select(f => new
            {
                Flight = f,
                PassengerCount = f.Tickets.Count
            })
            .OrderByDescending(x => x.PassengerCount)
            .Take(topN)
            .Select(x => x.Flight)
            .ToListAsync();

    /// <summary>
    /// Get flights with minimal duration
    /// </summary>
    public async Task<List<Flight>> GetFlightsWithMinimalDuration()
    {
        var minDuration = await dbContext.Flights.MinAsync(f => f.Duration);
        return await dbContext.Flights
            .Where(f => f.Duration == minDuration)
            .ToListAsync();
    }

    /// <summary>
    /// Get flights by departure and arrival cities
    /// </summary>
    public async Task<List<Flight>> GetFlightsByRoute(string departureCity, string arrivalCity) =>
        await dbContext.Flights
            .Where(f => f.DepartureCity == departureCity && f.ArrivalCity == arrivalCity)
            .ToListAsync();

    /// <summary>
    /// Get flights within specified time period
    /// </summary>
    public async Task<List<Flight>> GetFlightsInPeriod(DateTime startDate, DateTime endDate) =>
         await dbContext.Flights
             .Where(f => f.DepartureDate >= startDate && f.DepartureDate <= endDate)
             .ToListAsync();
}