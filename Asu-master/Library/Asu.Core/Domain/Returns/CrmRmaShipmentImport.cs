namespace Asu.Core.Domain.Returns
{
    using System;

    public class CrmRmaShipmentImport : BaseEntity
    {
        public int RmaShipmentId { get; set; }

        public int ShipmentId { get; set; }

        public DateTime ImportedOn { get; set; }

        public virtual RmaShipment RmaShipment { get; set; }

        public virtual CrmShipment CrmShipment { get; set; }
    }
}
