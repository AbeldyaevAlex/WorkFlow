namespace Asu.Data.Mapping.ProductGroups
{
    using Asu.Core.Domain.ProductGroups;

    public class CoverkingProductDataMap : NopEntityTypeConfiguration<CoverkingProductData>
    {
        public CoverkingProductDataMap()
        {
            this.ToTable("vw_CoverkingPricing");
            this.Ignore(m => m.Id);
            this.HasKey(m => m.ItemId);

            this.Property(m => m.Upc);
            this.Property(m => m.Description);
            this.Property(m => m.Cost);
            this.Property(m => m.Height);
            this.Property(m => m.Length);
            this.Property(m => m.Weight);
            this.Property(m => m.Width);
            this.Property(m => m.ProductFamilyId);
            this.Property(m => m.ProductFamilyDescription);
        }
    }
}
