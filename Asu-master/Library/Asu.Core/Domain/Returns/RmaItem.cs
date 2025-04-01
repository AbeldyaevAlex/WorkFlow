namespace Asu.Core.Domain.Returns
{
    using Asu.Core.Domain.Orders;

    public class RmaItem : BaseEntity
    {
        public int OrderLineId { get; set; }

        public int RmaId { get; set; }

        public int Quantity { get; set; }

        public virtual Rma Rma { get; set; }

        public virtual CrmSalesOrderLine OrderLine { get; set; }
    }
}
