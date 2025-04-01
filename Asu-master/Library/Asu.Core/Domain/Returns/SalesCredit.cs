namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    public class SalesCredit : BaseEntity
    {
        private ICollection<SalesCreditCharge> charges;

        private ICollection<SalesCancelCredit> cancelCredits;

        private ICollection<RefundCredit> refundCredits;

        public int OrderId { get; set; }

        public int TypeId { get; set; }

        public DateTime CreditedOn { get; set; }

        public int CreditedBy { get; set; }

        public virtual CrmSalesOrder Order { get; set; }

        public virtual CreditType Type { get; set; }

        public virtual ICollection<SalesCancelCredit> CancelCredits
        {
            get { return this.cancelCredits ?? (this.cancelCredits = new List<SalesCancelCredit>()); }
            protected set { this.cancelCredits = value; }
        }

        public virtual ICollection<RefundCredit> RefundCredits
        {
            get { return this.refundCredits ?? (this.refundCredits = new List<RefundCredit>()); }
            protected set { this.refundCredits = value; }
        }

        public virtual ICollection<SalesCreditCharge> Charges
        {
            get { return this.charges ?? (this.charges = new List<SalesCreditCharge>()); }
            protected set { this.charges = value; }
        }
    }
}
