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
    public int Create(AircraftFamily entity)
    {
        dbContext.AircraftFamilies.Add(entity);
        dbContext.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Return all AircraftFamily records
    /// </summary>
    public List<AircraftFamily> Read() =>
         dbContext.AircraftFamilies
            .AsNoTracking()
            .ToList();

    /// <summary>
    /// Return AircraftFamily by ID
    /// </summary>
    public AircraftFamily? Read(int id) =>
        dbContext.AircraftFamilies
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Update AircraftFamily by ID
    /// </summary>
    public AircraftFamily? Update(int id, AircraftFamily entity)
    {
        var existingEntity = dbContext.AircraftFamilies.Find(id);
        if (existingEntity == null) return null;

        existingEntity.Name = entity.Name;
        existingEntity.Manufacturer = entity.Manufacturer;
        dbContext.SaveChanges();

        return existingEntity;
    }

    /// <summary>
    /// Delete AircraftFamily by ID
    /// </summary>
    public bool Delete(int id)
    {
        var existingEntity = dbContext.AircraftFamilies.Find(id);

        if (existingEntity == null) return false;

        dbContext.AircraftFamilies.Remove(existingEntity);
        dbContext.SaveChanges();

        return true;
    }
}