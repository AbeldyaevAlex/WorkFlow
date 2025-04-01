namespace Asu.Data.Mapping.UserDefinedTableTypes
{
    using Asu.Core.Domain.UserDefinedTableTypes;

    public class ProductIdArrayMap : NopEntityTypeConfiguration<ProductIdArray>
    {
        public ProductIdArrayMap()
        {
            this.HasKey(t => t.Id);
        }
    }
}
