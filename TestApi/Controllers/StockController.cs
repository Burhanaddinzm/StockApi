using Microsoft.AspNetCore.Mvc;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var stocks = await _stockService.GetAllStocksAsync();

        if (stocks.Count == 0)
        {
            return NotFound("Stocks not found!");
        }
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int? id)
    {
        if (id == null || id == 0)
        {
            return BadRequest("Id is invalid!");
        }

        if (await _stockService.GetStockAsync(id.Value) is not Stock stock)
        {
            return NotFound("Stock not found!");
        }
        return Ok(stock);
    }
}
