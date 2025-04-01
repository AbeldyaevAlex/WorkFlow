namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class SalesCreditChargeMap : NopEntityTypeConfiguration<SalesCreditCharge>
    {
        public SalesCreditChargeMap()
        {
            this.ToTable("vw_crm_SalesCreditCharges");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.CreditId, m.TypeId });

            this.HasRequired(m => m.Credit).WithMany(m => m.Charges).HasForeignKey(m => m.CreditId);
            this.HasRequired(m => m.Type).WithMany().HasForeignKey(m => m.TypeId);
        }
    }
}
