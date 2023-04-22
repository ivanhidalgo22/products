using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public class Sku
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

        //public List<SkuPricing> SkuPricing { set; get; }


        public Product Product { get; set; }

        [NotMapped]
        public List<string> SupportedBillingCycles { get; set; }

        public List<SkuBillingCycle> SkuBillingCycle { get; set; }

        [NotMapped]
        public DynamicAttributes DynamicAttributes { set; get; }

        [NotMapped]
        public List<SkuRating> Ratings { set; get; }
        
        [NotMapped]
        public ReviewStatistics ReviewStatistics { set; get; }
    }

    public class DynamicAttributes
    {
        public string BillingType { get; set; }
        public string Category { get; set; }

        public bool IsAddon { get; set; }
        public bool HasAddOns { get; set; }
        public bool IsAutoRenewable { get; set; }

        public int Limit { get; set; }

        public int Rank { get; set; }
    }
}
