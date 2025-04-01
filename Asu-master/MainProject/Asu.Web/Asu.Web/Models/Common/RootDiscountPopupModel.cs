using Asu.Framework.Mvc;

namespace Asu.Web.Models.Common
{
    public partial class RootDiscountPopupModel : BaseNopModel
    {
        public bool ShowPopup { get; set; }
        public bool ShowCoupon { get; set; }
    }
}