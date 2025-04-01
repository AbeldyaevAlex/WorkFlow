using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public class GoogleImageMap : NopEntityTypeConfiguration<GoogleImage>
    {
        public GoogleImageMap()
        {
            this.ToTable("WC_GoogleImageDefaults");
            this.HasKey(ai => ai.ProductId);

            this.Property(ai => ai.PicturePath).HasMaxLength(128).IsRequired();
        }
    }
}