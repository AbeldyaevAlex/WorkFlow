using System;

namespace Asu.Core.Domain.Customization
{
    public class EbayMarketplaceAccountDeletionNotification : BaseEntity
    {
        public DateTime CreatedOn { get; set; }

        public string Request { get; set; }
    }
}
