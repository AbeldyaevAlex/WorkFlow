namespace Asu.Core.Domain.Vehicles
{
    public class ProductMake : BaseEntity
    {
        public int ProductId { get; set; }

        public int YearId { get; set; }

        public int MakeId { get; set; }

        public virtual Make Make { get; set; }
    }
}