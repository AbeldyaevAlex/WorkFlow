namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    public class ReturnRefusal : BaseEntity
    {
        private ICollection<ReturnRefusalItem> items;

        public int ReturnId { get; set; }

        public int PurchaseOrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public virtual Return Return { get; set; }

        public virtual PureReturnRefusal PureReturnRefusal { get; set; }

        public virtual RmaReturnRefusal RmaReturnRefusal { get; set; }

        public virtual ICollection<ReturnRefusalItem> Items
        {
            get { return this.items ?? (this.items = new List<ReturnRefusalItem>()); }
            protected set { this.items = value; }
        }
    }
}