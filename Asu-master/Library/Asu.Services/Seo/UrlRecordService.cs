using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Core;
using Asu.Core.Caching;
using Asu.Core.Data;
using Asu.Core.Domain.Localization;
using Asu.Core.Domain.Seo;

namespace Asu.Services.Seo
{
    /// <summary>
    /// Provides information about URL records
    /// </summary>
    public partial class UrlRecordService : IUrlRecordService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// {2} : language ID
        /// </remarks>
        private const string URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY = "Nop.urlrecord.active.id-name-language-{0}-{1}-{2}";

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// </remarks>
        private const string URL_RECORD_REDIRECT_ALL_KEY = "UrlRecord.Redirect.All.List";

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string URLRECORD_ALL_KEY = "Nop.urlrecord.all";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : slug
        /// </remarks>
        private const string URLRECORD_BY_SLUG_KEY = "Nop.urlrecord.active.slug-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string URLRECORD_PATTERN_KEY = "Nop.urlrecord.";

        #endregion

        #region Fields

        private readonly IRepository<UrlRecord> _urlRecordRepository;
        private readonly IRepository<UrlRecordRedirect> urlRecordRedirectRepository; //WC
        private readonly ICacheManager _cacheManager;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IRepository<VehicleUrlRecord> _vehicleUrlRecordRepository;   //WC
        private readonly IRepository<ProductGroupUrlRecord> _productGroupUrlRecordRepository;   //WC
        private readonly IRepository<VehicleUrlRecordRedirect> vehicleUrlRecordRedirectRepository; //WC

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="urlRecordRepository">URL record repository</param>
        /// <param name="localizationSettings">Localization settings</param>
        public UrlRecordService(ICacheManager cacheManager,
            IRepository<UrlRecord> urlRecordRepository,
            LocalizationSettings localizationSettings,
            IRepository<VehicleUrlRecord> vehicleUrlRecordRepository,
            IRepository<ProductGroupUrlRecord> productGroupUrlRecordRepository,
            IRepository<UrlRecordRedirect> urlRecordMappingRepository,
            IRepository<VehicleUrlRecordRedirect> vehicleUrlRecordRedirectRepository)
        {
            this._cacheManager = cacheManager;
            this._urlRecordRepository = urlRecordRepository;
            this._localizationSettings = localizationSettings;
            this._vehicleUrlRecordRepository = vehicleUrlRecordRepository;  //WC
            this._productGroupUrlRecordRepository = productGroupUrlRecordRepository;  //WC
            this.urlRecordRedirectRepository = urlRecordMappingRepository; // WC
            this.vehicleUrlRecordRedirectRepository = vehicleUrlRecordRedirectRepository; //WC
        }

        #endregion

        #region Utilities

        protected UrlRecordForCaching Map(UrlRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            var urlRecordForCaching = new UrlRecordForCaching
            {
                Id = record.Id,
                EntityId = record.EntityId,
                EntityName = record.EntityName,
                Slug = record.Slug,
                IsActive = record.IsActive,
                LanguageId = record.LanguageId,
                AdditionalEntityId = record.UrlRecordExtra == null ? null : (int?)record.UrlRecordExtra.AdditionalEntityId
            };

            return urlRecordForCaching;
        }

        protected UrlRecordForCaching Map(UrlRecordRedirect record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            var urlRecordForCaching = new UrlRecordForCaching
            {
                Id = record.Id,
                EntityId = record.NewEntityId ?? 0,
                EntityName = record.EntityName,
                Slug = record.NewSlug,
                IsActive = record.IsActive
            };

            return urlRecordForCaching;
        }

