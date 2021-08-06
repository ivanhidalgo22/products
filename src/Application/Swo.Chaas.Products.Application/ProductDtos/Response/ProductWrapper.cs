using Swo.Chaas.Products.Domain.Entities.Features.Product;
using System.Collections.Generic;

namespace Swo.Chaas.Products.Application.ProductDtos
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
