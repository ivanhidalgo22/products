using AutoMapper;
using Sample.Marketplace.Products.Application.ProductDtos;
using Sample.Marketplace.Products.Application.ProductDtos.Response;
using Sample.Marketplace.Products.Domain.Entities.Features.Product;

namespace Sample.Marketplace.Products.Application.Profiles
{
    public class MappingProductProfile : Profile
    {

        public MappingProductProfile()
        {
            CreateMap<ProductRequestDto, Product>().ReverseMap();
            CreateMap<SkuDto, Sku>().ReverseMap();
            CreateMap<SkuBillingCycleDto, SkuBillingCycle>().ReverseMap();
            CreateMap<RatingDto, SkuRating>().ReverseMap();
            CreateMap<ReviewStatisticsDto, ReviewStatistics>().ReverseMap();
            CreateMap<SkuRatingDto, Sku>().ReverseMap();

        }
    }
}
