namespace Asu.Core.Domain.Returns
{
    using Asu.Core.Domain.Orders;

    public class ReturnRequestItem : BaseEntity
    {
        public long? OrderItemId { get; set; }

        public int ReturnId { get; set; }

        public int Quantity { get; set; }

        public int ReasonId { get; set; }

        public string ImagePath { get; set; }

        public string Comment { get; set; }

        public int LineId { get; set; }

        public virtual ReturnRequest ReturnRequest { get; set; }

        public virtual ThubOrderItem OrderItem { get; set; }

        public virtual CrmSalesOrderLine OrderLine { get; set; }

        public virtual ReturnReason ReturnReason { get; set; }
    }
}