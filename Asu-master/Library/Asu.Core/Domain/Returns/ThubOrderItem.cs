namespace Asu.Core.Domain.Returns
{
    using Catalog;

    public class ThubOrderItem : BaseEntity
    {
        public long OrderItemId { get; set; }

        public long OrderId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public bool IsPurchaseOrderExist { get; set; }

        public virtual ThubOrder Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
