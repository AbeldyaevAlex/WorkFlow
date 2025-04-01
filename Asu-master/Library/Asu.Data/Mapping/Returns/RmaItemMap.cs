namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RmaItemMap : NopEntityTypeConfiguration<RmaItem>
    {
        public RmaItemMap()
        {
            this.ToTable("vw_crm_RmaItems");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.OrderLineId, i.RmaId });
            this.HasRequired(i => i.Rma).WithMany(r => r.RmaItems).HasForeignKey(i => i.RmaId);
        }
    }
}