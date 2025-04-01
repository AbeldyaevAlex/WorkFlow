namespace Asu.Web.Models.Catalog
{
    using System.Collections.Generic;

    using Asu.Framework.Mvc;

    using Asu.Web.Models.Media;

    public class BrandModel : BaseNopEntityModel
    {
        public BrandModel()
        {
            this.Pictures = new List<DigitalDataModel>();
            this.ProductGroups = new List<ProductGroupOverviewModel>();
            this.Categories = new List<BrandCategoryOverviewModel>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string SeName { get; set; }

        public List<DigitalDataModel> Pictures { get; set; }

        public IList<ProductGroupOverviewModel> ProductGroups { get; set; }

        public IList<BrandCategoryOverviewModel> Categories { get; set; }
    }
}