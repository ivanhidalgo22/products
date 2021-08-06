﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swo.Chaas.Products.Domain.Entities.Features.Product.Repositories
{
    public interface IProductRepository
    {

        public Task<IList<Product>> GetProducts(int pageindex, int pagesize);
        public Task<Product> GetProductById(Product product);

    }
}
