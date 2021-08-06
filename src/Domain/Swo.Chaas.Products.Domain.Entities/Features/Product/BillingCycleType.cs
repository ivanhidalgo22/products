using System.Collections.Generic;

namespace Swo.Chaas.Products.Domain.Entities.Features.Product
{
    public class BillingCycleType
    {

        public string Value { get; set; }
        public string Description { get; set; }
        public List<SkuBillingCycle> SkuBillingCycle { get; set; }        
    }
}
