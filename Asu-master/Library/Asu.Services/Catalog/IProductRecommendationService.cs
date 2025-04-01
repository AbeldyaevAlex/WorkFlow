using System.Collections.Generic;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customization;

namespace Asu.Services.Catalog
{
    /// <summary>
    /// Product template interface
    /// </summary>
    public partial interface IProductRecommendationService
    {
        /// <summary>
        /// Gets a list of parent products of recommended to customers by products they added to cart
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product collection</returns>
        int[] GetParentProductIdsOfProductRecommendations(int storeId, int productId, bool showHidden = false, int count = 0);

        /// <summary>
        /// Gets a list of products recommended to customers by products they added to cart
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product collection</returns>
        List<ProductRecommendation> GetProductsRecommendations(int storeId, int productId, bool showHidden = false);

        /// <summary>
        /// Gets a list of products (identifiers) recommended to customers by products they added to cart
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product collection</returns>
        int[] GetProductsRecommendationIds(int storeId, int productId, bool showHidden = false);


        /// <summary>
        /// Gets a list of products extra by products ids 
        /// </summary>
        /// <param name="productsIds">Products Ids</param>
        /// <returns>Product Extra collection</returns>
        IEnumerable<ProductExtra> GetProductExtras(int[] productsIds);


        /// <summary>
        /// Gets a list of product extras by products recommended to customers
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="productsIds">Parent product identifiers</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product collection</returns>
        IEnumerable<ProductExtra> GetProductExtrasByProductRecommendations(int storeId, int[] productsIds, bool showHidden = false);
    }
}
