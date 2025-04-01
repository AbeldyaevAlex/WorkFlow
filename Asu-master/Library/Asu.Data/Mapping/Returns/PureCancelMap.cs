namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class PureCancelMap : NopEntityTypeConfiguration<PureCancel>
    {
        public PureCancelMap()
        {
            this.ToTable("vw_crm_PureCancels");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.CancelId);
            this.HasRequired(i => i.ReturnReason).WithMany().HasForeignKey(i => i.ReasonId);
        }
    }
}