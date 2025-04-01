using System.Collections.Generic;
using System.Linq;
using Asu.Core;
using Asu.Core.Caching;
using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Security;
using Asu.Core.Domain.Stores;
using Asu.Core.Infrastructure;
using Asu.Services.Events;
using Asu.Services.Security;
using Asu.Services.Stores;

namespace Asu.Services.Catalog
{
    public sealed class WcCategoryService : CategoryService
    {
        private const string CATEGORIES_BY_PARENT_CATEGORY_ID_KEY = "Nop.category.byparent-{0}";
        private readonly ICacheManager _staticCacheManager;

        public WcCategoryService(ICacheManager cacheManager, IRepository<Category> categoryRepository, IRepository<ProductCategory> productCategoryRepository, IRepository<Product> productRepository, IRepository<AclRecord> aclRepository, IRepository<StoreMapping> storeMappingRepository, IWorkContext workContext, IStoreContext storeContext, IEventPublisher eventPublisher, IStoreMappingService storeMappingService, IAclService aclService, CatalogSettings catalogSettings, IRepository<ManufacturerPiesCategory> manufacturerPiesCategoryRepository) : base(cacheManager, categoryRepository, productCategoryRepository, productRepository, aclRepository, storeMappingRepository, workContext, storeContext, eventPublisher, storeMappingService, aclService, catalogSettings, manufacturerPiesCategoryRepository)
        {
            this._staticCacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");
        }

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        public override IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId, bool showHidden = false)
        {
            string key = string.Format(CATEGORIES_BY_PARENT_CATEGORY_ID_KEY, parentCategoryId);
            return _staticCacheManager.Get(key, () =>
            {
                var query = _categoryRepository.Table;
                if (!showHidden)
                    query = query.Where(c => c.Published);
                query = query.Where(c => c.ParentCategoryId == parentCategoryId);
                query = query.Where(c => !c.Deleted);
                query = query.OrderBy(c => c.DisplayOrder);

                if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        //ACL (access control list)
                        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                        query = from c in query
                                join acl in _aclRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                                from acl in c_acl.DefaultIfEmpty()
                                where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                                select c;
                    }
                    if (!_catalogSettings.IgnoreStoreLimitations)
                    {
                        //Store mapping
                        var currentStoreId = _storeContext.CurrentStore.Id;
                        query = from c in query
                                join sm in _storeMappingRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                                from sm in c_sm.DefaultIfEmpty()
                                where !c.LimitedToStores || currentStoreId == sm.StoreId
                                select c;
                    }
                    //only distinct categories (group by ID)
                    query = from c in query
                            group c by c.Id
                                into cGroup
                                orderby cGroup.Key
                                select cGroup.FirstOrDefault();
                    query = query.OrderBy(c => c.DisplayOrder);
                }

                var categories = query.ToList();
                return categories;
            });

        }
    }
}