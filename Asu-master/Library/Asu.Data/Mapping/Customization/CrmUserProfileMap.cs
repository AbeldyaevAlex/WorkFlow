namespace Asu.Data.Mapping.Customization
{
    using Asu.Core.Domain.Customization;

    public class CrmUserProfileMap : NopEntityTypeConfiguration<CrmUserProfile>
    {
        public CrmUserProfileMap()
        {
            this.ToTable("vw_crm_UserProfiles");
            this.HasKey(m => m.Id);
        }
    }
}
