using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customization;
using Asu.Services.Events;

namespace Asu.Services.Catalog
{
    /// <summary>
    /// Product template service
    /// </summary>
    public partial class ProductRecommendationService : IProductRecommendationService
    {
        #region Fields

        private readonly IRepository<ProductRecommendation> _productRecommendationRepository;
        private readonly IRepository<ProductExtra> _productExtraRepository;
        private readonly IRepository<Product> _productRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Gets a list of parent products (identifiers) of product purchased by other customers who purchased a specified product
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual int[] GetParentProductIdsOfProductRecommendations(int storeId, int productId, bool showHidden = false, int count = 0)
        {
            if (productId == 0)
                throw new ArgumentException("Product ID is not specified");
            //this inner query should retrieve all orders that contains a specified product ID
            var orderIdQuery = from productRecommend in _productRecommendationRepository.Table
                               where productRecommend.ProductId == productId
                               select productRecommend.ParentProductId;

            var orderItemProductQuery = from productRecommend in _productRecommendationRepository.Table
                                        join p in _productRepository.Table on productRecommend.ParentProductId equals p.Id
                                        where orderIdQuery.Contains(productRecommend.ParentProductId)
                                        && (showHidden || p.Published)
                                        && !p.Deleted && !p.CallForPrice && !p.DisableBuyButton
                                        && (p.StockQuantity > 0 || p.StockQuantity == 0 && p.ProductExtra.IsShippingFromManufacturer)
                                        select productRecommend;


            var products = orderItemProductQuery
                .OrderByDescending(p => p.SortOrder)
                .Select(p => p.ParentProductId).ToArray();

            var parentProducts = products.GroupBy(p => p).Select(p => p.First()).ToArray();

            return parentProducts;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productTemplateRepository">Product template repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductRecommendationService(IRepository<ProductRecommendation> productRecommendation,
            IRepository<ProductExtra> productExtraRepository,
            IRepository<Product> productRepository)
        {
            this._productRecommendationRepository = productRecommendation;
            this._productExtraRepository = productExtraRepository;
            this._productRepository = productRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of products (identifiers) purchased by other customers who purchased a specified product
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual int[] GetProductsRecommendationIds(int storeId, int productId, bool showHidden = false)
        {
            if (productId == 0)
            {
                throw new ArgumentException("Product ID is not specified");
            }
                
            //this inner query should retrieve all orders that contains a specified product ID
            var orderIdQuery = from productRecommend in _productRecommendationRepository.TableNoTracking
                where productRecommend.ParentProductId == productId
                select productRecommend.ProductId;

            var orderItemProductQuery = from pr in _productRecommendationRepository.TableNoTracking
                join p in _productRepository.TableNoTracking on pr.ProductId equals p.Id
                join o in orderIdQuery on pr.ProductId equals o
                where p.Id != productId && (showHidden || p.Published)
                      && !pr.Product.Deleted && !p.Deleted
                      && (showHidden || p.Published) && !p.CallForPrice && !p.DisableBuyButton
                      && (p.StockQuantity > 0 || p.StockQuantity == 0 && p.ProductExtra.IsShippingFromManufacturer)
                select pr;

            var products = orderItemProductQuery
                .OrderByDescending(p => p.SortOrder)
                .Select(p => p.ProductId).ToArray();

            return products;
        }

        public virtual IEnumerable<ProductExtra> GetProductExtrasByProductRecommendations(int storeId, int[] productsIds, bool showHidden = false)
        {
            var recommendations = new List<int>();
            foreach (var productId in productsIds)
            {
                recommendations.AddRange(GetProductsRecommendationIds(storeId, productId, showHidden));
            }

            recommendations = recommendations.GroupBy(p => p).Select(p => p.First()).ToList();

            return GetProductExtras(recommendations.ToArray());
        }

        /// <summary>
        /// Gets a list of products (identifiers) purchased by other customers who purchased a specified product
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="recordsToReturn">Records to return</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual List<ProductRecommendation> GetProductsRecommendations(int storeId, int productId, bool showHidden = false)
        {
            if (productId == 0)
                throw new ArgumentException("Product ID is not specified");
            //this inner query should retrieve all orders that contains a specified product ID
            var orderIdQuery = from productRecommend in _productRecommendationRepository.Table
                               where productRecommend.ParentProductId == productId
                               select productRecommend.ProductId;

            var orderItemProductQuery = from productRecommend in _productRecommendationRepository.Table
                                        join p in _productRepository.Table on productRecommend.ProductId equals p.Id
                                        where orderIdQuery.Contains(productRecommend.ProductId)
                                        && p.Id != productId && (showHidden || p.Published)
                                        && !productRecommend.Product.Deleted && !p.Deleted
                                        && (showHidden || p.Published) && !p.CallForPrice && !p.DisableBuyButton
                                        && (p.StockQuantity > 0 || p.StockQuantity == 0 && p.ProductExtra.IsShippingFromManufacturer)
                                        select productRecommend;


            var products = orderItemProductQuery
                .OrderByDescending(p => p.SortOrder)
                .Select(p => p).ToList();

            return products;
        }

        public virtual IEnumerable<ProductExtra> GetProductExtras(int[] productsIds)
        {
            return _productExtraRepository.Table.Where(p => productsIds.Contains(p.Product.Id)).Select(p => p).ToList();
        }

        #endregion
    }
}
