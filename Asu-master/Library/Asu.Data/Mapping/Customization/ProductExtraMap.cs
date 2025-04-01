using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class ProductExtraMap : NopEntityTypeConfiguration<ProductExtra>
    {
        public ProductExtraMap()
        {
            this.ToTable("WCS_ProductExtra");
            this.HasKey(pe => pe.Id);

            //this.Property(pe => pe.ManufacturerPartNumberClean).HasMaxLength(64).IsOptional();
            //this.Property(pe => pe.SkuClean).HasMaxLength(100).IsOptional();
            this.Ignore(pe => pe.ManufacturerPartNumberClean);
            this.Ignore(pe => pe.SkuClean);

            this.Property(pe => pe.RatingCount).IsRequired();
            this.Property(pe => pe.RatingScore).IsRequired();
            this.Property(pe => pe.IsShippingOverridePerItem).IsRequired();
            this.Property(pe => pe.ShippingOverride).IsRequired();
            this.Property(pe => pe.IsPriceHidden).IsRequired();
            this.Property(pe => pe.IsShippingFromManufacturer).IsRequired();
            this.Property(pe => pe.IsUniversal).IsRequired();
            this.Property(pe => pe.IsFreight).IsRequired();
            this.Property(pe => pe.PriceBelowUsQty).IsRequired();
            this.Property(pe => pe.ShippingType).IsRequired();
            this.Property(pe => pe.MinPrice).IsRequired(); 
            this.Property(pe => pe.MaxPrice).IsRequired();
            this.Property(pe => pe.IsGroup).IsRequired();

            this.HasRequired(pe => pe.Product).WithOptional(p => p.ProductExtra).Map(pe => pe.MapKey("ProductId"));
        }
    }
}
