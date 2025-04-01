using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class ProductModelMap : NopEntityTypeConfiguration<ProductModel>
    {
        public ProductModelMap()
        {
            this.ToTable("vw_ProductModels");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.ProductId, i.YearId, i.MakeId, i.ModelId });
            this.HasRequired(i => i.Model).WithMany().HasForeignKey(i => i.ModelId);
        }
    }
}