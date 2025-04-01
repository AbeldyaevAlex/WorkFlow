using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Asu.Core.Caching;
using Asu.Core.Data;
using Asu.Core.Domain.Vehicles;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Stores;


namespace Asu.Services.Vehicles
{
    using System;
    using System.Globalization;

    using Asu.Core;
    using Asu.Core.Domain.Catalog;
    using Asu.Data;

    using System.Data;

    using PriceRange = Asu.Core.Domain.Vehicles.PriceRange;
    using Asu.Data.Mapping.Catalog;

    public sealed class VehicleService : IVehicleService
    {
        #region Constants

        private const string VEHICLE_YEARS_KEY = "WC.Vehicle.Years";
        private const string VEHICLE_YEARS_BY_MODEL_KEY = "WC.Vehicle.YearsByModel-{0}";
        private const string VEHICLE_YEARS_BY_MAKE_KEY = "WC.Vehicle.YearsByMake-{0}";
        private const string VEHICLE_PRODUCT_YEARS_KEY = "WC.Vehicle.Product.Years-{0}";
        private const string VEHICLE_MAKE_KEY = "WC.Vehicle.Make-{0}";
        private const string VEHICLE_MAKES_KEY = "WC.Vehicle.Makes-{0}";
        private const string VEHICLE_POPULAR_MAKES_KEY = "WC.Vehicle.PopularMakes-{0}";
        private const string VEHICLE_ALL_MAKES_KEY = "WC.Vehicle.All.Makes";
        private const string VEHICLE_PRODUCT_MAKES_KEY = "WC.Vehicle.Product.Makes-{0}-{1}";
        private const string VEHICLE_MODEL_KEY = "WC.Vehicle.Model-{0}";
        private const string VEHICLE_ALL_MODELS_KEY = "WC.Vehicle.All.Models-{0}-{1}";
        private const string VEHICLE_MODELS_KEY = "WC.Vehicle.Models-{0}-{1}";
        private const string VEHICLE_POPULAR_MODELS_KEY = "WC.Vehicle.PopularModels-{0}-{1}";
        private const string VEHICLE_PRODUCT_MODELS_KEY = "WC.Vehicle.Product.Models-{0}-{1}-{2}";
        private const string VEHICLE_SUBMODELS_KEY = "WC.Vehicle.SubModels-{0}-{1}-{2}";
        private const string VEHICLE_PRODUCT_SUBMODELS_KEY = "WC.Vehicle.Product.SubModels-{0}-{1}-{2}-{3}";
        private const string VEHICLE_PRODUCT_GROUPPRODUCTS_KEY = "WC.Vehicle.Product.GroupProducts-{0}-{1}-{2}-{3}";
        private const string VEHICLE_PRODUCT_GROUPPRODUCTS_ATTRIBUTE_KEY = "WC.Vehicle.Product.GroupProducts-{0}-{1}-{2}-{3}-{4}-{5}-{6}";
        private const string VEHICLE_SUBMODELS_LIMITED_KEY = "WC.Vehicle.SubModels-{0}-{1}-{2}-{3}";
        private const string VEHICLE_KEY = "WC.Vehicle-{0}-{1}-{2}-{3}";
        private const string VEHICLE_GETBYIDS_KEY = "WC.Vehicle-getbyids--{0}-{1}-{2}-{3}";
        private const string VEHICLE_GETBYNAMES_KEY = "WC.Vehicle-getbynames--{0}-{1}-{2}-{3}";
        private const string VEHICLE_ID_KEY = "WC.Vehicle-{0}";
        private const string PRICE_RANGE_KEY = "WC.PriceRanges";
        private const string VEHICLE_MAKES_ACCESSORIES_KEY = "Wc.vehicle.makes.accessories";
        private const string VEHICLE_MAKES_MODELS_ACCESSORIES_KEY = "Wc.vehicle.makes.models.accessories";

        #endregion

        #region Fields

        private readonly ICacheManager cacheManager;
        private readonly IRepository<Vehicle> vehicleRepository;
        private readonly IRepository<BaseVehicle> baseVehicleRepository;
        private readonly IRepository<Year> yearRepository;
        private readonly IRepository<ProductYear> productYearRepository;
        private readonly IRepository<Make> makeRepository;
        private readonly IRepository<SeoMakeModel> seoMakeModelsRepository;
        private readonly IRepository<ProductMake> productMakeRepository;
        private readonly IRepository<Model> modelRepository;
        private readonly IRepository<ProductModel> productModelRepository;
        private readonly IRepository<SubModel> subModelRepository;
        private readonly IRepository<ProductSubModel> productSubModelRepository;
        private readonly IRepository<PriceRange> priceRangeRepository;
        private readonly IRepository<ProductVehicle> productVehicleRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<ProductExtra> productExtraRepository;
        private readonly IRepository<StoreMapping> storeMappingRepository;
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<ProductCategory> productCategoryRepository;
        private readonly IRepository<CustomerVehicleGarage> customerVehicleGarageRepository;
        private readonly IStoreContext storeContext;
        private readonly CatalogSettings _catalogSettings;
        private readonly IVehicleHelper vehicleHelper;
        private readonly IDataProvider dataProvider;
        private readonly IDbContext dbContext;
        private readonly CatalogSettings catalogSettings;
        private readonly IRepository<SpecificationAttributeOption> specificationAttributeOptionRepository;
        private readonly IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository;
        private readonly IRepository<OrderExtra> orderExtraRepository;
        private readonly IRepository<PopularMake> popularMakeRepository;
        private readonly IRepository<PopularModel> popularModelRepository;

        #endregion

        #region Ctor

        public VehicleService(ICacheManager cacheManager,
            IRepository<Vehicle> vehicleRepository,
            IRepository<BaseVehicle> baseVehicleRepository,
            IRepository<Year> yearRepository,
            IRepository<ProductYear> productYearRepository,
            IRepository<Make> makeRepository,
            IRepository<SeoMakeModel> seoMakeModelsRepository,
            IRepository<ProductMake> productMakeRepository,
            IRepository<Model> modelRepository,
            IRepository<ProductModel> productModelRepository,
            IRepository<SubModel> subModelRepository,
            IRepository<ProductSubModel> productSubModelRepository,
            IRepository<PriceRange> priceRangeRepository,
            IRepository<ProductVehicle> productVehicleRepository,
            IRepository<Product> productRepository,
            IRepository<ProductExtra> productExtraRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IRepository<Category> categoryRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<CustomerVehicleGarage> customerVehicleGarageRepository,
            IStoreContext storeContext,
            CatalogSettings _catalogSettings,
            IVehicleHelper vehicleHelper,
            IDataProvider dataProvider,
            IDbContext dbContext,
            CatalogSettings catalogSettings,
            IRepository<SpecificationAttributeOption> specificationAttributeOptionRepository,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository,
            IRepository<OrderExtra> orderExtraRepository,
            IRepository<PopularMake> popularMakeRepository,
            IRepository<PopularModel> popularModelRepository)
        {
            this.cacheManager = cacheManager;
            this.vehicleRepository = vehicleRepository;
            this.baseVehicleRepository = baseVehicleRepository;
            this.yearRepository = yearRepository;
            this.productYearRepository = productYearRepository;
            this.makeRepository = makeRepository;
            this.seoMakeModelsRepository = seoMakeModelsRepository;
            this.productMakeRepository = productMakeRepository;
            this.modelRepository = modelRepository;
            this.productModelRepository = productModelRepository;
            this.subModelRepository = subModelRepository;
            this.productSubModelRepository = productSubModelRepository;
            this.priceRangeRepository = priceRangeRepository;
            this.productVehicleRepository = productVehicleRepository;
            this.productRepository = productRepository;
            this.productExtraRepository = productExtraRepository;
            this.storeMappingRepository = storeMappingRepository;
            this.categoryRepository = categoryRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.customerVehicleGarageRepository = customerVehicleGarageRepository;
            this.storeContext = storeContext;
            this._catalogSettings = _catalogSettings;
            this.vehicleHelper = vehicleHelper;
            this.dataProvider = dataProvider;
            this.dbContext = dbContext;
            this.catalogSettings = catalogSettings;
            this.specificationAttributeOptionRepository = specificationAttributeOptionRepository;
            this.productSpecificationAttributeRepository = productSpecificationAttributeRepository;
            this.orderExtraRepository = orderExtraRepository;
            this.popularMakeRepository = popularMakeRepository;
            this.popularModelRepository = popularModelRepository;
        }

        #endregion

        #region Methods

