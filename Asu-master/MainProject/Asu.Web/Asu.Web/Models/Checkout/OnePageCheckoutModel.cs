using Asu.Framework.Mvc;

namespace Asu.Web.Models.Checkout
{
    public partial class OnePageCheckoutModel : BaseNopModel
    {
        public bool ShippingRequired { get; set; }
        public bool DisableBillingAddressCheckoutStep { get; set; }
    }
}