namespace Asu.Core.Domain.Returns
{
    using System;

    public class CrmShipmentEvent : BaseEntity
    {
        public int ShipmentId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Code { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public virtual CrmShipment Shipment { get; set; }

        public string Description { get; set; }
    }
}
