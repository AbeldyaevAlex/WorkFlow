using Asu.Core.Domain.Forums;

namespace Asu.Data.Mapping.Forums
{
    public partial class ForumGroupMap : NopEntityTypeConfiguration<ForumGroup>
    {
        public ForumGroupMap()
        {
            this.ToTable("Forums_Group");
            this.HasKey(fg => fg.Id);
            this.Property(fg => fg.Name).IsRequired().HasMaxLength(200);
        }
    }
}
