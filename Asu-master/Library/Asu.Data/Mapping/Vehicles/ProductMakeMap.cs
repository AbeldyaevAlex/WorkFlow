using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class ProductMakeMap : NopEntityTypeConfiguration<ProductMake>
    {
        public ProductMakeMap()
        {
            this.ToTable("vw_ProductMakes");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.ProductId, i.YearId, i.MakeId });
            this.HasRequired(i => i.Make).WithMany().HasForeignKey(i => i.MakeId);
        }
    }
}