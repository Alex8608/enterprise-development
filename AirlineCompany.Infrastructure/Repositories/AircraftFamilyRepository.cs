using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirlineCompany.Infrastructure.Repositories;

/// <summary>
/// Repository for managing AircraftFamily entities in the database
/// </summary>
public class AircraftFamilyRepository(AppDbContext dbContext) : IRepository<AircraftFamily>
{
    /// <summary>
    /// Create a new AircraftFamily record
    /// </summary>
    public async Task<int> Create(AircraftFamily entity)
    {
        await dbContext.AircraftFamilies.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all AircraftFamily records
    /// </summary>
    public async Task<List<AircraftFamily>> Read() =>
         await dbContext.AircraftFamilies
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return AircraftFamily by ID
    /// </summary>
    public async Task<AircraftFamily?> Read(int id) =>
         await dbContext.AircraftFamilies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update AircraftFamily by ID
    /// </summary>
    public async Task<AircraftFamily?> Update(int id, AircraftFamily entity)
    {
        var existingEntity = await dbContext.AircraftFamilies.FindAsync(id);
        if (existingEntity == null) return null;

        existingEntity.Name = entity.Name;
        existingEntity.Manufacturer = entity.Manufacturer;
        await dbContext.SaveChangesAsync();


        return existingEntity;
    }

    /// <summary>
    /// Delete AircraftFamily by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.AircraftFamilies.FindAsync(id);

        if (existingEntity == null) return false;

        dbContext.AircraftFamilies.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}