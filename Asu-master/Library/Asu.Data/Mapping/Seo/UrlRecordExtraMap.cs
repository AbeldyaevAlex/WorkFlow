namespace Asu.Data.Mapping.Seo
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Asu.Core.Domain.Seo;

    public class UrlRecordExtraMap : NopEntityTypeConfiguration<UrlRecordExtra>
    {
        public UrlRecordExtraMap()
        {
            this.ToTable("WCS_UrlRecordExtra");
            this.HasKey(lp => lp.Id);
            this.Property(lp => lp.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
