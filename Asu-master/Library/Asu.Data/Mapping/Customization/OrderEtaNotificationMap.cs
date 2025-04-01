using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class OrderEtaNotificationMap : NopEntityTypeConfiguration<OrderEtaNotification>
    {
        public OrderEtaNotificationMap()
        {
            this.ToTable("WCS_OrderEtaNotification");
            this.HasKey(or => or.Id);

            this.Property(or => or.OrderId).IsRequired();
            this.Property(or => or.CreatedOnUtc).IsRequired();
        }
    }
}