using API.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Services.Auth;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public TokenService(
        IConfiguration configuration,
        IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public Task<string> GenerateToken(
        string username,
        string email,
        string role)
    {
        if (string.IsNullOrWhiteSpace(_jwtOptions.SecretKey))
            throw new InvalidOperationException(
                "JWT SecretKey is missing from configurations.");

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, role),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials);

        return Task.FromResult(
            new JwtSecurityTokenHandler().WriteToken(token));
    }

    public Task<string> GenerateRefreshToken()
    {
        var randomNumber = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Task.FromResult(
            Convert.ToBase64String(randomNumber));
    }
}