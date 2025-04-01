namespace Asu.Data.Mapping.Customization
{
    using Asu.Core.Domain.Customization;

    public class ProductInventoryManageMethodMap : NopEntityTypeConfiguration<ProductInventoryManageMethod>
    {
        public ProductInventoryManageMethodMap()
        {
            this.ToTable("WCS_ManageInventory_Product_Mapping");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.ProductId, m.StoreId });
        }
    }
}
