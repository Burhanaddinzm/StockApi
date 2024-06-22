using System.Linq.Expressions;
using TestApi.Models;

namespace TestApi.Repositories.Interfaces;

public interface IStockRepository : IRepository<Stock>
{
    Task<List<Stock>> GetAllWithOrderAsync(
    string? sortBy,
    bool isDescending,
    Expression<Func<Stock, bool>>? expression = null,
    params string[] includes);
}
