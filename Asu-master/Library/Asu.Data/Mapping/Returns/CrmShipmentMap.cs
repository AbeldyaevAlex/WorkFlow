namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;
    public class CrmShipmentMap : NopEntityTypeConfiguration<CrmShipment>
    {
        public CrmShipmentMap()
        {
            this.ToTable("vw_crm_Shipments");
            this.HasKey(m => m.Id);

            this.HasRequired(m => m.ShippingService).WithMany().HasForeignKey(m => m.ShippingServiceId);
            this.HasMany(m => m.RmaShipments).WithRequired(m => m.Shipment).HasForeignKey(m => m.ShipmentId);

            this.HasMany(m => m.Events).WithRequired(m => m.Shipment).HasForeignKey(m => m.ShipmentId);
        }
    }
}
