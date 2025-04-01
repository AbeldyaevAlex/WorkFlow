using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class VendorProductCostMap : NopEntityTypeConfiguration<VendorProductCost>
    {
        public VendorProductCostMap()
        {
            this.ToTable("vw_ProductCostByVendor");
            this.HasKey(vpc => vpc.VendorProductCostId);

            this.Property(vpc => vpc.VendorProductCostId).HasColumnName("Id");
            this.Property(vpc => vpc.VendorName).HasMaxLength(400).HasColumnName("Vendor").IsOptional();
            this.Property(vpc => vpc.Cost).IsOptional();
            this.Property(vpc => vpc.AvailableQty).HasColumnName("Quantity").IsOptional();

            this.HasRequired(vpc => vpc.Product).WithMany(p => p.VendorQtyPrices).Map(vpc => vpc.MapKey("ProductId"));

            this.Ignore(vpc => vpc.Id);
        }
    }
}
