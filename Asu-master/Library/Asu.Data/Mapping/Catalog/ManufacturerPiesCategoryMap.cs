namespace Asu.Data.Mapping.Catalog
{
    using Asu.Core.Domain.Catalog;

    public class ManufacturerPiesCategoryMap : NopEntityTypeConfiguration<ManufacturerPiesCategory>
    {
        public ManufacturerPiesCategoryMap()
        {
            this.ToTable("WC_Manufacturer_PiesCategories");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.ManufacturerId, m.CategoryId });
        }
    }
}
