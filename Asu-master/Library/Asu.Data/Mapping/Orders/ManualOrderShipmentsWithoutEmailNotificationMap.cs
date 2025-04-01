using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public partial class ManualOrderShipmentsWithoutEmailNotificationMap : NopEntityTypeConfiguration<ManualOrderShipmentsWithoutEmailNotification>
    {
        public ManualOrderShipmentsWithoutEmailNotificationMap()
        {
            this.ToTable("vw_ManualOrderShipmentsWithoutEmailNotification");
            this.HasKey(o => o.Id);
        }
    }
}