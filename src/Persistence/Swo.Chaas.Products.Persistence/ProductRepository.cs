using Microsoft.EntityFrameworkCore;
using Swo.Chaas.Products.Domain.Entities.Features.Product;
using Swo.Chaas.Products.Domain.Entities.Features.Product.Repositories;
using Swo.Chaas.Products.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swo.Chaas.Products.Persistence
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
            /*var products = await ProductsDbContext.Products
                .Include(x => x.Provider)
                .Include(x => x.ProductType)
                .Include(x => x.Skus).ThenInclude( s => s.SkuBillingCycle)
                .Skip(pageindex)
                .Take(pagesize)
                .AsNoTracking()
                .ToListAsync();

            return products;*/
            return null;
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
