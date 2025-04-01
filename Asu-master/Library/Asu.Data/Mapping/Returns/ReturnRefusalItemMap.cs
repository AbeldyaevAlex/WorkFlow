namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnRefusalItemMap : NopEntityTypeConfiguration<ReturnRefusalItem>
    {
        public ReturnRefusalItemMap()
        {
            this.ToTable("vw_crm_ReturnRefusalItems");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.OrderLineId, i.RefusalId });
            this.HasRequired(p => p.ReturnRefusal).WithMany(i => i.Items).HasForeignKey(i => i.RefusalId);
        }
    }
}