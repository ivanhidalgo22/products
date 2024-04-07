using AutoMapper;
using Sample.Marketplace.Products.Application.ProductDtos;
using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using Sample.Marketplace.Products.Domain.Entities.Features.Product.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Marketplace.Products.Application
{
    public class ProductService : IProductService
    {
        public IProductRepository ProductRepository { set; get; }
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            ProductRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IList<ProductReponseDto>> GetProducts(int pageindex, int pagesize)
        {
            var products = await ProductRepository.GetProducts(pageindex, pagesize);
            var result = _mapper.Map<List<ProductReponseDto>>(products);
            return result;
        }

        public async Task<ProductReponseDto> GetProductById(string productId)
        {
            Product product = new Product() { Id = productId };
            var productReturned = await ProductRepository.GetProductById(product);
            var result = _mapper.Map<ProductReponseDto>(productReturned);
            return result;
        }

        public async Task CreateProduct(ProductRequestDto productRequestDto)
        {   
            var product = _mapper.Map<Product>(productRequestDto);
            await Task.Run(() => ProductRepository.CreateProduct(product));

        }
    }
}
