using Asu.Web.Models.ShoppingCart;

namespace Asu.Web.Models.Customization
{
    public partial class AmazonCheckoutModel : ShoppingCartModel
    {
        public string OrderReferenceId { get; set; }
        public AmazonOrderTotalsModel OrderTotalsModel { get; set; }
        public string ErrorMessage { get; set; }

        #region Nested Classes

        public partial class AmazonOrderTotalsModel : OrderTotalsModel
        {

        }

        #endregion
    }
}