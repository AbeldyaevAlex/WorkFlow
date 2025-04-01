using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public partial class CancelledManualOrderWithoutEmailNotificationMap : NopEntityTypeConfiguration<CancelledManualOrderWithoutEmailNotification>
    {
        public CancelledManualOrderWithoutEmailNotificationMap()
        {
            this.ToTable("vw_CancelledManualOrdersWithoutEmailNotification");
            this.HasKey(o => o.Id);
        }
    }
}