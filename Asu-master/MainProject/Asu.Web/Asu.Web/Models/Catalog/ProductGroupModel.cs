namespace Asu.Web.Models.Catalog
{
    using System.Collections.Generic;

    using Asu.Framework.Mvc;

    using Media;

    public class ProductGroupModel : BaseNopEntityModel
    {
        public ProductGroupModel()
        {
            this.PriceModel = new ProductGroupPriceModel();
            this.RatingModel = new ProductGroupRatingModel();
            this.Manufacturers = new List<ManufacturerModel>();
            this.DigitalDataModels = new List<DigitalDataModel>();
            this.Category = new BrandCategoryModel();
        }

        public string Name { get; set; }

        public string BrandCode { get; set; }

        public string LineCode { get; set; }

        public string MaterialCode { get; set; }

        public string Description { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public bool IsFreeShipping { get; set; }

        public bool IsShippingFromManufacturer { get; set; }

        public string SeName { get; set; }

        public List<DigitalDataModel> DigitalDataModels { get; set; }

        public BrandCategoryModel Category  { get; set; }

        public ProductGroupPriceModel PriceModel { get; set; }

        public ProductGroupRatingModel RatingModel { get; set; }

        public IList<ManufacturerModel> Manufacturers { get; set; }
    }
}