namespace Asu.Core.Domain.Customization
{
    public partial class AmazonOrderDetails
    {
        public int OrderId { get; set; }
        public string OrderReferenceId { get; set; }
        public string AmazonAuthorizationId { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
