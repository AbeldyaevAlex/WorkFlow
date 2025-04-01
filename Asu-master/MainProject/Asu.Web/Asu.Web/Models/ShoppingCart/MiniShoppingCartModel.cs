using System.Collections.Generic;
using Asu.Framework.Mvc;
using Asu.Web.Models.Catalog;
using Asu.Web.Models.Media;

namespace Asu.Web.Models.ShoppingCart
{
    public partial class MiniShoppingCartModel : BaseNopModel
    {
        public MiniShoppingCartModel()
        {
            Items = new List<ShoppingCartItemModel>();
        }

        public IList<ShoppingCartItemModel> Items { get; set; }
        public int TotalProducts { get; set; }
        public string SubTotal { get; set; }
        public bool DisplayShoppingCartButton { get; set; }
        public bool DisplayCheckoutButton { get; set; }
        public bool CurrentCustomerIsGuest { get; set; }
        public bool AnonymousCheckoutAllowed { get; set; }
        public bool ShowProductImages { get; set; }

        public string ShippingDeliveryInsurance { get; set; }
        public string ShippingDeliveryReturnExtension { get; set; }


        #region Nested Classes

        public partial class ShoppingCartItemModel : BaseNopEntityModel
        {
            public ShoppingCartItemModel()
            {
                Picture = new PictureModel();
                ManufacturerPicture = new PictureModel();
            }

            public bool IsInsurance { get; set; }
            public bool IsReturnExtension { get; set; }

            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public string ProductSeName { get; set; }

            public int Quantity { get; set; }

            public string UnitPrice { get; set; }

            public string AttributeInfo { get; set; }

            public PictureModel Picture { get; set; }

            #region WC

            public string PartNumber { get; set; }
            public string Manufacturer { get; set; }
            public PictureModel ManufacturerPicture { get; set; }

            public CustomProductReviewOverviewModel ProductReviewOverview { get; set; }

            #endregion
        }

        #endregion
    }
}