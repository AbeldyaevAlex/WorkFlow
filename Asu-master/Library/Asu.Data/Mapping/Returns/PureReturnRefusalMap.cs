namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class PureReturnRefusalMap : NopEntityTypeConfiguration<PureReturnRefusal>
    {
        public PureReturnRefusalMap()
        {
            this.ToTable("vw_crm_PureReturnRefusals");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.RefusalId);
            this.HasRequired(i => i.ReturnReason).WithMany().HasForeignKey(i => i.ReasonId);
        }
    }
}