using System;

namespace Sample.Marketplace.Products.Application.ProductDtos.Response
{
    public class RatingDto
    {
        public int? Rating { set; get; }
        public string Review { set; get; }
        public string ReviewerAccount { set; get; }
        public DateTime ReviewedDate { set; get; }
        public string Status { set; get; }
    }
}
