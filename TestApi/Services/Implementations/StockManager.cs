using System.Linq.Expressions;
using TestApi.Models;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;

public class StockManager : IStockService
{
    private readonly IStockRepository _stockRepository;

    public StockManager(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<List<Stock>> GetAllStocksAsync()
    {
        return await _stockRepository.GetAllAsync(null, "Comments", "Portfolios");
    }

    public async Task<Stock?> GetStockAsync(int id)
    {
        return await _stockRepository.GetByIdAsync(id);
    }

    public async Task<Stock?> GetStockAsync(Expression<Func<Stock, bool>> expression)
    {
        return await _stockRepository.GetAsync(expression, "Comments", "Portfolios");
    }
}
