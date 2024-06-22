using Microsoft.AspNetCore.Mvc;
using TestApi.Dtos.Stock;
using TestApi.Helpers.Query;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

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
