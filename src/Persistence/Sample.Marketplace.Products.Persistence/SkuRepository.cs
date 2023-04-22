using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using Sample.Marketplace.Products.Domain.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Sample.Marketplace.Products.Infrastructure.DataBase;
using System.Data.SqlClient;
using Sample.Marketplace.Products.Persistence.DbContext;

namespace Sample.Marketplace.Products.Persistence
{
    public class SkuRepository : ISkuRepository
    {
        public ProductsDbContext ProductsDbContext { set; get; }

        public SkuRepository(ProductsDbContext dbContext)
        {
            ProductsDbContext = dbContext;
        }
        public async Task<IList<Sku>> GetSkus(int pageindex, int pagesize)
        {
            var skus = await ProductsDbContext.Sku.Where( x => !string.IsNullOrEmpty(x.Country))
                .Include(x => x.SkuBillingCycle)
                .Skip(pageindex)
                .Take(pagesize)
                .AsNoTracking()
                .ToListAsync();

            return skus;
        }

        public async Task<IList<Sku>> GetSkuAddons(Sku sku)
        {
            var skus = await ProductsDbContext.Sku.Where(x => !string.IsNullOrEmpty(x.Country) && x.ParentId.Equals(sku.Id))
                .Include(x => x.SkuBillingCycle)
                .AsNoTracking()
                .ToListAsync();

            return skus;
        }

        public async Task<IList<Sku>> GetSkuWithFilters(Sku sku)
        {
            
            var skus = await ProductsDbContext.Sku.Where(x => !string.IsNullOrEmpty(x.Country) && x.HasAddOns == sku.HasAddOns && x.IsAddon == sku.IsAddon && x.IsTrial == sku.IsTrial)
                .Include(x => x.SkuBillingCycle)
                .AsNoTracking()
                .ToListAsync();

            return skus;
        }

        public async Task<Sku> GetSkuById(Sku sku)
        {
            var skuToReturn = await ProductsDbContext.Sku.Where(x => !string.IsNullOrEmpty(x.Country) && x.Id.Equals(sku.Id))
                .Include(x => x.SkuBillingCycle)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return skuToReturn;
        }


        public Sku GetSkuReviews(Sku sku)
        {
            sku.Ratings = new List<SkuRating>();
            var parameters = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@SkuId", sku.Id)
            };

            string procedure = $"[dbo].[uspGetProductReviews] @SkuId";

            using (var multiResultSet = ProductsDbContext.MultiResultSetSqlQuery($"EXEC {procedure}", parameters))
            {
                sku.Ratings = multiResultSet.ResultSetFor<SkuRating>().ToList();
                sku.ReviewStatistics = multiResultSet.ResultSetFor<ReviewStatistics>().ToList().FirstOrDefault();

            }

            return sku;
        }
    }
}
