using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirlineCompany.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Passenger entities in the database
/// </summary>
public class PassengerRepository(AppDbContext dbContext) : IRepository<Passenger>
{
    /// <summary>
    /// Create a new Passenger record
    /// </summary>
    public int Create(Passenger entity)
    {
        dbContext.Passengers.Add(entity);
        dbContext.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Return all Passenger records
    /// </summary>
    public List<Passenger> Read() =>
        dbContext.Passengers
            .AsNoTracking()
            .ToList();

    /// <summary>
    /// Return Passenger by ID
    /// </summary>
    public Passenger? Read(int id) =>
        dbContext.Passengers
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Update Passenger by ID
    /// </summary>
    public Passenger? Update(int id, Passenger entity)
    {
        var existingEntity = dbContext.Passengers.Find(id);
        if (existingEntity == null) return null;

        var passportExists = dbContext.Passengers
        .Any(p => p.PassportNumber == entity.PassportNumber && p.Id != id);

        if (passportExists)
        {
            throw new ArgumentException($"Passport number {entity.PassportNumber} is already used by another passenger");
        }

        existingEntity.PassportNumber = entity.PassportNumber;
        existingEntity.FullName = entity.FullName;
        existingEntity.DateOfBirth = entity.DateOfBirth;
        dbContext.SaveChanges();

        return existingEntity;
    }

    /// <summary>
    /// Delete Passenger by ID
    /// </summary>
    public bool Delete(int id)
    {
        var existingEntity = dbContext.Passengers.Find(id);

        if (existingEntity == null) return false;

        dbContext.Passengers.Remove(existingEntity);
        dbContext.SaveChanges();

        return true;
    }
}