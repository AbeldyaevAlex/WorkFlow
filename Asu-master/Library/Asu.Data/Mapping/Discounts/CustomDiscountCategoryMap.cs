using Asu.Core.Domain.Discounts;

namespace Asu.Data.Mapping.Discounts
{
    public partial class CustomDiscountCategoryMap : NopEntityTypeConfiguration<CustomDiscountCategory>
    {
        public CustomDiscountCategoryMap()
        {
            this.ToTable("CustomDiscountCategory");
            this.HasKey(d => d.Id);
            this.Property(d => d.DiscountId);
            this.Property(d => d.CategoryId);
            this.Property(d => d.CategoryTypeId);
            this.Property(d => d.CreatedOnUtc);
            this.Property(d => d.UpdatedOnUtc);

            this.Ignore(d => d.CategoryType);
        }
    }
}
