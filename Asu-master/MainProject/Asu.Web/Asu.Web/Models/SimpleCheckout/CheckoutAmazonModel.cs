namespace Asu.Web.Models.SimpleCheckout
{
    using Asu.Web.Models.ShoppingCart;

    public class CheckoutAmazonModel
    {
        public CheckoutAmazonModel()
        {
            this.ShoppingCartModel = new ShoppingCartModel();
        }

        public ShoppingCartModel ShoppingCartModel { get; set; }

        public string ScriptsUrl { get; set; }

        public string SellerId { get; set; }

        public string ClientId { get; set; }

        public string AccessToken { get; set; }

        public string OrderReferenceId { get; set; }

        public string ErrorMessage { get; set; }

        public bool ShowProp65Warning { get; set; }
    }
}