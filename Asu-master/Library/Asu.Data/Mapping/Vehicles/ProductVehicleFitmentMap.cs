using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class ProductVehicleFitmentMap : NopEntityTypeConfiguration<ProductVehicleFitment>
    {
        public ProductVehicleFitmentMap()
        {
            this.ToTable("vw_ProductVehicleFitment");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.ProductId, i.VehicleId });
        }
    }
}