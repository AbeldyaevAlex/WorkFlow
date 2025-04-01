using Asu.Core.Domain.Catalog;

namespace Asu.Core.Domain.Customization
{
    public partial class VendorProductCost : BaseEntity
    {
        public long VendorProductCostId { get; set; }
        public decimal? Cost { get; set; }
        public string VendorName { get; set; }
        public decimal? AvailableQty { get; set; }
        public virtual Product Product { get; set; }
    }
}
