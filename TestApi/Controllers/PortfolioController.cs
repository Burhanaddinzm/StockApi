using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApi.Extensions.Claims;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
/// <summary>
/// The PortfolioController class is responsible for handling HTTP requests related to portfolios.
/// </summary>
[ApiController]
[Route("api/[Controller]")]
[Authorize]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IPortfolioService _portfolioService;
    private readonly IStockService _stockService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PortfolioController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="portfolioService">The portfolio service.</param>
    /// <param name="stockService">The stock service.</param>
    public PortfolioController(
        UserManager<AppUser> userManager,
        IPortfolioService portfolioService,
        IStockService stockService)
    {
        _userManager = userManager;
        _portfolioService = portfolioService;
        _stockService = stockService;
    }

    /// <summary>
    /// Gets the portfolio of the current user.
    /// </summary>
    /// <returns>The user's portfolio.</returns>
    /// <response code="200">The user's portfolio.</response>
    /// <response code="404">The username or user not found.</response>
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

    /// <summary>
    /// Creates a new stock in the current user's portfolio.
    /// </summary>
    /// <param name="symbol">The symbol of the stock.</param>
    /// <returns>A response indicating the success or failure of the operation.</returns>
    /// <response code="201">The stock was successfully added to the portfolio.</response>
    /// <response code="400">The stock already exists in the portfolio.</response>
    /// <response code="404">The username, user, or stock not found.</response>
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

    /// <summary>
    /// Deletes a stock from the current user's portfolio.
    /// </summary>
    /// <param name="symbol">The symbol of the stock.</param>
    /// <returns>A response indicating the success or failure of the operation.</returns>
    /// <response code="204">The stock was successfully deleted from the portfolio.</response>
    /// <response code="400">The stock does not exist in the portfolio.</response>
    /// <response code="404">The username or user not found.</response>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string symbol)
    {
        var username = User.GetUserName();
        if (username == null) return NotFound("Username not found!");

        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return NotFound("User not found!");

        var userPortfolio = await _portfolioService.GetPortfolioAsync(user);

        var filteredStock = userPortfolio.Where(x => x.Symbol.ToLower().Trim() == symbol.ToLower().Trim()).ToList();

        if (filteredStock.Any())
        {
            await _portfolioService.DeletePortfolioAsync(user, symbol);
        }
        else
        {
            return BadRequest("Stock not in your portfolio!");
        }

        return NoContent();
    }
}
