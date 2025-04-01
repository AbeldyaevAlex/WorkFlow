namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    public class SalesCancel : BaseEntity
    {
        private ICollection<SalesCancelItem> items;

        private ICollection<SalesCancelCredit> cancelCredits;

        private ICollection<SalesCancelFee> cancelFees;

        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public virtual CrmSalesOrder CrmOrder { get; set; }

        public virtual PureCancel PureCancel { get; set; }

        public virtual ReturnCancel ReturnCancel { get; set; }

        public virtual RmaReturnCancel RmaReturnCancel { get; set; }

        public virtual ICollection<SalesCancelItem> Items
        {
            get { return this.items ?? (this.items = new List<SalesCancelItem>()); }
            protected set { this.items = value; }
        }

        public virtual ICollection<SalesCancelCredit> CancelCredits
        {
            get { return this.cancelCredits ?? (this.cancelCredits = new List<SalesCancelCredit>()); }
            protected set { this.cancelCredits = value; }
        }

        public virtual ICollection<SalesCancelFee> CancelFees
        {
            get { return this.cancelFees ?? (this.cancelFees = new List<SalesCancelFee>()); }
            protected set { this.cancelFees = value; }
        }
    }
}