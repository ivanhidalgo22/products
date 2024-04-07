using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Sample.Marketplace.Products.Domain.Entities.Features.Product.Repositories;
using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using Sample.Marketplace.Products.Persistence.DbContext;
using Microsoft.Extensions.Logging;

namespace Sample.Marketplace.Products.Persistence
{
    public class ProductRepository : IProductRepository
    {
        public ProductsDbContext ProductsDbContext { set; get; }
        private readonly ILogger<ProductRepository> _logger;


        public ProductRepository(ILogger<ProductRepository> logger,ProductsDbContext dbContext)
        {
            _logger = logger;
            ProductsDbContext = dbContext;
        }

        /// <summary>
        /// Gets product list.
        /// </summary>
        /// <param name="pageindex">list index</param>
        /// <param name="pagesize">number of products to be returned</param>
        /// <returns></returns>
        public async Task<IList<Product>> GetProducts(int pageindex, int pagesize)
        {
            /*var products = new List<Product>();
            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.ProductType = new ProductType();
            product.ProductType.Id = Guid.NewGuid().ToString();
            product.ProductType.SubType = secretValue;
            product.Provider = new Provider() {Id = 1, Name = "IBM"};
            products.Add(product);*/
            var products = await ProductsDbContext.Products
                .Include(x => x.Provider)
                .Include(x => x.ProductType)
                .Include(x => x.Skus).ThenInclude( s => s.SkuBillingCycle)
                .Skip(pageindex)
                .Take(pagesize)
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        /// <summary>
        /// Gets a product by its identifier.
        /// </summary>
        /// <param name="product">product to find into the database.</param>
        /// <returns>product from the database.</returns>
        public async Task<Product> GetProductById(Product product)
        {
            var products = await ProductsDbContext.Products
                .Where(y => y.Id.Equals(product.Id))
                .Include(x => x.Provider)
                .Include(x => x.ProductType)
                .Include(x => x.Skus).ThenInclude(s => s.SkuBillingCycle)
                .AsNoTracking()
              .FirstOrDefaultAsync();

            return products;
        }

        public async Task CreateProduct(Product product)
        {
            try
            {
                ProductsDbContext.Products.Add(product);
                await ProductsDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("error saving new product.." + ex);
            }
            
        }
    }
}
