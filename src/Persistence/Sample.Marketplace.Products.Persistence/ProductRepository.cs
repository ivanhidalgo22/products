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

namespace Sample.Marketplace.Products.Persistence
{
    public class ProductRepository : IProductRepository
    {
        public ProductsDbContext ProductsDbContext { set; get; }

        public ProductRepository(ProductsDbContext dbContext)
        {
            ProductsDbContext = dbContext;
        }

        public async Task<IList<Product>> GetProducts(int pageindex, int pagesize)
        {
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };

            string userAssignedClientId = "0e3d1b06-e8ee-4902-94bc-86c08464d9bd";
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });



            var client = new SecretClient(new Uri("https://raiderpockeyvault2.vault.azure.net/"), credential, options);

            KeyVaultSecret secret = client.GetSecret("chaasadmin");

            string secretValue = secret.Value;


            var products = new List<Product>();
            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.ProductType = new ProductType();
            product.ProductType.Id = Guid.NewGuid().ToString();
            product.ProductType.SubType = secretValue;
            product.Provider = new Provider() {Id = 1, Name = "IBM"};
            products.Add(product);
            /*var products = await ProductsDbContext.Products
                .Include(x => x.Provider)
                .Include(x => x.ProductType)
                .Include(x => x.Skus).ThenInclude( s => s.SkuBillingCycle)ls -
                .Skip(pageindex)
                .Take(pagesize)
                .AsNoTracking()
                .ToListAsync();

            return products;*/
            return products;
        }

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
    }
}
