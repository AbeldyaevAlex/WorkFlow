namespace Asu.Core.Domain.Returns
{
    public class RmaShipmentItem : BaseEntity
    {
        public long? OrderItemId { get; set; }

        public int LineId { get; set; }

        public int Quantity { get; set; }

        public int ShipmentId { get; set; }

        public virtual RmaShipment Shipment { get; set; }
    }
}
