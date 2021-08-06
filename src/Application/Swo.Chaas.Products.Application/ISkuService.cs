using Swo.Chaas.Products.Application.ProductDtos;
using Swo.Chaas.Products.Application.ProductDtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swo.Chaas.Products.Application
{
    public interface ISkuService
    {

        public Task<IList<SkuDto>> GetSkus(int pageindex, int pagesize);
        public Task<SkuDto> GetSkuById(string skuId);
        public Task<IList<SkuDto>> GetSkuAddons(string skuId);
        public Task<IList<SkuDto>> GetSkuWithFilters(SkuDto sku);
        public SkuRatingDto GetSkuReviews(string skuId);
    }
}
