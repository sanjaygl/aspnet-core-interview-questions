namespace API.Services.Identity.Models;

public class UserModel
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; // Default role

    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
}

// Simple thread-safe in-memory store for testing
public static class UserMockDatabase
{
    public static List<UserModel> Users { get; } = new();
}
