namespace Asu.Data.Mapping.Orders
{
    using Asu.Core.Domain.Orders;

    public class PurchaseOrderMap : NopEntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderMap()
        {
            this.ToTable("vw_crm_PurchaseOrders");
            this.HasKey(m => m.Id);

            this.HasRequired(m => m.Order).WithMany(m => m.PurchaseOrders).HasForeignKey(m => m.OrderId);
            this.HasRequired(m => m.ShippingMethod).WithMany().HasForeignKey(m => m.ShippingMethodId);

            this.HasMany(a => a.Shipments)
                          .WithMany(b => b.PurchaseOrders)
                          .Map(ru =>
                          {
                              ru.MapLeftKey("PurchaseOrderId");
                              ru.MapRightKey("ShipmentId");
                              ru.ToTable("vw_crm_PurchaseOrderShipments");
                          });
        }
    }
}
