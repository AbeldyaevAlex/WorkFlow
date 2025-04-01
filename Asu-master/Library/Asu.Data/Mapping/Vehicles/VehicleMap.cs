using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class VehicleMap : NopEntityTypeConfiguration<Vehicle>
    {
        public VehicleMap()
        {
            this.ToTable("WCS_Vehicle");
            this.HasKey(t => t.Id);
            this.Ignore(t => t.ShowUniversal);
        }
    }
}
