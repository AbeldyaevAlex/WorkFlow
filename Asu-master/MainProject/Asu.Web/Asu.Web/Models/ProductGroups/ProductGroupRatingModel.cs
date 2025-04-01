namespace Asu.Web.Models.ProductGroups
{
    using Asu.Framework.Mvc;

    public class ProductGroupRatingModel : BaseNopModel
    {
        public decimal RatingScore { get; set; }

        public int RatingCount { get; set; }
    }
}