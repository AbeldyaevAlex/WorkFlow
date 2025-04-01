using System.Collections.Generic;
using Asu.Services.Catalog;
using Asu.Web.Models.BannerPicture;
using Asu.Web.Models.Home;
using Asu.Web.Models.Vehicles;

namespace Asu.Web.Models.Catalog
{
    public sealed class CustomCategoryModel : CategoryModel
    {
        public CustomCategoryModel()
        {
            this.DisplayBreadcrumb = false;
            this.Breadcrumb = new List<BreadCrumb>();
            this.FilterModel = new FilterSearchModel();
            this.TireConfigurator = new TireConfiguratorModel();
            this.BannerPictureModel = new List<BannerModel>();
        }

        public bool DisplayBreadcrumb { get; set; }
        public IList<BreadCrumb> Breadcrumb { get; set; }
        public FilterSearchModel FilterModel { get; set; }
        public bool IsVehicleSeoCategory { get; set; }
        public string VehicleSeName { get; set; }
        public string CategoryManufacturerSeName { get; set; }
        public TireConfiguratorModel TireConfigurator { get; set; }
        public List<BannerModel> BannerPictureModel { get; set; }
    }
}