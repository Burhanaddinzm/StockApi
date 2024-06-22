using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TestApi.Data;
using TestApi.Models;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;

public class StockRepository : Repository<Stock>, IStockRepository
{
    public StockRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Stock>> GetAllWithOrderAsync(
        string? sortBy,
        bool isDescending,
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

        return await query.ToListAsync();
    }
}
