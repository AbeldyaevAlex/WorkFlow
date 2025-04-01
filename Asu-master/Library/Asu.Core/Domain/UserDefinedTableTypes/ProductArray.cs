namespace Asu.Core.Domain.UserDefinedTableTypes
{
    [UserDefinedTableType("ProductArrayTest")]
    public class ProductArray : UserDefinedTable
    {
        [UserDefinedTableTypeProperty("Id")]
        public int Id { get; set; }

        [UserDefinedTableTypeProperty("Mpn")]
        public string Mpn { get; set; }

        [UserDefinedTableTypeProperty("Price")]
        public decimal Price { get; set; }

        [UserDefinedTableTypeProperty("Cost")]
        public decimal? Cost { get; set; }

        [UserDefinedTableTypeProperty("Weight")]
        public decimal? Weight { get; set; }

        [UserDefinedTableTypeProperty("Width")]
        public decimal? Width { get; set; }

        [UserDefinedTableTypeProperty("Length")]
        public decimal? Length { get; set; }

        [UserDefinedTableTypeProperty("Height")]
        public decimal? Height { get; set; }

        [UserDefinedTableTypeProperty("Description")]
        public string Description { get; set; }

        [UserDefinedTableTypeProperty("Name")]
        public string Name { get; set; }

        [UserDefinedTableTypeProperty("ManufacturerId")]
        public int ManufacturerId { get; set; }

        [UserDefinedTableTypeProperty("Published")]
        public bool Published { get; set; }
    }
}
