using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApi.Extensions.Claims;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
[ApiController]
[Route("api/[Controller]")]
[Authorize]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IPortfolioService _portfolioService;
    private readonly IStockService _stockService;

    public PortfolioController(
        UserManager<AppUser> userManager,
        IPortfolioService portfolioService,
        IStockService stockService)
    {
        _userManager = userManager;
        _portfolioService = portfolioService;
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var username = User.GetUserName();
        if (username == null) return NotFound("Username not found!");

        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return NotFound("User not found!");

        var userPortfolio = await _portfolioService.GetPortfolioAsync(user);
        return Ok(userPortfolio);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string symbol)
    {
        var username = User.GetUserName();
        if (username == null) return NotFound("Username not found!");

        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return NotFound("User not found!");

        var stock = await _stockService.GetStockBySymbolAsync(symbol);
        if (stock == null) return NotFound("Stock not found!");

        var userPortfolio = await _portfolioService.GetPortfolioAsync(user);

        if (userPortfolio.Any(x => x.Symbol.ToLower().Trim() == symbol.ToLower().Trim()))
            return BadRequest("Can't add same stock twice!");

        var portfolio = await _portfolioService.CreatePortfolioAsync(user.Id, stock.Id);

        return Created();
    }
}
