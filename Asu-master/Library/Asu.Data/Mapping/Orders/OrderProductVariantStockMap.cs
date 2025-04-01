namespace Asu.Data.Mapping.Orders
{
    using Asu.Core.Domain.Orders;

    public class OrderProductVariantStockMap : NopEntityTypeConfiguration<OrderProductVariantStock>
    {
        public OrderProductVariantStockMap()
        {
            this.ToTable("WC_OrderProductVariantStockQTY");
            this.HasKey(m => m.Id);
            this.Property(m => m.Id).HasColumnName("OrderProductVariantId");
            this.Property(m => m.AvailableVendors).HasColumnName("availvendors");
            this.Property(m => m.AlwaysInStock).HasColumnName("Always");
            this.Property(m => m.TotalQtyAfterOrderPlaced).HasColumnName("StockQtyAfterOrder");
            this.Property(m => m.InStockLowestCost).HasColumnName("LowestInStockCost");
        }
    }
}
