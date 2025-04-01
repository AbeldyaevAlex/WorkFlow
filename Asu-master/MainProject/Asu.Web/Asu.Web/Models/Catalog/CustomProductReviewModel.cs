namespace Asu.Web.Models.Catalog
{
    public partial class CustomProductReviewOverviewModel : ProductReviewOverviewModel
    {
        public int RatingCount { get; set; }

        public double RatingScore { get; set; }
    }
}