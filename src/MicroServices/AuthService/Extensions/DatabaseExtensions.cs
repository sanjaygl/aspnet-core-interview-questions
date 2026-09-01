using AuthService.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<UserDbContext>();

                await context.Database.MigrateAsync();

                await DbSeeder.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while automatically migrating or seeding the database.");

                throw;
            }
        }
    }
}
