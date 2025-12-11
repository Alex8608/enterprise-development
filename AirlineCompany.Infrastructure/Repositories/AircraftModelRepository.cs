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
    public int Create(AircraftModel entity)
    {
        dbContext.AircraftModels.Add(entity);
        dbContext.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Return all AircraftModel records
    /// </summary>
    public List<AircraftModel> Read() =>
        dbContext.AircraftModels
            .Include(x => x.AircraftFamily)
            .AsNoTracking()
            .ToList();

    /// <summary>
    /// Return AircraftModel by ID
    /// </summary>
    public AircraftModel? Read(int id) =>
        dbContext.AircraftModels
            .Include(x => x.AircraftFamily)
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Update AircraftModel by ID
    /// </summary>
    public AircraftModel? Update(int id, AircraftModel entity)
    {
        var existingEntity = dbContext.AircraftModels.Find(id);

        if (existingEntity == null) return null;

        existingEntity.Name = entity.Name;
        existingEntity.AircraftFamilyId = entity.AircraftFamilyId;
        existingEntity.Range = entity.Range;
        existingEntity.PassengerCapacity = entity.PassengerCapacity;
        existingEntity.CargoCapacity = entity.CargoCapacity;
        dbContext.SaveChanges();

        dbContext.Entry(existingEntity)
            .Reference(m => m.AircraftFamily)
            .Load();

        return existingEntity;
    }

    /// <summary>
    /// Delete AircraftModel by ID
    /// </summary>
    public bool Delete(int id)
    {
        var existingEntity = dbContext.AircraftModels.Find(id);

        if (existingEntity == null) return false;

        dbContext.AircraftModels.Remove(existingEntity);
        dbContext.SaveChanges();

        return true;
    }
}