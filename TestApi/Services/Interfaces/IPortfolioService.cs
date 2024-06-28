using TestApi.Models;

namespace TestApi.Services.Interfaces;

public interface IPortfolioService
{
    Task<List<Stock>> GetPortfolioAsync(AppUser user);
    Task<Portfolio> CreatePortfolioAsync(string userId, int stockId);
}
