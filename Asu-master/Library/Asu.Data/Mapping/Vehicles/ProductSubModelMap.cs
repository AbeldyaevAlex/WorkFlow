using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class ProductSubModelMap : NopEntityTypeConfiguration<ProductSubModel>
    {
        public ProductSubModelMap()
        {
            this.ToTable("vw_ProductSubModels");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.ProductId, i.YearId, i.MakeId, i.ModelId, i.SubModelId });
            this.HasRequired(i => i.SubModel).WithMany().HasForeignKey(i => i.SubModelId);
        }
    }
}