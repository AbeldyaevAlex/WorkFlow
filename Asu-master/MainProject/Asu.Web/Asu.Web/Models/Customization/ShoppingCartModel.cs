using Asu.Framework.Mvc;

namespace Asu.Web.Models.ShoppingCart
{
    /// <summary>
    /// WC. Extending of Nop.Web.Models.ShoppingCart.ShoppingCartModel class
    /// </summary>
    public partial class ShoppingCartModel : BaseNopModel
    {
        public bool IsAdmin { get; set; }

        public partial class ShoppingCartItemModel : BaseNopEntityModel
        {
            public string AdminUnitPrice { get; set; }

            public bool IsAdminPriceDifferent
            {
                get { return this.AdminUnitPrice != this.UnitPrice; }
            }

            public bool IsFreeShipping { get; set; }
            public bool IsShippingFromManufacturer { get; set; }
        }
    }
}