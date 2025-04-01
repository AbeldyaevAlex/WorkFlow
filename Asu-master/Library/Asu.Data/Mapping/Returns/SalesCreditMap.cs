namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class SalesCreditMap : NopEntityTypeConfiguration<SalesCredit>
    {
        public SalesCreditMap()
        {
            this.ToTable("vw_crm_SalesCredits");
            this.HasKey(m => m.Id);

            this.HasRequired(m => m.Order).WithMany(m => m.SalesCredits).HasForeignKey(m => m.OrderId);
            this.HasRequired(m => m.Type).WithMany().HasForeignKey(m => m.TypeId);
            this.HasMany(m => m.RefundCredits).WithRequired(m => m.Credit).HasForeignKey(m => m.CreditId);
        }
    }
}
