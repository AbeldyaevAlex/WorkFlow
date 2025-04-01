namespace Asu.Data.Mapping.Catalog
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Asu.Core.Domain.Catalog;

    public class ShopperApprovedReviewMap : NopEntityTypeConfiguration<ShopperApprovedReview>
    {
        public ShopperApprovedReviewMap()
        {
            this.ToTable("WCS_ShopperApprovedReviews");
            this.HasKey(r => r.Id).Property(r => r.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(r => r.CustomerName).IsRequired().IsUnicode(true).HasMaxLength(64);
            this.Property(r => r.DisplayDate).IsRequired();
            this.Property(r => r.OrderId).IsOptional();
            this.Property(r => r.Url).IsRequired().IsUnicode(true).HasMaxLength(2083);
            this.Property(r => r.Comments).IsRequired().IsUnicode(true).IsMaxLength();
            this.Property(r => r.Overall).IsRequired().HasPrecision(18, 1);
            this.Property(r => r.CreatedOnUtc).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}
