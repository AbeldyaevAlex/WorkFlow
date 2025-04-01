namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class SalesCancelMap : NopEntityTypeConfiguration<SalesCancel>
    {
        public SalesCancelMap()
        {
            this.ToTable("vw_crm_SalesCancels");
            this.HasKey(i => i.Id);
            this.HasRequired(i => i.CrmOrder).WithMany(i => i.SalesCancels).HasForeignKey(i => i.OrderId);

            this.HasOptional(p => p.PureCancel).WithRequired(d => d.Cancel);
            this.HasOptional(p => p.ReturnCancel).WithRequired(d => d.Cancel);
            this.HasOptional(p => p.RmaReturnCancel).WithRequired(d => d.Cancel);
        }
    }
}