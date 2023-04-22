using System;

namespace Sample.Marketplace.Domain.Entities.Features.Product
{
    public class SkuPricing
    {
        public string SkuId { get; set; }
        public string Country { get; set; }
        public Decimal Price { get; set; }

        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public Decimal ErpPrice { get; set; }
    }
}
