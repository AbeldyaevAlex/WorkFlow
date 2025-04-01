namespace Asu.Core.Domain.Customization
{
    public partial class OrderWithRebates : BaseEntity
    {
        public int OrderId { get; set; }
        public string CouponCode { get; set; }
        public decimal RebateAmount { get; set; }
        public string Email { get; set; }
        public string CustomerFullName { get; set; }
    }
}