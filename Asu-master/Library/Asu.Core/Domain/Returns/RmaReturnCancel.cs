namespace Asu.Core.Domain.Returns
{
    public class RmaReturnCancel : BaseEntity
    {
        public int RmaId { get; set; }

        public int CancelId { get; set; }

        public int? ProofId { get; set; }

        public virtual Rma Rma { get; set; }

        public virtual SalesCancel Cancel { get; set; }
    }
}