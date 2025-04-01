using Asu.Core.Domain.StatusDirectory;

namespace Asu.Data.Mapping.StatusDirectory
{
    public partial class StatusDirectoryMap : NopEntityTypeConfiguration<DocumentStatus>
    {
        public StatusDirectoryMap()
        {
            this.ToTable("DocumentStatus");
            this.HasKey(a => a.Id);
        }
    }
}
