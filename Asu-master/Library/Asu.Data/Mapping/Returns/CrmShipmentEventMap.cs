namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class CrmShipmentEventMap : NopEntityTypeConfiguration<CrmShipmentEvent>
    {
        public CrmShipmentEventMap()
        {
            this.ToTable("vw_crm_ShipmentEvents");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.ShipmentId, m.Description });

            // this.HasRequired(m => m.Shipment).WithMany(m => m.Events).HasForeignKey(m => m.ShipmentId);
        }
    }
}
