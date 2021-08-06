using Swo.Chaas.Products.Domain.Entities.Features.Product;
using System.Collections.Generic;

namespace Swo.Chaas.Products.Application.ProductDtos
{
    public class SkuWrapper
    {

        public SkuWrapper()
        {
            Items = new List<Sku>();
        }
        public int TotalCount { set; get; }
        public List<Sku> Items { set; get; }
    }
}
