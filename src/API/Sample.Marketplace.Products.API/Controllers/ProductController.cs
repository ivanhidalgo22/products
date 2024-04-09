using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Sample.Marketplace.Products.API.ViewModel;
using Sample.Marketplace.Products.Application;
using Sample.Marketplace.Products.Application.ProductDtos;
using Sample.Marketplace.Products.Domain.Entities.Features.Product;
using System.Threading.Tasks;

namespace Sample.Marketplace.Products.API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductService ProductService { set; get; }
        private readonly ILogger<ProductController> _logger;



        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        /// <summary>
        /// Gets all Products from vendor catalog.
        /// </summary>
        /// <param name="pageindex">page index (it's by default 0)</param>
        /// <param name="pagesize">page size (it's by default 10)</param>
        /// <returns>paginated product list.</returns>
        [HttpGet]
        [Authorize("read:products")]
        public async Task<PaginatedElementsViewModel<ProductReponseDto>> GetProducts([FromQuery] int pageindex = 0, [FromQuery] int pagesize = 10)
        {
            var products = await ProductService.GetProducts(pageindex, pagesize);
            var modelReturn = new PaginatedElementsViewModel<ProductReponseDto>(
                                             pageindex, pagesize, products.Count, products);
            return modelReturn;
        }

        /// <summary>
        /// Gets product detail by its identifer.
        /// </summary>
        /// <param name="productId">Product identifier.</param>
        /// <returns>Product detail.</returns>
        [HttpGet("{productId}")]
        [Authorize("read:products")]
        public async Task<ProductReponseDto> GetProductDetailById(string productId)
        {
            var productToReturn = await ProductService.GetProductById(productId);
            
            return productToReturn;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [Authorize("create:products")]
        public async Task<ActionResult> CreateProduct([FromBody] ProductRequestDto productRequestDto)
        {
            await ProductService.CreateProduct(productRequestDto);
            return Ok();
        }

    }
}
