namespace Asu.Core.Domain.Orders
{
    using Asu.Core.Domain.Catalog;
    using Asu.Core.Domain.Returns;

    public class CrmSalesOrderLine : BaseEntity
    {
        public int OrderId { get; set; }

        public long? ThubOrderItemId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsWarranty { get; set; }

        public int? WarrantyLineId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public virtual CrmSalesOrder Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
