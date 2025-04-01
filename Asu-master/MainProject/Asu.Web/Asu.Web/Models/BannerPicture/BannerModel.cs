using Asu.Framework.Mvc;
using System;

namespace Asu.Web.Models.BannerPicture
{
    public partial class BannerModel : BaseNopEntityModel
    {
        public string BannerImageUrl { get; set; }

        public string MobileBannerImageUrl { get; set; }

        public DateTime? StartDateTimeUtc { get; set; }

        public DateTime? EndDateTimeUtc { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }
    }
}