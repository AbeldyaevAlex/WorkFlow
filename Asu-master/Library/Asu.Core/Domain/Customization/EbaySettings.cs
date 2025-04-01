using Asu.Core.Configuration;

namespace Asu.Core.Domain.Customization
{
    public class EbaySettings : ISettings
    {
        public string MarketplaceAccountDeletionNotificationToken { get; set; }

        public string MarketplaceAccountDeletionNotificationEndpoint { get; set; }
    }
}
