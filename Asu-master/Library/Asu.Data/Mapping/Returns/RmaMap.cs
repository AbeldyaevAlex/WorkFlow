namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RmaMap : NopEntityTypeConfiguration<Rma>
    {
        public RmaMap()
        {
            this.ToTable("vw_crm_Rmas");
            this.HasKey(i => i.Id);
            this.HasRequired(i => i.Return).WithMany(i => i.Rmas);
            this.HasMany(m => m.RmaShipments).WithRequired(m => m.Rma).HasForeignKey(m => m.RmaId);
        }
    }
}