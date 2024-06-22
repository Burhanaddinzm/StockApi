using TestApi.Dtos.Stock;
using TestApi.Helpers.Query;
using TestApi.Models;

namespace TestApi.Services.Interfaces;

public interface IStockService
{
    Task<List<Stock>> GetAllStocksAsync(QueryObject? query);
    Task<Stock?> GetStockAsync(int id);
    Task<Stock> CreateStockAsync(CreateStockDto stockDto);
    Task<Stock?> UpdateStockAsync(int id, UpdateStockDto stockDto);
    Task DeleteStockAsync(int id);
    Task<bool> StockExistsAsync(int id);
}
