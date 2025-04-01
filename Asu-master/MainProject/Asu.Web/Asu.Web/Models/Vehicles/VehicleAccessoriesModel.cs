using System.Collections.Generic;
using Asu.Framework.Mvc;
using Asu.Web.Models.Media;

namespace Asu.Web.Models.Vehicles
{
    public class VehicleAccessoriesModel : BaseNopEntityModel
    {
        public VehicleAccessoriesModel()
        {
            this.Categories = new List<CategoryModel>();
            this.VehicleSeoModel = new VehicleSeoModel();
            this.Vehicles = new List<VehicleSeoModel>();
        }

        public string Name { get { return this.VehicleSeoModel.Name; } }
        public string Description { get { return this.VehicleSeoModel.Description; } }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }

        public VehicleSeoModel VehicleSeoModel { get; set; }
        public IList<VehicleSeoModel> Vehicles { get; set; }
        public IList<CategoryModel> Categories { get; set; }

        #region Nested Classes

        public partial class CategoryModel : BaseNopEntityModel
        {
            public CategoryModel()
            {
                PictureModel = new PictureModel();
                this.ChildCategories = new List<CategoryModel>();
            }

            public string Name { get; set; }

            public string SeName { get; set; }

            public PictureModel PictureModel { get; set; }

            public IList<CategoryModel> ChildCategories { get; set; }
        }

        #endregion
    }
}