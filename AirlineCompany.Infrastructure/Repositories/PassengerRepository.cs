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
    public async Task<int> Create(Passenger entity)
    {
        await dbContext.Passengers.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all Passenger records
    /// </summary>
    public async Task<List<Passenger>> Read() =>
        await dbContext.Passengers
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return Passenger by ID
    /// </summary>
    public async Task<Passenger?> Read(int id) =>
        await dbContext.Passengers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update Passenger by ID
    /// </summary>
    public async Task<Passenger?> Update(int id, Passenger entity)
    {
        var existingEntity = await dbContext.Passengers.FindAsync(id);
        if (existingEntity == null) return null;

        var passportExists = await dbContext.Passengers
            .AnyAsync(p => p.PassportNumber == entity.PassportNumber && p.Id != id);

        if (passportExists)
        {
            throw new ArgumentException($"Passport number {entity.PassportNumber} is already used by another passenger");
        }

        existingEntity.PassportNumber = entity.PassportNumber;
        existingEntity.FullName = entity.FullName;
        existingEntity.DateOfBirth = entity.DateOfBirth;
        await dbContext.SaveChangesAsync();

        return existingEntity;
    }

    /// <summary>
    /// Delete Passenger by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.Passengers.FindAsync(id);

        if (existingEntity == null) return false;

        dbContext.Passengers.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}