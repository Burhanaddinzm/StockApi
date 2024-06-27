using TestApi.Models;

namespace TestApi.Repositories.Interfaces;

public interface IPortfolioRepository : IRepository<Portfolio>
{
    Task<List<Stock>> GetUserStocksAsync(AppUser user);
}
