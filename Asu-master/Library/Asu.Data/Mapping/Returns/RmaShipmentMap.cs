namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RmaShipmentMap : NopEntityTypeConfiguration<RmaShipment>
    {
        public RmaShipmentMap()
        {
            this.ToTable("WCS_RmaShipments");
            this.Property(m => m.CreatedOn).HasColumnType("datetime");
            this.HasKey(m => m.Id);
        }
    }
}
