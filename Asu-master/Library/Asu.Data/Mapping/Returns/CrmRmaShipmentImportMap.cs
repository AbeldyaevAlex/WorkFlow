namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class CrmRmaShipmentImportMap : NopEntityTypeConfiguration<CrmRmaShipmentImport>
    {
        public CrmRmaShipmentImportMap()
        {
            this.ToTable("vw_crm_RmaShipmentImports");
            this.Ignore(m => m.Id);
            this.HasKey(m => m.RmaShipmentId);

            this.HasRequired(m => m.RmaShipment).WithOptional(m => m.Import);
            this.HasRequired(m => m.CrmShipment).WithMany().HasForeignKey(m => m.ShipmentId);
        }
    }
}
