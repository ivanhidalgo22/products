using Swo.Chaas.Products.Application.ProductDtos;
using Swo.Chaas.Products.Domain.Entities.Features.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swo.Chaas.Products.Application
{
    public interface IProductService
    {
        public Task<IList<ProductReponseDto>> GetProducts(int pageindex, int pagesize);
        public Task<ProductReponseDto> GetProductById(string productId);
    }
}
