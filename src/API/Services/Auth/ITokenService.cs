namespace API.Services.Auth
{
    public interface ITokenService
    {
        string GenerateToken(string username, string email, string role);
        string GenerateRefreshToken();
    }
}