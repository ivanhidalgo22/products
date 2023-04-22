using Sample.Marketplace.Products.Domain.Entities.Features.Product;

namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public class SkuBillingCycle
    {
        public string SkuId { get; set; }
        public string BillingCycleId { get; set; }
        public string ProductId { get; set; }

        public BillingCycleType BillingCycle { get; set; }
        public Sku Sku { get; set; }

        public Product Product { get; set; }
    }
}
