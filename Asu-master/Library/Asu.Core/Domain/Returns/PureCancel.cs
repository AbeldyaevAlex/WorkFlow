namespace Asu.Core.Domain.Returns
{
    public class PureCancel : BaseEntity
    {
        public int CancelId { get; set; }

        public int ReasonId { get; set; }

        public virtual ReturnReason ReturnReason { get; set; }

        public virtual SalesCancel Cancel { get; set; }
    }
}