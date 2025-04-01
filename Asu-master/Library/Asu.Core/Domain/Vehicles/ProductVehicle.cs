namespace Asu.Core.Domain.Vehicles
{
    using System;

    using Catalog;

    public class ProductVehicle : BaseEntity
    {
        public int ProductId { get; set; }
        public int VehicleId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual Product Product { get; set; }
    }
}
