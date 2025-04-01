namespace Asu.Core.Domain.UserDefinedTableTypes
{
    [UserDefinedTableType("ProductCostArray")]
    public class ProductCostArray : UserDefinedTable
    {
        [UserDefinedTableTypeProperty("ProductId")]
        public int ProductId { get; set; }

        [UserDefinedTableTypeProperty("Cost")]
        public decimal Cost { get; set; }

        [UserDefinedTableTypeProperty("VendorId")]
        public int VendorId { get; set; }
    }
}
