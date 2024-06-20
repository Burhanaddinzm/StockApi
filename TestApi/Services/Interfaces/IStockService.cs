using TestApi.Dtos.Stock;
using TestApi.Models;

namespace TestApi.Services.Interfaces;

public interface IStockService
{
    Task<List<Stock>> GetAllStocksAsync();
    Task<Stock?> GetStockAsync(int id);
    Task<Stock> CreateStockAsync(CreateStockDto stockDto);
    Task<Stock?> UpdateStockAsync(int id, UpdateStockDto stockDto);
    Task DeleteStockAsync(int id);
}
