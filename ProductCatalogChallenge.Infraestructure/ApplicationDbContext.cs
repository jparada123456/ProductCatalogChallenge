using Microsoft.EntityFrameworkCore;
using ProductCatalogChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<FinalPrice> FinalPrices { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<ProductStatus>().HasKey(ps => ps.StatusId);
            modelBuilder.Entity<Discount>().HasKey(d => d.DiscountId);
            modelBuilder.Entity<FinalPrice>().HasKey(fp => fp.FinalPriceId);
            modelBuilder.Entity<Inventory>().HasKey(i => i.InventoryId);

            modelBuilder.Entity<Product>()
                .HasOne<ProductStatus>()
                .WithMany()
                .HasForeignKey(p => p.StatusId);

            modelBuilder.Entity<FinalPrice>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(fp => fp.ProductId);

            modelBuilder.Entity<FinalPrice>()
                .HasOne<Discount>()
                .WithMany()
                .HasForeignKey(fp => fp.DiscountId);

            modelBuilder.Entity<Inventory>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(i => i.ProductId);
        }
     }
}
