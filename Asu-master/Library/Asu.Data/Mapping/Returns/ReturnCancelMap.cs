namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnCancelMap : NopEntityTypeConfiguration<ReturnCancel>
    {
        public ReturnCancelMap()
        {
            this.ToTable("vw_crm_ReturnCancels");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.CancelId);
            this.HasRequired(i => i.Return).WithMany(i => i.ReturnCancels);
        }
    }
}