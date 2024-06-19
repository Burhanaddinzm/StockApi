using System.Linq.Expressions;
using TestApi.Models;

namespace TestApi.Services.Interfaces;

public interface IStockService
{
    Task<List<Stock>> GetAllStocksAsync();
    Task<Stock?> GetStockAsync(int id);
    Task<Stock?> GetStockAsync(Expression<Func<Stock, bool>> expression);
}
