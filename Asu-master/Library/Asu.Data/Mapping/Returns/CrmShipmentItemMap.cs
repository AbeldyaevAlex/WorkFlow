namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class CrmShipmentItemMap : NopEntityTypeConfiguration<CrmShipmentItem>
    {
        public CrmShipmentItemMap()
        {
            this.ToTable("vw_crm_ShipmentItems");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.ShipmentId, m.OrderLineId });

            this.HasRequired(m => m.Shipment).WithMany(m => m.Items).HasForeignKey(m => m.ShipmentId);
        }
    }
}
