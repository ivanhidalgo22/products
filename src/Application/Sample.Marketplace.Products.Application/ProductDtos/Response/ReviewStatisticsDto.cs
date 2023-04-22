namespace Sample.Marketplace.Products.Application.ProductDtos.Response
{
    public class ReviewStatisticsDto
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
