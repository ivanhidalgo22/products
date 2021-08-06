using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Swo.Chaas.Products.Domain.Entities.Features.Product;

#nullable disable

namespace Swo.Chaas.Products.Persistence.DbContext
{
    public class ProductsDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Sku> Sku { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<Term> Terms { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<BillingCycleType> BillingCycleTypes { get; set; }

        public DbSet<SkuBillingCycle> SkuBillingCycles { get; set; }
        public DbSet<SkuPricing> SkuPricing { set; get; }

        public ProductsDbContext() { }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options) { }


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(e => e.ProductType)
                .WithMany(c => c.Products);

            modelBuilder.Entity<SkuPricing>(entity =>
            {
                entity.HasKey(vf => new { vf.SkuId, vf.Country });
            });

            modelBuilder.Entity<Sku>(entity =>
            {
                entity.HasKey(vf => new { vf.Id, vf.ProductId });
            });

            modelBuilder.Entity<BillingCycleType>()
                .HasKey(x => x.Value);

            modelBuilder.Entity<Provider>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Term>()
                .HasKey(x => x.Description);

            modelBuilder.Entity<SkuBillingCycle>(entity =>
            {
                entity.HasKey(vf => new { vf.SkuId, vf.BillingCycleId, vf.ProductId });


            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType
                {
                    Id = "Azure",
                    DisplayName = "Azure",
                    SubType = null
                },
                new ProductType
                {
                    Id = "OnlineServices",
                    DisplayName = "OnlineServices",
                    SubType = null
                }
               );
        }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Products;Integrated Security=True;")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
            }
        }


    }
}
