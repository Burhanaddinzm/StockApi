using TestApi.Dtos.Stock;
using TestApi.Helpers.Query;
using TestApi.Models;

namespace TestApi.Services.Interfaces;

/// <summary>
/// Represents a service for managing stocks.
/// </summary>
public interface IStockService
{
    /// <summary>
    /// Asynchronously gets all stocks with optional query parameters.
    /// </summary>
    /// <param name="query">The optional query parameters.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of stocks.</returns>
    Task<List<Stock>> GetAllStocksAsync(QueryObject? query);

    /// <summary>
    /// Asynchronously gets a stock by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the stock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the stock with the specified identifier, or null if it does not exist.</returns>
    Task<Stock?> GetStockAsync(int id);

    /// <summary>
    /// Asynchronously gets a stock by its symbol.
    /// </summary>
    /// <param name="symbol">The symbol of the stock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the stock with the specified symbol, or null if it does not exist.</returns>
    Task<Stock?> GetStockBySymbolAsync(string symbol);

    /// <summary>
    /// Asynchronously creates a new stock.
    /// </summary>
    /// <param name="stockDto">The DTO containing the stock data.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created stock.</returns>
    Task<Stock> CreateStockAsync(CreateStockDto stockDto);

    /// <summary>
    /// Asynchronously updates a stock.
    /// </summary>
    /// <param name="id">The identifier of the stock.</param>
    /// <param name="stockDto">The DTO containing the updated stock data.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated stock, or null if it does not exist.</returns>
    Task<Stock?> UpdateStockAsync(int id, UpdateStockDto stockDto);

    /// <summary>
    /// Asynchronously deletes a stock.
    /// </summary>
    /// <param name="id">The identifier of the stock.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteStockAsync(int id);

    /// <summary>
    /// Asynchronously checks if a stock with the specified identifier exists.
    /// </summary>
    /// <param name="id">The identifier of the stock.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a value indicating whether the stock exists.</returns>
    Task<bool> StockExistsAsync(int id);
}
