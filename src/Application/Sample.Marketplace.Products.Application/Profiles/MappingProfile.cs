using AutoMapper;
using Sample.Marketplace.Products.Application.ProductDtos;
using Sample.Marketplace.Products.Application.ProductDtos.Response;
using Sample.Marketplace.Products.Domain.Entities.Features.Product;

namespace Sample.Marketplace.Products.Application.Profiles
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
