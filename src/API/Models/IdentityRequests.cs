namespace API.Models;

public record RegisterRequest(string Username, string Email, string Password);
public record LoginRequest(string Username, string Password);
public record TokenRequest(string AccessToken, string RefreshToken);
public record AuthResponse(bool Success, string Message, string? AccessToken = null, string? RefreshToken = null);
