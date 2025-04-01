using Asu.Core.Domain.Catalog;

namespace Asu.Data.Mapping.Catalog
{
    public partial class ProductGroupClubPriceMap : NopEntityTypeConfiguration<ProductGroupClubPrice>
    {
        public ProductGroupClubPriceMap()
        {
            this.ToTable("WCS_ProductGroupClubPrice");
            this.HasKey(tp => tp.Id);
            this.Property(tp => tp.ClubMemberMinPrice).HasPrecision(18, 4);
            this.Property(tp => tp.ClubMemberMaxPrice).HasPrecision(18, 4);

            this.HasRequired(tp => tp.Product)
                .WithMany(p => p.ProductGroupClubPrices)
                .HasForeignKey(tp => tp.ProductId);

            this.HasRequired(tp => tp.Store)
                .WithMany()
                .HasForeignKey(tp => tp.StoreId);
        }
    }
}