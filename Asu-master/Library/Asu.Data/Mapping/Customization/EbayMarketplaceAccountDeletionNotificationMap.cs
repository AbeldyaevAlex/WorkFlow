using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public class EbayMarketplaceAccountDeletionNotificationMap : NopEntityTypeConfiguration<EbayMarketplaceAccountDeletionNotification>
    {
        public EbayMarketplaceAccountDeletionNotificationMap()
        {
            this.ToTable("WCS_EbayMarketplaceAccountDeletionNotifications");
            this.HasKey(m => m.Id);
        }
    }
}
