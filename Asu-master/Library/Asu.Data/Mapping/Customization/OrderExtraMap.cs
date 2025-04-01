using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public class OrderExtraMap : NopEntityTypeConfiguration<OrderExtra>
    {
        public OrderExtraMap()
        {
            this.ToTable("WCS_OrderExtra");
            this.HasKey(or => or.Id);

            this.Property(l => l.OrderId).IsRequired();
        }
    }
}