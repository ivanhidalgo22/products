using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Marketplace.Products.Application.ProductDtos
{
    public class ProductRequestDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductTypeId { get; set; }
        public int? ProviderId { get; set; }
        public string PublisherName { get; set; }
        public List<SkuDto> Skus { get; set; }
    }
}
