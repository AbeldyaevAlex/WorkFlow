namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class SalesCancelCreditMap : NopEntityTypeConfiguration<SalesCancelCredit>
    {
        public SalesCancelCreditMap()
        {
            this.ToTable("vw_crm_SalesCancelCredits");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.CancelId, m.CreditId });

            this.HasRequired(m => m.Cancel).WithMany(m => m.CancelCredits).HasForeignKey(m => m.CancelId);
            this.HasRequired(m => m.Credit).WithMany(m => m.CancelCredits).HasForeignKey(m => m.CreditId);
        }
    }
}
