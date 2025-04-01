namespace Asu.Data.Mapping.Vehicles
{
    using Asu.Core.Domain.Vehicles;

    public class ProductGroupVehicleFitmentMap : NopEntityTypeConfiguration<ProductGroupVehicleFitment>
    {
        public ProductGroupVehicleFitmentMap()
        {
            this.ToTable("vw_ProductGroupVehicleFitments");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.GroupId, m.VehicleId });
        }
    }
}
