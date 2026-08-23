using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(role =>
            {
                role.ToTable(nameof(Role));
                role.HasKey(e => e.Id);
                role.Property(e => e.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.ToTable(nameof(User));
                user.HasKey(e => e.Id);
                user.HasIndex(e => e.UserName).IsUnique();
                user.HasIndex(e => e.Email).IsUnique();
                user.Property(e => e.PasswordHash).IsRequired();

                user.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserSession>(session =>
            {
                session.ToTable(nameof(UserSession));
                session.HasKey(e => e.UserId);

                session.HasOne(e => e.User)
                .WithOne(e => e.UserSession)
                .HasForeignKey<UserSession>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