        public IList<Year> GetYears(int? productId = null)
        {
            var key = productId.HasValue ? string.Format(VEHICLE_PRODUCT_YEARS_KEY, productId) : VEHICLE_YEARS_KEY;
            Func<List<Year>> getYears;
            if (productId.HasValue)
            {
                getYears = () =>
                {
                    return this.productYearRepository.TableNoTracking
                        .Where(m => m.ProductId.Equals(productId.Value) && m.YearId >= 1900)
                        .Select(m => m.Year)
                        .OrderByDescending(m => m.Id)
                        .ToList();
                };
            }
            else
            {
                getYears = () =>
                {
                    return this.yearRepository.TableNoTracking
                         .Where(m => m.Id >= 1900)
                         .Distinct()
                         .OrderByDescending(m => m.Id)
                         .ToList();
                };
            }

            return this.cacheManager.Get(key, getYears);
        }

        public IList<Year> GetYearsByMake(int makeId)
        {
            var key = string.Format(VEHICLE_YEARS_BY_MAKE_KEY, makeId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from a in this.yearRepository.TableNoTracking
                            join b in this.baseVehicleRepository.TableNoTracking on a.Id equals b.YearId
                            where b.MakeId == makeId
                            select a;

                return query.Distinct().OrderBy(y => y.Id).ToList();
            });
        }

