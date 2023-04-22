using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using System.Collections.Generic;

namespace Sample.Marketplace.Products.Application.ProductDtos
{
    public class ProductWrapper
    {

        public ProductWrapper()
        {
            Items = new List<Product>();
        }
        public int TotalCount { set; get; }
        public List<Product> Items { set; get; }


    }
}
