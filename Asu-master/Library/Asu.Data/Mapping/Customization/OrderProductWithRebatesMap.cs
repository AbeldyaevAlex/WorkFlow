using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class OrderProductWithRebatesMap : NopEntityTypeConfiguration<OrderProductWithRebates>
    {
        public OrderProductWithRebatesMap()
        {
            this.ToTable("WC_OrderWithRebates");
            this.HasKey(or => or.Id);

            this.Property(or => or.ProductId).IsRequired();
            this.Property(or => or.RebateAmount).IsRequired();
            this.Property(or => or.OrderProductVariantId).IsRequired();
        }
    }
}