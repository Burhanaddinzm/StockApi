using TestApi.Dtos.Stock;
using TestApi.Mappers;
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
        return await _stockRepository.GetByIdAsync(id, "Comments", "Portfolios");
    }

    public async Task<Stock> CreateStockAsync(CreateStockDto stockDto)
    {
        var stock = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stock);
        return stock;
    }

    public async Task<Stock?> UpdateStockAsync(int id, UpdateStockDto stockDto)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock != null)
        {
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            await _stockRepository.UpdateAsync(stock);
        }
        return stock;
    }

    public async Task DeleteStockAsync(int id)
    {
        await _stockRepository.DeleteAsync(id);
    }
}
