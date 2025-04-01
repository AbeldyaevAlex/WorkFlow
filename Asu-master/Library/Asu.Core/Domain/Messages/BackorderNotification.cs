using System;

namespace Asu.Core.Domain.Messages
{
    public class BackorderNotification : BaseEntity
    {
        public int PurchaseOrderId { get; set; }

        public int TypeId { get; set; }

        public DateTime? Esd { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime SentOn { get; set; }
    }
}
