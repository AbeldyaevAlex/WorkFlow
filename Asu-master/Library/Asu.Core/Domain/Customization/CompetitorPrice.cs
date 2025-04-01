using Asu.Core.Domain.Catalog;

namespace Asu.Core.Domain.Customization
{
    public partial class CompetitorPrice : BaseEntity
    {
        public string StoreName { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
