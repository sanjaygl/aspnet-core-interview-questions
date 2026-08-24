using API.Database.Entities;
using API.Extensions;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        IUserRepository userRepository,
        ITokenService tokenService,
        PasswordHasher<User> passwordHasher,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }

    private void SetTokenCookies(string accessToken, string refreshToken)
    {
        var context = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HTTP context is not available.");

        // Access token cookie.
        context.Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            IsEssential = true,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddMinutes(15)
        });

        // Refresh token cookie.
        context.Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            IsEssential = true,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);

        if (user == null)
            return new AuthResponse(false, "Invalid username and password.");

        var result = _passwordHasher.VerifyHashedPassword(
            user, user.PasswordHash, loginRequest.Password);

        if (result == PasswordVerificationResult.Failed)
            return new AuthResponse(false, "Invalid username and password.");

        var accessToken = await _tokenService.GenerateToken(
            user.UserName, user.Email, user.Role.Name);

        var refreshToken = await _tokenService.GenerateRefreshToken();

        // Create session if it doesn't exist.
        user.UserSession ??= new UserSession { UserId = user.Id };

        user.UserSession.RefreshToken = refreshToken;
        user.UserSession.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.SaveChangesAsync();
        SetTokenCookies(accessToken, refreshToken);

        return new AuthResponse(true, "Login successful!");
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var exists = await _userRepository.ExistsAsync(
            registerRequest.Username, registerRequest.Email);

        if (exists)
            return new AuthResponse(false, "Username is already taken");

        var user = new User
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Email,
            RoleId = 2
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user, registerRequest.Password);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new AuthResponse(true, "User registered successfully!");
    }

    public async Task<AuthResponse> RefreshTokenAsync()
    {
        var request = _httpContextAccessor.HttpContext?.Request;

        if (request == null)
            return new AuthResponse(false, "HTTP context is not available.");

        var accessToken = request.Cookies["X-Access-Token"];
        var refreshToken = request.Cookies["X-Refresh-Token"];

        if (string.IsNullOrWhiteSpace(accessToken) ||
            string.IsNullOrWhiteSpace(refreshToken))
            return new AuthResponse(false, "Authentication cookies are missing.");

        JwtSecurityToken jwtToken;

        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
        }
        catch
        {
            return new AuthResponse(false, "Invalid access token.");
        }

        var username = jwtToken.Claims.GetUsername();

        if (string.IsNullOrWhiteSpace(username))
            return new AuthResponse(false, "Username claim is missing from access token.");

        var user = await _userRepository.GetByUsernameAsync(username);

        if (user?.UserSession == null)
            return new AuthResponse(false, "User session does not exist.");

        if (user.UserSession.RefreshToken != refreshToken)
            return new AuthResponse(false, "Invalid refresh token.");

        if (user.UserSession.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return new AuthResponse(false, "Refresh token has expired.");

        var newAccessToken = await _tokenService.GenerateToken(
            user.UserName, user.Email, user.Role.Name);

        var newRefreshToken = await _tokenService.GenerateRefreshToken();

        // Rotate the refresh token.
        user.UserSession.RefreshToken = newRefreshToken;
        user.UserSession.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.SaveChangesAsync();
        SetTokenCookies(newAccessToken, newRefreshToken);

        return new AuthResponse(true, "Tokens refreshed successfully!");
    }
}