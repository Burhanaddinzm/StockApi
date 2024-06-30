using System.Security.Claims;

namespace TestApi.Extensions.Claims;

/// <summary>
/// Extension methods for <see cref="ClaimsPrincipal"/>.
/// </summary>
public static class ClaimsExtension
{
    /// <summary>
    /// Gets the user name from the claims principal.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The user name.</returns>
    public static string? GetUserName(this ClaimsPrincipal user) =>
        user.Claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;

    /// <summary>
    /// Gets the user email from the claims principal.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The user email.</returns>
    public static string? GetUserEmail(this ClaimsPrincipal user) =>
        user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
}
