using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Dtos.Account;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
/// <summary>
/// A controller for handling user account operations, such as registration and login.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    /// <summary>
    /// The user manager for handling user operations.
    /// </summary>
    private readonly UserManager<AppUser> _userManager;

    /// <summary>
    /// The sign in manager for handling user authentication.
    /// </summary>
    private readonly SignInManager<AppUser> _signInManager;

    /// <summary>
    /// The token service for generating JWT tokens.
    /// </summary>
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager for handling user operations.</param>
    /// <param name="signInManager">The sign in manager for handling user authentication.</param>
    /// <param name="tokenService">The token service for generating JWT tokens.</param>
    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerDto">The user registration data transfer object.</param>
    /// <returns>An action result indicating the success or failure of the registration.</returns>
    [HttpPost("[Action]")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var createResult = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(
                        new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createResult.Errors);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginDto">The user login data transfer object.</param>
    /// <returns>An action result indicating the success or failure of the login.</returns>
    [HttpPost("[Action]")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower().Trim());

        if (user == null)
        {
            return Unauthorized("Invalid credentials!");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid credentials!");
        }

        return Ok(new NewUserDto
        {
            UserName = user.UserName!,
            Email = user.Email!,
            Token = _tokenService.CreateToken(user)
        });
    }
}
