using TestApi.Models;

namespace TestApi.Repositories.Interfaces;

/// <summary>
/// Represents a repository for accessing portfolio data.
/// </summary>
public interface IPortfolioRepository : IRepository<Portfolio>
{
    /// <summary>
    /// Asynchronously gets the user's stocks.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of user's stocks.</returns>
    Task<List<Stock>> GetUserStocksAsync(AppUser user);

    /// <summary>
    /// Asynchronously deletes a portfolio item for the specified user and symbol.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="symbol">The symbol.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(AppUser user, string symbol);
}
