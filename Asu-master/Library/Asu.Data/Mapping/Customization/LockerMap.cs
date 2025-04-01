using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public class LockerMap : NopEntityTypeConfiguration<Locker>
    {
        public LockerMap()
        {
            this.ToTable("WCS_Locker");
            this.HasKey(or => or.Id);

            this.Property(l => l.Name).IsRequired();
            this.Property(l => l.IsLocked).IsRequired();
            this.Property(l => l.UpdatedOnUtc).IsRequired();
        }
    }
}