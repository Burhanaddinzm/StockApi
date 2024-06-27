using System.Security.Claims;

namespace TestApi.Extensions.Claims;

public static class ClaimsExtension
{
    public static string? GetUserName(this ClaimsPrincipal user) =>
        user.Claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;

    public static string? GetUserEmail(this ClaimsPrincipal user) =>
        user.Claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"))?.Value;
}
