using Asu.Core.Domain.Discounts;

namespace Asu.Data.Mapping.Discounts
{
    public partial class CustomDiscountManufacturerMap : NopEntityTypeConfiguration<CustomDiscountManufacturer>
    {
        public CustomDiscountManufacturerMap()
        {
            this.ToTable("CustomDiscountManufacturer");
            this.HasKey(d => d.Id);
            this.Property(d => d.DiscountId);
            this.Property(d => d.ManufacturerId);
            this.Property(d => d.ManufacturerTypeId);
            this.Property(d => d.CreatedOnUtc);
            this.Property(d => d.UpdatedOnUtc);

            this.Ignore(d => d.ManufacturerType);
        }
    }
}
