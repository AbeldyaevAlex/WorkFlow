using Asu.Core.Domain.BannerPicture;

namespace Asu.Data.Mapping.BannerPicture
{
    public partial class BannerMap : NopEntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {
            this.ToTable("WCS_Banners");
            this.HasKey(m => m.Id);
        }
    }
}
