using TestApi.Models;

namespace TestApi.Services.Interfaces;

/// <summary>
/// The interface for the Portfolio Service.
/// This service is responsible for managing portfolios.
/// </summary>
public interface IPortfolioService
{
    /// <summary>
    /// Asynchronously gets the portfolio for a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The list of stocks in the portfolio.</returns>
    Task<List<Stock>> GetPortfolioAsync(AppUser user);
    
    /// <summary>
    /// Asynchronously creates a portfolio for a user with a given stock.
    /// </summary>
    /// <param name="userId">The user's ID.</param>
    /// <param name="stockId">The stock's ID.</param>
    /// <returns>The created portfolio.</returns>
    Task<Portfolio> CreatePortfolioAsync(string userId, int stockId);
    
    /// <summary>
    /// Asynchronously deletes a stock from a user's portfolio.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="symbol">The stock's symbol.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    Task DeletePortfolioAsync(AppUser user, string symbol);
}
