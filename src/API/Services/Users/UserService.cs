using API.Database.Entities;
using API.Models;
using API.Repositories.Users;
using API.Services.Auth;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace API.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        ITokenService tokenService,
        PasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
        if (user == null)
        {
            return new AuthResponse(false, "Invalid username and password.");
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return new AuthResponse(false, "Invalid username and password.");
        }

        var accessToken = await _tokenService.GenerateToken(user.UserName, user.Email, user.Role.Name);
        var refreshToken = await _tokenService.GenerateRefreshToken();

        user.UserSession.RefreshToken = refreshToken;
        user.UserSession.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        return new AuthResponse(true, "Login successful!", accessToken, refreshToken);
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var userExist = await _userRepository.ExistsAsync(registerRequest.Username, registerRequest.Email);
        if (userExist)
        {
            return new AuthResponse(false, "Username is already taken");
        }

        var newUser = new User
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Email,
            RoleId = 2
        };

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerRequest.Password);

        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();

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

        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return new AuthResponse(false, "User does not exist.");
        }

        if (user.UserSession.RefreshToken != request.RefreshToken) return new AuthResponse(false, "Invalid refresh token.");
        if (user.UserSession.RefreshTokenExpiryTime <= DateTime.UtcNow) return new AuthResponse(false, "Refresh token has expired.");

        var newAccessToken = await _tokenService.GenerateToken(user.UserName, user.Email, user.Role.Name);
        var newRefreshToken = await _tokenService.GenerateRefreshToken();

        user.UserSession.RefreshToken = newRefreshToken;
        user.UserSession.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        return new AuthResponse(true, "Tokens refreshed successfully!", newAccessToken, newRefreshToken);
    }
}
