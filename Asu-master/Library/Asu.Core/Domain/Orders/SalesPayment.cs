namespace Asu.Core.Domain.Orders
{
    using Asu.Core.Domain.Returns;
    using System;
    using System.Collections.Generic;

    public class SalesPayment : BaseEntity
    {
        ICollection<SalesPaymentCharge> charges;

        public int OrderId { get; set; }

        public int TypeId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public virtual CrmSalesOrder Order { get; set; }

        public virtual ICollection<SalesPaymentCharge> Charges
        {
            get { return this.charges ?? (this.charges = new List<SalesPaymentCharge>()); }
            protected set { this.charges = value; }
        }
    }
}
