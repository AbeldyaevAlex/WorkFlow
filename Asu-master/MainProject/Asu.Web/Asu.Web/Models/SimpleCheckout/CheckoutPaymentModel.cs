namespace Asu.Web.Models.SimpleCheckout
{
    using Asu.Framework.Mvc;
    using Asu.Web.Models.Checkout;
    using Asu.Web.Models.ShoppingCart;

    public class CheckoutPaymentModel : BaseNopModel
    {
        public CheckoutPaymentModel() 
        {
            this.ShippingAddressModel = new CheckoutAddressModel();
            this.ShippingMethodModel = new CheckoutShippingMethodModel();
            this.PaymentMethodModel = new CheckoutPaymentMethodModel();
            this.BillingAddressModel = new CheckoutAddressModel();
            this.PaymentInfoModel = new CheckoutPaymentInfoModel();
            this.ShoppingCartModel = new ShoppingCartModel();
        }

        public CheckoutAddressModel ShippingAddressModel { get; set; }
        public CheckoutAddressModel BillingAddressModel { get; set; }
        public CheckoutShippingMethodModel ShippingMethodModel { get; set; }
        public CheckoutPaymentMethodModel PaymentMethodModel { get; set; }
        public CheckoutPaymentInfoModel PaymentInfoModel { get; set; }
        public ShoppingCartModel ShoppingCartModel { get; set; }
        public bool ShowProp65Warning { get; set; }
        public bool ApplyReturnExtension { get; set; }
        public string ReturnExtension { get; set; }

        public bool ApplyPackageDeliveryInsurance { get; set; }
    }
}