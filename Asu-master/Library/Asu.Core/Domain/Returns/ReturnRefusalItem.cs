namespace Asu.Core.Domain.Returns
{
    using Asu.Core.Domain.Orders;

    public class ReturnRefusalItem : BaseEntity
    {
        public int OrderLineId { get; set; }

        public int RefusalId { get; set; }

        public int Quantity { get; set; }

        public virtual ReturnRefusal ReturnRefusal { get; set; }

        public virtual CrmSalesOrderLine OrderLine { get; set; }
    }
}