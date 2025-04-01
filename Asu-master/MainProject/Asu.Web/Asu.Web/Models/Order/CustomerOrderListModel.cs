namespace Asu.Web.Models.Order
{
    using System;
    using System.Collections.Generic;
    using Asu.Framework.Mvc;
    using Media;

    public partial class CustomerOrderListModel : BaseNopModel
    {
        public CustomerOrderListModel()
        {
            this.Orders = new List<OrderDetailsModel>();
            this.RecurringOrders = new List<RecurringOrderModel>();
            this.CancelRecurringPaymentErrors = new List<string>();
        }

        public IList<OrderDetailsModel> Orders { get; set; }
        public IList<RecurringOrderModel> RecurringOrders { get; set; }
        public IList<string> CancelRecurringPaymentErrors { get; set; }


        #region Nested classes

        public partial class OrderDetailsModel : BaseNopEntityModel
        {
            public OrderDetailsModel()
            {
                this.Items = new List<OrderDetailsItemModel>();
            }

            public bool HasShipments { get; set; }
            public string OrderTotal { get; set; }
            public bool IsReturnRequestAllowed { get; set; }
            public string OrderStatus { get; set; }
            public bool IsCancelled { get; set; }
            public string PaymentStatus { get; set; }
            public string ShippingStatus { get; set; }
            public DateTime CreatedOn { get; set; }
            // WC.
            public bool NumberOfDaysReturnRequestAvailableValid { get; set; }
            // WC.
            public int? CrmOrderId { get; set; }
            // WC.
            public List<OrderDetailsItemModel> Items { get; set; }
        }

        // WC
        public partial class OrderDetailsItemModel : BaseNopEntityModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductSeName { get; set; }
            public PictureModel Picture { get; set; }

            public bool IsImageLoader
            {
                get
                {
                    if (this.Picture == null)
                    {
                        return false;
                    }

                    return this.Picture.ImageUrl.Contains("ImageLoader/");
                }
            }

            public string UnitPrice { get; set; }
            public string SubTotal { get; set; }
            public int Quantity { get; set; }
        }

        public partial class RecurringOrderModel : BaseNopEntityModel
        {
            public string StartDate { get; set; }
            public string CycleInfo { get; set; }
            public string NextPayment { get; set; }
            public int TotalCycles { get; set; }
            public int CyclesRemaining { get; set; }
            public int InitialOrderId { get; set; }
            public bool CanCancel { get; set; }
        }

        #endregion
    }
}