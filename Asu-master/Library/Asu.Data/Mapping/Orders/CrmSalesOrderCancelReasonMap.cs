using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public partial class CrmSalesOrderCancelReasonMap : NopEntityTypeConfiguration<CrmSalesOrderCancelReason>
    {
        public CrmSalesOrderCancelReasonMap()
        {
            this.ToTable("vw_crm_SalesOrderCancelReason");
            this.HasKey(o => o.Id);
        }
    }
}