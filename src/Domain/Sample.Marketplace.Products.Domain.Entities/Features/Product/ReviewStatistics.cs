namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public class ReviewStatistics
    {
        public int TotalReviews { set; get; }
        public int NewReviews { set; get; }
        public int AverageRating { set; get; }
        public int TotalFiveStars { set; get; }
        public string TotalFiveStarsPercentage { set; get; }
        public int TotalFourStars { set; get; }
        public string TotalFourStarsPercentage { set; get; }
        public int TotalThreeStars { set; get; }
        public string TotalThreeStarsPercentage { set; get; }
        public int TotalTwoStars { set; get; }
        public string TotalTwoStarsPercentage { set; get; }
        public int TotalOneStars { set; get; }
        public string TotalOneStarsPercentage { set; get; }


    }
}
