using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;

public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(AppDbContext context) : base(context)
    {
    }

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
}
