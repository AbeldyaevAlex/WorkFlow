namespace Asu.Data.Mapping.ProductGroups
{
    using Asu.Core.Domain.ProductGroups;

    public partial class BrandCategoryMap : NopEntityTypeConfiguration<BrandCategory>
    {
        public BrandCategoryMap()
        {
            this.ToTable("WCS_BrandCategory");
            this.HasKey(i => i.Id);
            this.Property(i => i.Name).IsRequired();

            this.HasRequired(i => i.Manufacturer).WithMany(i => i.BrandCategories);
            this.HasRequired(p => p.Picture).WithMany().HasForeignKey(p => p.DigitalDataId);
        }
    }
}