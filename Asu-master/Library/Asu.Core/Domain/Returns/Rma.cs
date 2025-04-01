namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    public class Rma : BaseEntity
    {
        private ICollection<RmaItem> rmaItems;
        private ICollection<RmaReturnCancel> rmaReturnCancels;
        private ICollection<RmaReturnRefusal> rmaReturnRefusals;
        private ICollection<CrmRmaShipment> rmaShipments;

        public int ReturnId { get; set; }

        public int PurchaseOrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public string Number { get; set; }

        public virtual Return Return { get; set; }

        public virtual ICollection<RmaItem> RmaItems
        {
            get { return this.rmaItems ?? (this.rmaItems = new List<RmaItem>()); }
            protected set { this.rmaItems = value; }
        }

        public virtual ICollection<RmaReturnCancel> RmaReturnCancels
        {
            get { return this.rmaReturnCancels ?? (this.rmaReturnCancels = new List<RmaReturnCancel>()); }
            protected set { this.rmaReturnCancels = value; }
        }

        public virtual ICollection<RmaReturnRefusal> RmaReturnRefusals
        {
            get { return this.rmaReturnRefusals ?? (this.rmaReturnRefusals = new List<RmaReturnRefusal>()); }
            protected set { this.rmaReturnRefusals = value; }
        }

        public virtual ICollection<CrmRmaShipment> RmaShipments
        {
            get { return this.rmaShipments ?? (this.rmaShipments = new List<CrmRmaShipment>()); }
            protected set { this.rmaShipments = value; }
        }
    }
}