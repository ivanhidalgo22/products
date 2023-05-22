using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Marketplace.Products.API.ActionsFilters;
using Sample.Marketplace.Products.API.ViewModel;
using Sample.Marketplace.Products.Application;
using Sample.Marketplace.Products.Application.ProductDtos;
using Sample.Marketplace.Products.Application.ProductDtos.Response;
using System.Threading.Tasks;

namespace Sample.Marketplace.Products.API.Controllers
{
    [Route("api/skus")]
    [ApiController]
    public class SkuController : ControllerBase
    {

        public ISkuService SkuService { set; get; }
        private readonly ILogger<SkuController> _logger;

        public SkuController(ILogger<SkuController> logger, ISkuService skuService)
        {
            _logger = logger;
            SkuService = skuService;
        }

        [HttpGet]
        [QueryStringConstraintAttribute("pageindex", false)]
        [QueryStringConstraintAttribute("pagesize", false)]
        public async Task<PaginatedElementsViewModel<SkuDto>> GetSkuWithFilters([FromQuery] bool hasAddons = false, [FromQuery] bool isAddon = false, [FromQuery] bool isTrial = false)
        {
            SkuDto skuDto = new SkuDto() { HasAddOns = hasAddons, IsAddon = isAddon, IsTrial = isTrial };
            var skus = await SkuService.GetSkuWithFilters(skuDto);
            var modelReturn = new PaginatedElementsViewModel<SkuDto>(
                                             0, 0, skus.Count, skus);
            return modelReturn;
        }

        [HttpGet("{skuId}/addons")]
        public async Task<PaginatedElementsViewModel<SkuDto>> GetSkusWithAddons(string skuId)
        {
            var skus = await SkuService.GetSkuAddons(skuId);
            var modelReturn = new PaginatedElementsViewModel<SkuDto>(
                                             0, 0, skus.Count, skus);
            return modelReturn;
        }

        [HttpGet("{skuId}")]
        public async Task<SkuDto> GetSkuDetailById(string skuId)
        {
            var skuToReturn = await SkuService.GetSkuById(skuId);
            return skuToReturn;
        }

        [HttpGet("{skuId}/ratings")]
        public SkuRatingDto GetSkuReviews(string skuId)
        {
            var skuToReturn = SkuService.GetSkuReviews(skuId);
            return skuToReturn;
        }

    }
}
