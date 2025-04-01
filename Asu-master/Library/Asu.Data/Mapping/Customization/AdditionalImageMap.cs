using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class AdditionalImageMap : NopEntityTypeConfiguration<AdditionalImage>
    {
        public AdditionalImageMap()
        {
            this.ToTable("WCS_ImageLoader");
            this.HasKey(ai => ai.ProductId);

            this.Property(ai => ai.PictureName).HasMaxLength(256).IsRequired();
        }
    }
}
