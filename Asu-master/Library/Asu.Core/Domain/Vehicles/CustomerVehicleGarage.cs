using Asu.Core.Domain.Customers;

namespace Asu.Core.Domain.Vehicles
{
    public class CustomerVehicleGarage : BaseEntity
    {
        //public CustomerVehicleGarage()
        //{
        //    Vehicle = new Vehicle();
        //}
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public bool IsMain { get; set; }
    }
}
