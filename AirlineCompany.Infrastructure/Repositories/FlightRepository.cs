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
    public int Create(Flight entity)
    {
        dbContext.Flights.Add(entity);
        dbContext.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Return all Flight records
    /// </summary>
    public List<Flight> Read() =>
        dbContext.Flights
            .Include(x => x.AircraftModel)
                .ThenInclude(m => m!.AircraftFamily)
            .Include(x => x.Tickets)
            .AsNoTracking()
            .ToList();

    /// <summary>
    /// Return Flight by ID
    /// </summary>
    public Flight? Read(int id) =>
        dbContext.Flights
            .Include(x => x.AircraftModel)
                .ThenInclude(m => m!.AircraftFamily)
            .Include(x => x.Tickets)
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Update Flight by ID
    /// </summary>
    public Flight? Update(int id, Flight entity)
    {
        var existingEntity = dbContext.Flights.Find(id);
        if (existingEntity == null) return null;

        existingEntity.Code = entity.Code;
        existingEntity.DepartureCity = entity.DepartureCity;
        existingEntity.ArrivalCity = entity.ArrivalCity;
        existingEntity.DepartureDate = entity.DepartureDate;
        existingEntity.ArrivalDate = entity.ArrivalDate;
        existingEntity.Duration = entity.Duration;
        existingEntity.AircraftModelId = entity.AircraftModelId;

        dbContext.SaveChanges();

        dbContext.Entry(existingEntity)
            .Reference(f => f.AircraftModel)
            .Load();

        dbContext.Entry(existingEntity.AircraftModel!)
            .Reference(m => m.AircraftFamily)
            .Load();

        return existingEntity;
    }

    /// <summary>
    /// Delete Flight by ID
    /// </summary>
    public bool Delete(int id)
    {
        var existingEntity = dbContext.Flights.Find(id);

        if (existingEntity == null) return false;

        dbContext.Flights.Remove(existingEntity);
        dbContext.SaveChanges();

        return true;
    }

    /// <summary>
    /// Get top N flights by passenger count
    /// </summary>
    public List<Flight> GetTopFlightsByPassengerCount(int topN) =>
        dbContext.Flights
            .Include(f => f.Tickets)
            .Select(f => new
            {
                Flight = f,
                PassengerCount = f.Tickets.Count
            })
            .OrderByDescending(x => x.PassengerCount)
            .Take(topN)
            .Select(x => x.Flight)
            .ToList();

    /// <summary>
    /// Get flights with minimal duration
    /// </summary>
    public List<Flight> GetFlightsWithMinimalDuration()
    {
        var minDuration = dbContext.Flights.Min(f => f.Duration);
        return dbContext.Flights
            .Where(f => f.Duration == minDuration)
            .ToList();
    }

    /// <summary>
    /// Get flights by departure and arrival cities
    /// </summary>
    public List<Flight> GetFlightsByRoute(string departureCity, string arrivalCity) =>
        dbContext.Flights
            .Where(f => f.DepartureCity == departureCity && f.ArrivalCity == arrivalCity)
            .ToList();

    /// <summary>
    /// Get flights within specified time period
    /// </summary>
    public List<Flight> GetFlightsInPeriod(DateTime startDate, DateTime endDate) =>
        dbContext.Flights
            .Where(f => f.DepartureDate >= startDate && f.DepartureDate <= endDate)
            .ToList();
}