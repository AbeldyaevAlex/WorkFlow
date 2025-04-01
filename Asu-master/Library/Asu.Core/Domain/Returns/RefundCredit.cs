namespace Asu.Core.Domain.Returns
{
    public class RefundCredit : BaseEntity
    {
        public int RefundId { get; set; }

        public int CreditId { get; set; }

        public virtual SalesCredit Credit { get; set; }

        public  virtual Refund Refund { get; set; }
    }
}
