using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class QueuedMessagesSendTaskLockerMap : NopEntityTypeConfiguration<QueuedMessagesSendTaskLocker>
    {
        public QueuedMessagesSendTaskLockerMap()
        {
            this.ToTable("WCS_QueuedMessagesSendTaskLocker");
            this.HasKey(apat => apat.Id);

            this.Property(apat => apat.IsBusy).IsRequired();
            this.Property(apat => apat.UpdatedOn).IsRequired();
        }
    }
}
