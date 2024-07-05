namespace TestApi.Common.Auth;

/// <summary>
/// This class contains constants for authentication.
/// </summary>
public static class AuthenticationConstants
{
    /// <summary>
    /// The name of the API key section in the configuration.
    /// </summary>
    public const string ApiKeySectionName = "Authentication:ApiKey";

    /// <summary>
    /// The name of the API key header.
    /// </summary>
    public const string ApiKeyHeaderName = "x-api-key";
}
