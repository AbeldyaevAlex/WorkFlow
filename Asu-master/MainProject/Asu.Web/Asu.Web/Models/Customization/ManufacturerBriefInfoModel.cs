using Asu.Framework.Mvc;
using Asu.Web.Models.Media;

namespace Asu.Web.Models.Catalog
{
    public partial class ManufacturerBriefInfoModel : BaseNopEntityModel
    {
        public ManufacturerBriefInfoModel()
        {
            this.Logo = new PictureModel();
        }

        public PictureModel Logo { get; set; }
    }
}