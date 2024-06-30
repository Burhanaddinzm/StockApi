using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;


/// <summary>
/// Represents a repository for portfolio entities.
/// </summary>
public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PortfolioRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public PortfolioRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets the list of stocks for a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The list of stocks.</returns>
    public async Task<List<Stock>> GetUserStocksAsync(AppUser user)
    {
        return await _context.Portfolios.Where(x => x.AppUserId == user.Id).Select(x => new Stock
        {
            Id = x.StockId,
            Symbol = x.Stock.Symbol,
            CompanyName = x.Stock.CompanyName,
            Purchase = x.Stock.Purchase,
            LastDiv = x.Stock.LastDiv,
            Industry = x.Stock.Industry,
            MarketCap = x.Stock.MarketCap,
        }).ToListAsync();
    }

    /// <summary>
    /// Deletes an entity with the specified id from the database.
    /// </summary>
    /// <param name="id">The id of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">PortfolioRepository can't use DeleteAsync(int id) method.</exception>
    public async new Task DeleteAsync(int id)
    {
        throw new InvalidOperationException("PortfolioRepository can't use DeleteAsync(int id) method.");
    }

    /// <summary>
    /// Deletes an entity with the specified user and symbol from the database.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="symbol">The symbol.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="Exception">
    /// Entity with userId \"{user.Id}\" does not exist or symbol \"{symbol}\" does not match!
    /// or
    /// Could not delete entity in the database.
    /// </exception>
    public async Task DeleteAsync(AppUser user, string symbol)
    {
        try
        {
            var entity = await _context.Set<Portfolio>()
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol.ToLower().Trim() == symbol.ToLower().Trim());

            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<Portfolio>().Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Entity with userId \"{user.Id}\" does not exist or symbol \"{symbol}\" does not match!");
            }
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Could not delete entity in the database.", ex);
        }
    }
}