        public IList<Year> GetYearsByModel(int modelId)
        {
            var key = string.Format(VEHICLE_YEARS_BY_MODEL_KEY, modelId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from a in this.yearRepository.TableNoTracking
                            join b in this.baseVehicleRepository.TableNoTracking on a.Id equals b.YearId
                            where b.ModelId == modelId
                            select a;

                return query.Distinct().OrderBy(y => y.Id).ToList();
            });
        }

        public Make GetMake(int id)
        {
            var key = string.Format(VEHICLE_MAKE_KEY, id);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.makeRepository.TableNoTracking
                            where m.Id == id
                            select m;

                return query.FirstOrDefault();
            });
        }

        public IList<Make> GetMakes(int year)
        {
            var key = string.Format(VEHICLE_MAKES_KEY, year);
            var storeId = this.storeContext.CurrentStore.Id;

            return this.cacheManager.Get(key, () =>
            {
                IQueryable<Make> query;
                var stageQuery = from m in this.makeRepository.TableNoTracking
                                 join bv in this.baseVehicleRepository.TableNoTracking on m.Id equals bv.MakeId
                                 join v in this.vehicleRepository.TableNoTracking on bv.Id equals v.BaseVehicleId
                                 join pv in this.productVehicleRepository.TableNoTracking on v.Id equals pv.VehicleId
                                 join p in this.productRepository.TableNoTracking on pv.ProductId equals p.Id
                                 where m.IsActiveForFilter
                                         && bv.YearId == year
                                         && !p.Deleted
                                         && p.Published
                                         && !p.DisableBuyButton
                                         && (p.StockQuantity > 0 || p.ProductExtra != null && p.ProductExtra.IsShippingFromManufacturer)
                                 select new { Make = m, ProductId = p.Id, p.LimitedToStores };

                if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
                {
                    query = from a in stageQuery
                            join b in this.storeMappingRepository.TableNoTracking
                            on new { EntityId = a.ProductId, EntityName = "Product" } equals new { EntityId = b.EntityId, EntityName = b.EntityName } into temp
                            from b in temp.DefaultIfEmpty()
                            where !a.LimitedToStores || storeId == b.StoreId
                            select a.Make;
                }
                else
                {
                    query = stageQuery.Select(m => m.Make);
                }

                return query.Distinct().OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Make> GetMakes()
        {
            var key = string.Format(VEHICLE_ALL_MAKES_KEY);
            return this.cacheManager.Get(key, () =>
            {
                var query = from a in this.makeRepository.TableNoTracking
                            select a;

                return query.OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Make> GetMakes(int year, int productId)
        {
            var key = string.Format(VEHICLE_PRODUCT_MAKES_KEY, year, productId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.productMakeRepository.TableNoTracking
                            where m.YearId == year && m.ProductId == productId
                            select m.Make;

                return query.OrderBy(m => m.Name).ToList();
            });
        }

        public Model GetModel(int id)
        {
            var key = string.Format(VEHICLE_MODEL_KEY, id);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.modelRepository.TableNoTracking
                            where m.Id == id
                            select m;

                return query.FirstOrDefault();
            });
        }

        public IList<Model> GetModels()
        {
            var key = string.Format(VEHICLE_ALL_MODELS_KEY);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.modelRepository.TableNoTracking
                            select m;

                return query.OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Model> GetModels(int makeId)
        {
            var query = from m in this.modelRepository.TableNoTracking
                        join bv in this.baseVehicleRepository.Table on m.Id equals bv.ModelId
                        where bv.MakeId == makeId
                        select m;

            return query.Distinct().OrderBy(m => m.Name).ToList();
        }

        public IList<Model> GetModels(int year, string make)
        {
            var key = string.Format(VEHICLE_MODELS_KEY, year, make);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.modelRepository.TableNoTracking
                            join bv in this.baseVehicleRepository.TableNoTracking on m.Id equals bv.ModelId
                            join v in this.vehicleRepository.TableNoTracking on bv.Id equals v.BaseVehicleId
                            where bv.YearId == year && bv.Make.Name == make
                            select m;

                return query.Distinct().OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Model> GetModels(int yearId, int makeId)
        {
            var key = string.Format(VEHICLE_MODELS_KEY, yearId, makeId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.modelRepository.TableNoTracking
                            join bv in this.baseVehicleRepository.TableNoTracking on m.Id equals bv.ModelId
                            join v in this.vehicleRepository.TableNoTracking on bv.Id equals v.BaseVehicleId
                            where bv.YearId == yearId && bv.MakeId == makeId
                            select m;

                return query.Distinct().OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Model> GetModels(int yearId, int makeId, int productId)
        {
            var key = string.Format(VEHICLE_PRODUCT_MODELS_KEY, yearId, makeId, productId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.productModelRepository.TableNoTracking
                            where m.YearId == yearId && m.MakeId == makeId && m.ProductId == productId
                            select m.Model;

                return query.OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Year> GetYears(int makeId, int modelId)
        {
            var query = from y in this.yearRepository.TableNoTracking
                        join bv in this.baseVehicleRepository.TableNoTracking on y.Id equals bv.YearId
                        where bv.MakeId == makeId && bv.ModelId == modelId
                        select y;

            return query.Distinct().OrderBy(y => y.Id).ToList();
        }

        public IList<SubModel> GetSubModels(int year, string make, string model)
        {
            var key = string.Format(VEHICLE_SUBMODELS_KEY, year, make, model);
            return this.cacheManager.Get(key, () =>
            {
                var query = from s in this.subModelRepository.TableNoTracking
                            join v in this.vehicleRepository.TableNoTracking on s.Id equals v.SubModelId
                            join bv in this.baseVehicleRepository.TableNoTracking on v.BaseVehicleId equals bv.Id
                            where bv.YearId == year && bv.Make.Name == make && bv.Model.Name == model
                            select s;

                return query.Distinct().OrderBy(s => s.Name).ToList();
            });
        }

        public IList<SubModel> GetSubModels(int yearId, int makeId, int modelId)
        {
            var key = string.Format(VEHICLE_SUBMODELS_KEY, yearId, makeId, modelId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from s in this.subModelRepository.TableNoTracking
                            join v in this.vehicleRepository.TableNoTracking on s.Id equals v.SubModelId
                            join bv in this.baseVehicleRepository.TableNoTracking on v.BaseVehicleId equals bv.Id
                            where bv.YearId == yearId && bv.MakeId == makeId && bv.ModelId == modelId
                            select s;


                return query.Distinct().OrderBy(s => s.Name).ToList();
            });
        }

        public IList<SubModel> GetSubModels(int yearId, int makeId, int modelId, int minProducts)
        {
            var key = string.Format(VEHICLE_SUBMODELS_LIMITED_KEY, yearId, makeId, modelId, minProducts);
            return this.cacheManager.Get(key, () =>
            {
                var query = from s in this.subModelRepository.TableNoTracking
                            join v in this.vehicleRepository.TableNoTracking on s.Id equals v.SubModelId
                            join bv in this.baseVehicleRepository.TableNoTracking on v.BaseVehicleId equals bv.Id
                            join pvm in this.productVehicleRepository.TableNoTracking on v.Id equals pvm.VehicleId
                            where bv.YearId == yearId && bv.MakeId == makeId && bv.ModelId == modelId && s.Id != 1  // 1 = ALL, we except ALL
                            select s;

                return query.GroupBy(s => s).Where(grp => (yearId > (DateTime.UtcNow.Year - 3) || grp.Count() >= minProducts)).Select(grp => grp.Key).OrderBy(s => s.Name).ToList();
            });
        }

        public IList<SubModel> GetProductSubModels(int yearId, int makeId, int modelId, int productId)
        {
            var key = string.Format(VEHICLE_PRODUCT_SUBMODELS_KEY, yearId, makeId, modelId, productId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.productSubModelRepository.TableNoTracking
                            where m.YearId == yearId && m.MakeId == makeId && m.ModelId == modelId && m.ProductId == productId && m.SubModelId != 1  // 1 = ALL, we except ALL
                            select m.SubModel;

                return query.OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Product> GetRelatedProducts(int productId, int yearId, int makeId, int modelId, int? subModelId)
        {
            var key = string.Format(VEHICLE_PRODUCT_GROUPPRODUCTS_KEY, yearId, makeId, modelId, productId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from m in this.productVehicleRepository.TableNoTracking
                            join p in this.productRepository.TableNoTracking on m.ProductId equals p.Id
                            join v in this.vehicleRepository.TableNoTracking on m.VehicleId equals v.Id
                            join bv in this.baseVehicleRepository.TableNoTracking on v.BaseVehicleId equals bv.Id
                            where p.ParentGroupedProductId == productId && p.Published && !p.Deleted && bv.YearId == yearId && bv.MakeId == makeId && bv.ModelId == modelId && (subModelId == null || v.SubModelId == subModelId)
                            select p;

                var vehicleSpecific = query.OrderByDescending(m => m.DisplayOrder).Distinct().ToList();

                if (yearId == 0 && makeId == 0 && modelId == 0)
                {
                    var vehicleMappedProductsIds = productVehicleRepository.TableNoTracking.Select(p => p.ProductId).Distinct();
                    var nonVehicleSpecificProducts = this.productRepository.TableNoTracking.Where(p =>
                        p.ParentGroupedProductId == productId && !p.Deleted && p.Published).ToList();
                    nonVehicleSpecificProducts =
                        nonVehicleSpecificProducts.Where(p => !vehicleMappedProductsIds.Contains(p.Id)).OrderByDescending(p => p.DisplayOrder).ToList();
                    vehicleSpecific.AddRange(nonVehicleSpecificProducts);
                }

                return vehicleSpecific;
            });
        }
        public IList<Product> GetRelatedProducts(int productId, int yearId, int makeId, int modelId,
            int? subModelId, int section, int aspect, int rim)
        {
            var key = string.Format(VEHICLE_PRODUCT_GROUPPRODUCTS_ATTRIBUTE_KEY, yearId, makeId, modelId,
                productId, section, aspect, rim);
            return this.cacheManager.Get(key, () =>
            {
                var products =( from p in this.productRepository.TableNoTracking
                            where p.ParentGroupedProductId == productId && p.Published && !p.Deleted
                            select p).ToList();

                var filteredSpecs = new List<int>() { section, aspect, rim };

                if (filteredSpecs != null && filteredSpecs.Any())
                {

                    var pParentProductId = dataProvider.GetParameter();
                    pParentProductId.ParameterName = "ParentGroupedProductId";
                    pParentProductId.Value = productId;
                    pParentProductId.DbType = DbType.Int32;

                    //prepare parameters
                    var pSpecAttributeOptionIds = dataProvider.GetParameter();
                    pSpecAttributeOptionIds.ParameterName = "FilteredSpecs";
                    pSpecAttributeOptionIds.Value = string.Join(",", filteredSpecs.Select(x => x));
                    pSpecAttributeOptionIds.DbType = DbType.String;


                     products = dbContext.ExecuteStoredProcedureList<Product>("WCS_GetProductsByTire", pParentProductId, pSpecAttributeOptionIds).ToList();

                    #region Linq query

                    //var filteredAttributes = this.specificationAttributeOptionRepository.TableNoTracking
                    //    .Where(sao => filteredSpecs.Contains(sao.Id)).Select(sao => sao.SpecificationAttributeId).Distinct();

                    //query = query.Where(p => !filteredAttributes.Except
                    //    (
                    //        this.specificationAttributeOptionRepository.TableNoTracking.Where(
                    //            sao => p.ProductSpecificationAttributes.Where(
                    //                psa => filteredSpecs.Contains(psa.SpecificationAttributeOptionId))
                    //            .Select(psa => psa.SpecificationAttributeOptionId).Contains(sao.Id))
                    //        .Select(sao => sao.SpecificationAttributeId).Distinct()
                    //    ).Any());

                    #endregion
                }

                if (yearId > 0 && makeId > 0 && modelId > 0)
                {
                    products = (from m in this.productVehicleRepository.TableNoTracking
                            join p in products on m.ProductId equals p.Id
                            join v in this.vehicleRepository.TableNoTracking on m.VehicleId equals v.Id
                            join bv in this.baseVehicleRepository.TableNoTracking on v.BaseVehicleId equals bv.Id
                            where p.ParentGroupedProductId == productId && p.Published && !p.Deleted && bv.YearId == yearId && bv.MakeId == makeId && bv.ModelId == modelId && (subModelId == null || v.SubModelId == subModelId)
                            select p).ToList();
                }
                var vehicleSpecific = products.OrderByDescending(m => m.DisplayOrder).Distinct().ToList();

                //if (yearId == 0 && makeId == 0 && modelId == 0)
                //{
                //    var vehicleMappedProductsIds = productVehicleRepository.TableNoTracking.Select(p => p.ProductId).Distinct();
                //    var nonVehicleSpecificProducts = this.productRepository.TableNoTracking.Where(p =>
                //        p.ParentGroupedProductId == productId && !p.Deleted && p.Published).ToList();
                //    nonVehicleSpecificProducts =
                //        nonVehicleSpecificProducts.Where(p => !vehicleMappedProductsIds.Contains(p.Id)).OrderByDescending(p => p.DisplayOrder).ToList();
                //    vehicleSpecific.AddRange(nonVehicleSpecificProducts);
                //}

                return vehicleSpecific;
            });
        }

        public BaseVehicle GetBaseVehicle(int yearId, int makeId, int modelId)
        {
            return this.baseVehicleRepository.TableNoTracking.FirstOrDefault(i => i.YearId == yearId && i.MakeId == makeId && i.ModelId == modelId);
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            if (vehicleId == 0)
                return null;

            /*var key = string.Format(VEHICLE_ID_KEY, vehicleId);
            return this.cacheManager.Get(key, () => this.vehicleRepository.GetById(vehicleId));*/
            return this.vehicleRepository.GetById(vehicleId);
        }

        public Vehicle GetVehicle(int year, string make, string model, string submodel)
        {
            var key = string.Format(VEHICLE_GETBYNAMES_KEY, year, make, model, submodel);
            return this.cacheManager.Get(key, () =>
            {
                var query = from v in this.vehicleRepository.TableNoTracking
                            join bv in this.baseVehicleRepository.TableNoTracking on v.BaseVehicleId equals bv.Id
                            join y in this.yearRepository.TableNoTracking on bv.YearId equals y.Id
                            join ma in this.makeRepository.TableNoTracking on bv.MakeId equals ma.Id
                            join mo in this.modelRepository.TableNoTracking on bv.ModelId equals mo.Id
                            join s in this.subModelRepository.TableNoTracking on v.SubModelId equals s.Id
                            where y.Id == year && ma.Name == make && mo.Name == model && s.Name == submodel
                            select v;

                return query.FirstOrDefault();
            });
        }

        public Vehicle GetVehicle(int yearId, int makeId, int modelId, int submodelId)
        {
            /*var key = string.Format(VEHICLE_KEY, yearId, makeId, modelId, submodelId);
            return this.cacheManager.Get(key, () =>
            {
                var query = from v in this.vehicleRepository.Table
                            join bv in this.baseVehicleRepository.Table on v.BaseVehicleId equals bv.Id
                            where bv.YearId == yearId && bv.MakeId == makeId && bv.ModelId == modelId && v.SubModelId == submodelId
                            select v;

                return query.FirstOrDefault();
            });*/

            //var key = string.Format(VEHICLE_GETBYIDS_KEY, yearId, makeId, modelId, submodelId);
            //return this.cacheManager.Get(key, () =>
            //{
            var query = from v in this.vehicleRepository.TableNoTracking
                        join bv in this.baseVehicleRepository.TableNoTracking on v.BaseVehicleId equals bv.Id
                        where bv.YearId == yearId && bv.MakeId == makeId && bv.ModelId == modelId && v.SubModelId == submodelId
                        select v;

            return query.FirstOrDefault();
            //});
        }

        public int? SetVehicleToCookies(int yearId, int makeId, int modelId, int submodelId, bool showUniversal)
        {
            var vehicle = this.GetVehicle(yearId, makeId, modelId, submodelId);
            if (vehicle == null)
            {
                return null;
            }

            this.vehicleHelper.SetVehicleToCookies(vehicle, showUniversal);
            return vehicle.Id;
        }

        public void SetVehicleSeoToCookies(int? yearId, int? makeId, int? modelId)
        {
            this.vehicleHelper.SetVehicleSeoIdToCookies(yearId, makeId, modelId);
            string makeName = string.Empty, modelName = string.Empty;
            if (makeId.HasValue)
            {
                var make = this.GetMake(makeId.Value);
                if (make != null)
                {
                    makeName = make.Name;
                }
            }

            if (modelId.HasValue)
            {
                var model = this.GetModel(modelId.Value);
                if (model != null)
                {
                    modelName = model.Name;
                }
            }

            this.vehicleHelper.SetVehicleSeoNameToCookies(yearId, makeName, modelName);
        }

        public bool GetVehicleFromCookies(out int yearId, out int makeId, out int modelId, out int submodelId, out bool showUniversal)
        {
            return this.vehicleHelper.GetVehicleFromCookies(out yearId, out makeId, out modelId, out submodelId, out showUniversal);
        }

        /// <summary>
        /// Get vehicle from cookies
        /// </summary>
        /// <returns>Return null - if there is no vehicle in cookies, VehicleId if there is vehicle in cookies</returns>
        public Vehicle GetVehicleFromCookies()
        {
            int yearId, makeId, modelId, submodelId;
            bool showUniversal;

            if (!this.vehicleHelper.GetVehicleFromCookies(out yearId, out makeId, out modelId, out submodelId, out showUniversal))
            {
                return null;
            }

            var vehicle = this.GetVehicle(yearId, makeId, modelId, submodelId);
            if (vehicle != null)
            {
                vehicle.ShowUniversal = showUniversal;
            }

            return vehicle;
        }

        public void ClearVehicleCookies()
        {
            this.vehicleHelper.ClearVehicleCookies();
        }

        public IList<PriceRange> GetPriceRanges()
        {
            var key = string.Format(PRICE_RANGE_KEY);
            return this.cacheManager.Get(key, () =>
            {
                var query = from pr in this.priceRangeRepository.Table select pr;
                return query.ToList();
            });
        }

        public IList<PriceRange> GetPriceRangesByIds(IList<int> ids)
        {
            var ranges = this.GetPriceRanges();
            return ranges.Where(p => ids.Contains(p.Id)).ToList();
        }

        public IList<int> GetSubcategoryIdsByVehicle(int parentCategoryId, int vehicleId)
        {
            //prepare parameters
            var pParentCategoryId = this.dataProvider.GetParameter();
            pParentCategoryId.ParameterName = "ParentId";
            pParentCategoryId.Value = parentCategoryId;
            pParentCategoryId.DbType = DbType.Int32;

            var pVehicleId = this.dataProvider.GetParameter();
            pVehicleId.ParameterName = "VehicleId";
            pVehicleId.Value = vehicleId;
            pVehicleId.DbType = DbType.Int32;

            //invoke stored procedure
            var subCategories = this.dbContext.ExecuteStoredProcedureList<IdList>(
                "WC_GetCategoriesByVehicle",
                pVehicleId,
                pParentCategoryId);

            var subCategoriesIds = new List<int>();
            foreach (var sub in subCategories)
                if (!subCategoriesIds.Any(x => x == sub.Id))
                    subCategoriesIds.Add(sub.Id);

            return subCategoriesIds;
        }

        public IList<Make> GetMakesActiveForSeo()
        {
            return this.cacheManager.Get(VEHICLE_MAKES_ACCESSORIES_KEY, () =>
            {
                var currentStoreId = storeContext.CurrentStore.Id;
                //var seoMakesToUpdate = from smm in this.seoMakeModelsRepository.TableNoTracking
                //    where smm.StoreId == currentStoreId && smm.IsActive && !smm.ModelId.HasValue
                //    select smm;
                var seoMakesToUpdate = this.seoMakeModelsRepository.TableNoTracking.Where(x =>
                    x.IsActive && x.StoreId == currentStoreId && !x.ModelId.HasValue);

                var query = from m in this.makeRepository.TableNoTracking
                    join u in seoMakesToUpdate on m.Id equals u.MakeId into ps
                    from u in ps.DefaultIfEmpty()
                    where (m.IsActiveForSeo && u == null) || (u != null && !u.Remove)
                    select m;

                return query.OrderBy(x => x.Name).ToList();
            });
        }

        public IList<KeyValuePair<Make, Model>> GetMakeModelsActiveForSeo()
        {
            return this.cacheManager.Get(VEHICLE_MAKES_MODELS_ACCESSORIES_KEY, () =>
            {
                var currentStoreId = storeContext.CurrentStore.Id;
                var seoMakeModelsToUpdate = this.seoMakeModelsRepository.TableNoTracking.Where(x =>
                    x.IsActive && x.StoreId == currentStoreId && x.ModelId.HasValue);

                var baseVehicleSeoToUpdate = from bv in this.baseVehicleRepository.TableNoTracking
                                             join smm in seoMakeModelsToUpdate on bv.ModelId equals smm.ModelId
                                             where bv.MakeId == smm.MakeId
                                             select new { baseVehicleId = bv.Id, seoToRemove = smm.Remove };

                var query = from bv in this.baseVehicleRepository.TableNoTracking
                            join make in this.makeRepository.TableNoTracking on bv.MakeId equals make.Id
                            join model in this.modelRepository.TableNoTracking on bv.ModelId equals model.Id
                            join bvSeo in baseVehicleSeoToUpdate on bv.Id equals bvSeo.baseVehicleId into ps
                            from bvSeo in ps.DefaultIfEmpty()
                            where (model.IsActiveForSeo && bvSeo == null) || (bvSeo != null && !bvSeo.seoToRemove)
                            group bv by new
                            {
                                bv.Make,
                                bv.Model
                            } into grouped
                            select new
                            {
                                grouped.Key.Make,
                                grouped.Key.Model
                            };

                return query.AsEnumerable().Select(x => new KeyValuePair<Make, Model>(x.Make, x.Model)).OrderBy(x => x.Key.Name).ThenBy(x => x.Value.Name).ToList();
            });
        }

        public IList<Category> GetVehicleCategories(int makeId, int? modelId, int? yearId, int storeId)
        {
            /*var topCategoriesQuery = from c in this.categoryRepository.Table where c.ParentCategoryId == 0 select c.Id;
            var query = from c in this.categoryRepository.Table
                join pcm in this.productCategoryRepository.Table on c.Id equals pcm.CategoryId
                join pv in this.productVehicleRepository.Table on pcm.ProductId equals pv.ProductId
                join v in this.vehicleRepository.Table on pv.VehicleId equals v.Id
                join bv in this.baseVehicleRepository.Table on v.BaseVehicleId equals bv.Id
                join make in this.makeRepository.Table on bv.MakeId equals make.Id
                join model in this.modelRepository.Table on bv.ModelId equals model.Id
                join year in this.yearRepository.Table on bv.YearId equals year.Id
                where topCategoriesQuery.Contains(c.ParentCategoryId) 
                    && make.Id == makeId 
                    && (!modelId.HasValue || model.Id == modelId.Value) 
                    && (!yearId.HasValue || year.Id == yearId.Value)
                select c;

            return query.Distinct().ToList();*/

            var pMakeId = this.dataProvider.GetParameter();
            pMakeId.ParameterName = "MakeId";
            pMakeId.Value = makeId;
            pMakeId.DbType = DbType.Int32;

            var pModelId = this.dataProvider.GetParameter();
            pModelId.ParameterName = "ModelId";
            pModelId.Value = modelId.HasValue ? (object)modelId.Value : DBNull.Value;
            pModelId.DbType = DbType.Int32;

            var pYearId = this.dataProvider.GetParameter();
            pYearId.ParameterName = "YearId";
            pYearId.Value = yearId.HasValue ? (object)yearId.Value : DBNull.Value;
            pYearId.DbType = DbType.Int32;

            var pStoreId = this.dataProvider.GetParameter();
            pStoreId.ParameterName = "StoreId";
            pStoreId.Value = storeId;
            pStoreId.DbType = DbType.Int32;

            var pTotalRecords = this.dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecords";
            pTotalRecords.Direction = ParameterDirection.Output;
            pTotalRecords.DbType = DbType.Int32;

            var categories = this.dbContext.ExecuteStoredProcedureList<Category>("WC_GetSeoVehicleCategories", pMakeId, pModelId, pYearId, pStoreId, pTotalRecords);
            return categories;
        }

        // Get Header Categories by product categories from Solr facets (when vehicle selected)
        public IList<HeaderCategories> GetHeaderCategories(int[] ids)
        {
            var dataTable = new DataTable("Categories");
            dataTable.Columns.Add("Id", typeof(int));
            ids.ToList().ForEach(i => dataTable.Rows.Add(i));

            /*var categories = this.dataProvider.GetParameter();
            categories.ParameterName = "Categories";
            categories.Value = dataTable;
            categories.DbType = DbType.Object;*/

            var categories = new SqlParameter("Categories", SqlDbType.Structured)
            {
                Value = dataTable,
                TypeName = "IntArray"
            };

            return this.dbContext.ExecuteStoredProcedureList<HeaderCategories>("WC_GetHeaderCategories", categories);
        }

        public IPagedList<ProductOverview> SearchProducts(
            out IList<int> originalCategoryIds,
            out IList<int> originalManufacturerIds,
            out IList<int> originalPriceRangeIds,
            out IList<int> availableCategoryIds,
            out IList<int> availableManufacturerIds,
            out IList<int> availablePriceRangeIds,
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int vehicleId = 0,
            bool loadUniversalProducts = false,
            bool loadOutStockProducts = false,
            int pageIndex = 0,
            int pageSize = 2147483647,  //Int32.MaxValue
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            IList<int> filteredSpecs = null,
            Core.Domain.Vehicles.ProductSortingEnum orderBy = Core.Domain.Vehicles.ProductSortingEnum.Position,
            bool showHidden = false,
            string filterableCategoryIds = null,
            string filterableManufacturerIds = null,
            string filterablePriceRangeIds = null,
            decimal? filterableMinPrice = null,
            decimal? filterableMaxPrice = null,
            PrimaryFilterEnum filterablePrimaryFilter = PrimaryFilterEnum.None)
        {
            filterableSpecificationAttributeOptionIds = new List<int>();
            originalCategoryIds = new List<int>();
            originalManufacturerIds = new List<int>();
            originalPriceRangeIds = new List<int>();
            availableCategoryIds = new List<int>();
            availableManufacturerIds = new List<int>();
            availablePriceRangeIds = new List<int>();

            //search by keyword
            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            #region Use stored procedure

            //pass category identifiers as comma-delimited string
            string commaSeparatedCategoryIds = "";
            if (categoryIds != null)
            {
                for (int i = 0; i < categoryIds.Count; i++)
                {
                    commaSeparatedCategoryIds += categoryIds[i].ToString(CultureInfo.InvariantCulture);
                    if (i != categoryIds.Count - 1)
                    {
                        commaSeparatedCategoryIds += ",";
                    }
                }
            }

            //pass specification identifiers as comma-delimited string
            string commaSeparatedSpecIds = "";
            if (filteredSpecs != null)
            {
                ((List<int>)filteredSpecs).Sort();
                for (int i = 0; i < filteredSpecs.Count; i++)
                {
                    commaSeparatedSpecIds += filteredSpecs[i].ToString(CultureInfo.InvariantCulture);
                    if (i != filteredSpecs.Count - 1)
                    {
                        commaSeparatedSpecIds += ",";
                    }
                }
            }

            //some databases don't support int.MaxValue
            if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            //prepare parameters
            var pCategoryIds = this.dataProvider.GetParameter();
            pCategoryIds.ParameterName = "CategoryIds";
            pCategoryIds.Value = commaSeparatedCategoryIds;
            pCategoryIds.DbType = DbType.String;

            var pManufacturerId = this.dataProvider.GetParameter();
            pManufacturerId.ParameterName = "ManufacturerId";
            pManufacturerId.Value = manufacturerId;
            pManufacturerId.DbType = DbType.Int32;

            var pStoreId = this.dataProvider.GetParameter();
            pStoreId.ParameterName = "StoreId";
            pStoreId.Value = !this.catalogSettings.IgnoreStoreLimitations ? storeId : 0;
            pStoreId.DbType = DbType.Int32;

            var pVendorId = this.dataProvider.GetParameter();
            pVendorId.ParameterName = "VendorId";
            pVendorId.Value = vendorId;
            pVendorId.DbType = DbType.Int32;

            var pWarehouseId = this.dataProvider.GetParameter();
            pWarehouseId.ParameterName = "WarehouseId";
            pWarehouseId.Value = warehouseId;
            pWarehouseId.DbType = DbType.Int32;

            var pProductTypeId = this.dataProvider.GetParameter();
            pProductTypeId.ParameterName = "ProductTypeId";
            pProductTypeId.Value = productType.HasValue ? (object)productType.Value : DBNull.Value;
            pProductTypeId.DbType = DbType.Int32;

            var pVisibleIndividuallyOnly = this.dataProvider.GetParameter();
            pVisibleIndividuallyOnly.ParameterName = "VisibleIndividuallyOnly";
            pVisibleIndividuallyOnly.Value = visibleIndividuallyOnly;
            pVisibleIndividuallyOnly.DbType = DbType.Int32;

            var pFeaturedProducts = this.dataProvider.GetParameter();
            pFeaturedProducts.ParameterName = "FeaturedProducts";
            pFeaturedProducts.Value = featuredProducts.HasValue ? (object)featuredProducts.Value : DBNull.Value;
            pFeaturedProducts.DbType = DbType.Boolean;

            var pKeywords = this.dataProvider.GetParameter();
            pKeywords.ParameterName = "Keywords";
            pKeywords.Value = keywords != null ? (object)keywords : DBNull.Value;
            pKeywords.DbType = DbType.String;

            var pSearchDescriptions = this.dataProvider.GetParameter();
            pSearchDescriptions.ParameterName = "SearchDescriptions";
            pSearchDescriptions.Value = searchDescriptions;
            pSearchDescriptions.DbType = DbType.Boolean;

            var pSearchSku = this.dataProvider.GetParameter();
            pSearchSku.ParameterName = "SearchSku";
            pSearchSku.Value = searchSku;
            pSearchSku.DbType = DbType.Boolean;

            var pFilteredSpecs = this.dataProvider.GetParameter();
            pFilteredSpecs.ParameterName = "FilteredSpecs";
            pFilteredSpecs.Value = commaSeparatedSpecIds != null ? (object)commaSeparatedSpecIds : DBNull.Value;
            pFilteredSpecs.DbType = DbType.String;

            var pOrderBy = this.dataProvider.GetParameter();
            pOrderBy.ParameterName = "OrderBy";
            pOrderBy.Value = (int)orderBy;
            pOrderBy.DbType = DbType.Int32;

            var pPageIndex = this.dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = this.dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            var pShowHidden = this.dataProvider.GetParameter();
            pShowHidden.ParameterName = "ShowHidden";
            pShowHidden.Value = showHidden;
            pShowHidden.DbType = DbType.Boolean;

            var pVehicleId = this.dataProvider.GetParameter();
            pVehicleId.ParameterName = "VehicleId";
            pVehicleId.Value = vehicleId;
            pVehicleId.DbType = DbType.Int32;

            var pLoadUniversalProducts = this.dataProvider.GetParameter();
            pLoadUniversalProducts.ParameterName = "LoadUniversalProducts";
            pLoadUniversalProducts.Value = loadUniversalProducts;
            pLoadUniversalProducts.DbType = DbType.Boolean;

            var pLoadOutStockProducts = this.dataProvider.GetParameter();
            pLoadOutStockProducts.ParameterName = "LoadOutStockProducts";
            pLoadOutStockProducts.Value = loadOutStockProducts;
            pLoadOutStockProducts.DbType = DbType.Boolean;

            var pFilterablePrimaryFilter = this.dataProvider.GetParameter();
            pFilterablePrimaryFilter.ParameterName = "FilterablePrimaryFilter";
            pFilterablePrimaryFilter.Value = (int)filterablePrimaryFilter;
            pFilterablePrimaryFilter.DbType = DbType.Int32;

            var pFilterableCategoryIds = this.dataProvider.GetParameter();
            pFilterableCategoryIds.ParameterName = "FilterableCategoryIds";
            pFilterableCategoryIds.Value = string.IsNullOrEmpty(filterableCategoryIds) ? DBNull.Value : (object)filterableCategoryIds;
            pFilterableCategoryIds.DbType = DbType.String;

            var pFilterableManufacturerIds = this.dataProvider.GetParameter();
            pFilterableManufacturerIds.ParameterName = "FilterableManufacturerIds";
            pFilterableManufacturerIds.Value = string.IsNullOrEmpty(filterableManufacturerIds) ? DBNull.Value : (object)filterableManufacturerIds;
            pFilterableManufacturerIds.DbType = DbType.String;

            var pFilterablePriceRangeIds = this.dataProvider.GetParameter();
            pFilterablePriceRangeIds.ParameterName = "FilterablePriceRangeIds";
            pFilterablePriceRangeIds.Value = string.IsNullOrEmpty(filterablePriceRangeIds) ? DBNull.Value : (object)filterablePriceRangeIds;
            pFilterablePriceRangeIds.DbType = DbType.String;

            var pFilterableMinPrice = this.dataProvider.GetParameter();
            pFilterableMinPrice.ParameterName = "FilterableMinPrice";
            pFilterableMinPrice.Value = filterableMinPrice.HasValue ? (object)filterableMinPrice.Value : DBNull.Value;
            pFilterableMinPrice.DbType = DbType.Decimal;

            var pFilterableMaxPrice = this.dataProvider.GetParameter();
            pFilterableMaxPrice.ParameterName = "FilterableMaxPrice";
            pFilterableMaxPrice.Value = filterableMaxPrice.HasValue ? (object)filterableMaxPrice.Value : DBNull.Value;
            pFilterableMaxPrice.DbType = DbType.Decimal;

            var pLoadFilterableSpecificationAttributeOptionIds = this.dataProvider.GetParameter();
            pLoadFilterableSpecificationAttributeOptionIds.ParameterName = "LoadFilterableSpecificationAttributeOptionIds";
            pLoadFilterableSpecificationAttributeOptionIds.Value = loadFilterableSpecificationAttributeOptionIds;
            pLoadFilterableSpecificationAttributeOptionIds.DbType = DbType.Boolean;

            var pFilterableSpecificationAttributeOptionIds = this.dataProvider.GetParameter();
            pFilterableSpecificationAttributeOptionIds.ParameterName = "FilterableSpecificationAttributeOptionIds";
            pFilterableSpecificationAttributeOptionIds.Direction = ParameterDirection.Output;
            pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
            pFilterableSpecificationAttributeOptionIds.DbType = DbType.String;

            var pOriginalCategoryIds = this.dataProvider.GetParameter();
            pOriginalCategoryIds.ParameterName = "OriginalCategoryIds";
            pOriginalCategoryIds.Direction = ParameterDirection.Output;
            pOriginalCategoryIds.Size = int.MaxValue - 1;
            pOriginalCategoryIds.DbType = DbType.String;

            var pOriginalManufacturerIds = this.dataProvider.GetParameter();
            pOriginalManufacturerIds.ParameterName = "OriginalManufacturerIds";
            pOriginalManufacturerIds.Direction = ParameterDirection.Output;
            pOriginalManufacturerIds.Size = int.MaxValue - 1;
            pOriginalManufacturerIds.DbType = DbType.String;

            var pOriginalPriceRangeIds = this.dataProvider.GetParameter();
            pOriginalPriceRangeIds.ParameterName = "OriginalPriceRangeIds";
            pOriginalPriceRangeIds.Direction = ParameterDirection.Output;
            pOriginalPriceRangeIds.Size = int.MaxValue - 1;
            pOriginalPriceRangeIds.DbType = DbType.String;

            var pAvailableCategoryIds = this.dataProvider.GetParameter();
            pAvailableCategoryIds.ParameterName = "AvailableCategoryIds";
            pAvailableCategoryIds.Direction = ParameterDirection.Output;
            pAvailableCategoryIds.Size = int.MaxValue - 1;
            pAvailableCategoryIds.DbType = DbType.String;

            var pAvailableManufacturerIds = this.dataProvider.GetParameter();
            pAvailableManufacturerIds.ParameterName = "AvailableManufacturerIds";
            pAvailableManufacturerIds.Direction = ParameterDirection.Output;
            pAvailableManufacturerIds.Size = int.MaxValue - 1;
            pAvailableManufacturerIds.DbType = DbType.String;

            var pAvailablePriceRangeIds = this.dataProvider.GetParameter();
            pAvailablePriceRangeIds.ParameterName = "AvailablePriceRangeIds";
            pAvailablePriceRangeIds.Direction = ParameterDirection.Output;
            pAvailablePriceRangeIds.Size = int.MaxValue - 1;
            pAvailablePriceRangeIds.DbType = DbType.String;

            var pTotalRecords = this.dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecords";
            pTotalRecords.Direction = ParameterDirection.Output;
            pTotalRecords.DbType = DbType.Int32;

            //invoke stored procedure
            var products = this.dbContext.ExecuteStoredProcedureList<ProductOverview>(
                "WC_ProductLoadAllPaged",
                pCategoryIds,
                pManufacturerId,
                pStoreId,
                pVendorId,
                pWarehouseId,
                pProductTypeId,
                pVisibleIndividuallyOnly,
                pFeaturedProducts,
                pKeywords,
                pSearchDescriptions,
                pSearchSku,
                pFilteredSpecs,
                pOrderBy,
                pPageIndex,
                pPageSize,
                pShowHidden,
                pLoadFilterableSpecificationAttributeOptionIds,
                pFilterableSpecificationAttributeOptionIds,
                pVehicleId,
                pLoadUniversalProducts,
                pLoadOutStockProducts,
                pFilterablePrimaryFilter,
                pFilterableCategoryIds,
                pFilterableManufacturerIds,
                pFilterablePriceRangeIds,
                pFilterableMinPrice,
                pFilterableMaxPrice,
                pOriginalCategoryIds,
                pOriginalManufacturerIds,
                pOriginalPriceRangeIds,
                pAvailableCategoryIds,
                pAvailableManufacturerIds,
                pAvailablePriceRangeIds,
                pTotalRecords);

            //get filterable specification attribute option identifier
            string filterableSpecificationAttributeOptionIdsStr = (pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value) ? (string)pFilterableSpecificationAttributeOptionIds.Value : "";
            if (loadFilterableSpecificationAttributeOptionIds && !string.IsNullOrWhiteSpace(filterableSpecificationAttributeOptionIdsStr))
            {
                filterableSpecificationAttributeOptionIds = filterableSpecificationAttributeOptionIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var originalCategoryIdsStr = (pOriginalCategoryIds.Value != DBNull.Value) ? (string)pOriginalCategoryIds.Value : "";
            if (!string.IsNullOrWhiteSpace(originalCategoryIdsStr))
            {
                originalCategoryIds = originalCategoryIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var originalManufacturerIdsStr = (pOriginalManufacturerIds.Value != DBNull.Value) ? (string)pOriginalManufacturerIds.Value : "";
            if (!string.IsNullOrWhiteSpace(originalManufacturerIdsStr))
            {
                originalManufacturerIds = originalManufacturerIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var originalPriceRangeIdsStr = (pOriginalPriceRangeIds.Value != DBNull.Value) ? (string)pOriginalPriceRangeIds.Value : "";
            if (!string.IsNullOrWhiteSpace(originalPriceRangeIdsStr))
            {
                originalPriceRangeIds = originalPriceRangeIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var availableCategoryIdsStr = (pAvailableCategoryIds.Value != DBNull.Value) ? (string)pAvailableCategoryIds.Value : "";
            if (!string.IsNullOrWhiteSpace(availableCategoryIdsStr))
            {
                availableCategoryIds = availableCategoryIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var availableManufacturerIdsStr = (pAvailableManufacturerIds.Value != DBNull.Value) ? (string)pAvailableManufacturerIds.Value : "";
            if (!string.IsNullOrWhiteSpace(availableManufacturerIdsStr))
            {
                availableManufacturerIds = availableManufacturerIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var availablePriceRangeIdsStr = (pAvailablePriceRangeIds.Value != DBNull.Value) ? (string)pAvailablePriceRangeIds.Value : "";
            if (!string.IsNullOrWhiteSpace(availablePriceRangeIdsStr))
            {
                availablePriceRangeIds = availablePriceRangeIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            //return products
            var totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return new PagedList<ProductOverview>(products, pageIndex, pageSize, totalRecords);

            #endregion
        }

        public IPagedList<ProductOverview> VehicleSearchProducts(
            out IList<int> originalCategoryIds,
            out IList<int> originalManufacturerIds,
            out IList<int> originalPriceRangeIds,
            out IList<int> availableCategoryIds,
            out IList<int> availableManufacturerIds,
            out IList<int> availablePriceRangeIds,
            out IList<int> filterableSpecificationAttributeOptionIds,
            int makeId,
            int? modelId = null,
            int? yearId = null,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            bool loadUniversalProducts = false,
            bool loadOutStockProducts = false,
            int pageIndex = 0,
            int pageSize = 2147483647,  //Int32.MaxValue
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            IList<int> filteredSpecs = null,
            Core.Domain.Vehicles.ProductSortingEnum orderBy = Core.Domain.Vehicles.ProductSortingEnum.Position,
            bool showHidden = false,
            string filterableCategoryIds = null,
            string filterableManufacturerIds = null,
            string filterablePriceRangeIds = null,
            decimal? filterableMinPrice = null,
            decimal? filterableMaxPrice = null,
            PrimaryFilterEnum filterablePrimaryFilter = PrimaryFilterEnum.None)
        {
            filterableSpecificationAttributeOptionIds = new List<int>();
            originalCategoryIds = new List<int>();
            originalManufacturerIds = new List<int>();
            originalPriceRangeIds = new List<int>();
            availableCategoryIds = new List<int>();
            availableManufacturerIds = new List<int>();
            availablePriceRangeIds = new List<int>();

            //search by keyword
            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            #region Use stored procedure

            //pass category identifiers as comma-delimited string
            string commaSeparatedCategoryIds = "";
            if (categoryIds != null)
            {
                for (int i = 0; i < categoryIds.Count; i++)
                {
                    commaSeparatedCategoryIds += categoryIds[i].ToString(CultureInfo.InvariantCulture);
                    if (i != categoryIds.Count - 1)
                    {
                        commaSeparatedCategoryIds += ",";
                    }
                }
            }

            //pass specification identifiers as comma-delimited string
            string commaSeparatedSpecIds = "";
            if (filteredSpecs != null)
            {
                ((List<int>)filteredSpecs).Sort();
                for (int i = 0; i < filteredSpecs.Count; i++)
                {
                    commaSeparatedSpecIds += filteredSpecs[i].ToString(CultureInfo.InvariantCulture);
                    if (i != filteredSpecs.Count - 1)
                    {
                        commaSeparatedSpecIds += ",";
                    }
                }
            }

            //some databases don't support int.MaxValue
            if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            //prepare parameters
            var pCategoryIds = this.dataProvider.GetParameter();
            pCategoryIds.ParameterName = "CategoryIds";
            pCategoryIds.Value = commaSeparatedCategoryIds;
            pCategoryIds.DbType = DbType.String;

            var pManufacturerId = this.dataProvider.GetParameter();
            pManufacturerId.ParameterName = "ManufacturerId";
            pManufacturerId.Value = manufacturerId;
            pManufacturerId.DbType = DbType.Int32;

            var pStoreId = this.dataProvider.GetParameter();
            pStoreId.ParameterName = "StoreId";
            pStoreId.Value = !this.catalogSettings.IgnoreStoreLimitations ? storeId : 0;
            pStoreId.DbType = DbType.Int32;

            var pVendorId = this.dataProvider.GetParameter();
            pVendorId.ParameterName = "VendorId";
            pVendorId.Value = vendorId;
            pVendorId.DbType = DbType.Int32;

            var pWarehouseId = this.dataProvider.GetParameter();
            pWarehouseId.ParameterName = "WarehouseId";
            pWarehouseId.Value = warehouseId;
            pWarehouseId.DbType = DbType.Int32;

            var pProductTypeId = this.dataProvider.GetParameter();
            pProductTypeId.ParameterName = "ProductTypeId";
            pProductTypeId.Value = productType.HasValue ? (object)productType.Value : DBNull.Value;
            pProductTypeId.DbType = DbType.Int32;

            var pVisibleIndividuallyOnly = this.dataProvider.GetParameter();
            pVisibleIndividuallyOnly.ParameterName = "VisibleIndividuallyOnly";
            pVisibleIndividuallyOnly.Value = visibleIndividuallyOnly;
            pVisibleIndividuallyOnly.DbType = DbType.Int32;

            var pFeaturedProducts = this.dataProvider.GetParameter();
            pFeaturedProducts.ParameterName = "FeaturedProducts";
            pFeaturedProducts.Value = featuredProducts.HasValue ? (object)featuredProducts.Value : DBNull.Value;
            pFeaturedProducts.DbType = DbType.Boolean;

            var pKeywords = this.dataProvider.GetParameter();
            pKeywords.ParameterName = "Keywords";
            pKeywords.Value = keywords != null ? (object)keywords : DBNull.Value;
            pKeywords.DbType = DbType.String;

            var pSearchDescriptions = this.dataProvider.GetParameter();
            pSearchDescriptions.ParameterName = "SearchDescriptions";
            pSearchDescriptions.Value = searchDescriptions;
            pSearchDescriptions.DbType = DbType.Boolean;

            var pSearchSku = this.dataProvider.GetParameter();
            pSearchSku.ParameterName = "SearchSku";
            pSearchSku.Value = searchSku;
            pSearchSku.DbType = DbType.Boolean;

            var pFilteredSpecs = this.dataProvider.GetParameter();
            pFilteredSpecs.ParameterName = "FilteredSpecs";
            pFilteredSpecs.Value = commaSeparatedSpecIds != null ? (object)commaSeparatedSpecIds : DBNull.Value;
            pFilteredSpecs.DbType = DbType.String;

            var pOrderBy = this.dataProvider.GetParameter();
            pOrderBy.ParameterName = "OrderBy";
            pOrderBy.Value = (int)orderBy;
            pOrderBy.DbType = DbType.Int32;

            var pPageIndex = this.dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = this.dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            var pShowHidden = this.dataProvider.GetParameter();
            pShowHidden.ParameterName = "ShowHidden";
            pShowHidden.Value = showHidden;
            pShowHidden.DbType = DbType.Boolean;

            var pMakeId = this.dataProvider.GetParameter();
            pMakeId.ParameterName = "MakeId";
            pMakeId.Value = makeId;
            pMakeId.DbType = DbType.Int32;

            var pModelId = this.dataProvider.GetParameter();
            pModelId.ParameterName = "ModelId";
            pModelId.Value = modelId.HasValue ? (object)modelId.Value : DBNull.Value;
            pModelId.DbType = DbType.Int32;

            var pYearId = this.dataProvider.GetParameter();
            pYearId.ParameterName = "YearId";
            pYearId.Value = yearId.HasValue ? (object)yearId.Value : DBNull.Value;
            pYearId.DbType = DbType.Int32;

            var pLoadUniversalProducts = this.dataProvider.GetParameter();
            pLoadUniversalProducts.ParameterName = "LoadUniversalProducts";
            pLoadUniversalProducts.Value = loadUniversalProducts;
            pLoadUniversalProducts.DbType = DbType.Boolean;

            var pLoadOutStockProducts = this.dataProvider.GetParameter();
            pLoadOutStockProducts.ParameterName = "LoadOutStockProducts";
            pLoadOutStockProducts.Value = loadOutStockProducts;
            pLoadOutStockProducts.DbType = DbType.Boolean;

            var pFilterablePrimaryFilter = this.dataProvider.GetParameter();
            pFilterablePrimaryFilter.ParameterName = "FilterablePrimaryFilter";
            pFilterablePrimaryFilter.Value = (int)filterablePrimaryFilter;
            pFilterablePrimaryFilter.DbType = DbType.Int32;

            var pFilterableCategoryIds = this.dataProvider.GetParameter();
            pFilterableCategoryIds.ParameterName = "FilterableCategoryIds";
            pFilterableCategoryIds.Value = string.IsNullOrEmpty(filterableCategoryIds) ? DBNull.Value : (object)filterableCategoryIds;
            pFilterableCategoryIds.DbType = DbType.String;

            var pFilterableManufacturerIds = this.dataProvider.GetParameter();
            pFilterableManufacturerIds.ParameterName = "FilterableManufacturerIds";
            pFilterableManufacturerIds.Value = string.IsNullOrEmpty(filterableManufacturerIds) ? DBNull.Value : (object)filterableManufacturerIds;
            pFilterableManufacturerIds.DbType = DbType.String;

            var pFilterablePriceRangeIds = this.dataProvider.GetParameter();
            pFilterablePriceRangeIds.ParameterName = "FilterablePriceRangeIds";
            pFilterablePriceRangeIds.Value = string.IsNullOrEmpty(filterablePriceRangeIds) ? DBNull.Value : (object)filterablePriceRangeIds;
            pFilterablePriceRangeIds.DbType = DbType.String;

            var pFilterableMinPrice = this.dataProvider.GetParameter();
            pFilterableMinPrice.ParameterName = "FilterableMinPrice";
            pFilterableMinPrice.Value = filterableMinPrice.HasValue ? (object)filterableMinPrice.Value : DBNull.Value;
            pFilterableMinPrice.DbType = DbType.Decimal;

            var pFilterableMaxPrice = this.dataProvider.GetParameter();
            pFilterableMaxPrice.ParameterName = "FilterableMaxPrice";
            pFilterableMaxPrice.Value = filterableMaxPrice.HasValue ? (object)filterableMaxPrice.Value : DBNull.Value;
            pFilterableMaxPrice.DbType = DbType.Decimal;

            var pLoadFilterableSpecificationAttributeOptionIds = this.dataProvider.GetParameter();
            pLoadFilterableSpecificationAttributeOptionIds.ParameterName = "LoadFilterableSpecificationAttributeOptionIds";
            pLoadFilterableSpecificationAttributeOptionIds.Value = loadFilterableSpecificationAttributeOptionIds;
            pLoadFilterableSpecificationAttributeOptionIds.DbType = DbType.Boolean;

            var pFilterableSpecificationAttributeOptionIds = this.dataProvider.GetParameter();
            pFilterableSpecificationAttributeOptionIds.ParameterName = "FilterableSpecificationAttributeOptionIds";
            pFilterableSpecificationAttributeOptionIds.Direction = ParameterDirection.Output;
            pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
            pFilterableSpecificationAttributeOptionIds.DbType = DbType.String;

            var pOriginalCategoryIds = this.dataProvider.GetParameter();
            pOriginalCategoryIds.ParameterName = "OriginalCategoryIds";
            pOriginalCategoryIds.Direction = ParameterDirection.Output;
            pOriginalCategoryIds.Size = int.MaxValue - 1;
            pOriginalCategoryIds.DbType = DbType.String;

            var pOriginalManufacturerIds = this.dataProvider.GetParameter();
            pOriginalManufacturerIds.ParameterName = "OriginalManufacturerIds";
            pOriginalManufacturerIds.Direction = ParameterDirection.Output;
            pOriginalManufacturerIds.Size = int.MaxValue - 1;
            pOriginalManufacturerIds.DbType = DbType.String;

            var pOriginalPriceRangeIds = this.dataProvider.GetParameter();
            pOriginalPriceRangeIds.ParameterName = "OriginalPriceRangeIds";
            pOriginalPriceRangeIds.Direction = ParameterDirection.Output;
            pOriginalPriceRangeIds.Size = int.MaxValue - 1;
            pOriginalPriceRangeIds.DbType = DbType.String;

            var pAvailableCategoryIds = this.dataProvider.GetParameter();
            pAvailableCategoryIds.ParameterName = "AvailableCategoryIds";
            pAvailableCategoryIds.Direction = ParameterDirection.Output;
            pAvailableCategoryIds.Size = int.MaxValue - 1;
            pAvailableCategoryIds.DbType = DbType.String;

            var pAvailableManufacturerIds = this.dataProvider.GetParameter();
            pAvailableManufacturerIds.ParameterName = "AvailableManufacturerIds";
            pAvailableManufacturerIds.Direction = ParameterDirection.Output;
            pAvailableManufacturerIds.Size = int.MaxValue - 1;
            pAvailableManufacturerIds.DbType = DbType.String;

            var pAvailablePriceRangeIds = this.dataProvider.GetParameter();
            pAvailablePriceRangeIds.ParameterName = "AvailablePriceRangeIds";
            pAvailablePriceRangeIds.Direction = ParameterDirection.Output;
            pAvailablePriceRangeIds.Size = int.MaxValue - 1;
            pAvailablePriceRangeIds.DbType = DbType.String;

            var pTotalRecords = this.dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecords";
            pTotalRecords.Direction = ParameterDirection.Output;
            pTotalRecords.DbType = DbType.Int32;

            //invoke stored procedure
            var products = this.dbContext.ExecuteStoredProcedureList<ProductOverview>(
                "WC_ProductLoadAllPagedSeo",
                pCategoryIds,
                pManufacturerId,
                pStoreId,
                pVendorId,
                pWarehouseId,
                pProductTypeId,
                pVisibleIndividuallyOnly,
                pFeaturedProducts,
                pKeywords,
                pSearchDescriptions,
                pSearchSku,
                pFilteredSpecs,
                pOrderBy,
                pPageIndex,
                pPageSize,
                pShowHidden,
                pLoadFilterableSpecificationAttributeOptionIds,
                pFilterableSpecificationAttributeOptionIds,
                pMakeId,
                pModelId,
                pYearId,
                pLoadUniversalProducts,
                pLoadOutStockProducts,
                pFilterablePrimaryFilter,
                pFilterableCategoryIds,
                pFilterableManufacturerIds,
                pFilterablePriceRangeIds,
                pFilterableMinPrice,
                pFilterableMaxPrice,
                pOriginalCategoryIds,
                pOriginalManufacturerIds,
                pOriginalPriceRangeIds,
                pAvailableCategoryIds,
                pAvailableManufacturerIds,
                pAvailablePriceRangeIds,
                pTotalRecords);

            //get filterable specification attribute option identifier
            string filterableSpecificationAttributeOptionIdsStr = (pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value) ? (string)pFilterableSpecificationAttributeOptionIds.Value : "";
            if (loadFilterableSpecificationAttributeOptionIds && !string.IsNullOrWhiteSpace(filterableSpecificationAttributeOptionIdsStr))
            {
                filterableSpecificationAttributeOptionIds = filterableSpecificationAttributeOptionIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var originalCategoryIdsStr = (pOriginalCategoryIds.Value != DBNull.Value) ? (string)pOriginalCategoryIds.Value : "";
            if (!string.IsNullOrWhiteSpace(originalCategoryIdsStr))
            {
                originalCategoryIds = originalCategoryIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var originalManufacturerIdsStr = (pOriginalManufacturerIds.Value != DBNull.Value) ? (string)pOriginalManufacturerIds.Value : "";
            if (!string.IsNullOrWhiteSpace(originalManufacturerIdsStr))
            {
                originalManufacturerIds = originalManufacturerIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var originalPriceRangeIdsStr = (pOriginalPriceRangeIds.Value != DBNull.Value) ? (string)pOriginalPriceRangeIds.Value : "";
            if (!string.IsNullOrWhiteSpace(originalPriceRangeIdsStr))
            {
                originalPriceRangeIds = originalPriceRangeIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var availableCategoryIdsStr = (pAvailableCategoryIds.Value != DBNull.Value) ? (string)pAvailableCategoryIds.Value : "";
            if (!string.IsNullOrWhiteSpace(availableCategoryIdsStr))
            {
                availableCategoryIds = availableCategoryIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var availableManufacturerIdsStr = (pAvailableManufacturerIds.Value != DBNull.Value) ? (string)pAvailableManufacturerIds.Value : "";
            if (!string.IsNullOrWhiteSpace(availableManufacturerIdsStr))
            {
                availableManufacturerIds = availableManufacturerIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            var availablePriceRangeIdsStr = (pAvailablePriceRangeIds.Value != DBNull.Value) ? (string)pAvailablePriceRangeIds.Value : "";
            if (!string.IsNullOrWhiteSpace(availablePriceRangeIdsStr))
            {
                availablePriceRangeIds = availablePriceRangeIdsStr
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x.Trim()))
                    .ToList();
            }

            //return products
            var totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return new PagedList<ProductOverview>(products, pageIndex, pageSize, totalRecords);

            #endregion
        }

        public IList<Make> GetPopularMakes(int year)
        {
            var key = string.Format(VEHICLE_POPULAR_MAKES_KEY, year);
            var storeId = this.storeContext.CurrentStore.Id;

            return this.cacheManager.Get(key, () =>
            {
                var query = this.popularMakeRepository.TableNoTracking
                    .Where(m => m.Year == year)
                    .Join(this.makeRepository.TableNoTracking, a => a.MakeId, b => b.Id, (a, b) => b);

                return query.Distinct().OrderBy(m => m.Name).ToList();
            });
        }

        public IList<Model> GetPopularModels(int year, int makeId)
        {
            var key = string.Format(VEHICLE_POPULAR_MODELS_KEY, year, makeId);
            var storeId = this.storeContext.CurrentStore.Id;

            return this.cacheManager.Get(key, () =>
            {
                var query = this.popularModelRepository.TableNoTracking
                    .Where(m => m.Year == year && m.MakeId == makeId)
                    .Join(this.modelRepository.TableNoTracking, a => a.ModelId, b => b.Id, (a, b) => b);

                return query.Distinct().OrderBy(m => m.Name).ToList();
            });
        }

        public void AddVehicleToCustomerGarage(int vehicleId, int customerId, bool isMain = false)
        {
            if (this.customerVehicleGarageRepository.TableNoTracking.Where(x => x.CustomerId == customerId && x.VehicleId == vehicleId).Count() > 0)
                return;

            var garageValue = new CustomerVehicleGarage { CustomerId = customerId, VehicleId = vehicleId, IsMain = isMain };
            this.customerVehicleGarageRepository.Insert(garageValue);
        }

        public void ClearCustomerGarage(int customerId)
        {
            var customerVehicles = this.customerVehicleGarageRepository.Table.Where(x => x.CustomerId == customerId).ToList();
            this.customerVehicleGarageRepository.Delete(customerVehicles);
        }

        /// <summary>
        /// Remove Vehicle From Customer Garage. If the vehicle was main then set the next one as main.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="vehicleId"></param>
        /// <returns>Returns main customer vehicle</returns>
        public Vehicle RemoveVehicleFromCustomerGarage(int customerId, int vehicleId)
        {
            var customerVehicleToRemove = this.customerVehicleGarageRepository.Table.Where(x => x.CustomerId == customerId && x.VehicleId == vehicleId).ToList();
            this.customerVehicleGarageRepository.Delete(customerVehicleToRemove);

            return this.customerVehicleGarageRepository.TableNoTracking.FirstOrDefault(x => x.CustomerId == customerId && x.IsMain)?.Vehicle;
        }

        public void SetNoMainVehicleGarage(int customerId)
        {
            var customerMainVehicle = this.customerVehicleGarageRepository.Table.FirstOrDefault(x => x.CustomerId == customerId && x.IsMain == true);
            if (customerMainVehicle != null)
            {
                customerMainVehicle.IsMain = false;
                this.customerVehicleGarageRepository.Update(customerMainVehicle);
            }
        }

        public void UpdateMainVehicleGarage(int vehicleId, int customerId)
        {
            var mainVehicle = this.customerVehicleGarageRepository.Table.FirstOrDefault(x => x.CustomerId == customerId && x.IsMain == true);
            var vehicleToMain = this.customerVehicleGarageRepository.Table.FirstOrDefault(x => x.CustomerId == customerId && x.IsMain == false && x.VehicleId == vehicleId);
            if (vehicleToMain != null)
            {
                vehicleToMain.IsMain = true;
                this.customerVehicleGarageRepository.Update(vehicleToMain);
                if (mainVehicle != null)
                {
                    mainVehicle.IsMain = false;
                    this.customerVehicleGarageRepository.Update(mainVehicle);
                }
            }
        }

        #endregion
    }
}