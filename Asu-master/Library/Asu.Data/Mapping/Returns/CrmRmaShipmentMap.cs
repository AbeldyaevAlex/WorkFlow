namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class CrmRmaShipmentMap : NopEntityTypeConfiguration<CrmRmaShipment>
    {
        public CrmRmaShipmentMap()
        {
            this.ToTable("vw_crm_RmaShipments");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.RmaId, m.ShipmentId });
        }
    }
}
