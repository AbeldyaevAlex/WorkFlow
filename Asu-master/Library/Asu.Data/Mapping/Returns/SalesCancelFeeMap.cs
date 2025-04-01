namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class SalesCancelFeeMap : NopEntityTypeConfiguration<SalesCancelFee>
    {
        public SalesCancelFeeMap()
        {
            this.ToTable("vw_crm_SalesCancelFees");
            this.HasKey(m => m.Id);
            this.HasRequired(m => m.Cancel).WithMany(m => m.CancelFees).HasForeignKey(m => m.CancelId);
        }
    }
}
