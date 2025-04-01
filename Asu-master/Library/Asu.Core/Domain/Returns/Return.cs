namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    public class Return : BaseEntity
    {
        private ICollection<ReturnItem> returnItems;
        private ICollection<Rma> rmas;
        private ICollection<ReturnCancel> returnCancels;
        private ICollection<ReturnRefusal> returnRefusals;

        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public virtual ICollection<ReturnItem> ReturnItems
        {
            get { return this.returnItems ?? (this.returnItems = new List<ReturnItem>()); }
            protected set { this.returnItems = value; }
        }

        public virtual ICollection<Rma> Rmas
        {
            get { return this.rmas ?? (this.rmas = new List<Rma>()); }
            protected set { this.rmas = value; }
        }

        public virtual ICollection<ReturnCancel> ReturnCancels
        {
            get { return this.returnCancels ?? (this.returnCancels = new List<ReturnCancel>()); }
            protected set { this.returnCancels = value; }
        }

        public virtual ICollection<ReturnRefusal> ReturnRefusals
        {
            get { return this.returnRefusals ?? (this.returnRefusals = new List<ReturnRefusal>()); }
            protected set { this.returnRefusals = value; }
        }
    }
}