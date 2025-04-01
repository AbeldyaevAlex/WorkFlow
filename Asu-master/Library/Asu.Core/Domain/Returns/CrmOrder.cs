namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    using Orders;

    public class CrmSalesOrder : BaseEntity
    {
        private ICollection<SalesCancel> salesCancels;

        private ICollection<SalesCredit> salesCredits;

        private ICollection<PurchaseOrder> purchaseOrders;

        private ICollection<Refund> refunds;

        private ICollection<CrmSalesOrderLine> lines;

        private ICollection<SalesPayment> payments;

        //public int Version { get; set; }

        public int? ShippingAddressId { get; set; }

        public int? BillingAddressId { get; set; }

        public CrmOrderStatus? OrderStatus { get; set; }

        public int? SalesOrderImportId { get; set; }

        public DateTime? ImportedOn { get; set; }

        public string OrderStatusName { get; set; }

        public bool IsImported { get; set; }

        public long? ThubOrderId { get; set; }

        public string Number { get; set; }

        public int ChannelId { get; set; }

        public DateTime CreatedOn { get; set; }

        public Channel Channel => (Channel)this.ChannelId;

        //public virtual ThubOrder ThubOrder { get; set; }

        public virtual CrmAddress BillingAddress { get; set; }

        public virtual CrmChannel CrmChannel { get; set; }

        public virtual CrmAddress ShippingAddress { get; set; }

        public virtual ICollection<SalesCancel> SalesCancels
        {
            get { return  this.salesCancels ?? (this.salesCancels = new List<SalesCancel>()); }
            protected set { this.salesCancels = value; }
        }

        public virtual ICollection<SalesCredit> SalesCredits
        {
            get { return this.salesCredits ?? (this.salesCredits = new List<SalesCredit>()); }
            protected set { this.salesCredits = value; }
        }

        public virtual ICollection<PurchaseOrder> PurchaseOrders
        {
            get { return this.purchaseOrders ?? (this.purchaseOrders = new List<PurchaseOrder>()); }
            protected set { this.purchaseOrders = value; }
        }

        public virtual ICollection<Refund> Refunds
        {
            get { return this.refunds ?? (this.refunds = new List<Refund>()); }
            protected set { this.refunds = value; } 
        }

        public virtual ICollection<CrmSalesOrderLine> Lines
        {
            get { return this.lines ?? (this.lines = new List<CrmSalesOrderLine>()); }
            protected set { this.lines = value; }
        }

        public virtual ICollection<SalesPayment> Payments
        {
            get { return this.payments ?? (this.payments = new List<SalesPayment>()); }
            protected set { this.payments = value; }
        }
    }
}
