using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class OrderWithRebatesNotificationMap : NopEntityTypeConfiguration<OrderWithRebatesNotification>
    {
        public OrderWithRebatesNotificationMap()
        {
            this.ToTable("WCS_OrderWithRebatesNotification");
            this.HasKey(or => or.Id);

            this.Property(or => or.OrderId).IsRequired();
            this.Property(or => or.CreatedOnUtc).IsRequired();
        }
    }
}