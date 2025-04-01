namespace Asu.Core.Domain.Orders
{
    using System;
    using System.Collections.Generic;

    using Asu.Core.Domain.Returns;
    using Asu.Core.Domain.Shipping;

    public class PurchaseOrder : BaseEntity
    {
        private ICollection<CrmShipment> shipments;

        public int OrderId { get; set; }

        public int VendorId { get; set; }

        public decimal ShippingCost { get; set; }

        public int ShippingMethodId { get; set; }

        public string VendorNote { get; set; }

        public int PurchaseMethodId { get; set; }

        public string Reference { get; set; }

        public DateTime Date { get; set; }

        public DateTime OrderedOn { get; set; }

        public DateTime? EstimatedTimeArrival { get; set; }

        public decimal? VendorDropShipCost { get; set; }

        public decimal? ManufacturerDropShipCost { get; set; }

        public decimal Discount { get; set; }

        public virtual CrmSalesOrder Order { get; set; }

        public virtual CrmShippingMethod ShippingMethod { get; set; }

        public virtual ICollection<CrmShipment> Shipments
        {
            get { return this.shipments ?? (this.shipments = new List<CrmShipment>()); }
            protected set { this.shipments = value; }
        }
    }
}
