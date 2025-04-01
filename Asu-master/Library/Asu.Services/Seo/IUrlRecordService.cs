using Asu.Core;
using Asu.Core.Domain.Seo;

namespace Asu.Services.Seo
{
    using System;

    /// <summary>
    /// Provides information about URL records
    /// </summary>
    public partial interface  IUrlRecordService
    {
        /// <summary>
        /// Deletes an URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void DeleteUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// Gets an URL record
        /// </summary>
        /// <param name="urlRecordId">URL record identifier</param>
        /// <returns>URL record</returns>
        UrlRecord GetUrlRecordById(int urlRecordId);

        /// <summary>
        /// Inserts an URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void InsertUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// Updates the URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void UpdateUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// Find URL record
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecord GetBySlug(string slug);

        /// <summary>
        /// Find URL record (cached version).
        /// This method works absolutely the same way as "GetBySlug" one but caches the results.
        /// Hence, it's used only for performance optimization in public store
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecordService.UrlRecordForCaching GetBySlugCached(string slug);

        /// <summary>
        /// Gets all URL records
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customer collection</returns>
        IPagedList<UrlRecord> GetAllUrlRecords(string slug, int pageIndex, int pageSize);

        /// <summary>
        /// Find slug
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Found slug</returns>
        string GetActiveSlug(int entityId, string entityName, int languageId);

        /// <summary>
        /// Save slug
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="slug">Slug</param>
        /// <param name="languageId">Language ID</param>
        void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity, ISlugSupported;

        #region WC

        string GetActiveSlug(int entityId, int additionalEntityId, string entityName, int languageId);

        VehicleUrlRecord GetVehicleBySlug(string slug);

        UrlRecordService.UrlRecordForCaching GetRedirectBySlugCached(string slug);

        VehicleUrlRecordRedirect GetVehicleRedirectBySlug(string slug);

        VehicleUrlRecord GetVehicleBySlugCached(string slug);

        string GetVehicleSlug(int entityId, string entityName, int? year, int? make, int? model);

        VehicleUrlRecord GetVehicleUrlRecord(int entityId, string entityName, int? year, int? make, int? model);

        ProductGroupUrlRecord GetProductGroupBySlug(string slug);

        ProductGroupUrlRecord GetProductGroupBySlug(string parentEntitySlug, string slug);

        void GetProductGroupSlug(int productGroupId, out string parentEntitySlug, out string entitySlug);

        void GetBrandCategorySlug(int categoryId, out string parentEntitySlug, out string entitySlug);

        #endregion
    }
}