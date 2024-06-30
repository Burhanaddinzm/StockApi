using TestApi.Dtos.Stock;
using TestApi.Helpers.Query;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;

/// <summary>
/// The StockManager class is responsible for managing stock data operations.
/// It provides methods for retrieving stocks, creating, updating, and deleting stocks.
/// </summary>
public class StockManager : IStockService
{
    /// <summary>
    /// The stock repository instance.
    /// </summary>
    private readonly IStockRepository _stockRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="StockManager"/> class.
    /// </summary>
    /// <param name="stockRepository">The stock repository instance.</param>
    public StockManager(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    /// <summary>
    /// Retrieves all stocks from the repository with optional query parameters.
    /// </summary>
    /// <param name="query">The query parameters.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of stocks.</returns>
    public async Task<List<Stock>> GetAllStocksAsync(QueryObject? query)
    {
        const int DefaultPage = 1;
        const int DefaultPageSize = 1;

        if (query != null)
        {
            if (query.CompanyName != null && query.Symbol != null)
            {
                return await _stockRepository.GetAllWithQueryParamsAsync(
                    query.SortBy,
                    query.IsDescending,
                    query.Page,
                    query.PageSize,
                    x => x.Symbol.ToLower().Contains(query.Symbol.ToLower().Trim()) &&
                    x.CompanyName.ToLower().Contains(query.CompanyName.ToLower().Trim()),
                    "Comments",
                    "Portfolios");
            }
            else if (query.Symbol != null)
            {
                return await _stockRepository.GetAllWithQueryParamsAsync(
                    query.SortBy,
                    query.IsDescending,
                    query.Page,
                    query.PageSize,
                    x => x.Symbol.ToLower().Contains(query.Symbol.ToLower().Trim()),
                    "Comments",
                    "Portfolios");
            }
            else if (query.CompanyName != null)
            {
                return await _stockRepository.GetAllWithQueryParamsAsync(
                    query.SortBy,
                    query.IsDescending,
                    query.Page,
                    query.PageSize,
                    x => x.CompanyName.ToLower().Contains(query.CompanyName.ToLower().Trim()),
                    "Comments",
                    "Portfolios");
            }
            else
            {
                return await _stockRepository.GetAllWithQueryParamsAsync(
                    query.SortBy,
                    query.IsDescending,
                    query.Page,
                    query.PageSize,
                    null,
                    "Comments",
                    "Portfolios");
            }
        }
        return await _stockRepository.GetAllWithQueryParamsAsync(
            null,
            false,
            DefaultPage,
            DefaultPageSize,
            null,
            "Comments",
            "Portfolios");
    }

    /// <summary>
    /// Retrieves a stock by its ID from the repository.
    /// </summary>
    /// <param name="id">The ID of the stock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the stock.</returns>
    public async Task<Stock?> GetStockAsync(int id)
    {
        return await _stockRepository.GetByIdAsync(id, "Comments", "Portfolios");
    }

    /// <summary>
    /// Retrieves a stock by its symbol from the repository.
    /// </summary>
    /// <param name="symbol">The symbol of the stock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the stock.</returns>
    public async Task<Stock?> GetStockBySymbolAsync(string symbol)
    {
        return await _stockRepository.GetAsync(
            x => x.Symbol.ToLower().Trim() == symbol.ToLower().Trim(),
            "Comments",
            "Portfolios");
    }

    /// <summary>
    /// Creates a new stock in the repository.
    /// </summary>
    /// <param name="stockDto">The stock data transfer object.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created stock.</returns>
    public async Task<Stock> CreateStockAsync(CreateStockDto stockDto)
    {
        var stock = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stock);
        return stock;
    }

    /// <summary>
    /// Updates an existing stock in the repository.
    /// </summary>
    /// <param name="id">The ID of the stock to update.</param>
    /// <param name="stockDto">The stock data transfer object.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated stock.</returns>
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

    /// <summary>
    /// Deletes a stock from the repository.
    /// </summary>
    /// <param name="id">The ID of the stock to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteStockAsync(int id)
    {
        await _stockRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Checks if a stock with the specified ID exists in the repository.
    /// </summary>
    /// <param name="id">The ID of the stock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a value indicating whether the stock exists.</returns>
    public async Task<bool> StockExistsAsync(int id)
    {
        return await _stockRepository.IsExistsAsync(id);
    }
}
