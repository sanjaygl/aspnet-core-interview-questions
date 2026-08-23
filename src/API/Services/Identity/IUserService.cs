using API.Models;

namespace API.Services.Identity;
public interface IUserService
{
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponse> RefreshTokenAsync(TokenRequest request);
}
