using Asu.Framework.Mvc;

namespace Asu.Web.Models.ShoppingCart
{
    /// <summary>
    /// WC Extendion of Nop.Web.Models.ShoppingCart.OrderTotalsModel
    /// </summary>
    public partial class OrderTotalsModel : BaseNopModel
    {
        public bool ShowOrderTotalData { get; set; }
        public bool IsAdmin { get; set; }
        public decimal? AdminShipping { get; set; }
        public string AdminShippingText { get; set; }
        public bool IsAdminShippingDifferent => this.AdminShippingText != this.Shipping;
        public int ItemsTotal { get; set; }
    }
}