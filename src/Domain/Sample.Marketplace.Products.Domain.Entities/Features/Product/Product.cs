using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public class Product
    {
        public Product()
        {
            Skus = new List<Sku>();
        }

        [Required]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductTypeId { get; set; }
        public int? ProviderId { get; set; }
        public string PublisherName { get; set; }

        public virtual List<Sku> Skus { get; set; }

        public ProductType ProductType { get; set; }

        public Provider Provider { get; set; }
    }
}
