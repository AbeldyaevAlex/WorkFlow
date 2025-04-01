using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class MakeMap : NopEntityTypeConfiguration<Make>
    {
        public MakeMap()
        {
            this.ToTable("WCS_Make");
            this.HasKey(t => t.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(64);
            this.Property(p => p.IsActiveForFilter).IsRequired();
            this.Property(p => p.IsActiveForSeo).IsRequired();
        }
    }
}
