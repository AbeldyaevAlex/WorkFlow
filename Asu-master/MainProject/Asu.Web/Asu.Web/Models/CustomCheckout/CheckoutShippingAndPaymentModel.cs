namespace Asu.Web.Models.CustomCheckout
{
    using Asu.Framework.Mvc;
    using Checkout;

    public class CheckoutShippingAndPaymentModel : BaseNopModel
    {
        public CheckoutShippingAndPaymentModel()
        {
            this.ShippingAddressModel = new CheckoutShippingAddressModel();
            this.ShippingMethodModel = new CheckoutShippingMethodModel();
            this.PaymentMethodModel = new CheckoutPaymentMethodModel();
            this.BillingAddressModel = new CheckoutBillingAddressModel();
        }

        public CheckoutShippingAddressModel ShippingAddressModel { get; set; }
        public CheckoutBillingAddressModel BillingAddressModel { get; set; }
        public CheckoutShippingMethodModel ShippingMethodModel { get; set; }
        public CheckoutPaymentMethodModel PaymentMethodModel { get; set; }
        public CheckoutPaymentInfoModel PaymentInfoModel { get; set; }
    }
}