using TestApi.Models;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;

public class PortfolioManager : IPortfolioService
{
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioManager(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<List<Stock>> GetPortfolioAsync(AppUser user)
    {
        //var portfolios = await _portfolioRepository.GetAllAsync(x => x.AppUserId == user.Id, "Stock");
        //return portfolios.Select(x => new Stock
        //{
        //    Id = x.StockId,
        //    Symbol = x.Stock.Symbol,
        //    CompanyName = x.Stock.CompanyName,
        //    Purchase = x.Stock.Purchase,
        //    LastDiv = x.Stock.LastDiv,
        //    Industry = x.Stock.Industry,
        //    MarketCap = x.Stock.MarketCap,
        //}).ToList();

        return await _portfolioRepository.GetUserStocksAsync(user);
    }

    public async Task<Portfolio> CreatePortfolioAsync(string userId, int stockId)
    {
        var portfolio = new Portfolio
        {
            StockId = stockId,
            AppUserId = userId,
        };

        await _portfolioRepository.CreateAsync(portfolio);
        return portfolio;
    }
}
