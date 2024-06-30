using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestApi.Data;
using TestApi.Models.Common;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;


/// <summary>
/// Repository for handling CRUD operations for entities.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public class Repository<T> : IRepository<T> where T : BaseAuditableEntity
{
    protected readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{T}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public virtual async Task CreateAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Could not save entity to the database.", ex);
        }
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public virtual async Task UpdateAsync(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Could not update entity in the database.", ex);
        }
    }

    /// <summary>
    /// Deletes an entity with the specified id from the database.
    /// </summary>
    /// <param name="id">The id of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public virtual async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Entity with id {id} does not exist!");
            }
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Could not delete entity in the database.", ex);
        }
    }

    /// <summary>
    /// Retrieves an entity with the specified id from the database.
    /// </summary>
    /// <param name="id">The id of the entity to retrieve.</param>
    /// <param name="includes">The navigation properties to include.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    public virtual async Task<T?> GetByIdAsync(int id, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Retrieves an entity that satisfies the specified condition from the database.
    /// </summary>
    /// <param name="expression">The condition to filter the entities.</param>
    /// <param name="includes">The navigation properties to include.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>>? expression, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return expression != null
            ? await query.FirstOrDefaultAsync(expression)
            : await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves all entities from the database.
    /// </summary>
    /// <param name="expression">The condition to filter the entities.</param>
    /// <param name="includes">The navigation properties to include.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (expression != null)
        {
            query = query.Where(expression);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Checks if an entity with the specified id exists in the database.
    /// </summary>
    /// <param name="id">The id of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a value indicating whether the entity exists.</returns>
    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Set<T>().AnyAsync(x => x.Id == id);
    }
}
