namespace Asu.Core.Domain.Vehicles
{
    public class ProductVehicleFitment : BaseEntity
    {
        public int ProductId { get; set; }

        public int VehicleId { get; set; }

        public int BaseVehicleId { get; set; }

        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int SubModelId { get; set; }

        public string SubModel { get; set; }
    }
}