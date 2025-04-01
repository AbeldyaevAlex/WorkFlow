using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public partial class CustomerVehicleGarageMap : NopEntityTypeConfiguration<CustomerVehicleGarage>
    {
        public CustomerVehicleGarageMap()
        {
            this.ToTable("WCS_CustomerVehicleGarage");
            this.HasKey(o => o.Id);
        }
    }
}