        /// <summary>
        /// Gets all cached URL records
        /// </summary>
        /// <returns>cached URL records</returns>
        protected virtual IList<UrlRecordForCaching> GetAllUrlRecordsCached()
        {
            //cache
            string key = string.Format(URLRECORD_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = from ur in _urlRecordRepository.Table
                            select ur;
                var urlRecords = query.ToList();
                var list = new List<UrlRecordForCaching>();
                foreach (var ur in urlRecords)
                {
                    var urlRecordForCaching = Map(ur);
                    list.Add(urlRecordForCaching);
                }
                return list;
            });
        }

        /// <summary>
        /// Gets all cached redirect URL records
        /// </summary>
        /// <returns>cached redirect URL records</returns>
        protected virtual IList<UrlRecordForCaching> GetAllRedirectUrlRecordsCached()
        {
            return this._cacheManager.Get(URL_RECORD_REDIRECT_ALL_KEY, () =>
            {
                var urlRecords = (from m in this.urlRecordRedirectRepository.Table where m.IsActive select m).ToList();
                return urlRecords.Select(this.Map).ToList();
            });
        }

        #endregion

        #region Nested classes

        [Serializable]
        public class UrlRecordForCaching
        {
            public int Id { get; set; }
            public int EntityId { get; set; }
            public string EntityName { get; set; }
            public string Slug { get; set; }
            public bool IsActive { get; set; }
            public int LanguageId { get; set; }

            #region WC

            public int? AdditionalEntityId { get; set; }

            #endregion
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        public virtual void DeleteUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Delete(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Gets an URL record
        /// </summary>
        /// <param name="urlRecordId">URL record identifier</param>
        /// <returns>URL record</returns>
        public virtual UrlRecord GetUrlRecordById(int urlRecordId)
        {
            if (urlRecordId == 0)
                return null;

            return _urlRecordRepository.GetById(urlRecordId);
        }

        /// <summary>
        /// Inserts an URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        public virtual void InsertUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Insert(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        public virtual void UpdateUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            _urlRecordRepository.Update(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Find URL record
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        public virtual UrlRecord GetBySlug(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;

            var query = from ur in _urlRecordRepository.Table
                        where ur.Slug == slug
                        select ur;
            var urlRecord = query.FirstOrDefault();
            return urlRecord;
        }

        /// <summary>
        /// Find redirect URL record
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found redirect URL record</returns>
        public virtual UrlRecord GetRedirectBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }
                
            var query = from a in this.urlRecordRedirectRepository.Table
                        join b in this._urlRecordRepository.Table on a.NewSlug equals b.Slug
                        where a.OldSlug == slug
                        select b;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Find URL record (cached version).
        /// This method works absolutely the same way as "GetBySlug" one but caches the results.
        /// Hence, it's used only for performance optimization in public store
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        public virtual UrlRecordForCaching GetBySlugCached(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }
                
            if (this._localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var source = this.GetAllUrlRecordsCached();
                var query = from m in source
                            where m.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase)
                            select m;

                var urlRecordForCaching = query.FirstOrDefault();

                return urlRecordForCaching;
            }

            //gradual loading
            var key = string.Format(URLRECORD_BY_SLUG_KEY, slug);
            return this._cacheManager.Get(key, () =>
            {
                
                var urlRecord = this.GetBySlug(slug);
                if (urlRecord == null)
                {
                    return null;
                }

                var urlRecordForCaching = this.Map(urlRecord);

                return urlRecordForCaching;
            });
        }

        /// <summary>
        /// Find URL record (cached version).
        /// This method works absolutely the same way as "GetBySlug" one but caches the results.
        /// Hence, it's used only for performance optimization in public store
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        public virtual UrlRecordForCaching GetRedirectBySlugCached(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }

            if (this._localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var source = this.GetAllRedirectUrlRecordsCached();
                var query = from m in source
                            where m.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase)
                            select m;

                var urlRecordForCaching = query.FirstOrDefault();

                return urlRecordForCaching;
            }

            //gradual loading
            var key = string.Format(URLRECORD_BY_SLUG_KEY, slug);
            return this._cacheManager.Get(key, () =>
                {

                    var urlRecord = this.GetRedirectBySlug(slug);
                    if (urlRecord == null)
                    {
                        return null;
                    }

                    var urlRecordForCaching = this.Map(urlRecord);

                    return urlRecordForCaching;
                });
        }

        /// <summary>
        /// Gets all URL records
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customer collection</returns>
        public virtual IPagedList<UrlRecord> GetAllUrlRecords(string slug, int pageIndex, int pageSize)
        {
            var query = _urlRecordRepository.Table;
            if (!String.IsNullOrWhiteSpace(slug))
                query = query.Where(ur => ur.Slug.Contains(slug));
            query = query.OrderBy(ur => ur.Slug);

            var urlRecords = new PagedList<UrlRecord>(query, pageIndex, pageSize);
            return urlRecords;
        }

        /// <summary>
        /// Find slug
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Found slug</returns>
        public virtual string GetActiveSlug(int entityId, string entityName, int languageId)
        {
            if (_localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                string key = string.Format(URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY, entityId, entityName, languageId);
                return _cacheManager.Get(key, () =>
                {
                    //load all records (we know they are cached)
                    var source = GetAllUrlRecordsCached();
                    var query = from ur in source
                                where ur.EntityId == entityId &&
                                ur.EntityName == entityName &&
                                ur.LanguageId == languageId &&
                                ur.IsActive
                                orderby ur.Id descending
                                select ur.Slug;
                    var slug = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (slug == null)
                        slug = "";
                    return slug;
                });
            }
            else
            {
                //gradual loading
                string key = string.Format(URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY, entityId, entityName, languageId);
                return _cacheManager.Get(key, () =>
                {
                    var source = _urlRecordRepository.Table;
                    var query = from ur in source
                                where ur.EntityId == entityId &&
                                ur.EntityName == entityName &&
                                ur.LanguageId == languageId &&
                                ur.IsActive
                                orderby ur.Id descending
                                select ur.Slug;
                    var slug = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (slug == null)
                        slug = "";
                    return slug;
                });
            }
        }

        /// <summary>
        /// Save slug
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="slug">Slug</param>
        /// <param name="languageId">Language ID</param>
        public virtual void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity, ISlugSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from ur in _urlRecordRepository.Table
                        where ur.EntityId == entityId &&
                        ur.EntityName == entityName &&
                        ur.LanguageId == languageId
                        orderby ur.Id descending 
                        select ur;
            var allUrlRecords = query.ToList();
            var activeUrlRecord = allUrlRecords.FirstOrDefault(x => x.IsActive);

            if (activeUrlRecord == null && !string.IsNullOrWhiteSpace(slug))
            {
                //find in non-active records with the specified slug
                var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                    .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                if (nonActiveRecordWithSpecifiedSlug != null)
                {
                    //mark non-active record as active
                    nonActiveRecordWithSpecifiedSlug.IsActive = true;
                    UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);
                }
                else
                {
                    //new record
                    var urlRecord = new UrlRecord
                    {
                        EntityId = entity.Id,
                        EntityName = entityName,
                        Slug = slug,
                        LanguageId = languageId,
                        IsActive = true,
                    };
                    InsertUrlRecord(urlRecord);
                }
            }

            if (activeUrlRecord != null && string.IsNullOrWhiteSpace(slug))
            {
                //disable the previous active URL record
                activeUrlRecord.IsActive = false;
                UpdateUrlRecord(activeUrlRecord);
            }

            if (activeUrlRecord != null && !string.IsNullOrWhiteSpace(slug))
            {
                //is it the same slug as in active URL record?
                if (activeUrlRecord.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
                {
                    //yes. do nothing
                    //P.S. wrote this way for more source code readability
                }
                else
                {
                    //find in non-active records with the specified slug
                    var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                        .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                    if (nonActiveRecordWithSpecifiedSlug != null)
                    {
                        //mark non-active record as active
                        nonActiveRecordWithSpecifiedSlug.IsActive = true;
                        UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);

                        //disable the previous active URL record
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }
                    else
                    {
                        //insert new record
                        //we do not update the existing record because we should track all previously entered slugs
                        //to ensure that URLs will work fine
                        var urlRecord = new UrlRecord
                        {
                            EntityId = entity.Id,
                            EntityName = entityName,
                            Slug = slug,
                            LanguageId = languageId,
                            IsActive = true,
                        };
                        InsertUrlRecord(urlRecord);

                        //disable the previous active URL record
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }

                }
            }
        }

        #endregion

        #region WC

        private const string URLRECORD_ACTIVE_BY_ID_ADDITIONALID_NAME_LANGUAGE_KEY = "Nop.urlrecord.active.id-addid-name-language-{0}-{1}-{2}-{3}";
        private const string VEHICLE_URLRECORD_BY_SLUG_KEY = "WC.vehicle.urlrecord.slug-{0}";
        private const string VEHICLE_URLRECORD_SLUG_KEY = "WC.vehicle.urlrecord.slug.by.all.params-{0}-{1}-{2}-{3}-{4}";
        private const string VEHICLE_URLRECORD_KEY = "WC.vehicle.urlrecord-{0}-{1}-{2}-{3}-{4}";
        private const string PRODUCT_GROUP_URLRECORD_KEY = "WC.ProductGroup.urlrecord-{0}";
        private const string BRAND_CATEGORY_URLRECORD_KEY = "WC.BrandCategory.urlrecord-{0}";

        /// <summary>
        /// Find slug
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="additionalEntityId">Additional Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Found slug</returns>
        public virtual string GetActiveSlug(int entityId, int additionalEntityId, string entityName, int languageId)
        {
            if (_localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                string key = string.Format(URLRECORD_ACTIVE_BY_ID_ADDITIONALID_NAME_LANGUAGE_KEY, entityId, additionalEntityId, entityName, languageId);
                return _cacheManager.Get(key, () =>
                {
                    //load all records (we know they are cached)
                    var source = GetAllUrlRecordsCached();
                    var query = from ur in source
                                where ur.EntityId == entityId && 
                                ur.AdditionalEntityId.HasValue && ur.AdditionalEntityId.Value == additionalEntityId &&
                                ur.EntityName == entityName &&
                                ur.LanguageId == languageId &&
                                ur.IsActive
                                orderby ur.Id descending
                                select ur.Slug;
                    var slug = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (slug == null)
                        slug = "";
                    return slug;
                });
            }
            else
            {
                //gradual loading
                string key = string.Format(URLRECORD_ACTIVE_BY_ID_ADDITIONALID_NAME_LANGUAGE_KEY, entityId, additionalEntityId, entityName, languageId);
                return _cacheManager.Get(key, () =>
                {
                    var source = _urlRecordRepository.Table;
                    var query = from ur in source
                                where ur.EntityId == entityId && 
                                ur.UrlRecordExtra != null && ur.UrlRecordExtra.AdditionalEntityId == additionalEntityId &&
                                ur.EntityName == entityName &&
                                ur.LanguageId == languageId &&
                                ur.IsActive
                                orderby ur.Id descending
                                select ur.Slug;
                    var slug = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (slug == null)
                        slug = "";
                    return slug;
                });
            }
        }

        public virtual VehicleUrlRecord GetVehicleBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }

            var query = from m in this._vehicleUrlRecordRepository.TableNoTracking
                        where m.Slug == slug
                        select m;
            var urlRecord = query.FirstOrDefault();

            return urlRecord;
        }

        public virtual VehicleUrlRecordRedirect GetVehicleRedirectBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }

            var query = from m in this.vehicleUrlRecordRedirectRepository.TableNoTracking
                        where m.OldSlug == slug
                        select m;
            var urlRecord = query.FirstOrDefault();

            return urlRecord;
        }

        public virtual VehicleUrlRecord GetVehicleBySlugCached(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;

            string key = string.Format(VEHICLE_URLRECORD_BY_SLUG_KEY, slug);
            return _cacheManager.Get(key, () => GetVehicleBySlug(slug));
        }

        public virtual string GetVehicleSlug(int entityId, string entityName, int? year, int? make, int? model)
        {
            //gradual loading
            string key = string.Format(VEHICLE_URLRECORD_SLUG_KEY, entityId, entityName, make, model, year);
            return _cacheManager.Get(key, () =>
            {
                var query = from ur in _vehicleUrlRecordRepository.TableNoTracking
                            where ur.EntityId == entityId && ur.EntityName == entityName
                            && (ur.MakeId == make.Value)
                            && (ur.ModelId == model.Value)
                            && (ur.YearId == year.Value)
                            orderby ur.Id descending
                            select ur.Slug;
                var slug = query.FirstOrDefault();

                //little hack here. nulls aren't cacheable so set it to ""
                if (slug == null)
                    slug = "";
                return slug;
            });
        }

        public virtual VehicleUrlRecord GetVehicleUrlRecord(int entityId, string entityName, int? year, int? make, int? model)
        {
            //gradual loading
            string key = string.Format(VEHICLE_URLRECORD_KEY, entityId, entityName, make, model, year);
            return _cacheManager.Get(key, () =>
            {
                var source = _vehicleUrlRecordRepository.Table;
                var query = from ur in source
                            where ur.EntityId == entityId && ur.EntityName == entityName
                            && (ur.MakeId == make.Value)
                            && (ur.ModelId == model.Value)
                            && (ur.YearId == year.Value)
                            orderby ur.Id descending
                            select ur;
                var urlRecord = query.FirstOrDefault();
                return urlRecord;
            });
        }

        public virtual ProductGroupUrlRecord GetProductGroupBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            var query = from ur in this._productGroupUrlRecordRepository.Table
                        where ur.ParentEntitySlug == null && ur.EntitySlug == slug
                        select ur;

            var urlRecord = query.FirstOrDefault();
            return urlRecord;
        }

        public virtual ProductGroupUrlRecord GetProductGroupBySlug(string parentEntitySlug, string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            var query = from ur in this._productGroupUrlRecordRepository.Table
                        where ur.ParentEntitySlug == parentEntitySlug && ur.EntitySlug == slug
                        select ur;

            var urlRecord = query.FirstOrDefault();
            return urlRecord;
        }

        public virtual void GetProductGroupSlug(int productGroupId, out string parentEntitySlug, out string entitySlug)
        {
            var key = string.Format(PRODUCT_GROUP_URLRECORD_KEY, productGroupId);
            var urlRecord = this._cacheManager.Get(key, () =>
            {
                var query = from ur in this._productGroupUrlRecordRepository.Table
                            where ur.EntityId == productGroupId && ur.EntityType == GroupEntityType.ProductGroup
                            select ur;

                return query.SingleOrDefault();
            });

            parentEntitySlug = null;
            entitySlug = null;
            if (urlRecord == null)
            {
                return;
            }

            parentEntitySlug = urlRecord.ParentEntitySlug;
            entitySlug = urlRecord.EntitySlug;
        }

        public virtual void GetBrandCategorySlug(int categoryId, out string parentEntitySlug, out string entitySlug)
        {
            var key = string.Format(BRAND_CATEGORY_URLRECORD_KEY, categoryId);
            var urlRecord = this._cacheManager.Get(key, () =>
            {
                var query = from ur in this._productGroupUrlRecordRepository.Table
                            where ur.EntityId == categoryId && ur.EntityType == GroupEntityType.BrandCategory
                            select ur;

                return query.SingleOrDefault();
            });

            parentEntitySlug = null;
            entitySlug = null;
            if (urlRecord == null)
            {
                return;
            }

            parentEntitySlug = urlRecord.ParentEntitySlug;
            entitySlug = urlRecord.EntitySlug;
        }

        public UrlRecordForCaching GetByEnhancedSlugCached(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }

            if (this._localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var source = GetAllUrlRecordsCached();
                var query = from m in source
                            where m.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase)
                            select m;
                var urlRecordForCaching = query.FirstOrDefault();
                return urlRecordForCaching;
            }

            //gradual loading
            var key = string.Format(URLRECORD_BY_SLUG_KEY, slug);
            return _cacheManager.Get(key, () =>
                {
                    var urlRecord = GetBySlug(slug);
                    if (urlRecord == null)
                        return null;

                    var urlRecordForCaching = Map(urlRecord);
                    return urlRecordForCaching;
                });
        }

        #endregion
    }
}