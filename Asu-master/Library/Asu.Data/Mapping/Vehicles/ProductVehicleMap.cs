namespace Asu.Data.Mapping.Vehicles
{
    using Asu.Core.Domain.Vehicles;

    public class ProductVehicleMap : NopEntityTypeConfiguration<ProductVehicle>
    {
        public ProductVehicleMap()
        {
            this.ToTable("WCS_Product_Vehicle_Mapping");
            this.HasKey(pv => pv.Id);

            this.HasRequired(pv => pv.Product)
                .WithMany(p => p.ProductVehicles)
                .HasForeignKey(pv => pv.ProductId);

            this.HasRequired(pv => pv.Vehicle)
                .WithMany()
                .HasForeignKey(pv => pv.VehicleId);
        }
    }
}
