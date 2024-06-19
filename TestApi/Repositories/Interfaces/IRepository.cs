using System.Linq.Expressions;
using TestApi.Models.Common;

namespace TestApi.Repositories.Interfaces;

public interface IRepository<T> where T : BaseAuditableEntity
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>>? expression, params string[] includes);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes);
}
