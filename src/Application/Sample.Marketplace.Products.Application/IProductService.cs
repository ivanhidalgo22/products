using Sample.Marketplace.Products.Application.ProductDtos;
using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Marketplace.Products.Application
{
    public interface IProductService
    {
        public Task<IList<ProductReponseDto>> GetProducts(int pageindex, int pagesize);
        public Task<ProductReponseDto> GetProductById(string productId);
        public Task CreateProduct(ProductRequestDto productRequestDto);
    }
}
