namespace Asu.Core.Domain.Vehicles
{
    public class ProductModel : BaseEntity
    {
        public int ProductId { get; set; }

        public int YearId { get; set; }

        public int MakeId { get; set; }

        public int ModelId { get; set; }

        public virtual Model Model { get; set; }
    }
}