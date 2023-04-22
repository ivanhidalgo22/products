using Sample.Marketplace.Products.Application.ProductDtos.Response;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sample.Marketplace.Products.Application.ProductDtos
{
    public class SkuDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
        public int? MinimumQuantity { get; set; }
        public int? MaximumQuantity { get; set; }
        public bool? IsTrial { get; set; }

        public string BillingType { get; set; }
        public string Category { get; set; }

        public bool IsAddon { get; set; }
        public bool HasAddOns { get; set; }
        public bool IsAutoRenewable { get; set; }

        public int Limit { get; set; }

        public int Rank { get; set; }
        public string Country { get; set; }

        public string Locate { get; set; }

        public string Billing { get; set; }

        public string LogoUrl { get; set; }

        public string Term { get; set; }

        public string ParentId { set; get; }
        public Decimal? Price { get; set; }

        public List<SkuBillingCycleDto> SkuBillingCycle { get; set; }
        List<SkuPricingDto> Pricing { set; get; }
        public List<RatingDto> Ratings { set; get; }
        public ReviewStatisticsDto ReviewStatistics { set; get; }   
    }
}
