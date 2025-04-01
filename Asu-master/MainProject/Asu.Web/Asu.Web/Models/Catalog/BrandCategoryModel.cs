namespace Asu.Web.Models.Catalog
{
    using System.Collections.Generic;

    using Asu.Framework.Mvc;

    public class BrandCategoryModel : BaseNopEntityModel
    {
        public BrandCategoryModel()
        {
            this.ProductGroups = new List<ProductGroupOverviewModel>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string BrandName { get; set; }

        public string SeName { get; set; }

        public string BrandSlug { get; set; }

        public IList<ProductGroupOverviewModel> ProductGroups { get; set; }
    }
}