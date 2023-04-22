using AutoMapper;
using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using Sample.Marketplace.Products.Domain.Entities.Repositories;
using Sample.Marketplace.Products.Application.ProductDtos;
using Sample.Marketplace.Products.Application.ProductDtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Marketplace.Products.Application
{
    public class SkuService : ISkuService
    {

        public ISkuRepository SkuRepository { set; get; }
        private readonly IMapper _mapper;

        public SkuService(IMapper mapper, ISkuRepository skuRepository)
        {
            SkuRepository = skuRepository;
            _mapper = mapper;
        }
        public async Task<IList<SkuDto>> GetSkus(int pageindex, int pagesize)
        {
            var skus = await SkuRepository.GetSkus(pageindex, pagesize);
            var result = _mapper.Map<List<SkuDto>>(skus);
            return result;
        }

        public async Task<SkuDto> GetSkuById(string skuId)
        {
            Sku sku = new Sku() { Id = skuId };
            var skuReturned = await SkuRepository.GetSkuById(sku);
            var result = _mapper.Map<SkuDto>(skuReturned);
            return result;
        }

        public async Task<IList<SkuDto>> GetSkuAddons(string skuId)
        {
            Sku sku = new Sku() { Id = skuId };
            var skuWithAddons = await SkuRepository.GetSkuAddons(sku);
            var result = _mapper.Map<List<SkuDto>>(skuWithAddons);
            return result;
        }

        public async Task<IList<SkuDto>> GetSkuWithFilters(SkuDto skuDto)
        {
            Sku sku = new Sku() { HasAddOns = skuDto.HasAddOns, IsAddon = skuDto.IsAddon, IsTrial = skuDto.IsTrial };
            var skuWithAddons = await SkuRepository.GetSkuWithFilters(sku);
            var result = _mapper.Map<List<SkuDto>>(skuWithAddons);
            return result;
        }

        public SkuRatingDto GetSkuReviews(string skuId)
        {
            Sku sku = new Sku() { Id = skuId };
            var skuReturned = SkuRepository.GetSkuReviews(sku);
            var result = _mapper.Map<SkuRatingDto>(skuReturned);
            return result;
        }
    }
}
