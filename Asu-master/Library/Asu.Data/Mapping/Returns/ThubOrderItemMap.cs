namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ThubOrderItemMap : NopEntityTypeConfiguration<ThubOrderItem>
    {
        public ThubOrderItemMap()
        {
            this.ToTable("vw_thub_OrderItems");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.OrderItemId);

            this.HasRequired(orderItem => orderItem.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId);

            this.HasRequired(i => i.Product).WithMany().HasForeignKey(i => i.ProductId);
        }
    }
}