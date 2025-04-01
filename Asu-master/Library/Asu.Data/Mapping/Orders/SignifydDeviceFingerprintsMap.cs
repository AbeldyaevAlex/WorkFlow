using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public class SignifydDeviceFingerprintsMap : NopEntityTypeConfiguration<SignifydDeviceFingerprints>
    {
        public SignifydDeviceFingerprintsMap()
        {
            this.ToTable("SignifydDeviceFingerprints");
            this.HasKey(o => o.Id);
            this.Property(o => o.OrderId).IsRequired();
            this.Property(o => o.SessionId).IsRequired();

            this.HasRequired(on => on.Order)
                .WithMany(o => o.SignifydDeviceFingerprints)
                .HasForeignKey(on => on.OrderId).WillCascadeOnDelete(true);
        }
    }
}