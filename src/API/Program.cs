using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

// QUESTION 1:
// What is WebApplication.CreateBuilder(args)?
// It creates the WebApplicationBuilder used to configure application services, configuration, logging, environment, and the web server.
var builder = WebApplication.CreateBuilder(args);

// QUESTION 2:
// What is builder.Services?
// It is the IServiceCollection used to register services in the Dependency Injection container.
// QUESTION 3:
// What is AddControllers()?
// It registers the services required for controller-based APIs and enables ASP.NET Core to discover and execute controller actions.
builder.Services.AddControllers();

// QUESTION 4:
// Why do we use a custom AddServices() extension method?
// It keeps Dependency Injection registrations organized instead of placing all service registrations directly in Program.cs.
builder.Services.AddServices();

// QUESTION: What is the purpose of AddEndpointsApiExplorer() and AddSwaggerGen()? They enable API endpoint discovery and Swagger/OpenAPI documentation generation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// QUESTION: What is AddRateLimiter()? It registers ASP.NET Core rate-limiting services so we can configure policies that control how many requests can be processed.
builder.Services.AddRateLimiter(options =>
{
    // QUESTION: What is a Fixed Window rate limiter? It allows a configured number of requests during a fixed time window; here, only 2 requests are allowed every 1 minute.
    options.AddFixedWindowLimiter(policyName: "Fixed", options =>
    {
        options.PermitLimit = 2;
        options.Window = TimeSpan.FromMinutes(1);

        // QUESTION: What is QueueProcessingOrder? It determines which queued request is processed first; OldestFirst means the request that entered the queue first is processed first.
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    // QUESTION: What happens when the rate limit is exceeded? The request is rejected with HTTP 429 Too Many Requests.
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});


// QUESTION: What does builder.Build() do? It builds the configured application and creates the WebApplication object used to configure the HTTP request pipeline.
var app = builder.Build();

// QUESTION: What is app.Environment.IsDevelopment()? It checks whether the application is running in the Development environment so development-specific features can be enabled.
if (app.Environment.IsDevelopment())
{
    // QUESTION: What does UseSwagger() do? It enables the Swagger/OpenAPI middleware that serves the generated API documentation.
    app.UseSwagger();

    // QUESTION: What does UseSwaggerUI() do? It provides a browser-based UI for viewing and testing the documented API endpoints.
    app.UseSwaggerUI();
}

// QUESTION: What does UseMiddleware<CustomMiddleware>() do? It adds our custom middleware to the HTTP request pipeline, and its position determines when that middleware executes.
app.UseMiddleware<CustomMiddleware>();

// QUESTION: What does UseRateLimiter() do? It adds rate-limiting middleware to the request pipeline so configured rate-limiting policies can control incoming requests.
app.UseRateLimiter();

// QUESTION: What does UseHttpsRedirection() do? It redirects HTTP requests to HTTPS to help ensure communication between the client and server is encrypted.
app.UseHttpsRedirection();

// QUESTION: What does UseAuthorization() do? It adds authorization middleware that checks whether the current user is allowed to access protected resources.
app.UseAuthorization();

// QUESTION: What does MapControllers() do? It maps controller actions to endpoints so incoming HTTP requests can be routed to the appropriate controller action.
app.MapControllers();

// QUESTION: What does app.Run() do? It starts the application and begins listening for incoming HTTP requests.
app.Run();