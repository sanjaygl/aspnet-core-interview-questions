using API.Database.Entities;
namespace API.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string userName);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(string userName, string email);
    }
}
