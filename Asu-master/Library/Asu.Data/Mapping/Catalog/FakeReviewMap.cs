using Asu.Core.Domain.Catalog;

namespace Asu.Data.Mapping.Catalog
{
    public partial class FakeReviewMap : NopEntityTypeConfiguration<FakeReview>
    {
        public FakeReviewMap()
        {
            this.ToTable("WCS_FakeReview");
            this.HasKey(pr => pr.Id);
            this.Property(pr => pr.Title).HasMaxLength(128);
            this.Property(pr => pr.ReviewText).IsRequired();
            this.Property(pr => pr.Rating).IsRequired();
            this.Property(pr => pr.CustomerName).HasMaxLength(32).IsRequired();
            this.Property(pr => pr.CreatedOnUtc).IsRequired();
        }
    }
}