using System.Security.Claims;

namespace AuthService.Extensions;

public static class ClaimsPrincipalExtensions
{
    // Gets username from an authenticated ClaimsPrincipal.
    public static string? GetUsername(this ClaimsPrincipal user) =>
        user.FindFirst("unique_name")?.Value ??
        user.FindFirst(ClaimTypes.Name)?.Value;

    // Gets username from JWT claims.
    public static string? GetUsername(this IEnumerable<Claim> claims) =>
        claims.FirstOrDefault(c => c.Type == "unique_name")?.Value ??
        claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
}