using Asu.Core.Domain.Catalog;

namespace Asu.Data.Mapping.Catalog
{
    public partial class SpecificationAttributeMap : NopEntityTypeConfiguration<SpecificationAttribute>
    {
        public SpecificationAttributeMap()
        {
            this.ToTable("SpecificationAttribute");
            this.HasKey(sa => sa.Id);
            this.Property(sa => sa.Name).IsRequired();

            this.HasMany(sa => sa.Descriptors)
                .WithRequired(sad => sad.Attribute)
                .HasForeignKey(sa => new { sa.AttributeId });
        }
    }
}