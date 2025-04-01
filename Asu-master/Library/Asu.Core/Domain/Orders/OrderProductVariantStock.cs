namespace Asu.Core.Domain.Orders
{
    using System;

    public class OrderProductVariantStock : BaseEntity  
    {
        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public int TotalQty { get; set; }

        public int? AvailableVendors { get; set; }

        public string AlwaysInStock { get; set; }

        public int DropShip { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? TotalQtyAfterOrderPlaced { get; set; }

        public decimal? Cost { get; set; }

        public decimal? InStockLowestCost { get; set; }
    }
}
