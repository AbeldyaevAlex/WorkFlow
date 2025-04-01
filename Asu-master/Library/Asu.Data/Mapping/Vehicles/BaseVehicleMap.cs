using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class BaseVehicleMap : NopEntityTypeConfiguration<BaseVehicle>
    {
        public BaseVehicleMap()
        {
            this.ToTable("WCS_BaseVehicle");
            this.HasKey(t => t.Id);
        }
    }
}
