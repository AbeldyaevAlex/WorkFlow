namespace Asu.Data.Mapping.ProductGroups
{
    using Asu.Core.Domain.Seo;

    public class ProductGroupUrlRecordMap : NopEntityTypeConfiguration<ProductGroupUrlRecord>
    {
        public ProductGroupUrlRecordMap()
        {
            this.ToTable("WCS_ProductGroupUrlRecord");
            this.HasKey(t => t.Id);
        }
    }
}
