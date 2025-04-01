using Asu.Core.Domain.Messages;

namespace Asu.Data.Mapping.Messages
{
    public class BackorderNotificationMap : NopEntityTypeConfiguration<BackorderNotification>
    {
        public BackorderNotificationMap()
        {
            this.ToTable("WCS_BackorderNotifications");
            this.HasKey(m => m.Id);
        }
    }
}
