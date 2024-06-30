using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TestApi.Data;
using TestApi.Models;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;


/// <summary>
/// Represents a repository for managing stock data in the database.
/// </summary>
public class StockRepository : Repository<Stock>, IStockRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StockRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public StockRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves a list of stocks with query parameters.
    /// </summary>
    /// <param name="sortBy">The property to sort the results by.</param>
    /// <param name="isDescending">Indicates whether to sort the results in descending order.</param>
    /// <param name="page">The page number of results to retrieve.</param>
    /// <param name="pageSize">The number of results per page.</param>
    /// <param name="expression">An optional condition to filter the results.</param>
    /// <param name="includes">The navigation properties to include.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of stocks.</returns>
    public async Task<List<Stock>> GetAllWithQueryParamsAsync(
        string? sortBy,
        bool isDescending,
        int page,
        int pageSize,
        Expression<Func<Stock, bool>>? expression = null,
        params string[] includes)
    {
        IQueryable<Stock> query = _context.Set<Stock>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (sortBy != null)
        {
            var parameter = Expression.Parameter(typeof(Stock), "x");
            var property = typeof(Stock).GetProperties()
                                        .FirstOrDefault(p => p.Name.Equals(sortBy, StringComparison.OrdinalIgnoreCase));
            if (property != null)
            {
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                var orderByMethod = isDescending ? "OrderByDescending" : "OrderBy";
                var orderByCall = Expression.Call(
                    typeof(Queryable),
                    orderByMethod,
                    new Type[] { typeof(Stock), property.PropertyType },
                    query.Expression,
                    Expression.Quote(orderByExpression));

                query = query.Provider.CreateQuery<Stock>(orderByCall);
            }
        }

        int skipNumber = (page - 1) * pageSize;

        return await query.Skip(skipNumber).Take(pageSize).ToListAsync();
    }
}
