using Asu.Core.Domain.Seo;

namespace Asu.Data.Mapping.Seo
{
    public partial class UrlRecordMap : NopEntityTypeConfiguration<UrlRecord>
    {
        public UrlRecordMap()
        {
            this.ToTable("UrlRecord");
            this.HasKey(lp => lp.Id);

            this.Property(lp => lp.EntityName).IsRequired().HasMaxLength(400);
            this.Property(lp => lp.Slug).IsRequired().HasMaxLength(400);

            this.HasOptional(x => x.UrlRecordExtra).WithRequired();
        }
    }
}