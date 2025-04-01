namespace Asu.Core.Domain.Returns
{
    using System.Collections.Generic;

    public class SalesCancelFee : BaseEntity
    {
        private ICollection<SalesCancelFeeCharge> feeCharges;

        public int CancelId { get; set; }

        public string Name { get; set; }

        public virtual SalesCancel Cancel { get; set; }

        public virtual ICollection<SalesCancelFeeCharge> FeeCharges
        {
            get => this.feeCharges ?? (this.feeCharges = new List<SalesCancelFeeCharge>());
            protected set => this.feeCharges = value;
        }
    }
}
