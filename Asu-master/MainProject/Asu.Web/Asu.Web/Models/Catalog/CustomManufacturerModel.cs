using Asu.Web.Models.BannerPicture;
using Asu.Web.Models.Vehicles;
using System.Collections.Generic;

namespace Asu.Web.Models.Catalog
{
    public class CustomManufacturerModel : ManufacturerModel
    {
        public CustomManufacturerModel()
        {
            this.FilterModel = new FilterSearchModel();
            this.BannerPictureModel = new List<BannerModel>();
        }

        public FilterSearchModel FilterModel { get; set; }

        public string ManufacturerCategorySeName { get; set; }

        public List<BannerModel> BannerPictureModel { get; set; }
    }
}