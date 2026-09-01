namespace AuthService.Services.Auth
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string username, string email, string role);
        Task<string> GenerateRefreshToken();
    }
}