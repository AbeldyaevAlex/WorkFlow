namespace Asu.Core.Domain.Returns
{
    public class ReturnReason : BaseEntity
    {
        public string Name { get; set; }

        public int FaultTypeId { get; set; }

        public virtual FaultType FaultType { get; set; }

        public InitiationType InitiationType { get; set; }
    }
}