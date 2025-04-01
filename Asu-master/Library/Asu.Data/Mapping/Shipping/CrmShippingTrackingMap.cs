namespace Asu.Data.Mapping.Shipping
{
    using Asu.Core.Domain.Shipping;

    public class CrmShipmentTrackingMap : NopEntityTypeConfiguration<CrmShipmentTracking>
    {
        public CrmShipmentTrackingMap()
        {
            this.ToTable("vw_crm_ShipmentTrackings");
            this.HasKey(m => m.ShipmentId);

            this.HasRequired(m => m.Status);
            this.HasRequired(m => m.Shipment);
        }
    }
}
