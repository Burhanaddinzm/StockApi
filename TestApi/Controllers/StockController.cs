using Microsoft.AspNetCore.Mvc;
using TestApi.Dtos.Stock;
using TestApi.Helpers.Query;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
/// <summary>
/// The StockController class is responsible for handling requests related to Stocks.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    /// <summary>
    /// The stock service.
    /// </summary>
    private readonly IStockService _stockService;

    /// <summary>
    /// Initializes a new instance of the <see cref="StockController"/> class.
    /// </summary>
    /// <param name="stockService">The stock service.</param>
    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    /// <summary>
    /// Gets all stocks.
    /// </summary>
    /// <param name="query">The query object.</param>
    /// <returns>The list of stocks.</returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject? query)
    {
        var stocks = await _stockService.GetAllStocksAsync(query);

        if (stocks.Count == 0)
        {
            return NotFound("Stocks not found!");
        }
        return Ok(stocks.Select(x => x.ToStockDto()));
    }

    /// <summary>
    /// Gets a stock by id.
    /// </summary>
    /// <param name="id">The stock id.</param>
    /// <returns>The stock.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int? id)
    {
        if (id == null || id <= 0)
        {
            return BadRequest("Invalid id!");
        }

        if (await _stockService.GetStockAsync(id.Value) is not Stock stock)
        {
            return NotFound("Stock not found!");
        }
        return Ok(stock.ToStockDto());
    }

    /// <summary>
    /// Creates a new stock.
    /// </summary>
    /// <param name="stockDto">The stock DTO.</param>
    /// <returns>The created stock.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var stock = await _stockService.CreateStockAsync(stockDto);
        return CreatedAtAction(nameof(Get), new { id = stock.Id }, stock.ToStockDto());
    }

    /// <summary>
    /// Updates a stock.
    /// </summary>
    /// <param name="id">The stock id.</param>
    /// <param name="stockDto">The stock DTO.</param>
    /// <returns>The updated stock.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int? id, [FromBody] UpdateStockDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (id == null || id <= 0)
        {
            return BadRequest("Invalid id!");
        }

        var stock = await _stockService.UpdateStockAsync(id.Value, stockDto);

        if (stock == null)
        {
            return NotFound("Stock not found!");
        }
        return Ok(stock.ToStockDto());
    }

    /// <summary>
    /// Deletes a stock.
    /// </summary>
    /// <param name="id">The stock id.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int? id)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (id == null || id <= 0)
        {
            return BadRequest("Invalid id!");
        }

        await _stockService.DeleteStockAsync(id.Value);
        return NoContent();
    }
}
