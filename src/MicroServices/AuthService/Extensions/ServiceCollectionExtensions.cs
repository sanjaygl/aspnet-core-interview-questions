using AuthService.Database;
using AuthService.Database.Entities;
using AuthService.Options;
using AuthService.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.RateLimiting;

namespace AuthService.Extensions;

public static class ServiceCollectionExtensions
{
    // Scans the provided assembly (or the executing assembly) for concrete classes
    // that have a matching interface named I{ClassName} and registers them as scoped.
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {
        assembly ??= Assembly.GetExecutingAssembly();

        services.RegisterApplicationServices(assembly);
        services.AddAuthenticationServices();
        services.AddCorsPolicy(configuration);
        services.AddJwtOptions(configuration);
        services.AddDatabase(configuration);
        services.AddRateLimiting();

        return services;
    }

    private static IServiceCollection RegisterApplicationServices(
        this IServiceCollection services,
        Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                !t.IsGenericTypeDefinition);

        foreach (var implementation in types)
        {
            var interfaceType =
                implementation.GetInterface($"I{implementation.Name}");

            if (interfaceType == null ||
                interfaceType == typeof(ITokenService))
            {
                continue;
            }

            services.AddScoped(interfaceType, implementation);
        }

        return services;
    }

    private static IServiceCollection AddAuthenticationServices(
        this IServiceCollection services)
    {
        services.AddScoped<PasswordHasher<User>>();
        services.AddSingleton<ITokenService, TokenService>();

        return services;
    }

    private static IServiceCollection AddCorsPolicy(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var allowedOrigins =
            configuration.GetSection("Cors:AllowedOrigins")
                         .Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy("AngularAppPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }

    private static IServiceCollection AddJwtOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention());

        return services;
    }

    private static IServiceCollection AddRateLimiting(
        this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("Fixed", limiterOptions =>
            {
                limiterOptions.PermitLimit = 100;
                limiterOptions.Window = TimeSpan.FromMinutes(1);
                limiterOptions.QueueProcessingOrder =
                    QueueProcessingOrder.OldestFirst;
            });

            options.RejectionStatusCode =
                StatusCodes.Status429TooManyRequests;
        });

        return services;
    }
}