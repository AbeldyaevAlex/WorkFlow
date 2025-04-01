namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class SalesCancelFeeChargeMap : NopEntityTypeConfiguration<SalesCancelFeeCharge>
    {
        public SalesCancelFeeChargeMap()
        {
            this.ToTable("vw_crm_SalesCancelFeeCharges");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.FeeId, m.TypeId });

            this.HasRequired(m => m.Type).WithMany().HasForeignKey(m => m.TypeId);
            this.HasRequired(m => m.Fee).WithMany(m => m.FeeCharges).HasForeignKey(m => m.FeeId);
        }
    }
}
