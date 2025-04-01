namespace Asu.Core.Domain.Returns
{
    using Asu.Core.Domain.Orders;

    public class ReturnItem : BaseEntity
    {
        public int OrderLineId { get; set; }

        public int ReturnId { get; set; }

        public int Quantity { get; set; }

        public int ReasonId { get; set; }
        
        public string ImagePath { get; set; }

        public string Comment { get; set; }

        public virtual Return Return { get; set; }

        public virtual ReturnReason ReturnReason { get; set; }

        public virtual CrmSalesOrderLine OrderLine { get; set; }
    }
}