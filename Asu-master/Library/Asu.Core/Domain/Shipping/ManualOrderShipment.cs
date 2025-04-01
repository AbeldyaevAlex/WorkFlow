namespace Asu.Core.Domain.Shipping
{
    using System;
    using System.Collections.Generic;

    public class ManualOrderShipment
    {
        private ICollection<ShipmentLine> shipmentLines;

        public int Id { get; set; }

        public string Email { get; set; }

        public int StoreId { get; set; }

        public string CustomerFullName { get; set; }

        public string OrderNumber { get; set; }

        public string ShippingMethod { get; set; }

        public string TrackingNumber { get; set; }

        public string ShippingLine1 { get; set; }

        public string ShippingLine2 { get; set; }

        public string ShippingCity { get; set; }

        public string ShippingStateProvince { get; set; }

        public string ShippingZipPostalCode { get; set; }

        public string ShippingCountry { get; set; }

        public string BillingPhoneNumber { get; set; }

        public string BillingEmail { get; set; }

        public string BillingLine1 { get; set; }

        public string BillingLine2 { get; set; }

        public string BillingCity { get; set; }

        public string BillingStateProvince { get; set; }

        public string BillingZipPostalCode { get; set; }

        public string BillingCountry { get; set; }

        public decimal Subtotal { get; set; }

        public decimal? Shipping { get; set; }

        public decimal? Tax { get; set; }

        public decimal? Discount { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<ShipmentLine> ShipmentLines
        {
            get { return this.shipmentLines ?? (this.shipmentLines = new List<ShipmentLine>()); }
            protected set { this.shipmentLines = value; }
        }
    }
}
