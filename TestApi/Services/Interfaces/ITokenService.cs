using TestApi.Models;

namespace TestApi.Services.Interfaces;

/// <summary>
/// Interface for the Token Service. Provides methods for generating a new JSON Web Token (JWT).
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a new JSON Web Token (JWT) for the given user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    /// <returns>The generated JSON Web Token.</returns>
    string CreateToken(AppUser user);
}
