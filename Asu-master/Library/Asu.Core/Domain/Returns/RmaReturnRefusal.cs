namespace Asu.Core.Domain.Returns
{
    public class RmaReturnRefusal : BaseEntity
    {
        public int RmaId { get; set; }

        public int RefusalId { get; set; }

        public int ReasonId { get; set; }

        public int ProofId { get; set; }

        public virtual Rma Rma { get; set; }

        public virtual ReturnRefusal ReturnRefusal { get; set; }

        public virtual RmaRefusalReason ReturnReason { get; set; }
    }
}