namespace Asu.Web.Models.Catalog
{
    using Asu.Framework.Mvc;
    using Asu.Web.Models.Media;

    public class ProductGroupOverviewModel : BaseNopEntityModel
    {
        public ProductGroupOverviewModel()
        {
            this.PriceModel = new ProductGroupPriceModel();
            this.RatingModel = new ProductGroupRatingModel();
            this.DigitalDataModel = new DigitalDataModel();
        }

        public string Name { get; set; }

        public string SeName { get; set; }

        public string BrandSlug { get; set; }

        public bool IsFreeShipping { get; set; }

        public DigitalDataModel DigitalDataModel { get; set; }

        public ProductGroupPriceModel PriceModel { get; set; }

        public ProductGroupRatingModel RatingModel { get; set; }
    }
}