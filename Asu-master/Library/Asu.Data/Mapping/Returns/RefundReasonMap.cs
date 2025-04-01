namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RefundReasonMap : NopEntityTypeConfiguration<RefundReason>
    {
        public RefundReasonMap()
        {
            this.ToTable("vw_crm_RefundReasons");
            this.HasKey(m => m.Id);
        }
    }
}
