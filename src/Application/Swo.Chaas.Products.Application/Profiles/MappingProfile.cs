using AutoMapper;
using Swo.Chaas.Products.Application.ProductDtos;
using Swo.Chaas.Products.Application.ProductDtos.Response;
using Swo.Chaas.Products.Domain.Entities.Features.Product;

namespace Swo.Chaas.Products.Application.Profiles
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductReponseDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();
            CreateMap<Provider, ProviderDto>().ReverseMap();
            CreateMap<Sku, SkuDto>().ReverseMap();
            CreateMap<SkuBillingCycle, SkuBillingCycleDto>().ReverseMap();
            CreateMap<SkuRating, RatingDto>().ReverseMap();
            CreateMap<ReviewStatistics, ReviewStatisticsDto>().ReverseMap();
            CreateMap<Sku, SkuRatingDto>().ReverseMap();

        }
    }
}
