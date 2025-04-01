namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    public class RmaShipment : BaseEntity
    {
        private ICollection<RmaShipmentItem> items;

        public int RmaId { get; set; }

        public string TrackingNumber { get; set; }

        public int ShippingServiceId { get; set; }

        public virtual CrmRmaShipmentImport Import { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<RmaShipmentItem> Items
        {
            get { return this.items ?? (this.items = new List<RmaShipmentItem>()); }
            set { this.items = value; }
        }
    }
}
