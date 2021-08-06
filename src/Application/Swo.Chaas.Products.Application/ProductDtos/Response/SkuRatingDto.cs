﻿using System.Collections.Generic;

namespace Swo.Chaas.Products.Application.ProductDtos.Response
{
    public class SkuRatingDto
    {
        public string Id { get; set; }
        public List<RatingDto> Ratings { set; get; }
        public ReviewStatisticsDto ReviewStatistics { set; get; }
    }
}
