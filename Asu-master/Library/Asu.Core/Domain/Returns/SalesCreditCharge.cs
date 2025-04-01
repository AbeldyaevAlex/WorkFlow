namespace Asu.Core.Domain.Returns
{
    public class SalesCreditCharge : BaseEntity
    {
        public int CreditId { get; set; }

        public int TypeId { get; set; }

        public decimal Amount { get; set; }

        public virtual SalesCredit Credit { get; set; }

        public virtual ChargeType Type { get; set; }
    }
}
