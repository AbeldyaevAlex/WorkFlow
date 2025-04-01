using System;

namespace Asu.Core.Domain.Returns
{
    using System.Collections.Generic;

    public class Refund : BaseEntity
    {
        private ICollection<RefundCredit> credits;

        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public int ReasonId { get; set; }

        public virtual RefundReason Reason { get; set; }

        public virtual CrmSalesOrder Order { get; set; }

        public virtual ICollection<RefundCredit> Credits
        {
            get { return this.credits ?? (this.credits = new List<RefundCredit>()); }
            protected set { this.credits = value; }
        }
    }
}
