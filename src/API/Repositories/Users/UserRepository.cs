using API.Database;
using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DemoDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.UserSession)
            .FirstOrDefaultAsync(u =>
                u.UserName.ToLower() == userName.ToLower());
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.UserSession)
            .FirstOrDefaultAsync(u =>
                u.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.UserSession)
            .FirstOrDefaultAsync(u =>
                u.UserSession != null &&
                u.UserSession.RefreshToken == refreshToken);
    }

    public async Task<bool> ExistsAsync(string userName, string email)
    {
        return await _context.Users.AnyAsync(u =>
            u.UserName.ToLower() == userName.ToLower() &&
            u.Email.ToLower() == email.ToLower());
    }
}