using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : IdentityDbContext<Account>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Courier> Couriers => Set<Courier>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // ← Add this as the first line

            modelBuilder.Entity<Order>()
                .OwnsMany(o => o.OrderItems, oi =>
                {
                    oi.WithOwner().HasForeignKey("OrderId");
                    oi.Property<Guid>("Id");
                    oi.HasKey("Id");
                });
}
    }

}