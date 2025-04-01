namespace Asu.Data.Mapping.Catalog
{
    using Asu.Core.Domain.Catalog;

    public class ProductSpecificationAttributeDescriptorMap : NopEntityTypeConfiguration<SpecificationAttributeDescriptor>
    {
        public ProductSpecificationAttributeDescriptorMap()
        {
            this.ToTable("WCS_SpecificationAttributeDescriptors");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.AttributeId, m.DescriptorAttributeId });
        }
    }
}
