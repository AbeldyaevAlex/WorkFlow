namespace Asu.Core.Domain.Shipping
{
    public class CrmTrackingStatus: BaseEntity
    {
        public string Name { get; set; }

        public bool Final { get; set; }

        public bool NeedsAttention { get; set; }
    }
}
