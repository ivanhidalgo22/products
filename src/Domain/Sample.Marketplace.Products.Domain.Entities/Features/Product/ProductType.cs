using System.Collections.Generic;

namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public class ProductType
    {

        public ProductType()
        {
            Products = new HashSet<Product>();
        }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string SubType { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public enum ProductTypeEnum : int
    {
        Azure = 0,
        OnlineServices = 1
    }
}
