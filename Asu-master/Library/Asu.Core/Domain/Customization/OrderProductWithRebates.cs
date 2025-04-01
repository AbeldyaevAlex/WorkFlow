namespace Asu.Core.Domain.Customization
{
    public partial class OrderProductWithRebates : BaseEntity
    {
        public int ProductId { get; set; }
        public int OrderProductVariantId { get; set; }
        public decimal RebateAmount { get; set; }
    }
}
