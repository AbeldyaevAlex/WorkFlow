using System;

namespace Asu.Core.Domain.Orders
{
    public class Backorder : BaseEntity
    {
        public int PurchaseOrderId { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? Esd { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime EndedOn { get; set; }

        public int ChannelId { get; set; }
    }
}
