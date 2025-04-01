using Asu.Core.Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Discounts
{
    public partial interface ICustomDiscountService
    {
        #region Custom Discount Category

        /// <summary>
        /// Get all custom discount categories
        /// </summary>
        /// <param name="discountCategoryType">Discount categor type</param>
        /// <param name="discountId">Discount identifier</param>
        /// <returns>Custom discount categories</returns>
        IList<CustomDiscountCategory> GetAllCustomDiscountCategories(DiscountCategoryType? discountCategoryType, int discountId = 0);

        /// <summary>
        /// Get custom discount category
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Custom discount category</returns>
        CustomDiscountCategory GetCustomDiscountCategoryByDiscountIdAndCategoryId(int discountId
            , int categoryId);

        /// <summary>
        /// Insert custom discount category
        /// </summary>
        /// <param name="customDiscountCategory">Custom discount category</param>
        void InsertCustomDiscountCategory(CustomDiscountCategory customDiscountCategory);

        /// <summary>
        /// Update custom discount category
        /// </summary>
        /// <param name="customDiscountCategory">Custom discount category</param>
        void UpdateCustomDiscountCategory(CustomDiscountCategory customDiscountCategory);

        /// <summary>
        /// Delete custom discount category
        /// </summary>
        /// <param name="customDiscountCategory">Custom discount category</param>
        void DeleteCustomDiscountCategory(CustomDiscountCategory customDiscountCategory);

        #endregion

        #region Custom Discount manufacturer

        /// <summary>
        /// Get all custom discount manufacturers
        /// </summary>
        /// <param name="discountManufacturerType">Discount manufacturer type</param>
        /// <param name="discountId">Discount identifier</param>
        /// <returns>Custom discount manufacturers</returns>
        IList<CustomDiscountManufacturer> GetAllCustomDiscountManufacturers(DiscountManufacturerType? discountManufacturerType, int discountId = 0);

        /// <summary>
        /// Get custom discount manufacturer
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>Custom discount manufacturer</returns>
        CustomDiscountManufacturer GetCustomDiscountManufacturerByDiscountIdAndManufacturerId(int discountId
            , int manufacturerId);

        /// <summary>
        /// Insert custom discount manufacturer
        /// </summary>
        /// <param name="customDiscountManufacturer">Custom discount manufacturer</param>
        void InsertCustomDiscountManufacturer(CustomDiscountManufacturer customDiscountManufacturer);

        /// <summary>
        /// Update custom discount manufacturer
        /// </summary>
        /// <param name="customDiscountManufacturer">Custom discount manufacturer</param>
        void UpdateCustomDiscountManufacturer(CustomDiscountManufacturer customDiscountManufacturer);

        /// <summary>
        /// Delete custom discount manufacturer
        /// </summary>
        /// <param name="customDiscountManufacturer">Custom discount manufacturer</param>
        void DeleteCustomDiscountManufacturer(CustomDiscountManufacturer customDiscountManufacturer);

        #endregion
    }
}
