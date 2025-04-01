using Asu.Core.Domain.Media;

namespace Asu.Data.Mapping.Media
{
    public partial class DownloadMap : NopEntityTypeConfiguration<Download>
    {
        public DownloadMap()
        {
            this.ToTable("Download");
            this.HasKey(p => p.Id);
            this.Property(p => p.DownloadBinary).IsMaxLength();
        }
    }
}