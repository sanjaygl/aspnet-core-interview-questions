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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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

                // One Role -> Many Users
                user.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserSession>(session =>
            {
                session.ToTable(nameof(UserSession));
                session.HasKey(e => e.UserId);

                // One User -> One Session
                session.HasOne(e => e.User)
                .WithOne(e => e.UserSession)
                .HasForeignKey<UserSession>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2); // Correct scale definition for currency/decimals
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);

                // One User -> Many Orders
                entity.HasOne(o => o.User)
                      .WithMany()
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Wiping a user deletes their order history history logs
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PriceAtPurchase).HasPrecision(18, 2);

                entity.HasOne(d => d.Order)
                      .WithMany()
                      .HasForeignKey(d => d.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product)
                      .WithMany()
                      .HasForeignKey(d => d.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
