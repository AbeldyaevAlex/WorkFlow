using System;

namespace Asu.Core.Domain.Customization
{
    public partial class OrderShipmentEta : BaseEntity
    {
        public int OrderId { get; set; }

        public string Email { get; set; }

        public string CustomerFullName { get; set; }

        public DateTime ShipmentEta { get; set; }

        public int StoreId { get; set; }
    }
}