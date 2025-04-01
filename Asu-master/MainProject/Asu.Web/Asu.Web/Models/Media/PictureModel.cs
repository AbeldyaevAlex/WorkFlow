using Asu.Framework.Mvc;

namespace Asu.Web.Models.Media
{
    public partial class PictureModel : BaseNopModel
    {
        public string ImageUrl { get; set; }

        public string FullSizeImageUrl { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }
    }
}