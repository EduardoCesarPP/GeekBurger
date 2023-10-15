global using GeekBurger.Shared;
global using GeekBurger.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekBurger.Shared
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<FoodCharacteristics> FoodCharacteristics { get; set; }
        public DbSet<FoodRestrictions> FoodRestrictions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderChangedEvent> OrderChangedEvents { get; set; }

    }
}