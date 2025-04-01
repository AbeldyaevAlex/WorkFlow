namespace Asu.Core.Domain.Orders
{
    public class SalesPaymentCharge : BaseEntity
    {
        public int PaymentId { get; set; }

        public int TypeId { get; set; }

        public decimal Amount { get; set; }

        public SalesPaymentChargeType Type => (SalesPaymentChargeType)this.TypeId;

        public virtual SalesPayment Payment { get; set; }
    }
}
