using System;

namespace Asu.Core.Domain.Customization
{
    public partial class OrderEtaNotification : BaseEntity
    {
        public int OrderId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}