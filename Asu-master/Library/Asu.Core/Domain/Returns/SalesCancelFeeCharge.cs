namespace Asu.Core.Domain.Returns
{
    public class SalesCancelFeeCharge : BaseEntity
    {
        public int FeeId { get; set; }

        public int TypeId { get; set; }

        public decimal Amount { get; set; }

        public virtual ChargeType Type { get; set; }

        public virtual SalesCancelFee Fee { get; set; }
    }
}
