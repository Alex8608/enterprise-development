using AirlineCompany.Core.Entities;
using AirlineCompany.Core.Repositories;
using AirlineCompany.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirlineCompany.Infrastructure.Repositories;

/// <summary>
/// Repository for managing AircraftModel entities in the database
/// </summary>
public class AircraftModelRepository(AppDbContext dbContext) : IRepository<AircraftModel>
{
    /// <summary>
    /// Create a new AircraftModel record
    /// </summary>
    public async Task<int> Create(AircraftModel entity)
    {
        await dbContext.AircraftModels.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Return all AircraftModel records
    /// </summary>
    public async Task<List<AircraftModel>> Read() =>
        await dbContext.AircraftModels
            .Include(x => x.AircraftFamily)
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Return AircraftModel by ID
    /// </summary>
    public async Task<AircraftModel?> Read(int id) =>
        await dbContext.AircraftModels
            .Include(x => x.AircraftFamily)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Update AircraftModel by ID
    /// </summary>
    public async Task<AircraftModel?> Update(int id, AircraftModel entity)
    {
        var existingEntity = await dbContext.AircraftModels.FindAsync(id);

        if (existingEntity == null) return null;

        existingEntity.Name = entity.Name;
        existingEntity.AircraftFamilyId = entity.AircraftFamilyId;
        existingEntity.Range = entity.Range;
        existingEntity.PassengerCapacity = entity.PassengerCapacity;
        existingEntity.CargoCapacity = entity.CargoCapacity;
        await dbContext.SaveChangesAsync();

        await dbContext.Entry(existingEntity)
            .Reference(m => m.AircraftFamily)
            .LoadAsync();

        return existingEntity;
    }

    /// <summary>
    /// Delete AircraftModel by ID
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var existingEntity = await dbContext.AircraftModels.FindAsync(id);

        if (existingEntity == null) return false;

        dbContext.AircraftModels.Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return true;
    }
}