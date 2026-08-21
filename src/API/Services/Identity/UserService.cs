using API.Services.Auth;
using API.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace API.Services.Identity;

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<UserModel> _passwordHasher;

    public UserService(
        ITokenService tokenService,
        PasswordHasher<UserModel> passwordHasher)
    {
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = UserMockDatabase.Users.FirstOrDefault(u => u.Username.ToLower() == loginRequest.Username.ToLower());
        if (user == null)
        {
            return new AuthResponse(false, "Invalid username and password.");
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return new AuthResponse(false, "Invalid username and password.");
        }

        var accessToken = _tokenService.GenerateToken(user.Username, user.Email, user.Role);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        return new AuthResponse(true, "Login successful!", accessToken, refreshToken);
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var userExist = UserMockDatabase.Users.Any(u => u.Username.ToLower() == registerRequest.Username.ToLower());
        if (userExist)
        {
            return new AuthResponse(false, "Username is already taken");
        }

        var newUser = new UserModel
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            Role = "User"
        };

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerRequest.Password);

        UserMockDatabase.Users.Add(newUser);

        return new AuthResponse(true, "User registerd successfully!");
    }

    public async Task<AuthResponse> RefreshTokenAsync(TokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken;

        try
        {
            jwtToken = tokenHandler.ReadJwtToken(request.AccessToken);
        }
        catch
        {
            return new AuthResponse(false, "Invalid token provided.");
        }

        var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name" || c.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username)) return new AuthResponse(false, "Invalid access token data.");

        var user = UserMockDatabase.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        if (user == null) return new AuthResponse(false, "User does not exist.");

        if (user.RefreshToken != request.RefreshToken) return new AuthResponse(false, "Invalid refresh token.");
        if (user.RefreshTokenExpiryTime <= DateTime.UtcNow) return new AuthResponse(false, "Refresh token has expired.");

        var newAccessToken = _tokenService.GenerateToken(user.Username, user.Email, user.Role);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        return new AuthResponse(true, "Tokens refreshed successfully!", newAccessToken, newRefreshToken);
    }
}
