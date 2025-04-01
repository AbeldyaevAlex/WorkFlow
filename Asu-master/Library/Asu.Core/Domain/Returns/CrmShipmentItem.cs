namespace Asu.Core.Domain.Returns
{
    using Asu.Core.Domain.Orders;

    public class CrmShipmentItem : BaseEntity
    {
        public int ShipmentId { get; set; }

        public int OrderLineId { get; set; }

        public int Quantity { get; set; }

        public virtual CrmShipment Shipment { get; set; }

        public virtual CrmSalesOrderLine OrderLine { get; set; }
    }
}
