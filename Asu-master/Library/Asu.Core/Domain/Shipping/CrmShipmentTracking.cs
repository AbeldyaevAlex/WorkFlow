namespace Asu.Core.Domain.Shipping
{
    using System;

    using Asu.Core.Domain.Returns;

    public class CrmShipmentTracking : BaseEntity
    {
        public int ShipmentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int StatusId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CheckedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime? ProlongedOn { get; set; }

        public string Content { get; set; }

        public int NotificationId { get; set; }

        public virtual CrmTrackingStatus Status { get; set; }

        public virtual CrmShipment Shipment { get; set; }
    }
}
