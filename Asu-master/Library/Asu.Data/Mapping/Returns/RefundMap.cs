namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class RefundMap : NopEntityTypeConfiguration<Refund>
    {
        public RefundMap()
        {
            this.ToTable("vw_crm_Refunds");
            this.HasKey(m => m.Id);

            this.HasRequired(m => m.Order).WithMany(m => m.Refunds).HasForeignKey(m => m.OrderId);
            this.HasRequired(m => m.Reason).WithMany().HasForeignKey(m => m.ReasonId);
            this.HasMany(m => m.Credits).WithRequired(m => m.Refund).HasForeignKey(m => m.RefundId);
        }      
    }
}
