namespace Asu.Core.Domain.Vehicles
{
    public class BaseVehicle : BaseEntity
    {
        public int YearId { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public virtual Year Year { get; set; }
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }
    }
}
