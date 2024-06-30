using TestApi.Models;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;


/// <summary>
/// Represents a service for managing portfolios.
/// </summary>
public class PortfolioManager : IPortfolioService
{
    /// <summary>
    /// The repository for portfolio entities.
    /// </summary>
    private readonly IPortfolioRepository _portfolioRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PortfolioManager"/> class.
    /// </summary>
    /// <param name="portfolioRepository">The repository for portfolio entities.</param>
    public PortfolioManager(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    /// <summary>
    /// Asynchronously gets the list of stocks for a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The list of stocks.</returns>
    public async Task<List<Stock>> GetPortfolioAsync(AppUser user)
    {
        return await _portfolioRepository.GetUserStocksAsync(user);
    }

    /// <summary>
    /// Asynchronously creates a new portfolio for a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="stockId">The ID of the stock.</param>
    /// <returns>The created portfolio.</returns>
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

    /// <summary>
    /// Asynchronously deletes a portfolio for a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="symbol">The symbol of the stock.</param>
    public async Task DeletePortfolioAsync(AppUser user, string symbol)
    {
        await _portfolioRepository.DeleteAsync(user, symbol);
    }
}
