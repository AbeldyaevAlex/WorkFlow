namespace Asu.Core.Domain.Vehicles
{
    public class PriceRange : BaseEntity
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
