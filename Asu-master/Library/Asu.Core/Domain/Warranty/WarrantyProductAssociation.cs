namespace Asu.Core.Domain.Warranty
{
    using System;

    public class WarrantyProductAssociation : BaseEntity
    {
        public int WarrantyProductId { get; set; }

        public int ProductId { get; set; }

        public int OrderItemId { get; set; }

        public int WarrantyOrderItemId { get; set; }

        public int? SalesOrderLineId { get; set; }

        public int? SalesOrderWarrantyLineId { get; set; }

        public int OrderId { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
