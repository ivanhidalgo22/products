using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Marketplace.Products.Application.ProductDtos.Response
{
    public class SkuPricingDto
    {
        public string SkuId { get; set; }
        public string Country { get; set; }
        public Decimal Price { get; set; }

        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public Decimal ErpPrice { get; set; }
    }
}
