namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public class Availability
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string SkuId { get; set; }
        public string CatalogItemId { get; set; }
        public string DefaultCurrency { get; set; }
        public string Segment { get; set; }
        public string Country { get; set; }
        public bool IsPurchasable { get; set; }
        public bool IsRenewable { get; set; }
        public string Term { get; set; }

        public virtual Term TermNavigation { get; set; }
    }
}
