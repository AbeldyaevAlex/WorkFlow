using System;

namespace Asu.Web.Models.Returns
{
    public class ReturnRequestOrderModel
    {
        public int CrmOrderId { get; set; }

        public string OrderNumber { get; set; }

        public string Channel { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public decimal ShippingAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal OrderSubTotal { get; set; }

        public decimal CreditTotal { get; set; }

        public string Phone { get; set; }

        public string BillingEmail { get; set; }

        public long ChannelId { get; set; }
    }
}