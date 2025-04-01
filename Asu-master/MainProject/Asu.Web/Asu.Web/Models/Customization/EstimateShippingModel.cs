namespace Asu.Web.Models.ShoppingCart
{
    using Asu.Framework.Mvc;

    /// <summary>
    /// WC Extendion of Nop.Web.Models.ShoppingCart.EstimateShippingModel
    /// </summary>
    public partial class EstimateShippingModel : BaseNopModel
    {
        public partial class ShippingOptionModel : BaseNopModel
        {
            public string ShippingRateComputationMethodSystemName { get; set; }

            public bool Selected { get; set; }
        }
    }
}