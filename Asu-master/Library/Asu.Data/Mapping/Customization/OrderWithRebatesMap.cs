using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class OrderWithRebatesMap : NopEntityTypeConfiguration<OrderWithRebates>
    {
        public OrderWithRebatesMap()
        {
            this.ToTable("vw_OrderWithRebates");
            this.HasKey(or => or.OrderId);

            this.Ignore(or => or.Id);

            this.Property(or => or.OrderId).IsRequired();
            this.Property(or => or.RebateAmount).IsRequired();
            this.Property(or => or.CustomerFullName).IsRequired();
            this.Property(or => or.Email).IsRequired();
            this.Property(or => or.CouponCode).IsRequired();
        }
    }
}
