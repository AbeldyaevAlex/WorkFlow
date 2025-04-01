namespace Asu.Core.Domain.Vehicles
{
    public class ProductSubModel : BaseEntity
    {
        public int ProductId { get; set; }

        public int YearId { get; set; }

        public int MakeId { get; set; }

        public int ModelId { get; set; }

        public int SubModelId { get; set; }

        public virtual SubModel SubModel { get; set; }
    }
}