﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Swo.Chaas.Products.Application.ProductDtos
{
    public class ProductReponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductTypeId { get; set; }
        public int? ProviderId { get; set; }
        public string PublisherName { get; set; }
        public List<SkuDto> Skus { get; set; }
        public ProductTypeDto ProductType { get; set; }
        public ProviderDto Provider { get; set; }
    }
}
