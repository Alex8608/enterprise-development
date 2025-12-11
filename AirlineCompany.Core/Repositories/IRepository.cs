namespace AirlineCompany.Core.Repositories;

/// <summary>
/// Generic repository interface for CRUD operations
/// </summary>
/// <typeparam name="TEntity">Type of entity</typeparam>
public interface IRepository<TEntity>
{
    /// <summary>
    /// Create a new entity
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <returns>Created entity ID</returns>
    public int Create(TEntity entity);

    /// <summary>
    /// Return all entities from repository
    /// </summary>
    /// <returns>List of all entities</returns>
    public List<TEntity> Read();

    /// <summary>
    /// Return entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Entity or null if not found</returns>
    public TEntity? Read(int id);

    /// <summary>
    /// Update entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="entity">Updated entity data</param>
    /// <returns>Updated entity or null if not found</returns>
    public TEntity? Update(int id, TEntity entity);

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>True if deleted, false if not found</returns>
    public bool Delete(int id);
}