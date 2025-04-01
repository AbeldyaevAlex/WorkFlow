namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.Shipping;

    public class CrmShipment : BaseEntity
    {
        private ICollection<CrmShipmentItem> items;
        private ICollection<CrmRmaShipment> rmaShipments;
        private ICollection<PurchaseOrder> purchaseOrders;
        private ICollection<CrmShipmentEvent> shipmentEvents;

        public int ShippingServiceId { get; set; }

        public string TrackingNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ShippedOn { get; set; }

        public DateTime? DeliveredOn { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public virtual ShippingService ShippingService { get; set; }

        public virtual CrmShipmentTracking Tracking { get; set; }

        public virtual ICollection<CrmRmaShipment> RmaShipments
        {
            get { return this.rmaShipments ?? (this.rmaShipments = new List<CrmRmaShipment>()); }
            protected set { this.rmaShipments = value; }
        }

        public virtual ICollection<CrmShipmentItem> Items
        {
            get { return this.items ?? (this.items = new List<CrmShipmentItem>()); }
            protected set { this.items = value; }
        }

        public virtual ICollection<PurchaseOrder> PurchaseOrders
        {
            get { return this.purchaseOrders ?? (this.purchaseOrders = new List<PurchaseOrder>()); }
            protected set { this.purchaseOrders = value; }
        }

        public virtual ICollection<CrmShipmentEvent> Events
        {
            get { return this.shipmentEvents ?? (this.shipmentEvents = new List<CrmShipmentEvent>()); }
            protected set { this.shipmentEvents = value; }
        }
    }
}
