using System;

namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public class SkuRating
    {

        public int RatingId { set; get; }
        public string SkuId { set; get; }
        public int? Rating { set; get; }
        public string Review { set; get; }
        public string ReviewerAccount { set; get; }
        public DateTime ReviewedDate { set; get; }
        public string Status { set; get; }

    }
}
