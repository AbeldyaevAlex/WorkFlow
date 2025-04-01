using System;
using System.Collections.Generic;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Order
{
    using Asu.Web.Models.Common;

    public partial class ShipmentDetailsModel : BaseNopEntityModel
    {
        public ShipmentDetailsModel()
        {
            this.ShipmentStatusEvents = new List<ShipmentStatusEventModel>();
            this.Items = new List<ShipmentItemModel>();
        }

        public string OrderId { get; set; }

        public int CrmOrderId { get; set; }

        public DateTime OrderDate { get; set; }
        public string TrackingNumber { get; set; }
        // WC.
        public string Carrier { get; set; }
        public string TrackingNumberUrl { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        // WC.
        public DateTime? EstimateDeliveryDate { get; set; }
        public IList<ShipmentStatusEventModel> ShipmentStatusEvents { get; set; }
        public bool ShowSku { get; set; }
        public IList<ShipmentItemModel> Items { get; set; }

        #region Nested Classes

        public partial class ShipmentItemModel : BaseNopEntityModel
        {
            public string Sku { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductSeName { get; set; }
            public string AttributeInfo { get; set; }

            public int QuantityOrdered { get; set; }
            public int QuantityShipped { get; set; }
        }

        public partial class ShipmentStatusEventModel : BaseNopModel
        {
            public string EventName { get; set; }
            public string Location { get; set; }
            public string Country { get; set; }
            public DateTime? Date { get; set; }
        }

		#endregion
    }
}