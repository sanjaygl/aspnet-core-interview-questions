using AuthService.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Database
{
    public class DbSeeder
    {
        public static async Task SeedAsync(UserDbContext context)
        {
            if (!await context.Roles.AnyAsync())
            {
                var defaultRoles = new List<Role>
                {
                    new Role{  Name = "Admin"},
                    new Role {  Name ="User"}
                };
                await context.Roles.AddRangeAsync(defaultRoles);
                await context.SaveChangesAsync();
            }

            if (!await context.Users.AnyAsync())
            {
                var adminRole = await context.Roles.SingleAsync(r => r.Name == "Admin");
                var passwordHasher = new PasswordHasher<User>();
                var adminUser = new User
                {
                    UserName = "sbopche",
                    Email = "admin@company.com",
                    RoleId = adminRole.Id,
                };
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin@123");

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
