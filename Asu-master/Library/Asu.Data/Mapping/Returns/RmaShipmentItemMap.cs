namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RmaShipmentItemMap : NopEntityTypeConfiguration<RmaShipmentItem>
    {
        public RmaShipmentItemMap()
        {
            this.ToTable("WCS_RmaShipmentItems");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.LineId, m.ShipmentId });

            this.HasRequired(m => m.Shipment).WithMany(m => m.Items).HasForeignKey(m => m.ShipmentId);
        }
    }
}
