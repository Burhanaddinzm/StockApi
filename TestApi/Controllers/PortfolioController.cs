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

    public PortfolioController(
        UserManager<AppUser> userManager,
        IPortfolioService portfolioService)
    {
        _userManager = userManager;
        _portfolioService = portfolioService;
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
}
