namespace Asu.Data.Mapping.ProductGroups
{
    using Asu.Core.Domain.ProductGroups;

    public partial class ProductGroupMap : NopEntityTypeConfiguration<ProductGroup>
    {
        public ProductGroupMap()
        {
            this.ToTable("WCS_ProductGroup");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Active).IsRequired();

            this.HasRequired(p => p.Manufacturer).WithMany(i => i.ProductGroups);
            this.HasRequired(p => p.Category).WithMany(i => i.ProductGroups);
            this.HasRequired(p => p.Template).WithMany().HasForeignKey(p => p.TemplateId);
        }
    }
}