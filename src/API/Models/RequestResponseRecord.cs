using API.Database.Entities;

namespace API.Models;

public record RegisterRequest(string Username, string Email, string Password);
public record LoginRequest(string Username, string Password);
public record TokenRequest(string AccessToken, string RefreshToken);
public record AuthResponse(bool Success, string Message, string? AccessToken = null, string? RefreshToken = null);
public record OrderResponse(bool IsSuccess, string Message, Order? Data = null);
public record ProductDto(int Id, string Name, string Description, decimal Price, int StockQuantity);
public record CreateProductDto(string Name, string Description, decimal Price, int StockQuantity);
