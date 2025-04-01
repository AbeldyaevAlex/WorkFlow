using Asu.Core.Domain.Common;

namespace Asu.Data.Mapping.Common
{
    public class CrmStateMap : NopEntityTypeConfiguration<CrmState>
    {
        public CrmStateMap()
        {
            this.ToTable("vw_crm_States");
            this.HasKey(m => m.Id);
            this.HasRequired(m => m.Country)
                .WithMany()
                .HasForeignKey(m => m.CountryId);          
        }
    }
}
