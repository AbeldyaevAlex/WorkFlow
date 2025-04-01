namespace Asu.Core.Domain.Shipping
{
    public class ShipmentLine
    {
        public int ShipmentId { get; set; }

        public decimal Subtotal { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string ManufacturerPartNumber { get; set; }
    }
}

