namespace Asu.Core.Domain.Shipping
{
    using Domain.Orders;

    public class OriginalShippingRate : BaseEntity
    {
        public int OrderId { get; set; }

        public decimal Value { get; set; }
    }
}