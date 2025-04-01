namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnRequestItemMap : NopEntityTypeConfiguration<ReturnRequestItem>
    {
        public ReturnRequestItemMap()
        {
            this.ToTable("WCS_ReturnRequestItem");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.LineId, i.ReturnId });

            this.HasRequired(i => i.ReturnRequest).WithMany(r => r.Items).HasForeignKey(i => i.ReturnId);
            this.HasRequired(i => i.OrderItem).WithMany().HasForeignKey(i => i.OrderItemId);
            this.HasRequired(i => i.ReturnReason).WithMany().HasForeignKey(i => i.ReasonId);
            this.HasRequired(i => i.OrderLine).WithMany().HasForeignKey(i => i.LineId);
        }
    }
}