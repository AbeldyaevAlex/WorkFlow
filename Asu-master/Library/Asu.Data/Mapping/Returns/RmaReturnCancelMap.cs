namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RmaReturnCancelMap : NopEntityTypeConfiguration<RmaReturnCancel>
    {
        public RmaReturnCancelMap()
        {
            this.ToTable("vw_crm_RmaReturnCancels");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.CancelId);
            this.HasRequired(i => i.Rma).WithMany(i => i.RmaReturnCancels);
        }
    }
}