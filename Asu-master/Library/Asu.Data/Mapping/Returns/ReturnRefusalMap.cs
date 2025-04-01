namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnRefusalMap : NopEntityTypeConfiguration<ReturnRefusal>
    {
        public ReturnRefusalMap()
        {
            this.ToTable("vw_crm_ReturnRefusals");
            this.HasKey(i => i.Id);
            this.HasRequired(i => i.Return).WithMany(i => i.ReturnRefusals).HasForeignKey(i => i.ReturnId);

            this.HasOptional(i => i.PureReturnRefusal).WithRequired(i => i.ReturnRefusal);
            this.HasOptional(i => i.RmaReturnRefusal).WithRequired(i => i.ReturnRefusal);
        }
    }
}