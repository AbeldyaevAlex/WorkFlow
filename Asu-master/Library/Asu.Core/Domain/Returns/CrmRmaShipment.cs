namespace Asu.Core.Domain.Returns
{
    public class CrmRmaShipment : BaseEntity
    {
        public int RmaId { get; set; }

        public int ShipmentId { get; set; }

        public virtual Rma Rma { get; set; }

        public virtual CrmShipment Shipment { get; set; }
    }
}
