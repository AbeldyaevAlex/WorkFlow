namespace Asu.Core.Domain.Returns
{
    public class ReturnCancel : BaseEntity
    {
        public int ReturnId { get; set; }

        public int CancelId { get; set; }

        public int ProofId { get; set; }

        public virtual Return Return { get; set; }

        public virtual SalesCancel Cancel { get; set; }
    }
}