using E_ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.data.Concrete.EfCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardItem> CardItems { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-CH0UVQ2; Database=shopDb; User Id=sa; Password=bedir123456;");

        }
        //Fluent api yapacağız.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                   .HasKey(c => new { c.CategoryId, c.ProductId });
            //Bunu ekledikten sonra migrations oluştur .
            //dotnet ef migrations add InıtialCreate --start-project ../E-ticaret.Webui
            //
        }

    }
}
