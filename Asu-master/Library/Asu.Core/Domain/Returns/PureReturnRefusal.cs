namespace Asu.Core.Domain.Returns
{
    public class PureReturnRefusal : BaseEntity
    {
        public int RefusalId { get; set; }

        public int ReasonId { get; set; }

        public int ProofId { get; set; }

        public virtual ReturnRefusal ReturnRefusal { get; set; }

        public virtual ReturnRefusalReason ReturnReason { get; set; }
    }
}