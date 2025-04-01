namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RefundCreditMap : NopEntityTypeConfiguration<RefundCredit>
    {
        public RefundCreditMap()
        {
            this.ToTable("vw_crm_RefundCredits");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.RefundId, m.CreditId });
        }
    }
}
