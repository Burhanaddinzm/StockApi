using System.Linq.Expressions;
using TestApi.Models;

namespace TestApi.Repositories.Interfaces;

public interface IStockRepository : IRepository<Stock>
{
    Task<List<Stock>> GetAllWithQueryParamsAsync(
    string? sortBy,
    bool isDescending,
    int page,
    int pageSize,
    Expression<Func<Stock, bool>>? expression = null,
    params string[] includes);
}
