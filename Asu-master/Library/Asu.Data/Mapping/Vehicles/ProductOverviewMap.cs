namespace Asu.Data.Mapping.Vehicles
{
    using Asu.Core.Domain.Vehicles;

    public class ProductOverviewMap : NopEntityTypeConfiguration<ProductOverview>
    {
        public ProductOverviewMap()
        {
            this.ToTable("vw_ProductOverview");
            this.HasKey(pv => pv.Id);

            /*this.HasRequired(pv => pv.Product)
                .WithMany(p => p.ProductVehicles)
                .HasForeignKey(pv => pv.ProductId);

            this.HasRequired(pv => pv.Vehicle)
                .WithMany()
                .HasForeignKey(pv => pv.VehicleId);*/
        }
    }
}
