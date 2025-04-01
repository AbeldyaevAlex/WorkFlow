using Asu.Framework.Mvc;

namespace Asu.Web.Models.Checkout
{
    public partial class CheckoutProgressModel : BaseNopModel
    {
        public CheckoutProgressStep CheckoutProgressStep { get; set; }
        public int? OrderNumber { get; set; }
    }

    public enum CheckoutProgressStep
    {
        Cart,
        Address,
        Shipping,
        Payment,
        Confirm,
        Complete,
        ShippingAndPayment
    }
}