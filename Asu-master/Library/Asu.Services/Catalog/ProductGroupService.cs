namespace Asu.Services.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Transactions;

    using Core.Caching;
    using Core.Data;
    using Core.Domain.ProductGroups;
    using Core.Domain.UserDefinedTableTypes;

    using Data;
    using Logging;
    using Media;

    public class ProductGroupService : IProductGroupService
    {
        private const string BrandCategoriesByIdKey = "Nop.BrandCategory.id-{0}";

        private readonly IRepository<ProductGroup> productGroupRepository;
        private readonly IRepository<BrandCategory> brandCategoryRepository;
        private readonly IDigitalDataService digitalDataService;
        private readonly IRepository<CoverkingProductData> coverkingProductDataRepository;
        private readonly IDbContext dbContext;
        private readonly ICacheManager cacheManager;
        private readonly ILogger logger;

        public ProductGroupService(IRepository<ProductGroup> productGroupRepository,
            IRepository<BrandCategory> brandCategoryRepository,
            IRepository<CoverkingProductData> coverkingProductDataRepository, IDbContext dbContext,
            IDigitalDataService digitalDataService,
            ICacheManager cacheManager,
            ILogger logger)
        {
            this.productGroupRepository = productGroupRepository;
            this.brandCategoryRepository = brandCategoryRepository;
            this.digitalDataService = digitalDataService;
            this.coverkingProductDataRepository = coverkingProductDataRepository;
            this.dbContext = dbContext;
            this.cacheManager = cacheManager;
            this.logger = logger;
        }

        public ProductGroup GetProductGroupById(int productGroupId)
        {
            if (productGroupId <= 0)
                return null;

            var query = from a in this.productGroupRepository.TableNoTracking
                        where a.Id == productGroupId
                        select a;

            return query.SingleOrDefault();
        }

        public BrandCategory GetBrandCategoryById(int categoryId)
        {
            if (categoryId <= 0)
                return null;

            var key = string.Format(BrandCategoriesByIdKey, categoryId);
            return this.cacheManager.Get(key, () => this.brandCategoryRepository.GetById(categoryId));
        }

        public IList<ProductIdArray> InsertOrUpdateProducts(IList<ProductArray> products)
        {
            return this.dbContext.ExecuteStoredProcedureList<ProductIdArray>("WCS_AddOrUpdateProduct", UserDefinedTable.ToSqlParameter("productArray", products));
        }

        public void InsertOrUpdateProductCost(IList<ProductCostArray> productCosts)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoplicityConnectionString"].ConnectionString))
                {
                    var cmd = new SqlCommand("WCS_AddOrUpdateProductCost", connection) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.Insert(0, UserDefinedTable.ToSqlParameter("ProductCostArray", productCosts));
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                this.logger.Error("InsertOrUpdateProductCost() failed", ex);
            }
        }

        public string GetDefaultPictureUrl(int productGroupId, int maxWidth = 0, int maxHeight = 0)
        {
            var productGroup = this.GetProductGroupById(productGroupId);
            if (productGroup == null)
            {
                return this.digitalDataService.GetDefaultPictureUrl();
            }

            return this.GetDefaultPictureUrl(productGroup, maxWidth, maxHeight);
        }

        public string GetDefaultPictureUrl(ProductGroup productGroup, int maxWidth = 0, int maxHeight = 0)
        {
            string url = null;
            var digitalData = productGroup.ProductGroupDigitalData.Where(i => i.DigitalData.Type == DigitalDataType.Picture).OrderBy(i => i.DisplayOrder).FirstOrDefault()?.DigitalData;
            if (digitalData != null)
            {
                url = this.digitalDataService.GetThumbUrl(digitalData, maxWidth, maxHeight);
            }

            if (!string.IsNullOrEmpty(url))
            {
                return url;
            }

            return this.digitalDataService.GetDefaultPictureUrl();
        }

        public IList<CoverkingProductData> GetVendorData(string[] itemIds)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.Serializable
                    }))
            {

                var query = from a in this.coverkingProductDataRepository.TableNoTracking
                            join b in itemIds on a.ItemId equals b
                            select a;

                var entities = query.ToList();
                scope.Complete();
                return entities;
            }
        }
    }
}