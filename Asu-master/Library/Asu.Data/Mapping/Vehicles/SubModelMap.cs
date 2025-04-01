using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class SubModelMap : NopEntityTypeConfiguration<SubModel>
    {
        public SubModelMap()
        {
            this.ToTable("WCS_SubModel");
            this.HasKey(t => t.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(128);
        }
    }
}
