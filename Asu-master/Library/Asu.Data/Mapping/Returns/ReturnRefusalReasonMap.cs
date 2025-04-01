namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnRefusalReasonMap : NopEntityTypeConfiguration<ReturnRefusalReason>
    {
        public ReturnRefusalReasonMap()
        {
            this.ToTable("vw_crm_ReturnRefusalReasons");
            this.HasKey(i => i.Id);
        }
    }
}