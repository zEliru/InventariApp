using InventariApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventariApp.Data
{
    public class InventoryContext: DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;

        public string DbPath { get; }

        public InventoryContext()
        {
            DbPath = "inventory.db"; 
        }

        public InventoryContext(DbContextOptions<InventoryContext> options)
           : base(options)
        {
            DbPath = "inventory.db";

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data source={DbPath}");
        }

        public void Populate()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adding products from the get-go to visualize/test a bit faster
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Logitech G403 Hero",
                    Description = "Un ratolí prou bo, la veritat",
                    Quantity = 10,
                    Price = 52
                },
                new Product
                {
                    Id = 2,
                    Name = "iPhone 16 Pro Max",
                    Description = "Un mòbil tan car com bo",
                    Quantity = 15,
                    Price = 1600
                }
            );
            

            
        }
    }
}
