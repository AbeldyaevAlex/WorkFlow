namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RmaReturnRefusalMap : NopEntityTypeConfiguration<RmaReturnRefusal>
    {
        public RmaReturnRefusalMap()
        {
            this.ToTable("vw_crm_RmaReturnRefusals");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.RefusalId);
            this.HasRequired(i => i.Rma).WithMany(i => i.RmaReturnRefusals);
            this.HasRequired(i => i.ReturnReason).WithMany().HasForeignKey(i => i.ReasonId);
        }
    }
}