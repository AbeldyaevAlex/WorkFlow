using Asu.Core.Domain.Messages;

namespace Asu.Data.Mapping.Messages
{
    public partial class QueuedEmailSendGridMap : NopEntityTypeConfiguration<QueuedEmailSendGrid>
    {
        public QueuedEmailSendGridMap()
        {
            this.ToTable("WCS_QueuedEmails");
            this.HasKey(m => m.Id);
        }
    }
}