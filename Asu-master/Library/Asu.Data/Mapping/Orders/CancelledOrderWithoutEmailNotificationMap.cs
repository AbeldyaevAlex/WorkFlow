using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public partial class CancelledOrderWithoutEmailNotificationMap : NopEntityTypeConfiguration<CancelledOrderWithoutEmailNotification>
    {
        public CancelledOrderWithoutEmailNotificationMap()
        {
            this.ToTable("vw_CancelledOrdersWithoutEmailNotification");
            this.HasKey(o => o.Id);
        }
    }
}