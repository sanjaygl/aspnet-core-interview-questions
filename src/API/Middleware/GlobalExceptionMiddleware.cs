using API.Models;
using System.Net;
using System.Text.Json;

namespace API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Forward request seamlessly to the next middleware (Controllers, etc.)
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled application exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Default to a standard 500 Internal Server error code status
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Custom error tailoring based on database exception categories
        string message = "A critical database or internal server error occurred.";
        if (exception.Message.Contains("duplicate key value"))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict; // 409 Conflict
            message = "This record already exists inside the database engine.";
        }

        // Only reveal raw stack traces to the frontend UI logs when running inside Local Development mode
        var response = _env.IsDevelopment()
            ? new ErrorDetails(context.Response.StatusCode, exception.Message, exception.StackTrace)
            : new ErrorDetails(context.Response.StatusCode, message);

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}
