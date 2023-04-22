using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Marketplace.Products.Domain.Entities.Repositories
{
    public interface ISkuRepository
    {
        public Task<IList<Sku>> GetSkus(int pageindex, int pagesize);
        public Task<Sku> GetSkuById(Sku sku);
        public Task<IList<Sku>> GetSkuAddons(Sku sku);
        public Task<IList<Sku>> GetSkuWithFilters(Sku sku);
        public Sku GetSkuReviews(Sku sku);


    }
}
