namespace Asu.Core.Domain.Customization
{
    public class ProductInventoryManageMethod : BaseEntity
    {
        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int ManageInventoryMethodId { get; set; }
    }
}
