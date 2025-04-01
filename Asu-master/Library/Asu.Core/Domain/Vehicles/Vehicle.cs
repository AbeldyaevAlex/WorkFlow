namespace Asu.Core.Domain.Vehicles
{
    public class Vehicle : BaseEntity
    {
        public int BaseVehicleId { get; set; }
        public int SubModelId { get; set; }
        public virtual BaseVehicle BaseVehicle { get; set; }
        public virtual SubModel SubModel { get; set; }
        public bool ShowUniversal { set; get; }
    }
}
