using System.Linq.Expressions;
using TestApi.Models.Common;

namespace TestApi.Repositories.Interfaces;

/// <summary>
/// Represents a generic repository that can be used to perform CRUD operations on entities.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IRepository<T> where T : BaseAuditableEntity
{
    /// <summary>
    /// Asynchronously creates a new entity.
    /// </summary>
    /// <param name="entity">The entity to be created.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateAsync(T entity);

    /// <summary>
    /// Asynchronously updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Asynchronously deletes an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(int id);

    /// <summary>
    /// Asynchronously gets an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to be retrieved.</param>
    /// <param name="includes">The navigation properties to be included.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified ID.</returns>
    Task<T?> GetByIdAsync(int id, params string[] includes);

    /// <summary>
    /// Asynchronously gets an entity based on the specified expression.
    /// </summary>
    /// <param name="expression">The expression to be used for filtering.</param>
    /// <param name="includes">The navigation properties to be included.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity that satisfies the specified expression.</returns>
    Task<T?> GetAsync(Expression<Func<T, bool>>? expression, params string[] includes);

    /// <summary>
    /// Asynchronously gets all entities.
    /// </summary>
    /// <param name="expression">The expression to be used for filtering.</param>
    /// <param name="includes">The navigation properties to be included.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes);

    /// <summary>
    /// Asynchronously checks if an entity with the specified ID exists.
    /// </summary>
    /// <param name="id">The ID of the entity to be checked.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a value indicating whether the entity exists.</returns>
    Task<bool> IsExistsAsync(int id);
}
