using System;
using System.Collections.Generic;
using Asu.Framework.Mvc;
using Asu.Web.Models.Common;

namespace Asu.Web.Models.Order
{
    using Asu.Core;
    using Asu.Core.Infrastructure;
    using Asu.Web.Extensions;
    using Asu.Web.Models.Media;
    using Asu.Web.Models.Returns;

    public partial class OrderDetailsModel : BaseNopEntityModel
    {
        public OrderDetailsModel()
        {
            this.TaxRates = new List<TaxRate>();
            this.GiftCards = new List<GiftCard>();
            this.Items = new List<OrderItemModel>();
            this.OrderNotes = new List<OrderNote>();
            this.Shipments = new List<ShipmentBriefModel>();
            this.BillingAddress = new AddressModel();
            this.ShippingAddress = new AddressModel();
            this.Channel = EngineContext.Current.Resolve<IStoreContext>().CurrentStore.GetStoreChannel();
            this.ReturnRequests = new List<ReturnRequestModel>();
            this.PureCancels = new List<ReturnItemModel>();
        }

        public bool PrintMode { get; set; }

        // WC.
        public Channel Channel { get; set; }

        // WC.
        public int? CrmOrderId { get; set; }

        // WC.
        public string DisplayOrderReference { get; set; }

        // WC.
        public bool ViewOnlyMode { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OrderStatus { get; set; }

        public bool IsReOrderAllowed { get; set; }

        public bool IsReturnRequestAllowed { get; set; }
        public bool IsCancelled { get; set; }

        public bool AllItemsReturned { get; set; }

        public bool IsShippable { get; set; }
        public bool PickUpInStore { get; set; }
        public string ShippingStatus { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public string ShippingMethod { get; set; }
        public IList<ShipmentBriefModel> Shipments { get; set; }

        public AddressModel BillingAddress { get; set; }

        public string VatNumber { get; set; }

        public string PaymentMethod { get; set; }
        public string PaymentMethodStatus { get; set; }
        public bool CanRePostProcessPayment { get; set; }
        public bool DisplayPurchaseOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }

        public string OrderSubtotal { get; set; }
        public string OrderSubTotalDiscount { get; set; }
        public string OrderShipping { get; set; }
        public string PaymentMethodAdditionalFee { get; set; }
        public string CheckoutAttributeInfo { get; set; }

        public bool PricesIncludeTax { get; set; }
        public bool DisplayTaxShippingInfo { get; set; }
        public string Tax { get; set; }
        public IList<TaxRate> TaxRates { get; set; }
        public bool DisplayTax { get; set; }
        public bool DisplayTaxRates { get; set; }

        public string OrderTotalDiscount { get; set; }
        public int RedeemedRewardPoints { get; set; }
        public string RedeemedRewardPointsAmount { get; set; }
        public string OrderTotal { get; set; }
        
        public IList<GiftCard> GiftCards { get; set; }

        public bool ShowSku { get; set; }
        public IList<OrderItemModel> Items { get; set; }
        
        public IList<OrderNote> OrderNotes { get; set; }

        public IList<ReturnItemModel> PureCancels { get; set; }

        public decimal PureCancelsCreditAmount { get; set; }

        public IList<ReturnRequestModel> ReturnRequests { get; set; }

		#region Nested Classes

        public partial class OrderItemModel : BaseNopEntityModel
        {
            public Guid OrderItemGuid { get; set; }
            public string Sku { get; set; }
            public int? ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductSeName { get; set; }
            public string UnitPrice { get; set; }
            public string SubTotal { get; set; }
            public int Quantity { get; set; }
            public string AttributeInfo { get; set; }
            
            //downloadable product properties
            public int DownloadId { get; set; }
            public int LicenseId { get; set; }
            public bool IsInsurance { get; set; }
            public bool IsReturnExtension { get; set; }

            // WC.
            public PictureModel Picture { get; set; }
            // WC.
            public bool IsImageLoader
            {
                get
                {
                    if (this.Picture == null || string.IsNullOrEmpty(this.Picture.ImageUrl))
                    {
                        return false;
                    }

                    return this.Picture.ImageUrl.Contains("ImageLoader/");
                }
            }
        }

        public partial class TaxRate : BaseNopModel
        {
            public string Rate { get; set; }
            public string Value { get; set; }
        }

        public partial class GiftCard : BaseNopModel
        {
            public string CouponCode { get; set; }
            public string Amount { get; set; }
        }

        public partial class OrderNote : BaseNopEntityModel
        {
            public bool HasDownload { get; set; }
            public string Note { get; set; }
            public DateTime CreatedOn { get; set; }
        }

        public partial class ShipmentBriefModel : BaseNopEntityModel
        {
            public string CarrierUrl { get; set; }
            public string TrackingNumber { get; set; }
            public DateTime? ShippedDate { get; set; }
            public DateTime? DeliveryDate { get; set; }
            public DateTime? EstimateDeliveryDate { get; set; }

            public string Status { get; set; }

            public string CarrierName { get; set; }

            // WC.
            public bool TrackPackageAvailable { get; set; }
        }
		#endregion
    }
}