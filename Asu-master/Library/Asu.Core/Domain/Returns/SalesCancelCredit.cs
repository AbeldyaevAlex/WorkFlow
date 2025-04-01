namespace Asu.Core.Domain.Returns
{
    public class SalesCancelCredit : BaseEntity
    {
        public int CancelId { get; set; }

        public int CreditId { get; set; }

        public virtual SalesCredit Credit { get; set; }

        public virtual SalesCancel Cancel { get; set; }
    }
}
