namespace Asu.Core.Domain.Returns
{
    using Asu.Core.Domain.Orders;

    public class SalesCancelItem : BaseEntity
    {
        public int OrderLineId { get; set; }

        public int CancelId { get; set; }

        public int Quantity { get; set; }

        public virtual SalesCancel Cancel { get; set; }

        public virtual CrmSalesOrderLine OrderLine { get; set; }
    }
}