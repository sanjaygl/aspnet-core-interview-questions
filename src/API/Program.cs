using API.Database;
using API.Extensions;
using API.Middleware;
using API.Options;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularAppPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Essential for secure cross-origin HttpOnly cookies
    });
});

builder.Services.AddValidation();
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.SectionName));

builder.Services.AddDbContext<DemoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "Fixed", options =>
    {
        options.PermitLimit = 100; // OPTIMIZATION: Raised limit from 2 to 100 for dev testing stability
        options.Window = TimeSpan.FromMinutes(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AngularAppPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseMiddleware<CustomMiddleware>();
app.MapControllers();

// Automated schema migration blueprint execution runs smoothly at the very end
await app.SeedDatabaseAsync();

app.Run();
