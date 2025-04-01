using Asu.Core.Caching;
using Asu.Core.Data;
using Asu.Core.Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Discounts
{
    public partial class CustomDiscountService : ICustomDiscountService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : discountid
        /// </remarks>
        private const string CUSTOM_DISCOUNTS_CATEGORY_ALL_KEY = "Nop.custom.discount.category.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CUSTOM_DISCOUNTS_CATEGORY_PATTERN_KEY = "Nop.custom.discount.category.";

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : discountid
        /// </remarks>
        private const string CUSTOM_DISCOUNTS_MANUFACTURER_ALL_KEY = "Nop.custom.discount.manufacturer.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CUSTOM_DISCOUNTS_MANUFACTURER_PATTERN_KEY = "Nop.custom.discount.manufacturer.";

        #endregion

        #region Fields

        private readonly IRepository<CustomDiscountCategory> _customDiscountCategoryRepository;
        private readonly IRepository<CustomDiscountManufacturer> _customDiscountManufacturerRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public CustomDiscountService(IRepository<CustomDiscountCategory> customDiscountCategoryRepository,
            IRepository<CustomDiscountManufacturer> customDiscountManufacturerRepository,
            ICacheManager cacheManager)
        {
            _customDiscountCategoryRepository = customDiscountCategoryRepository;
            _customDiscountManufacturerRepository = customDiscountManufacturerRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Custom Discount Category

        /// <summary>
        /// Get all custom discount categories
        /// </summary>
        /// <param name="discountCategoryType">Discount categor type</param>
        /// <param name="discountId">Discount identifier</param>
        /// <returns>Custom discount categories</returns>
        public virtual IList<CustomDiscountCategory> GetAllCustomDiscountCategories(DiscountCategoryType? discountCategoryType, int discountId = 0)
        {
            string key = string.Format(CUSTOM_DISCOUNTS_CATEGORY_ALL_KEY, discountId);
            var result = _cacheManager.Get(key, () =>
            {
                var query = _customDiscountCategoryRepository.Table;
                if (discountId > 0)
                {
                    query = query.Where(d => d.DiscountId == discountId);
                }
                var discounts = query.ToList();
                return discounts;
            });
            if (discountCategoryType.HasValue)
            {
                result = result.Where(d => d.CategoryType == discountCategoryType.Value).ToList();
            }
            return result;
        }

        /// <summary>
        /// Get custom discount category
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Custom discount category</returns>
        public virtual CustomDiscountCategory GetCustomDiscountCategoryByDiscountIdAndCategoryId(int discountId
            , int categoryId)
        {
            if (discountId <= 0 || categoryId <= 0)
                return null;

            var query = _customDiscountCategoryRepository.Table;

            return query.Where(x => x.DiscountId == discountId && x.CategoryId == categoryId).FirstOrDefault();
        }

        /// <summary>
        /// Insert custom discount category
        /// </summary>
        /// <param name="customDiscountCategory">Custom discount category</param>
        public virtual void InsertCustomDiscountCategory(CustomDiscountCategory customDiscountCategory)
        {
            if (customDiscountCategory == null)
                throw new ArgumentNullException(nameof(customDiscountCategory));

            _customDiscountCategoryRepository.Insert(customDiscountCategory);

            _cacheManager.RemoveByPattern(CUSTOM_DISCOUNTS_CATEGORY_PATTERN_KEY);
        }

        /// <summary>
        /// Update custom discount category
        /// </summary>
        /// <param name="customDiscountCategory">Custom discount category</param>
        public virtual void UpdateCustomDiscountCategory(CustomDiscountCategory customDiscountCategory)
        {
            if (customDiscountCategory == null)
                throw new ArgumentNullException(nameof(customDiscountCategory));

            _customDiscountCategoryRepository.Update(customDiscountCategory);

            _cacheManager.RemoveByPattern(CUSTOM_DISCOUNTS_CATEGORY_PATTERN_KEY);
        }

        /// <summary>
        /// Delete custom discount category
        /// </summary>
        /// <param name="customDiscountCategory">Custom discount category</param>
        public virtual void DeleteCustomDiscountCategory(CustomDiscountCategory customDiscountCategory)
        {
            if (customDiscountCategory == null)
                throw new ArgumentNullException(nameof(customDiscountCategory));

            _customDiscountCategoryRepository.Delete(customDiscountCategory);

            _cacheManager.RemoveByPattern(CUSTOM_DISCOUNTS_CATEGORY_PATTERN_KEY);
        }

        #endregion

        #region Custom Discount manufacturer

        /// <summary>
        /// Get all custom discount manufacturers
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <param name="discountManufacturerType">Discount manufacturer type</param>
        /// <returns>Custom discount manufacturers</returns>
        public virtual IList<CustomDiscountManufacturer> GetAllCustomDiscountManufacturers(DiscountManufacturerType? discountManufacturerType, int discountId = 0)
        {
            string key = string.Format(CUSTOM_DISCOUNTS_MANUFACTURER_ALL_KEY, discountId);
            var result = _cacheManager.Get(key, () =>
            {
                var query = _customDiscountManufacturerRepository.Table;
                if (discountId > 0)
                {
                    query = query.Where(d => d.DiscountId == discountId);
                }
                var discounts = query.ToList();
                return discounts;
            });
            if (discountManufacturerType.HasValue)
            {
                result = result.Where(d => d.ManufacturerType == discountManufacturerType.Value).ToList();
            }
            return result;
        }

        /// <summary>
        /// Get custom discount manufacturer
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>Custom discount manufacturer</returns>
        public virtual CustomDiscountManufacturer GetCustomDiscountManufacturerByDiscountIdAndManufacturerId(int discountId
            , int manufacturerId)
        {
            if (discountId <= 0 || manufacturerId <= 0)
                return null;

            var query = _customDiscountManufacturerRepository.Table;

            return query
                .Where(x => x.DiscountId == discountId && x.ManufacturerId == manufacturerId).FirstOrDefault();
        }

        /// <summary>
        /// Insert custom discount manufacturer
        /// </summary>
        /// <param name="customDiscountManufacturer">Custom discount manufacturer</param>
        public virtual void InsertCustomDiscountManufacturer(CustomDiscountManufacturer customDiscountManufacturer)
        {
            if (customDiscountManufacturer == null)
                throw new ArgumentNullException(nameof(customDiscountManufacturer));

            _customDiscountManufacturerRepository.Insert(customDiscountManufacturer);

            _cacheManager.RemoveByPattern(CUSTOM_DISCOUNTS_MANUFACTURER_PATTERN_KEY);
        }

        /// <summary>
        /// Update custom discount manufacturer
        /// </summary>
        /// <param name="customDiscountManufacturer">Custom discount manufacturer</param>
        public virtual void UpdateCustomDiscountManufacturer(CustomDiscountManufacturer customDiscountManufacturer)
        {
            if (customDiscountManufacturer == null)
                throw new ArgumentNullException(nameof(customDiscountManufacturer));

            _customDiscountManufacturerRepository.Update(customDiscountManufacturer);

            _cacheManager.RemoveByPattern(CUSTOM_DISCOUNTS_MANUFACTURER_PATTERN_KEY);
        }

        /// <summary>
        /// Delete custom discount manufacturer
        /// </summary>
        /// <param name="customDiscountManufacturer">Custom discount manufacturer</param>
        public virtual void DeleteCustomDiscountManufacturer(CustomDiscountManufacturer customDiscountManufacturer)
        {
            if (customDiscountManufacturer == null)
                throw new ArgumentNullException(nameof(customDiscountManufacturer));

            _customDiscountManufacturerRepository.Delete(customDiscountManufacturer);

            _cacheManager.RemoveByPattern(CUSTOM_DISCOUNTS_MANUFACTURER_PATTERN_KEY);
        }

        #endregion
    }
}
