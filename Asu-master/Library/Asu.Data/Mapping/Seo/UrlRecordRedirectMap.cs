namespace Asu.Data.Mapping.Seo
{
    using Asu.Core.Domain.Seo;

    public class UrlRecordRedirectMap : NopEntityTypeConfiguration<UrlRecordRedirect>
    {
        public UrlRecordRedirectMap()
        {
            this.ToTable("WCS_UrlRecordRedirect");
            this.HasKey(m => m.Id);
        }
    }
}
