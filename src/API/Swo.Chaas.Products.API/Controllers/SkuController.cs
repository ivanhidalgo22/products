using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swo.Chaas.Products.API.ActionsFilters;
using Swo.Chaas.Products.API.ViewModel;
using Swo.Chaas.Products.Application;
using Swo.Chaas.Products.Application.ProductDtos;
using Swo.Chaas.Products.Application.ProductDtos.Response;
using System.Threading.Tasks;

namespace Swo.Chaas.Products.API.Controllers
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
        [QueryStringConstraintAttribute("pageindex", true)]
        [QueryStringConstraintAttribute("pagesize", true)]
        public async Task<PaginatedElementsViewModel<SkuDto>> GetSkus([FromQuery] int pageindex = 0, [FromQuery] int pagesize = 10)
        {
            var skus = await SkuService.GetSkus(pageindex, pagesize);
            var modelReturn = new PaginatedElementsViewModel<SkuDto>(
                                             pageindex, pagesize, skus.Count, skus);
            return modelReturn;
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
