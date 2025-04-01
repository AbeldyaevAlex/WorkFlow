namespace Asu.Data.Mapping.Shipping
{
    using Asu.Core.Domain.Shipping;

    public class CrmTrackingStatusMap : NopEntityTypeConfiguration<CrmTrackingStatus>
    {
        public CrmTrackingStatusMap()
        {
            this.ToTable("vw_crm_TrackingStatuses");
            this.HasKey(m => m.Id);
        }
    }
}
