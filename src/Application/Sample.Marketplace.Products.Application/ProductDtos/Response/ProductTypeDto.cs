using System.Collections.Generic;

namespace Sample.Marketplace.Products.Application.ProductDtos
{
    public class ProductTypeDto
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string SubType { get; set; }
    }

    public enum ProductTypeEnum : int
    {
        Azure = 0,
        OnlineServices = 1
    }
}
