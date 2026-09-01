using AuthService.Database.Entities;

namespace AuthService.Repositories.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync(string userName);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task<bool> ExistsAsync(string userName, string email);
}