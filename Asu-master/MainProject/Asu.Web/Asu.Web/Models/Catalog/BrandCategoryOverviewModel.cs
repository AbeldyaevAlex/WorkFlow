namespace Asu.Web.Models.Catalog
{
    using Asu.Framework.Mvc;
    using Asu.Web.Models.Media;

    public class BrandCategoryOverviewModel : BaseNopEntityModel
    {
        public BrandCategoryOverviewModel()
        {
            this.DigitalDataModel = new DigitalDataModel();
        }

        public string Name { get; set; }

        public string BrandName { get; set; }

        public string SeName { get; set; }

        public string BrandSlug { get; set; }

        public DigitalDataModel DigitalDataModel { get; set; }
    }
}