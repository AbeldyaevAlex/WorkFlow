namespace Asu.Data.Mapping.Common
{
    using Asu.Core.Domain.Common;

    public class CrmCountryMap : NopEntityTypeConfiguration<CrmCountry>
    {
        public CrmCountryMap()
        {
            this.ToTable("vw_crm_Countries");
            this.HasKey(m => m.Id);
        }
    }
}
