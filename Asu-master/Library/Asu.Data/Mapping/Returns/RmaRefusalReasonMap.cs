namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RmaRefusalReasonMap : NopEntityTypeConfiguration<RmaRefusalReason>
    {
        public RmaRefusalReasonMap()
        {
            this.ToTable("vw_crm_RmaRefusalReasons");
            this.HasKey(i => i.Id);
        }
    }
}