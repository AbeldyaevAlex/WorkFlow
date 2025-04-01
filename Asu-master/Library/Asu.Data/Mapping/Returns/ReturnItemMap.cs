namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnItemMap : NopEntityTypeConfiguration<ReturnItem>
    {
        public ReturnItemMap()
        {
            this.ToTable("vw_crm_ReturnItems");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.OrderLineId, i.ReturnId });
            this.HasRequired(p => p.Return).WithMany(i => i.ReturnItems);
            this.HasRequired(i => i.ReturnReason).WithMany().HasForeignKey(i => i.ReasonId);
        }
    }
}