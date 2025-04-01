namespace Asu.Core.Domain.Customization
{
    public class OrderExtra : BaseEntity
    {
        public int OrderId { get; set; }
        public string SwapOrderNumber { get; set; }
        public int? VehicleId { get; set; }
        public int? BaseVehicleId { get; set; }
        //public int? KountScore { get; set; }
        //public string KountResponse { get; set; }
    }
}
