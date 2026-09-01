using AuthService.Extensions;
using AuthService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// MVC / API
builder.Services.AddControllers();
builder.Services.AddValidation();

// Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();

// Application services
builder.Services.AddServices(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AngularAppPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

// Apply database migrations and seed initial data
await app.SeedDatabaseAsync();

app.Run();