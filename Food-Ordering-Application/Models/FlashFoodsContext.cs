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
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserFavoriteRestaurants> UserFavoriteRestaurants { get; set; }
        public DbSet<UserFavoriteMenuItems> UserFavoriteMenuItems { get; set; }
        public DbSet<CartItems> CartItems { get; set; }


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
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.NoAction)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(mi => mi.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Restaurant>()
            .HasOne(r => r.Owner)
            .WithMany() // Assuming each owner can own multiple restaurants
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithMany()
            .HasForeignKey(o => o.PaymentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.MenuItem)
                .WithMany()
                .HasForeignKey(od => od.menuItemId)
                .OnDelete(DeleteBehavior.NoAction); //when you have multiple foreign key relationships with cascading deletes, which can potentially create cycles or conflicting paths.to solve we used this

            //configuring primary key for userFavoriteRestaurant table and giving relationships

            modelBuilder.Entity<UserFavoriteRestaurants>()
                .HasKey(ufr => new { ufr.Id, ufr.RestaurantId });  //Define composite primary key

            // Configure many-to-many relationship for favoriteRestaurants
            modelBuilder.Entity<UserFavoriteRestaurants>()
                .HasOne(ufr => ufr.User)
                .WithMany(u => u.favoriteRestaurants)
                .HasForeignKey(ufr => ufr.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserFavoriteRestaurants>()
                .HasOne(ufr => ufr.Restaurant)
                .WithMany(r => r.UserFavoriteRestaurants)
                .HasForeignKey(ufr => ufr.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            //configuring priamry key for userFavoriteMeniItems table and giving relatinships

            modelBuilder.Entity<UserFavoriteMenuItems>()
                .HasKey(u => new { u.Id, u.MenuItemId }); // Define composite primary key

            // Configure many-to-many relationship
            modelBuilder.Entity<UserFavoriteMenuItems>()
                .HasOne<User>(ufmi => ufmi.User)
                .WithMany(u => u.favoriteMenuItems)
                .HasForeignKey(ufmi => ufmi.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserFavoriteMenuItems>()
                .HasOne<MenuItem>(ufmi => ufmi.MenuItem)
                .WithMany(m => m.FavoriteMenuItems)
                .HasForeignKey(ufmi => ufmi.MenuItemId)
                .OnDelete(DeleteBehavior.NoAction);

            //Cart relationship
            modelBuilder.Entity<CartItems>()
            .HasOne(ci => ci.MenuItem)
            .WithMany()
            .HasForeignKey(ci => ci.MenuItemId)
            .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
