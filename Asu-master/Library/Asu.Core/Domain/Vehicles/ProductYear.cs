namespace Asu.Core.Domain.Vehicles
{
    public class ProductYear : BaseEntity
    {
        public int ProductId { get; set; }

        public int YearId { get; set; }

        public virtual Year Year { get; set; }
    }
}