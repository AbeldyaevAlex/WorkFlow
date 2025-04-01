
namespace Asu.Core.Domain.Catalog
{
    /// <summary>
    /// Represents an products recommendations
    /// </summary>
    public partial class ProductRecommendation : BaseEntity
    {
        /// <summary>
        /// Gets or sets the parent product identifier
        /// </summary>
        public int ParentProductId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets product sort order
        /// </summary>
        public decimal? SortOrder { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets the parent product
        /// </summary>
        public virtual Product ParentProduct { get; set; }

    }
}
