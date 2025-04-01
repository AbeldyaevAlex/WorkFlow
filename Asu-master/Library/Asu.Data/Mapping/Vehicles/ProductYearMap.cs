using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class ProductYearMap : NopEntityTypeConfiguration<ProductYear>
    {
        public ProductYearMap()
        {
            this.ToTable("vw_ProductYears");
            this.Ignore(i => i.Id);
            this.HasKey(i => new { i.ProductId, i.YearId });
            this.HasRequired(i => i.Year).WithMany().HasForeignKey(i => i.YearId);
        }
    }
}