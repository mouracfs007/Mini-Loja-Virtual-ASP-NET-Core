using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Category
            mb.Entity<Category>().HasKey(c => c.CategoryId);

            mb.Entity<Category>().
                Property(c => c.Name).
                    HasMaxLength(100).
                        IsRequired();
            // Product
            mb.Entity<Product>().
                Property(p => p.Name).
                    HasMaxLength(100).
                        IsRequired();
            mb.Entity<Product>().
                Property(p => p.Description).
                    HasMaxLength(255).
                        IsRequired();
            mb.Entity<Product>().
                Property(p => p.ImageURL).
                    HasMaxLength(255).
                        IsRequired();

            mb.Entity<Product>().
                Property(p => p.Price).
                    HasPrecision(12, 2);

            mb.Entity<Category>()
                .HasMany(p => p.Products)
                    .WithOne(c => c.Category)
                    .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Category>().HasData(
                    new Category
                    {
                        CategoryId = 1,
                        Name = "Material Escolar",
                    },
                    new Category
                    {
                        CategoryId = 2,
                        Name = "Acessórios",
                    }
                );
        }
    }
}
