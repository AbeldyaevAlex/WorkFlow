namespace Asu.Data.Mapping.Shipping
{
    using Asu.Core.Domain.Shipping;

    public class FreeShippingProductMap : NopEntityTypeConfiguration<FreeShippingProduct>
    {
        public FreeShippingProductMap()
        {
            this.ToTable("WCS_FreeShipping_Mapping");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.ProductId, m.StoreId });
        }
    }
}
