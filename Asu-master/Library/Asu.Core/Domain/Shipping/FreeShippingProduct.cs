namespace Asu.Core.Domain.Shipping
{
    public class FreeShippingProduct : BaseEntity
    {
        public int ProductId { get; set; }

        public int StoreId { get; set; }
    }
}
