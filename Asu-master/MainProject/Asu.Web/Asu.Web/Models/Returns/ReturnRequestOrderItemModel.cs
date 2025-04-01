namespace Asu.Web.Models.Returns
{
    public class OrderItemModel
    {
        public long? OrderItemId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int OrderLineId { get; set; }
    }
}