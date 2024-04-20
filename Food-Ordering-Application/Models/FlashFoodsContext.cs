using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Food_Ordering_Application.Models
{
    public class FlashFoodsContext:IdentityDbContext<User>

    { 
        public FlashFoodsContext(DbContextOptions<FlashFoodsContext> options) : base(options) 
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }


        //Seeding Roles
        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                    new IdentityRole() { Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "Customer" },
                    new IdentityRole() { Name = "Owner", ConcurrencyStamp = "3", NormalizedName = "Owner" }
                );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedRoles(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RestaurantId);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(mi => mi.CategoryId);

            modelBuilder.Entity<Restaurant>()
            .HasOne(r => r.Owner)
            .WithMany() // Assuming each owner can own multiple restaurants
            .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithMany()
            .HasForeignKey(o => o.PaymentId)
            .IsRequired(false);
        }

    }
}
