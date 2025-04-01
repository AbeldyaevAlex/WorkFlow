namespace Asu.Web.Models.SimpleCheckout
{
    using FluentValidation.Attributes;

    using Asu.Web.Models.Customer;
    using Asu.Web.Validators.SimpleCheckout;
    using Asu.Web.Models.ShoppingCart;

    [Validator(typeof(CommonCheckoutAddressModelValidator))]
    public class CommonCheckoutAddressModel
    {
        public CommonCheckoutAddressModel()
        {
            this.BillingAddress = new CheckoutAddressModel();
            this.ShippingAddress = new CheckoutAddressModel();
            this.ShoppingCartModel = new ShoppingCartModel();
            this.IsBillingSameAsShipping = true;
        }

        public LoginModel Login { get; set; }

        public CheckoutAddressModel BillingAddress { get; set; }

        public CheckoutAddressModel ShippingAddress { get; set; }

        public ShoppingCartModel ShoppingCartModel { get; set; }

        public bool IsBillingSameAsShipping { get; set; }

        public bool CurrentCustomerIsGuest { get; set; }

        public bool IsAddressPreselected { get; set; }

        public bool ApplyReturnExtension { get; set; }
        public string ShippingDeliveryReturnExtension { get; set; }
    }
}