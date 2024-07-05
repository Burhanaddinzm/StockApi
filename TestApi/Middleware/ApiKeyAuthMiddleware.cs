using TestApi.Common.Auth;

namespace TestApi.Middleware;

/// <summary>
/// Middleware for API key authentication.
/// </summary>
public class ApiKeyAuthMiddleware
{
    /// <summary>
    /// The next request delegate in the pipeline.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// The configuration settings.
    /// </summary>
    private readonly IConfiguration _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeyAuthMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next request delegate in the pipeline.</param>
    /// <param name="config">The configuration settings.</param>
    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the API key header exists
        if (!context.Request.Headers.TryGetValue(AuthenticationConstants.ApiKeyHeaderName, out var extractedApiKey))
        {
            // Respond with 401 Unauthorized and an appropriate message
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key missing");
            return;
        }

        // Get the API key from the configuration
        var apiKey = _config[AuthenticationConstants.ApiKeySectionName];

        // Check if the extracted API key matches the configured API key
        if (!apiKey.Equals(extractedApiKey))
        {
            // Respond with 401 Unauthorized and an appropriate message
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        // Call the next request delegate in the pipeline
        await _next(context);
    }
}
