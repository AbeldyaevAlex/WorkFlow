namespace Asu.Web.Models.Catalog
{
    using Asu.Framework.Mvc;

    public class ProductGroupRatingModel : BaseNopModel
    {
        public decimal RatingScore { get; set; }

        public int RatingCount { get; set; }
    }
}