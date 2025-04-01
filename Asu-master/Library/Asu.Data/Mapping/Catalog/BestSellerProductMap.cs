using Asu.Core.Domain.Catalog;

namespace Asu.Data.Mapping.Catalog
{
    public class BestSellerProductMap : NopEntityTypeConfiguration<BestSellerProduct>
    {
        public BestSellerProductMap()
        {
            this.ToTable("View_SalesbyProduct");
            this.Ignore(m => m.Id);
            this.HasKey(m => m.ProductId);
        }
    }
}
