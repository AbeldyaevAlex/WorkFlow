namespace Asu.Data.Mapping.Orders
{
    using Asu.Core.Domain.Orders;

    public class CrmSalesOrderLineMap : NopEntityTypeConfiguration<CrmSalesOrderLine>
    {
        public CrmSalesOrderLineMap()
        {
            this.ToTable("vw_crm_SalesOrderLines");
            this.HasKey(m => m.Id);
            this.HasOptional(m => m.Product).WithMany().HasForeignKey(m => m.ProductId);
        }
    }
}
