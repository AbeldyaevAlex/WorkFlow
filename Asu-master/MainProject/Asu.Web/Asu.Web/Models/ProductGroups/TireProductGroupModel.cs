namespace Asu.Web.Models.ProductGroups
{
    using Asu.Framework.Mvc;
    using Asu.Web.Models.Catalog;
    using System.Collections.Generic;

    public class ProductGroupModel : BaseNopEntityModel
    {
        public ProductGroupModel()
        {
            this.Manufacturers = new List<ManufacturerModel>();
            this.Resources = new List<ProductGroupResourceModel>();
            this.VariantModel = new ProductGroupVariantModel();
            this.Product = new ProductModel();
        }

        public string Name { get; set; }

        public decimal RatingScore { get; set; }

        public int Ratings { get; set; }

        public string Description { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public bool FreeShippingNotificationEnabled { get; set; }

        public ProductGroupVehicleModel Vehicle { get; set; }

        public ProductGroupRatingModel Rating { get; set; }

        public ProductGroupPriceModel Price { get; set; }

        public IList<ManufacturerModel> Manufacturers { get; set; }

        public IList<ProductGroupResourceModel> Resources { get; set; }

        public ProductGroupVariantModel VariantModel { get; set; }

        public ProductModel Product { get; set; }
    }
}