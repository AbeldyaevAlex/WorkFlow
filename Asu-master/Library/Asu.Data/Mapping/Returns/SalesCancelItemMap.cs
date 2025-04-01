namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class SalesCancelItemMap : NopEntityTypeConfiguration<SalesCancelItem>
    {
        public SalesCancelItemMap()
        {
            this.ToTable("vw_crm_SalesCancelItems");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.OrderLineId, i.CancelId });
            this.HasRequired(p => p.Cancel).WithMany(i => i.Items);
            this.HasRequired(p => p.OrderLine).WithMany().HasForeignKey(i => i.OrderLineId);
        }
    }
}