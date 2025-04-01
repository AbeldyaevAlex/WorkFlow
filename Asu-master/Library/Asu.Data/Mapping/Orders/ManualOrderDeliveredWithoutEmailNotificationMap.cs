using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public partial class ManualOrderDeliveredWithoutEmailNotificationMap : NopEntityTypeConfiguration<ManualOrderDeliveredWithoutEmailNotification>
    {
        public ManualOrderDeliveredWithoutEmailNotificationMap()
        {
            this.ToTable("vw_ManualOrderDeliveredWithoutEmailNotification");
            this.HasKey(o => o.Id);
        }
    }
}