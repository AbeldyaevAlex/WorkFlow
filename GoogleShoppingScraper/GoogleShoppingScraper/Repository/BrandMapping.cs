namespace GoogleShoppingScraper.Repository
{
    using System.Data.Linq.Mapping;

    [Table(Name = "BrandMapping")]
    public class BrandMapping
    {
        [Column(Name = "APBrand")]
        public string Name { get; set; }

        [Column(Name = "GoogleBrand")]
        public string GoogleName { get; set; }
    }
}