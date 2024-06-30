using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;

/// <summary>
/// Class that handles token creation.
/// </summary>
public class TokenService : ITokenService
{
    /// <summary>
    /// Configuration settings for JWT.
    /// </summary>
    private readonly IConfiguration _config;

    /// <summary>
    /// Symmetric security key used for signing tokens.
    /// </summary>
    private readonly SymmetricSecurityKey _key;

    /// <summary>
    /// Constructor that initializes the configuration and key.
    /// </summary>
    /// <param name="config">Configuration settings.</param>
    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
    }

    /// <summary>
    /// Creates a JWT token for the given user.
    /// </summary>
    /// <param name="user">User to create the token for.</param>
    /// <returns>The created JWT token.</returns>
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.GivenName, user.UserName!)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
