using System;

namespace Asu.Core.Domain.Returns
{
    using System.Collections.Generic;

    using Asu.Core.Domain.Orders;

    public class ThubOrder : BaseEntity
    {
        private ICollection<ThubOrderItem> orderItems;

        public long OrderId { get; set; }

        public string DisplayOrderReference { get; set; }

        public long CustomerId { get; set; }

        public long ChannelId { get; set; }

        public Channel Channel => (Channel)this.ChannelId;

        public string ChannelName { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public string BillingFirstName { get; set; }

        public string BillingLastName { get; set; }

        public string BillingAddressLine1 { get; set; }

        public string BillingAddressLine2 { get; set; }

        public string BillingAddressLine3 { get; set; }

        public string BillingCity { get; set; }

        public string BillingState { get; set; }

        public string BillingZip { get; set; }

        public string BillingCountry { get; set; }

        public string BillingEmail { get; set; }

        public string BillingPhone { get; set; }

        public string ShippingFirstName { get; set; }

        public string ShippingLastName { get; set; }

        public string ShippingAddressLine1 { get; set; }

        public string ShippingAddressLine2 { get; set; }

        public string ShippingAddressLine3 { get; set; }

        public string ShippingCity { get; set; }

        public string ShippingState { get; set; }

        public string ShippingZip { get; set; }

        public string ShippingCountry { get; set; }

        public string ShippingEmail { get; set; }

        public string ShippingPhone { get; set; }

        public decimal ShippingAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public string BillingCompany { get; set; }

        public string ShippingCompany { get; set; }

        public decimal TaxAmount { get; set; }


        public virtual ICollection<ThubOrderItem> OrderItems
        {
            get { return this.orderItems ?? (this.orderItems = new List<ThubOrderItem>()); }
            protected set { this.orderItems = value; }
        }
    }
}
