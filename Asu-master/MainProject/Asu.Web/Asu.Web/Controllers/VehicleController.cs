using Asu.Core;
using Asu.Core.Caching;
using Asu.Core.Domain.BannerPicture;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Media;
//using Asu.Core.Domain.Solr;
using Asu.Core.Domain.Vehicles;
using Asu.Core.Infrastructure;
using Asu.Services.BannerPicture;
using Asu.Services.Catalog;
using Asu.Services.Common;
using Asu.Services.Customers;
using Asu.Services.Customization;
using Asu.Services.Directory;
using Asu.Services.Localization;
using Asu.Services.Media;
using Asu.Services.Orders;
using Asu.Services.Security;
using Asu.Services.Seo;
//using Asu.Services.Solr;
using Asu.Services.Stores;
using Asu.Services.Vehicles;
using Asu.Web.Extensions;
using Asu.Framework.Security;
using Asu.Framework.UI;
using Asu.Web.Infrastructure.Cache;
using Asu.Web.Models.BannerPicture;
using Asu.Web.Models.Catalog;
using Asu.Web.Models.Home;
using Asu.Web.Models.Media;
using Asu.Web.Models.Vehicles;
//using SolrNet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.WebPages;
namespace Asu.Web.Controllers
{
    public class VehicleController : BasePublicController
    {
        //private const int CacheTime = 60 * 24 * 10; // 10 days
        //private const string CATEGORIES_BY_ID_LIST_KEY = "Wc.cat.by.id.list-{0}";
        //private const string MANUFACTURERS_BY_ID_LIST_KEY = "Wc.man.by.id.list-{0}";
        //private const string MANUFACTURERS_ENHANCED_CATEGORIES_KEY = "WC.Manufacturer.Categories.Id.List-{0}";
        //private const string PRICES_BY_ID_LIST_KEY = "Wc.prices.by.id.list-{0}";
        //private const string SUBCATEGORIES_ID_LIST_KEY = "Wc.subcategories.id.list-{0}";
        //private const string VEHICLE_CATEGORIES_MODEL_KEY = "Wc.vehicle.categories.model-{0}-{1}-{2}";
        //private const string VEHICLE_CATEGORY_MODEL_KEY = "Wc.vehicle.category.model-{0}-{1}-{2}-{3}";
        //private const string VEHICLE_TOP_MENU_KEY = "WC.Vehicle.TopMenu-{0}";
        //private const string MAKE_MODELS = "WC.Vehicle.Make.Models";
        //private const string CATEGORIES_ALL_KEY = "WC.Categories.All.List";
        //private const string MANUFACTURERS_ALL_KEY = "WC.Manufacturers.All.List";
        //private const string SPEC_ATTRIBUTES_ALL_KEY = "WC.SpecAttributes";
        //private const int sectionSpecificationAttributeId = 15336;
        //private const int aspectSpecificationAttributeId = 14528;
        //private const int rimSpecificationAttributeId = 9197;
        //private readonly int[] attributeIDs = { 15336, 14528, 9197, 1994, 15496, 15497, 15502, 14686, 15501, 6525, 13295, 11419 };

        //private readonly string[] tireSpecificationAttributeNames = { "Section", "Aspect", "Rim" };
        //private readonly Dictionary<string, string> tireCategoryNameMappings = new Dictionary<string, string>
        //{
        //    { "HP", "High Performance"},
        //    { "HPLT", "High Performance Light Truck"},
        //    { "HT", "Highway Terrain"},
        //    { "LT", "Light Truck"},
        //    { "MM", "MM"},
        //    { "UHP", "Ultra High Performance"},
        //    { "P", "Passenger Car"},
        //    { "MC", "Motorcycle"},
        //    { "XL", "Extra Load" },
        //    { "OTR", "Off-the-road" },
        //    { "ST", "Special Trailer" }
        //};

        ///// <summary>
        ///// Key for banner picture caching
        ///// </summary>
        ///// <remarks>
        ///// {0} : entity id
        ///// {1} : Entity Type name
        ///// {2} : store id
        ///// {3} : Banner Ids
        ///// </remarks>
        //public const string BANNER_PICTURE_MODEL_KEY = "Nop.banner.picture-{0}-{1}-{2}-{3}";

        //#region Fields

        //private readonly IVehicleService vehicleService;
        //private readonly ICategoryService categoryService;
        //private readonly IManufacturerService manufacturerService;
        //private readonly IProductService productService;
        //private readonly IProductGroupService productGroupService;
        //private readonly IPictureService pictureService;
        //private readonly IDigitalDataService digitalDataService;
        //private readonly ICategoryTemplateService categoryTemplateService;
        //private readonly IManufacturerTemplateService manufacturerTemplateService;
        //private readonly IGenericAttributeService genericAttributeService;
        //private readonly ICurrencyService currencyService;
        //private readonly ILocalizationService localizationService;
        //private readonly IAclService aclService;
        //private readonly IStoreMappingService storeMappingService;
        //private readonly IPermissionService permissionService;
        //private readonly IPriceCalculationService priceCalculationService;
        //private readonly IUrlRecordService urlRecordService;
        //private readonly IPriceFormatter priceFormatter;
        //private readonly IWorkContext workContext;
        //private readonly IStoreContext storeContext;
        //private readonly IWebHelper webHelper;
        //private readonly ICacheManager staticCacheManager;

        //private readonly CatalogSettings catalogSettings;
        //private readonly MediaSettings mediaSettings;

        ////private readonly ISolrService solrService;
        //private readonly IGoogleTagManagerService googleTagManagerService;
        //private readonly ISpecificationAttributeService specificationAttributeService;
        //private readonly IBannerService _bannerService;

        //private IPagedList<Category> AllCategoriesCached => this.staticCacheManager.Get(CATEGORIES_ALL_KEY, CacheTime, () => this.categoryService.GetAllCategories());

        //#endregion

        //#region Ctor

        //public VehicleController(IVehicleService vehicleService,
        //    ICategoryService categoryService,
        //    IManufacturerService manufacturerService,
        //    IProductService productService,
        //    IProductGroupService productGroupService,
        //    IPictureService pictureService,
        //    IDigitalDataService digitalDataService,
        //    ICategoryTemplateService categoryTemplateService,
        //    IManufacturerTemplateService manufacturerTemplateService,
        //    IGenericAttributeService genericAttributeService,
        //    ICurrencyService currencyService,
        //    ILocalizationService localizationService,
        //    IAclService aclService,
        //    IStoreMappingService storeMappingService,
        //    IPermissionService permissionService,
        //    IPriceCalculationService priceCalculationService,
        //    IUrlRecordService urlRecordService,
        //    IPriceFormatter priceFormatter,
        //    IWorkContext workContext,
        //    IStoreContext storeContext,
        //    IWebHelper webHelper,
        //    CatalogSettings catalogSettings,
        //    MediaSettings mediaSettings,
        //    //ISolrService solrService,
        //    IGoogleTagManagerService googleTagManagerService,
        //    ISpecificationAttributeService specificationAttributeService,
        //    IBannerService bannerService)
        //{
        //    this.vehicleService = vehicleService;
        //    this.categoryService = categoryService;
        //    this.manufacturerService = manufacturerService;
        //    this.productService = productService;
        //    this.productGroupService = productGroupService;
        //    this.pictureService = pictureService;
        //    this.digitalDataService = digitalDataService;
        //    this.categoryTemplateService = categoryTemplateService;
        //    this.manufacturerTemplateService = manufacturerTemplateService;
        //    this.genericAttributeService = genericAttributeService;
        //    this.currencyService = currencyService;
        //    this.localizationService = localizationService;
        //    this.aclService = aclService;
        //    this.storeMappingService = storeMappingService;
        //    this.permissionService = permissionService;
        //    this.priceCalculationService = priceCalculationService;
        //    this.urlRecordService = urlRecordService;
        //    this.priceFormatter = priceFormatter;
        //    this.storeContext = storeContext;
        //    this.workContext = workContext;
        //    this.webHelper = webHelper;
        //    this.catalogSettings = catalogSettings;
        //    this.mediaSettings = mediaSettings;
        //    //this.staticCacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");
        //    //this.solrService = solrService;
        //    this.googleTagManagerService = googleTagManagerService;
        //    this.specificationAttributeService = specificationAttributeService;
        //    _bannerService = bannerService;
        //}

        //#endregion

        //#region Public methods

        //[HttpPost]
        //public JsonResult Years(int? productId = null)
        //{
        //    var years = productId.HasValue ? this.vehicleService.GetYears(productId.Value) : this.vehicleService.GetYears();
        //    var model = years.Select(i => new { Id = i.Id, Name = i.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
        //    model.Insert(0, new { Id = 0, Name = "Select Year" });
        //    return this.Json(model);
        //}

        //[HttpPost]
        //public JsonResult Makes(int year, int? productId = null)
        //{
        //    if (year == 0)
        //    {
        //        return this.Json(new { Id = 0, Name = "Select Make" });
        //    }

        //    var makes = productId.HasValue ? this.vehicleService.GetMakes(year, productId.Value) : this.vehicleService.GetMakes(year);


        //    if (this.storeContext.CurrentStore.Id == (int)NopStore.Cycleplicity)
        //    {
        //        makes = makes.Where(m => m.VehicleTypeGroupId != 2).ToList();
        //    }

        //    var model = makes
        //       .Select(i => new { i.Id, i.Name, Type = 1 })
        //       .ToList();

        //    if (!productId.HasValue && this.storeContext.CurrentStore.Id != (int)NopStore.Cycleplicity)
        //    {
        //        var popularMakes = this.vehicleService.GetPopularMakes(year);
        //        model = model
        //            .Union(popularMakes.Select(i => new { i.Id, i.Name, Type = 2 }))
        //            .ToList();
        //    }

        //    model.Insert(0, new { Id = 0, Name = "Select Make", Type = 0 });

        //    return this.Json(model);
        //}

        //[HttpPost]
        //public JsonResult Models(int yearId, int makeId, int? productId = null)
        //{
        //    if (yearId == 0 || makeId == 0)
        //    {
        //        return this.Json(new { Id = 0, Name = "Select Model" });
        //    }

        //    var models = productId.HasValue ? this.vehicleService.GetModels(yearId, makeId, productId.Value) : this.vehicleService.GetModels(yearId, makeId);
        //    if (models.Any(m => m.IsActiveForFilter))
        //    {
        //        models = models.Where(m => m.IsActiveForFilter).ToList();
        //    }

        //    if (this.storeContext.CurrentStore.Id == (int)NopStore.Cycleplicity)
        //    {
        //        models = models.Where(m => m.VehicleTypeGroupId != 2).ToList();
        //    }

        //    var model = models
        //       .Select(i => new { i.Id, i.Name, Type = 1 })
        //       .ToList();

        //    if (!productId.HasValue && this.storeContext.CurrentStore.Id != (int)NopStore.Cycleplicity)
        //    {
        //        var popularModels = this.vehicleService.GetPopularModels(yearId, makeId);
        //        model = model.Union(popularModels.Select(i => new { i.Id, i.Name, Type = 2 }))
        //        .ToList();
        //    }

        //    model.Insert(0, new { Id = 0, Name = "Select Model", Type = 0 });

        //    return this.Json(model);
        //}

        //[HttpPost]
        //public JsonResult SubModels(int yearId, int makeId, int modelId, int? productId = null)
        //{
        //    if (yearId == 0 || makeId == 0 || modelId == 0)
        //    {
        //        return this.Json(new { Id = 0, Name = "Select Submodel" });
        //    }

        //    var subModels = productId.HasValue ? this.vehicleService.GetProductSubModels(yearId, makeId, modelId, productId.Value) : this.vehicleService.GetSubModels(yearId, makeId, modelId, 20);
        //    var model = subModels.Select(i => new { i.Id, i.Name }).ToList();

        //    model.Insert(0, new { Id = 0, Name = "Select Submodel" });


        //    return this.Json(model);
        //}

        //[HttpPost]
        //public JsonResult SetVehicle(int yearId, int makeId, int modelId, int subModelId, bool showUniversal = false)
        //{
        //    var vehicleId = this.vehicleService.SetVehicleToCookies(yearId, makeId, modelId, subModelId, showUniversal);
        //    if (vehicleId != null)
        //    {
        //        this.vehicleService.AddVehicleToCustomerGarage((int)vehicleId, workContext.CurrentCustomer.Id, true);
        //    }

        //    IList<HeaderCategories> headerCategories = new List<HeaderCategories>();
        //    if (vehicleId.HasValue)
        //    {
        //        var key = string.Format(VEHICLE_TOP_MENU_KEY, vehicleId);
        //        headerCategories = this.staticCacheManager.Get(key, () =>
        //        {
        //            var vehicleCategories = this.solrService.GetVehicleCategories(vehicleId.Value);
        //            return this.vehicleService.GetHeaderCategories(vehicleCategories);
        //        });
        //    }


        //    return this.Json(new { topMenu = this.RenderPartialViewToString("TopMenu", headerCategories) });
        //}

        //[HttpPost]
        //public JsonResult ClearVehicle()
        //{
        //    this.vehicleService.ClearVehicleCookies();
        //    this.vehicleService.SetNoMainVehicleGarage(workContext.CurrentCustomer.Id);
        //    return this.Json(new { topMenu = this.RenderPartialViewToString("TopMenu", new List<HeaderCategories>()) });
        //}

        //[HttpPost]
        //public JsonResult ClearGarageVehicles()
        //{
        //    this.vehicleService.ClearCustomerGarage(workContext.CurrentCustomer.Id);
        //    return this.Json(new { topMenu = this.RenderPartialViewToString("TopMenu", new List<HeaderCategories>()) });
        //}

        //[HttpPost]
        //public JsonResult RemoveGarageVehicleById(int vehicleId)
        //{
        //    var mainVehicleId = this.vehicleService.RemoveVehicleFromCustomerGarage(workContext.CurrentCustomer.Id, vehicleId);

        //    IList<HeaderCategories> headerCategories = new List<HeaderCategories>();
        //    if (mainVehicleId != null)
        //    {
        //        var key = string.Format(VEHICLE_TOP_MENU_KEY, vehicleId);
        //        headerCategories = this.staticCacheManager.Get(key, () =>
        //        {
        //            var vehicleCategories = this.solrService.GetVehicleCategories(mainVehicleId.Id);
        //            return this.vehicleService.GetHeaderCategories(vehicleCategories);
        //        });
        //    }

        //    return this.Json(new { topMenu = this.RenderPartialViewToString("TopMenu", headerCategories) });
        //}

        //[HttpPost]
        //public JsonResult SetMainGarageVehicle(int vehicleId)
        //{
        //    this.vehicleService.UpdateMainVehicleGarage(vehicleId, workContext.CurrentCustomer.Id);

        //    IList<HeaderCategories> headerCategories = new List<HeaderCategories>();
        //    var key = string.Format(VEHICLE_TOP_MENU_KEY, vehicleId);
        //    headerCategories = this.staticCacheManager.Get(key, () =>
        //    {
        //        var vehicleCategories = this.solrService.GetVehicleCategories(vehicleId);
        //        return this.vehicleService.GetHeaderCategories(vehicleCategories);
        //    });

        //    return this.Json(new { topMenu = this.RenderPartialViewToString("TopMenu", headerCategories) });
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //[NopHttpsRequirement(SslRequirement.No)]
        //[ValidateInput(false)]
        //public JsonResult FilterSearch(FilterSearchModel model)
        //{
        //    var jsonModel = new FilterSearchModel.FilterSearchJsonModel();
        //    if (model != null)
        //    {

        //        if (!string.IsNullOrWhiteSpace(model.SearchTerms) && model.SearchTerms.Length >= this.catalogSettings.ProductSearchTermMinimumLength || model.CId > 0 || model.MId > 0)
        //        {
        //            model = this.SolrSearch(model);
        //            this.googleTagManagerService.SetEcommerceImpressions(model.ToImpressions());

        //            jsonModel.DataLayerPush = this.googleTagManagerService.GetDataLayerPushScript();
        //            jsonModel.ProductsHtml = this.RenderPartialViewToString("_FilterProductsList", model.Products);
        //            jsonModel.FilterHtml = this.RenderPartialViewToString("_FilterCheckBoxes", model);
        //            jsonModel.TotalProducts = model.TotalProducts;
        //            jsonModel.PageNumber = model.PFC.PageNumber;
        //            jsonModel.HasNextPage = model.PFC.HasNextPage;
        //            jsonModel.HasPreviousPage = model.PFC.HasPreviousPage;
        //            jsonModel.PagerHtml = this.RenderPartialViewToString("_FilterPager", model.PFC);
        //            jsonModel.VehicleMessageHtml = this.RenderPartialViewToString("_VehicleMessage", model.V);
        //            jsonModel.AsideFiltersHtml = this.RenderPartialViewToString("_AsideFilters", model);
        //            jsonModel.ResultsRangeHtml = GetResultsRangeLabel(model.TotalProducts, jsonModel.PageNumber);
        //        }
        //    }

        //    var result = this.Json(jsonModel);
        //    result.MaxJsonLength = int.MaxValue;
        //    return result;
        //}

        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //[NopHttpsRequirement(SslRequirement.No)]
        //[ValidateInput(false)]
        //public ActionResult SearchPage(FilterSearchModel model)
        //{
        //    this.googleTagManagerService.SetPage(Core.Domain.GoogleTagManager.PageType.Search, Core.Domain.GoogleTagManager.GroupingPageType.SearchPages);

        //    if (model != null)
        //    {
        //        if (string.IsNullOrWhiteSpace(model.SearchTerms) || model.SearchTerms.Length < this.catalogSettings.ProductSearchTermMinimumLength)
        //        {
        //            model.Warning = string.Format(this.localizationService.GetResource("Search.SearchTermMinimumLengthIsNCharacters"), this.catalogSettings.ProductSearchTermMinimumLength);
        //        }
        //        else
        //        {
        //            var viewModel = this.SolrSearch(model);
        //            if (viewModel.FilterManufacturers.Any(i => i.Name.Equals(viewModel.SearchTerms, StringComparison.InvariantCultureIgnoreCase)))
        //            {
        //                return this.RedirectToRoute("Manufacturer", new { viewModel.FilterManufacturers.First(i => i.Name.Equals(viewModel.SearchTerms, StringComparison.InvariantCultureIgnoreCase)).SeName });
        //            }

        //            viewModel.PageType = "Search Results";
        //            this.googleTagManagerService.SetEcommerceImpressions(viewModel.ToImpressions());
        //            return this.View(model);
        //        }
        //    }
        //    else
        //    {
        //        model = new FilterSearchModel();
        //    }

        //    return this.View(model);
        //}

        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //[NopHttpsRequirement(SslRequirement.No)]
        //[ValidateInput(false)]
        //public ActionResult Tires(FilterSearchModel model)
        //{
        //    this.googleTagManagerService.SetPage(Core.Domain.GoogleTagManager.PageType.Tires, Core.Domain.GoogleTagManager.GroupingPageType.SearchPages);
        //    model.PageType = "Tires";
        //    var viewModel = this.SolrSearch(model);
        //    viewModel.TireConfigurator = new TireConfiguratorModel();
        //    viewModel.PageType = model.PageType;
        //    this.googleTagManagerService.SetEcommerceImpressions(viewModel.ToImpressions());

        //    return this.View(model);
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //public ActionResult Category(int categoryId, FilterSearchModel filter, int? manufacturerId = null)
        //{
        //    var category = this.categoryService.GetCategoryById(categoryId);
        //    if (category == null || category.Deleted || !category.Published)
        //        return InvokeHttp404();

        //    //Store mapping
        //    var manufacturer = manufacturerId.HasValue ? this.manufacturerService.GetManufacturerById(manufacturerId.Value) : null;
        //    if (!this.storeMappingService.Authorize(category) | (manufacturer != null && !this.storeMappingService.Authorize(manufacturer)))
        //    {
        //        return this.InvokeHttp404();
        //    }

        //    //'Continue shopping' URL
        //    this.genericAttributeService.SaveAttribute(this.workContext.CurrentCustomer,
        //        SystemCustomerAttributeNames.LastContinueShoppingPage,
        //        this.webHelper.GetThisPageUrl(false),
        //        this.storeContext.CurrentStore.Id);

        //    var model = category.ToCustomModel(manufacturer);
        //    if (manufacturerId.HasValue)
        //    {
        //        model.CategoryManufacturerSeName = SeoExtensions.GetSeName(categoryId, manufacturerId.Value, "CategoryManufacturer", 0);
        //    }

        //    //category breadcrumb
        //    model.DisplayBreadcrumb = this.catalogSettings.CategoryBreadcrumbEnabled;
        //    if (model.DisplayBreadcrumb)
        //    {
        //        string cacheKey = $"Nop.category.breadcumb-{category.Id}-{false}-{storeContext.CurrentStore.Id}-{string.Join(",", workContext.CurrentCustomer.CustomerRoles.Select(x => x.Id))}";

        //        model.Breadcrumb = staticCacheManager.Get(cacheKey, () => category.GetBreadCrumb(this.categoryService, this.aclService, this.storeMappingService));
        //    }

        //    //template
        //    var templateCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_TEMPLATE_MODEL_KEY, category.CategoryTemplateId);
        //    var templateViewPath = this.staticCacheManager.Get(templateCacheKey, () =>
        //    {
        //        var template = this.categoryTemplateService.GetCategoryTemplateById(category.CategoryTemplateId);
        //        if (template == null)
        //            template = this.categoryTemplateService.GetAllCategoryTemplates().FirstOrDefault();
        //        if (template == null)
        //            throw new Exception("No default template could be loaded");
        //        return template.ViewPath;
        //    });

        //    //if root category
        //    if (category.ParentCategoryId == 0)
        //    {
        //        filter.IsRootCategoryPageRequested = true;
        //    }

        //    //Search products
        //    model.FilterModel = filter;
        //    model.FilterModel.CId = categoryId;
        //    /*if (model.FilterModel.SearchCategoryIdsArray.Count == 0)
        //    {
        //        model.FilterModel.SearchCategoryIdsArray = this.GetChildCategoryIds(categoryId).ToList();
        //    }*/
        //    model.FilterModel = this.SolrSearch(model.FilterModel);

        //    model.FilterModel.PageType = manufacturerId > 0 ? "CategoryManufacturer Results" : "Category Results";
        //    this.googleTagManagerService.SetEcommerceImpressions(model.FilterModel.ToImpressions());
        //    this.googleTagManagerService.SetProductIds(model.FilterModel.Products.Select(i => i.Id).Take(3).ToArray());

        //    #region banner

        //    model.BannerPictureModel = PrepareBannerModel(category.Id, BannerEntityType.CATEGORY, category.Name);

        //    #endregion

        //    this.googleTagManagerService.SetPage(Core.Domain.GoogleTagManager.PageType.Category, Core.Domain.GoogleTagManager.GroupingPageType.SearchPages);
        //    this.googleTagManagerService.SetCategoryData(category);

        //    return this.View(templateViewPath, model);
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //public ActionResult VehicleAccessories(int makeId, int? modelId, int? yearId)
        //{
        //    var viewModel = new VehicleAccessoriesModel();
        //    viewModel.VehicleSeoModel = this.urlRecordService.GetVehicleUrlRecord(0, "Accessories", yearId, makeId, modelId).ToModel();
        //    var make = this.vehicleService.GetMake(makeId);
        //    Model model = null;

        //    if (!modelId.HasValue)
        //    {
        //        viewModel.Vehicles = this.vehicleService.GetModels(makeId).Select(x => new KeyValuePair<Make, Model>(make, x)).ToList().ToModel("Accessories", "Parts and Accessories");
        //    }
        //    else if (!yearId.HasValue)
        //    {
        //        model = this.vehicleService.GetModel(modelId.Value);
        //        viewModel.Vehicles = this.vehicleService.GetYears(makeId, modelId.Value).Select(x => new Tuple<Make, Model, Year>(make, model, x)).ToList()
        //            .ToModel("Accessories", "Parts and Accessories").OrderByDescending(x => x.VehicleName).ToList();
        //    }

        //    //this.vehicleService.ClearVehicleCookies();

        //    viewModel.Categories = this.staticCacheManager.Get(string.Format(VEHICLE_CATEGORIES_MODEL_KEY, makeId, modelId, yearId), () =>
        //    {
        //        var vehicleCategories = this.vehicleService.GetVehicleCategories(makeId, modelId, yearId, this.storeContext.CurrentStore.Id);
        //        var subCategories = vehicleCategories
        //            .GroupBy(x => x.ParentCategoryId)
        //            .Join(vehicleCategories, a => a.Key, b => b.Id, (a, b) => b)
        //            .Select(x => x);

        //        var categories = vehicleCategories.Except(subCategories);

        //        return subCategories.Select(x =>
        //        {
        //            return this.staticCacheManager.Get(string.Format(VEHICLE_CATEGORY_MODEL_KEY, x.Id, makeId, modelId, yearId), () =>
        //            {
        //                var vehicleName = viewModel.VehicleSeoModel.VehicleName;
        //                var subCatModel = new VehicleAccessoriesModel.CategoryModel
        //                {
        //                    Id = x.Id,
        //                    Name = $"{vehicleName} {x.GetLocalized(y => y.Name)}",
        //                    SeName = x.GetVehicleSeName(makeId, modelId, yearId)
        //                };

        //                subCatModel.ChildCategories = categories.Where(c => c.ParentCategoryId == x.Id).Select(c => new VehicleAccessoriesModel.CategoryModel
        //                {
        //                    Id = c.Id,
        //                    Name = $"{vehicleName} {c.GetLocalized(y => y.Name)}",
        //                    SeName = c.GetVehicleSeName(makeId, modelId, yearId)
        //                })
        //                .ToList();

        //                return subCatModel;
        //            });
        //        });
        //    }).ToList();

        //    this.googleTagManagerService.SetPage(Core.Domain.GoogleTagManager.PageType.Vehicle, Core.Domain.GoogleTagManager.GroupingPageType.SearchPages);

        //    return View("VehicleTemplate.Accessories", viewModel);
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //public ActionResult VehicleCategory(int categoryId, FilterSearchModel filter, int makeId, int? modelId, int? yearId)
        //{
        //    var category = this.categoryService.GetCategoryById(categoryId);
        //    if (category == null || category.Deleted || !category.Published)
        //        return InvokeHttp404();

        //    filter.CategoryUrl = category.GetSeName();
        //    filter.V.Make = makeId;
        //    filter.V.Model = modelId.HasValue ? modelId.Value : 0;
        //    filter.V.Year = yearId.HasValue ? yearId.Value : 0;
        //    filter.V.Seo = true;

        //    this.vehicleService.ClearVehicleCookies();
        //    this.vehicleService.SetVehicleSeoToCookies(yearId, makeId, modelId);

        //    //'Continue shopping' URL
        //    this.genericAttributeService.SaveAttribute(this.workContext.CurrentCustomer,
        //        SystemCustomerAttributeNames.LastContinueShoppingPage,
        //        this.webHelper.GetThisPageUrl(false),
        //        this.storeContext.CurrentStore.Id);

        //    var model = category.ToCustomModel();
        //    model.IsVehicleSeoCategory = true;
        //    model.VehicleSeName = category.GetVehicleSeName(makeId, modelId, yearId);
        //    var vehicleName = this.vehicleService.GetMake(makeId).Name;
        //    if (modelId.HasValue)
        //        vehicleName += " " + this.vehicleService.GetModel(modelId.Value).Name;
        //    if (yearId.HasValue)
        //        vehicleName = yearId.Value + " " + vehicleName;

        //    model.Name = model.MetaTitle = model.MetaDescription = model.MetaKeywords = vehicleName + " " + model.Name;

        //    //category breadcrumb
        //    model.DisplayBreadcrumb = this.catalogSettings.CategoryBreadcrumbEnabled;
        //    if (model.DisplayBreadcrumb)
        //    {
        //        model.Breadcrumb = category.GetBreadCrumb(this.categoryService, this.aclService, this.storeMappingService);
        //    }

        //    //template
        //    var templateCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_TEMPLATE_MODEL_KEY, category.CategoryTemplateId);
        //    var templateViewPath = this.staticCacheManager.Get(templateCacheKey, () =>
        //    {
        //        var template = this.categoryTemplateService.GetCategoryTemplateById(category.CategoryTemplateId);
        //        if (template == null)
        //            template = this.categoryTemplateService.GetAllCategoryTemplates().FirstOrDefault();
        //        if (template == null)
        //            throw new Exception("No default template could be loaded");
        //        return template.ViewPath;
        //    });

        //    //if root category
        //    if (category.ParentCategoryId == 0)
        //    {
        //        var customerRolesIds = this.workContext.CurrentCustomer.CustomerRoles.Where(cr => cr.Active).Select(cr => cr.Id).ToList();
        //        //subcategories
        //        string subCategoriesCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_SUBCATEGORIES_KEY,
        //            categoryId,
        //            string.Join(",", customerRolesIds),
        //            this.storeContext.CurrentStore.Id,
        //            this.workContext.WorkingLanguage.Id,
        //            this.webHelper.IsCurrentConnectionSecured());

        //        model.SubCategories = this.staticCacheManager.Get(subCategoriesCacheKey, () => this.categoryService.GetAllCategoriesByParentCategoryId(categoryId).Select(x =>
        //        {
        //            var subCatModel = new CategoryModel.SubCategoryModel
        //            {
        //                Id = x.Id,
        //                Name = x.GetLocalized(y => y.Name),
        //                SeName = x.GetSeName()
        //            };

        //            //prepare picture model
        //            int pictureSize = this.mediaSettings.CategoryThumbPictureSize;
        //            var categoryPictureCacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_PICTURE_MODEL_KEY, x.Id, pictureSize, true, this.workContext.WorkingLanguage.Id,
        //                this.webHelper.IsCurrentConnectionSecured(), this.storeContext.CurrentStore.Id);

        //            subCatModel.PictureModel = this.staticCacheManager.Get(categoryPictureCacheKey, () =>
        //            {
        //                var picture = this.pictureService.GetPictureById(x.PictureId);
        //                var pictureModel = new PictureModel
        //                {
        //                    FullSizeImageUrl = this.pictureService.GetPictureUrl(picture),
        //                    ImageUrl = this.pictureService.GetPictureUrl(picture, pictureSize),
        //                    Title = string.Format(this.localizationService.GetResource("Media.Category.ImageLinkTitleFormat"), subCatModel.Name),
        //                    AlternateText = string.Format(this.localizationService.GetResource("Media.Category.ImageAlternateTextFormat"), subCatModel.Name)
        //                };
        //                return pictureModel;
        //            });

        //            return subCatModel;
        //        }).ToList());

        //        var vehicle = this.vehicleService.GetVehicleFromCookies();
        //        if (vehicle != null && vehicle.Id > 0)
        //        {
        //            var categoryList = GetSubcategoriesIdByVehicle(category.Id, vehicle.Id);
        //            var selectedCategories = new List<CategoryModel.SubCategoryModel>();
        //            foreach (var sub in model.SubCategories)
        //            {
        //                if (categoryList.Any(x => x == sub.Id))
        //                    selectedCategories.Add(sub);
        //            }
        //            model.SubCategories = selectedCategories;
        //        }

        //        return View(templateViewPath, model);
        //    }

        //    //Search products
        //    model.FilterModel = filter;
        //    model.FilterModel.CId = categoryId;
        //    /*if (model.FilterModel.SearchCategoryIdsArray.Count == 0)
        //    {
        //        model.FilterModel.SearchCategoryIdsArray = this.GetChildCategoryIds(categoryId).ToList();
        //    }*/

        //    model.FilterModel = this.SolrSearch(model.FilterModel);//this.VehicleSearch(model.FilterModel);
        //    model.FilterModel.PageType = "VehicleCategory Results";
        //    this.googleTagManagerService.SetPage(Core.Domain.GoogleTagManager.PageType.Vehicle, Core.Domain.GoogleTagManager.GroupingPageType.SearchPages);
        //    this.googleTagManagerService.SetEcommerceImpressions(model.FilterModel.ToImpressions());

        //    return View(templateViewPath, model);
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //public ActionResult CategoryManufacturer(int categoryId, int manufacturerId, FilterSearchModel filter)
        //{
        //    if (manufacturerId > 0 && !filter.SelectedManufacturerIdsArray.Contains(manufacturerId))
        //    {
        //        filter.SelectedManufacturerIdsArray.Add(manufacturerId);
        //        filter.PF = PrimaryFilterEnum.Manufacturer;
        //    }

        //    return this.Category(categoryId, filter, manufacturerId > 0 ? manufacturerId : (int?)null);
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //public ActionResult Manufacturer(int manufacturerId, FilterSearchModel filter, int? categoryId)
        //{
        //    var manufacturer = this.manufacturerService.GetManufacturerById(manufacturerId);
        //    if (manufacturer == null || manufacturer.Deleted || !manufacturer.Published)
        //        return InvokeHttp404();

        //    //Store mapping
        //    var category = categoryId.HasValue ? this.categoryService.GetCategoryById(categoryId.Value) : null;
        //    if (!this.storeMappingService.Authorize(manufacturer) | (category != null && !this.storeMappingService.Authorize(category)))
        //    {
        //        return this.InvokeHttp404();
        //    }


        //    this.googleTagManagerService.SetPage(Core.Domain.GoogleTagManager.PageType.Manufacturer, Core.Domain.GoogleTagManager.GroupingPageType.SearchPages);
        //    this.googleTagManagerService.SetManufacturerData(manufacturer);

        //    //Check whether the current user has a "Manage catalog" permission
        //    //It allows him to preview a manufacturer before publishing
        //    if (!manufacturer.Published && !this.permissionService.Authorize(StandardPermissionProvider.ManageManufacturers))
        //        return InvokeHttp404();

        //    //ACL (access control list)
        //    if (!this.aclService.Authorize(manufacturer))
        //        return InvokeHttp404();

        //    //'Continue shopping' URL
        //    this.genericAttributeService.SaveAttribute(this.workContext.CurrentCustomer,
        //        SystemCustomerAttributeNames.LastContinueShoppingPage,
        //        this.webHelper.GetThisPageUrl(false),
        //        this.storeContext.CurrentStore.Id);

        //    var model = manufacturer.ToCustomModel(category);
        //    if (categoryId.HasValue)
        //    {
        //        model.ManufacturerCategorySeName = SeoExtensions.GetSeName(manufacturerId, categoryId.Value, "ManufacturerCategory", 0);
        //    }

        //    //template
        //    var templateCacheKey = string.Format(ModelCacheEventConsumer.MANUFACTURER_TEMPLATE_MODEL_KEY, manufacturer.ManufacturerTemplateId);
        //    var templateViewPath = this.staticCacheManager.Get(templateCacheKey, () =>
        //    {
        //        var template = this.manufacturerTemplateService.GetManufacturerTemplateById(manufacturer.ManufacturerTemplateId);
        //        if (template == null)
        //            template = this.manufacturerTemplateService.GetAllManufacturerTemplates().FirstOrDefault();
        //        if (template == null)
        //            throw new Exception("No default template could be loaded");
        //        return template.ViewPath;
        //    });

        //    model.FilterModel = filter;
        //    model.FilterModel.MId = manufacturerId;
        //    model.FilterModel.PageType = categoryId > 0 ? "ManufacturerCategory Results" : "Manufacturer Results";
        //    model.FilterModel.IsManufacturerPageRequested = !categoryId.HasValue || categoryId.Value == 0;
        //    model.FilterModel = this.SolrSearch(model.FilterModel);
        //    this.googleTagManagerService.SetEcommerceImpressions(model.FilterModel.ToImpressions());
        //    this.googleTagManagerService.SetProductIds(model.FilterModel.Products.Select(i => i.Id).Take(3).ToArray());

        //    #region Banners

        //    model.BannerPictureModel = PrepareBannerModel(manufacturer.Id, BannerEntityType.MANUFACTURER, manufacturer.Name);

        //    #endregion

        //    return View(templateViewPath, model);
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //public ActionResult ManufacturerCategory(int manufacturerId, int categoryId, FilterSearchModel filter)
        //{
        //    if (categoryId > 0 && !filter.SelectedCategoryIdsArray.Contains(categoryId))
        //    {
        //        filter.SelectedCategoryIdsArray.Add(categoryId);
        //        filter.PF = PrimaryFilterEnum.Category;
        //    }
        //    return this.Manufacturer(manufacturerId, filter, categoryId > 0 ? categoryId : (int?)null);
        //}

        //public JsonResult ProductFitment(int productId)
        //{
        //    int vehicleToProduct = -1;
        //    var vehicleListHtml = string.Empty;

        //    var product = productService.GetProductById(productId);
        //    if (product != null && !product.Deleted)
        //    {
        //        if (product.ProductVehicles != null && product.ProductVehicles.Count > 0)
        //        {
        //            var userVehicle = this.vehicleService.GetVehicleFromCookies();
        //            if (userVehicle != null)
        //            {
        //                vehicleToProduct = product.ProductVehicles.Any(pv => pv.VehicleId == userVehicle.Id) ? 1 : 0;
        //                if (vehicleToProduct == 0)
        //                    vehicleToProduct = product.ProductVehicles.Any(pv => pv.Vehicle.BaseVehicleId == userVehicle.BaseVehicleId && pv.Vehicle.SubModel.Id == 1 /* ALL */) ? 1 : 0;
        //            }

        //            var vehicles = product.ProductVehicles.Take(100).ToList().ToModel();

        //            vehicleListHtml = this.RenderPartialViewToString("~/Views/Product/_ProductVehiclesFitment.cshtml", vehicles);
        //        }
        //    }

        //    var result = Json(new { VehicleListHtml = vehicleListHtml, VehicleToProduct = vehicleToProduct });
        //    result.MaxJsonLength = int.MaxValue;
        //    return result;
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //public ActionResult VehicleMakeAccessories()
        //{
        //    return this.View("Accessories", this.vehicleService.GetMakesActiveForSeo().ToModel("Accessories", "Parts and Accessories"));
        //}

        //[NopHttpsRequirement(SslRequirement.No)]
        //public ActionResult VehicleMakeModelAccessories()
        //{
        //    return this.View("Accessories", this.vehicleService.GetMakeModelsActiveForSeo().ToModel("Accessories", "Parts and Accessories"));
        //}

        //[ChildActionOnly]
        //public ActionResult TopMenu()
        //{
        //    IList<HeaderCategories> headerCategories = new List<HeaderCategories>();
        //    var vehicle = this.vehicleService.GetVehicleFromCookies();
        //    if (vehicle != null && vehicle.Id > 0)
        //    {
        //        var key = string.Format(VEHICLE_TOP_MENU_KEY, vehicle.Id);
        //        headerCategories = this.staticCacheManager.Get(key, () =>
        //        {
        //            var vehicleCategories = this.solrService.GetVehicleCategories(vehicle.Id);
        //            return this.vehicleService.GetHeaderCategories(vehicleCategories);
        //        });
        //    }

        //    return PartialView(headerCategories);
        //}

        //[ChildActionOnly]
        //public ActionResult MobileMenu()
        //{
        //    IList<HeaderCategories> headerCategories = new List<HeaderCategories>();
        //    var vehicle = this.vehicleService.GetVehicleFromCookies();
        //    if (vehicle != null && vehicle.Id > 0)
        //    {
        //        var key = string.Format(VEHICLE_TOP_MENU_KEY, vehicle.Id);
        //        headerCategories = this.staticCacheManager.Get(key, () =>
        //        {
        //            var vehicleCategories = this.solrService.GetVehicleCategories(vehicle.Id);
        //            return this.vehicleService.GetHeaderCategories(vehicleCategories);
        //        });
        //    }

        //    return PartialView(headerCategories);
        //}

        //#endregion

        //#region Private methods

        //private List<int> GetSubcategoriesIdByVehicle(int parentCategoryId, int vehicleId)
        //{
        //    return this.vehicleService.GetSubcategoryIdsByVehicle(parentCategoryId, vehicleId).ToList();
        //}

        //[NonAction]
        //private static string GetResultsRangeLabel(int totalProducts, int pageNumber, int itemsPerPage = 75)
        //{
        //    itemsPerPage = totalProducts > itemsPerPage ? itemsPerPage : totalProducts;
        //    var boundary = (pageNumber - 1) * itemsPerPage;
        //    var end = boundary + itemsPerPage;
        //    return $"{boundary + 1} - {(end > totalProducts ? totalProducts : end)} of {totalProducts:##,###} results";
        //}

        //[NonAction]
        //private FilterSearchModel SolrSearch(FilterSearchModel model)
        //{
        //    if (model == null)
        //    {
        //        model = new FilterSearchModel();
        //    }

        //    if (string.IsNullOrWhiteSpace(model.SearchTerms))
        //    {
        //        model.SearchTerms = string.Empty;
        //    }
        //    else
        //    {
        //        Func<string, string> escapeSpecialCharacters = (i) =>
        //        {
        //            var escapeChar = @"\";
        //            // important to make '\' as leading character to prevent double replacing
        //            var charsToEscape = new[] { "\\", "/", "+", "-", "!", "(", ")", "{", "}", "[", "]", "^", "\"", "~", "*", "?", ":" };
        //            foreach (var c in charsToEscape)
        //            {
        //                i = i.Replace(c, string.Concat(escapeChar, c));
        //            }

        //            return i.Trim();
        //        };

        //        model.SearchTerms = escapeSpecialCharacters(model.SearchTerms);
        //    }

        //    var hasFilters = !((model.SelectedCategoryIdsArray.Count == 0 || model.SelectedCategoryIdsArray.Count == 1 && model.SelectedCategoryIdsArray.Contains(model.CId))
        //        && (model.SelectedManufacturerIdsArray.Count == 0 || model.SelectedManufacturerIdsArray.Count == 1 && model.SelectedManufacturerIdsArray.Contains(model.MId))
        //        && model.SelectedPriceRangeIdsArray.Count == 0

        //        // Tire attributes 
        //        && model.SelectedPerformanceAttributes.Count == 0
        //        && model.SelectedTireLoadAttributes.Count == 0
        //        && model.SelectedTireSpeedAttributes.Count == 0
        //        && model.SelectedTreadTypeAttributes.Count == 0
        //        && model.SelectedSidewallAttributes.Count == 0
        //        && model.SelectedLoadRangeAttributes.Count == 0
        //        && model.SelectedUtqgAttributes.Count == 0
        //        && model.SelectedServiceDescriptionAttributes.Count == 0
        //        && !model.SelectedTireSizeAttributes.Any()
        //        && !model.SelectedTireRimSizeAttributes.Any());

        //    if (model.CId != 0 && model.SelectedCategoryIdsArray.Count == 0 && !model.SelectedCategoryIdsArray.Contains(model.CId))
        //    {
        //        model.SelectedCategoryIdsArray.Insert(0, model.CId);
        //    }

        //    if (model.CId == 214)
        //    {
        //        if (!model.SelectedCategoryIdsArray.Any(sc => sc == 7636))
        //        {
        //            model.SelectedCategoryIdsArray.Add(7636);
        //            model.SelectedCategoryIdsArray.Remove(214);
        //        }

        //        hasFilters = true;
        //    }

        //    if (model.MId != 0 && model.SelectedManufacturerIdsArray.Count == 0 && !model.SelectedManufacturerIdsArray.Contains(model.MId))
        //    {
        //        model.SelectedManufacturerIdsArray.Insert(0, model.MId);
        //    }

        //    Vehicle vehicle;
        //    SolrQueryResults<SolrProduct> products;
        //    string searchTerms;
        //    bool searchByCategoryName;

        //    var manufacturers = this.staticCacheManager.Get(MANUFACTURERS_ALL_KEY, () => this.manufacturerService.GetAllManufacturers());
        //    var facets = this.SolrGetOriginalFacets(model, manufacturers, out products, out vehicle, out searchTerms, out searchByCategoryName);    // need request to get "original" filters
        //    model.OriginalCategories = facets.OriginalCategories;
        //    model.OriginalManufacturers = facets.OriginalManufacturers;
        //    model.OriginalPriceRanges = facets.OriginalPriceRanges;
        //    model.OriginalSubCategories = facets.OriginalSubCategories;

        //    // Tire attributes
        //    model.OriginalPerformanceAttributes = facets.OriginalPerformanceAttributes;
        //    model.OriginalTireLoadAttributes = facets.OriginalTireLoadAttributes;
        //    model.OriginalTireSpeedAttributes = facets.OriginalTireSpeedAttributes;
        //    model.OriginalTreadTypeAttributes = facets.OriginalTreadTypeAttributes;
        //    model.OriginalSidewallAttributes = facets.OriginalSidewallAttributes;
        //    model.OriginalLoadRangeAttributes = facets.OriginalLoadRangeAttributes;
        //    model.OriginalUtqgAttributes = facets.OriginalUtqgAttributes;
        //    model.OriginalServiceDescriptionAttributes = facets.OriginalServiceDescriptionAttributes;
        //    model.OriginalTireSizeAttributes = facets.OriginalTireSizeAttributes;
        //    model.OriginalTireRimSizeAttributes = facets.OriginalTireRimSizeAttributes;

        //    if (model.MinPrice.HasValue && model.MaxPrice.HasValue && model.MaxPrice > 0)
        //    {
        //        model.SPRIds = null;
        //    }

        //    if (hasFilters)
        //    {
        //        products = this.solrService.Search(new Core.Domain.Solr.SearchModel
        //        {
        //            SearchTerms = searchTerms,
        //            Categories = model.SelectedCategoryIdsArray.ToArray(),
        //            Brands = model.SelectedManufacturerIdsArray.ToArray(),
        //            Prices = model.SelectedPriceRangeIdsArray.ToArray(),
        //            MinPrice = model.MinPrice,
        //            MaxPrice = model.MaxPrice,
        //            Page = model.PFC.PageIndex,
        //            PageSize = this.catalogSettings.SearchPageProductsPerPage,
        //            ShowOutOfStock = model.OS,
        //            SortBy = model.PFC.Sort,
        //            VehicleId = vehicle == null ? null : (int?)vehicle.Id,
        //            MakeId = model.V == null || model.V.Make == 0 ? (int?)null : model.V.Make,
        //            ModelId = model.V == null || model.V.Model == 0 ? (int?)null : model.V.Model,
        //            ShowUniversal = vehicle == null ? null : (bool?)vehicle.ShowUniversal,
        //            StoreId = this.storeContext.CurrentStore.Id,
        //            IsManufacturerPageRequested = model.IsManufacturerPageRequested,
        //            // Tire attributes 
        //            Section = model.Section,
        //            Aspect = model.Aspect,
        //            Rim = model.Rim,
        //            PerformanceAttributes = model.SelectedPerformanceAttributes.ToArray(),
        //            TireLoadAttributes = model.SelectedTireLoadAttributes.ToArray(),
        //            TireSpeedAttributes = model.SelectedTireSpeedAttributes.ToArray(),
        //            TreadTypeAttributes = model.SelectedTreadTypeAttributes.ToArray(),
        //            SidewallAttributes = model.SelectedSidewallAttributes.ToArray(),
        //            LoadRangeAttributes = model.SelectedLoadRangeAttributes.ToArray(),
        //            UtqgAttributes = model.SelectedUtqgAttributes.ToArray(),
        //            ServiceDescriptionAttributes = model.SelectedServiceDescriptionAttributes.ToArray(),
        //            TireSizeAttributes = model.SelectedTireSizeAttributes.ToArray(),
        //            TireRimSizeAttributes = model.SelectedTireRimSizeAttributes.ToArray()
        //        });
        //    }

        //    var specificationAttributes = this.staticCacheManager.Get(SPEC_ATTRIBUTES_ALL_KEY, CacheTime, () => this.specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttributeIds(this.attributeIDs));
        //    if (products.Any())
        //    {
        //        var orderService = EngineContext.Current.Resolve<IOrderService>();
        //        model.ShowMembersClubPrices = this.workContext.CurrentCustomer.IsClubMember();
        //        model.Products = this.PrepareProductOverviewModels(products, manufacturers, specificationAttributes, model.ShowMembersClubPrices).ToList();
        //    }

        //    model.NoResults = !model.Products.Any();
        //    model.TotalProducts = (int)products.NumFound;

        //    if (!model.NoResults)
        //    {
        //        this.PrepareSortingOptions(model.PFC);
        //        model.PFC.PageSize = this.catalogSettings.SearchPageProductsPerPage;
        //        model.PFC.LoadPagedList(new PagedList<SolrProduct>(products, model.PFC.PageIndex, this.catalogSettings.SearchPageProductsPerPage, model.TotalProducts));
        //    }

        //    model.FilterCategories = PrepareFilterCheckBoxList(model.OriginalCategories, model.OriginalCategories.Select(i => i.Id).Except(new[] { model.CId }), model.SelectedCategoryIdsArray, model.MId);
        //    model.FilterManufacturers = PrepareFilterCheckBoxList(model.OriginalManufacturers, model.OriginalManufacturers.Select(i => i.Id).Except(new[] { model.MId }), model.SelectedManufacturerIdsArray, model.CId);
        //    //model.PriceRanges = PrepareFilterCheckBoxList(model.OriginalPriceRanges, model.OriginalPriceRanges.Select(i => i.Id), model.SelectedPriceRangeIdsArray);

        //    // Tire attributes 
        //    model.PerformanceAttributes = PrepareFilterCheckBoxList(model.OriginalPerformanceAttributes, model.SelectedPerformanceAttributes).OrderBy(m => m.Name).ToList();
        //    foreach (var facet in model.PerformanceAttributes)
        //    {
        //        string alias = null;
        //        if (tireCategoryNameMappings.TryGetValue(facet.Name, out alias))
        //        {
        //            facet.Name = alias;
        //        }
        //    }

        //    // Tire attributes 
        //    model.TireLoadAttributes = PrepareFilterCheckBoxList(model.OriginalTireLoadAttributes, model.SelectedTireLoadAttributes).OrderBy(m => m.Name).ToList();
        //    model.TireSpeedAttributes = PrepareFilterCheckBoxList(model.OriginalTireSpeedAttributes, model.SelectedTireSpeedAttributes).OrderBy(m => m.Name).ToList();
        //    model.TreadTypeAttributes = PrepareFilterCheckBoxList(model.OriginalTreadTypeAttributes, model.SelectedTreadTypeAttributes).OrderBy(m => m.Name).ToList();
        //    model.SidewallAttributes = PrepareFilterCheckBoxList(model.OriginalSidewallAttributes, model.SelectedSidewallAttributes).OrderBy(m => m.Name).ToList();
        //    model.LoadRangeAttributes = PrepareFilterCheckBoxList(model.OriginalLoadRangeAttributes, model.SelectedLoadRangeAttributes).OrderBy(m => m.Name).ToList();
        //    model.UtqgAttributes = PrepareFilterCheckBoxList(model.OriginalUtqgAttributes, model.SelectedUtqgAttributes).OrderBy(m => m.Name).ToList();
        //    model.ServiceDescriptionAttributes = PrepareFilterCheckBoxList(model.OriginalServiceDescriptionAttributes, model.SelectedServiceDescriptionAttributes).OrderBy(m => m.Name).ToList();
        //    model.TireSizeAttributes = PrepareFilterCheckBoxList(model.OriginalTireSizeAttributes, model.SelectedTireSizeAttributes).OrderBy(m => m.Name).ToList();
        //    model.TireRimSizeAttributes = PrepareFilterCheckBoxList(model.OriginalTireRimSizeAttributes, model.SelectedTireRimSizeAttributes).OrderBy(m => m.Name).ToList();

        //    if (model.PageType == "Tires")
        //    {
        //        if (!string.IsNullOrEmpty(model.Section))
        //        {
        //            model.SectionText = specificationAttributes.SingleOrDefault(m => m.Id == int.Parse(model.Section))?.Name;
        //        }

        //        if (!string.IsNullOrEmpty(model.Aspect))
        //        {
        //            model.AspectText = specificationAttributes.SingleOrDefault(m => m.Id == int.Parse(model.Aspect))?.Name;
        //        }

        //        if (!string.IsNullOrEmpty(model.Rim))
        //        {
        //            model.RimText = specificationAttributes.SingleOrDefault(m => m.Id == int.Parse(model.Rim))?.Name;
        //        }
        //    }


        //    return model;
        //}

        //[NonAction]
        //private FilterSearchModel SolrGetOriginalFacets(FilterSearchModel searchModel, IPagedList<Manufacturer> manufacturers, out SolrQueryResults<SolrProduct> products, out Vehicle vehicle, out string searchTerms, out bool searchByCategoryName)
        //{
        //    searchByCategoryName = false;
        //    if (searchModel == null)
        //    {
        //        throw new ArgumentNullException(nameof(searchModel));
        //    }

        //    if (searchModel.V == null || searchModel.V.Year == 0 || searchModel.V.Make == 0 || searchModel.V.Model == 0 || searchModel.V.SubModel == 0)
        //    {
        //        vehicle = this.vehicleService.GetVehicleFromCookies();
        //    }
        //    else
        //    {
        //        vehicle = this.vehicleService.GetVehicle(searchModel.V.Year, searchModel.V.Make, searchModel.V.Model, searchModel.V.SubModel);
        //        if (vehicle != null)
        //        {
        //            vehicle.ShowUniversal = searchModel.V.SU;
        //            this.vehicleService.SetVehicleToCookies(searchModel.V.Year, searchModel.V.Make, searchModel.V.Model, searchModel.V.SubModel, searchModel.V.SU);
        //        }
        //    }


        //    var tirePageRequested = searchModel.PageType == "Tires";
        //    if (tirePageRequested)
        //    {
        //        vehicle = null;
        //        searchModel.V = new FilterSearchModel.FilterVehicle();
        //    }

        //    searchTerms = searchModel.SearchTerms; // copy search terms to cut found make-model-category occurences
        //    if (vehicle == null && searchModel.V?.Model == 0 && searchModel.V?.Make == 0 && searchModel.V?.Year == 0 && !string.IsNullOrEmpty(searchTerms))
        //    {
        //        // this.FindAndSetVehicle(ref searchTerms, searchModel);
        //    }
        //    else if (vehicle == null && (searchModel.V?.Model > 0 || searchModel.V?.Make > 0 || searchModel.V?.Year > 0))
        //    {
        //        string makeName = null, modelName = null, year = null;
        //        if (searchModel.V.Make > 0)
        //        {
        //            makeName = this.vehicleService.GetMake(searchModel.V.Make)?.Name;
        //        }

        //        searchModel.V.FullName = makeName;
        //        if (searchModel.V.Model > 0)
        //        {
        //            modelName = this.vehicleService.GetModel(searchModel.V.Model)?.Name;
        //        }

        //        searchModel.V.FullName = $"{searchModel.V.FullName}{(!string.IsNullOrEmpty(modelName) ? $" {modelName}" : string.Empty)}";
        //        if ((searchModel.V.Make > 0 || searchModel.V.Model > 0) && searchModel.V?.Year > 0)
        //        {
        //            year = searchModel.V?.Year.ToString();
        //        }

        //        searchModel.V.FullName = $"{searchModel.V.FullName} {year ?? string.Empty}".Trim();
        //    }

        //    if (searchModel.V != null && vehicle == null)
        //    {
        //        searchModel.V.SearchTerm = searchTerms;
        //    }

        //    var searchCategories = new List<int>();
        //    var searchBrands = new List<int>();

        //    // Disabled category filters
        //    //if (!string.IsNullOrEmpty(searchTerms))
        //    //{
        //    //    searchCategories = this.FindAndSetCategories(searchTerms);
        //    //    searchByCategoryName = searchCategories.Any();

        //    //    /*if (!searchCategories.Any())
        //    //    {
        //    //        searchBrands = this.FindAndSetBrands(ref searchTerms);
        //    //    }*/
        //    //}

        //    if (searchModel.MinPrice.HasValue && searchModel.MaxPrice.HasValue && searchModel.MaxPrice > 0)
        //    {
        //        searchModel.SPRIds = null;
        //    }

        //    if (vehicle != null && vehicle.BaseVehicle != null)
        //    {
        //        searchModel.V.FullName = $"{vehicle.BaseVehicle.YearId} {vehicle.BaseVehicle.Make.Name} {vehicle.BaseVehicle.Model.Name} {vehicle.SubModel.Name}";
        //        searchModel.V.Year = vehicle.BaseVehicle.YearId;
        //        searchModel.V.Make = vehicle.BaseVehicle.MakeId;
        //        searchModel.V.Model = vehicle.BaseVehicle.ModelId;
        //        searchModel.V.SubModel = vehicle.SubModelId;
        //        searchModel.V.SU = vehicle.ShowUniversal;
        //    }

        //    var solrSearchModel = new Core.Domain.Solr.SearchModel
        //    {
        //        SearchTerms = searchTerms,
        //        Categories = searchModel.CId == 0 ? (searchCategories.Any() ? searchCategories.ToArray() : null) : new[] { searchModel.CId },
        //        Brands = searchModel.MId == 0 ? (searchBrands.Any() ? searchBrands.ToArray() : null) : new[] { searchModel.MId },
        //        Prices = null,
        //        MinPrice = !searchModel.MinPrice.HasValue && !searchModel.MaxPrice.HasValue ? (decimal?)null : (searchModel.MinPrice ?? 0),
        //        MaxPrice = !searchModel.MinPrice.HasValue && !searchModel.MaxPrice.HasValue ? (decimal?)null : (searchModel.MaxPrice ?? 999999),
        //        Page = searchModel.PFC.PageIndex,
        //        PageSize = this.catalogSettings.SearchPageProductsPerPage,
        //        ShowOutOfStock = searchModel.OS,
        //        SortBy = searchModel.PFC.Sort,
        //        VehicleId = vehicle == null ? null : (int?)vehicle.Id,
        //        BaseVehicleId = searchModel.V == null || searchModel.V.BaseVehicleId == 0 ? null : (int?)searchModel.V.BaseVehicleId,
        //        MakeId = searchModel.V == null || searchModel.V.Make == 0 ? (int?)null : searchModel.V.Make,
        //        ModelId = searchModel.V == null || searchModel.V.Model == 0 ? (int?)null : searchModel.V.Model,
        //        ShowUniversal = vehicle == null ? null : (bool?)vehicle.ShowUniversal,
        //        Year = searchModel.V == null || searchModel.V.Year == 0 ? (int?)null : searchModel.V.Year,
        //        ShowSubCategories = searchModel.IsRootCategoryPageRequested,
        //        StoreId = this.storeContext.CurrentStore.Id,
        //        IsManufacturerPageRequested = searchModel.IsManufacturerPageRequested,
        //        // Tire attributes
        //        Spec_Type = searchModel.Spec_Type,
        //        Section = searchModel.Section,
        //        Aspect = searchModel.Aspect,
        //        Rim = searchModel.Rim,
        //        PerformanceAttributes = searchModel.PerformanceAttributeFacets.Any() ? searchModel.PerformanceAttributeFacets.ToArray() : null,
        //        TireLoadAttributes = searchModel.TireLoadAttributeFacets.Any() ? searchModel.TireLoadAttributeFacets.ToArray() : null,
        //        TireSpeedAttributes = searchModel.TireSpeedAttributeFacets.Any() ? searchModel.TireSpeedAttributeFacets.ToArray() : null,
        //        TreadTypeAttributes = searchModel.TreadTypeAttributeFacets.Any() ? searchModel.TreadTypeAttributeFacets.ToArray() : null,
        //        SidewallAttributes = searchModel.SidewallAttributeFacets.Any() ? searchModel.SidewallAttributeFacets.ToArray() : null,
        //        LoadRangeAttributes = searchModel.LoadRangeAttributeFacets.Any() ? searchModel.LoadRangeAttributeFacets.ToArray() : null,
        //        UtqgAttributes = searchModel.UtqgAttributeFacets.Any() ? searchModel.UtqgAttributeFacets.ToArray() : null,
        //        ServiceDescriptionAttributes = searchModel.ServiceDescriptionAttributeFacets.Any() ? searchModel.ServiceDescriptionAttributeFacets.ToArray() : null,
        //        TireSizeAttributes = searchModel.TireSizeAttributeFacets.Any() ? searchModel.TireSizeAttributeFacets.ToArray() : null,
        //        TireRimSizeAttributes = searchModel.TireRimSizeAttributeFacets.Any() ? searchModel.TireRimSizeAttributeFacets.ToArray() : null
        //    };

        //    // if search model PF is not none then this query is only usefull for get facets, so in this product not required, products are fecthing by different quaery
        //    if (searchModel.PF != PrimaryFilterEnum.None)
        //        solrSearchModel.PageSize = 0;

        //    products = this.solrService.Search(solrSearchModel);

        //    // if no products found and we do filter by categories, extracted from searchTerm, then we clear categories filter and search only by term
        //    if (!products.Any() && searchModel.CId == 0 && searchByCategoryName)
        //    {
        //        solrSearchModel.Categories = null;

        //        // if search model PF is not none then this query is only usefull for get facets, so in this product not required, products are fecthing by different quaery
        //        if (searchModel.PF != PrimaryFilterEnum.None)
        //            solrSearchModel.PageSize = 0;

        //        products = this.solrService.Search(solrSearchModel);
        //    }

        //    // Get original facets without selected filters
        //    List<int> originalCategoryIds;
        //    if (searchCategories.Count > 0)
        //    {
        //        originalCategoryIds = searchCategories.Intersect(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key))).ToList();
        //        //originalCategoryIds = products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)).ToList();
        //    }
        //    else
        //    {
        //        originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)).OrderBy(i => i));
        //    }

        //    // var originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)).OrderBy(i => i));
        //    var originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)).OrderBy(i => i));
        //    // var originalPriceRangeIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "pricerange").Value.Select(i => int.Parse(i.Key)).OrderBy(i => i));
        //    var originalSubCategoryIds = new List<int>();
        //    if (searchModel.IsRootCategoryPageRequested && searchModel.CId != 0)
        //    {
        //        originalSubCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "subcatid").Value.Select(i => int.Parse(i.Key)));
        //    }

        //    var performanceAttributeFacets = new List<int>();
        //    var tireLoadAttributeFacets = new List<int>();
        //    var tireSpeedAttributeFacets = new List<int>();
        //    var treadTypeAttributeFacets = new List<int>();
        //    var sidewallAttributeFacets = new List<int>();
        //    var loadRangeAttributeFacets = new List<int>();
        //    var utqgAttributeFacets = new List<int>();
        //    var serviceDescriptionAttributeFacets = new List<int>();
        //    var tireSizeAttributeFacets = new List<int>();
        //    var tireRimSizeAttributeFacets = new List<int>();

        //    tirePageRequested = tirePageRequested || solrSearchModel.Categories?.Count() == 1
        //        && (solrSearchModel.Categories?.FirstOrDefault() == 17 || solrSearchModel.Categories?.FirstOrDefault() == 214 || solrSearchModel.Categories?.FirstOrDefault() == 7636);

        //    if (tirePageRequested)
        //    {
        //        performanceAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)).ToList();
        //        tireLoadAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)).ToList();
        //        tireSpeedAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)).ToList();
        //        treadTypeAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)).ToList();
        //        sidewallAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)).ToList();
        //        loadRangeAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)).ToList();
        //        utqgAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)).ToList();
        //        serviceDescriptionAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)).ToList();
        //        tireSizeAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)).ToList();
        //        tireRimSizeAttributeFacets = products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)).ToList();
        //    }

        //    // Get original facet with selected filters
        //    if (searchModel.PF != PrimaryFilterEnum.None)
        //    {
        //        GetOriginalFacetWithSelectedFilter(searchModel, solrSearchModel);

        //        solrSearchModel.PageSize = this.catalogSettings.SearchPageProductsPerPage;
        //        products = this.solrService.Search(solrSearchModel);

        //        PrepareFacets(searchModel, products, originalCategoryIds, originalManufacturerIds, performanceAttributeFacets, tireLoadAttributeFacets, tireSpeedAttributeFacets, treadTypeAttributeFacets,
        //      sidewallAttributeFacets, loadRangeAttributeFacets, utqgAttributeFacets, serviceDescriptionAttributeFacets, tireSizeAttributeFacets, tireRimSizeAttributeFacets);

        //    }

        //    if (searchModel.IsManufacturerPageRequested)
        //    {
        //        searchModel.OriginalCategories = this.staticCacheManager.Get(
        //            string.Format(MANUFACTURERS_ENHANCED_CATEGORIES_KEY, searchModel.MId), CacheTime,
        //            () => this.categoryService.GetPiesCategoriesByManufacturerId(searchModel.MId).ToList())
        //            .Join(originalCategoryIds, a => a.Id, b => b, (a, b) => a).ToList();
        //    }
        //    else
        //    {
        //        searchModel.OriginalCategories = this.AllCategoriesCached.Join(originalCategoryIds, a => a.Id, b => b, (a, b) => a).ToList();
        //    }

        //    searchModel.OriginalManufacturers = manufacturers
        //        .Join(originalManufacturerIds, a => a.Id, b => b, (a, b) => a)
        //        .OrderBy(m => m.Name)
        //        .ToList();

        //    //searchModel.OriginalPriceRanges = this.staticCacheManager.Get(string.Format(PRICES_BY_ID_LIST_KEY, string.Join(",", originalPriceRangeIds.OrderBy(i => i))), () => this.vehicleService.GetPriceRangesByIds(originalPriceRangeIds).OrderBy(p => p.Id)).ToList();

        //    if (searchModel.CId != 0)
        //    {
        //        if (searchModel.IsRootCategoryPageRequested)
        //        {
        //            // if searchModel.CId is a rootCategory
        //            searchModel.OriginalSubCategories = this.AllCategoriesCached
        //                .Join(originalSubCategoryIds, a => a.Id, b => b, (a, b) => a)
        //                .Where(m => m.ParentCategoryId == searchModel.CId).OrderBy(m => m.Name).ToList();

        //            var subCategories = this.AllCategoriesCached.Where(i => i.ParentCategoryId == searchModel.CId);

        //            searchModel.OriginalCategories = searchModel.OriginalCategories.Join(subCategories, a => a.ParentCategoryId, b => b.Id, (a, b) => a).ToList();
        //        }
        //        else
        //        {
        //            if (this.AllCategoriesCached.Any(i => i.ParentCategoryId == searchModel.CId))
        //            {
        //                // if searchModel.CId is a subCategory
        //                searchModel.OriginalCategories = searchModel.OriginalCategories.Where(i => i.ParentCategoryId == searchModel.CId).ToList();
        //            }
        //            else
        //            {
        //                // if searchModel.CId is a partTerminology
        //                searchModel.OriginalCategories = new List<Category>();
        //            }
        //        }
        //    }

        //    var specificationAttributes = this.staticCacheManager.Get(SPEC_ATTRIBUTES_ALL_KEY, CacheTime, () => this.specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttributeIds(this.attributeIDs));

        //    searchModel.OriginalPerformanceAttributes = specificationAttributes.Join(performanceAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalTireLoadAttributes = specificationAttributes.Join(tireLoadAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalTireSpeedAttributes = specificationAttributes.Join(tireSpeedAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalTreadTypeAttributes = specificationAttributes.Join(treadTypeAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalSidewallAttributes = specificationAttributes.Join(sidewallAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalLoadRangeAttributes = specificationAttributes.Join(loadRangeAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalUtqgAttributes = specificationAttributes.Join(utqgAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalServiceDescriptionAttributes = specificationAttributes.Join(serviceDescriptionAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalTireSizeAttributes = specificationAttributes.Join(tireSizeAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();
        //    searchModel.OriginalTireRimSizeAttributes = specificationAttributes.Join(tireRimSizeAttributeFacets, a => a.Id, b => b, (a, b) => a).ToList();

        //    return searchModel;
        //}

        //private void PrepareFacets(FilterSearchModel searchModel, SolrQueryResults<SolrProduct> products, IList<int> originalCategoryIds, IList<int> originalManufacturerIds,
        //    IList<int> performanceAttributeFacets, IList<int> tireLoadAttributeFacets, IList<int> tireSpeedAttributeFacets, IList<int> treadTypeAttributeFacets,
        //    IList<int> sidewallAttributeFacets, IList<int> loadRangeAttributeFacets, IList<int> utqgAttributeFacets, IList<int> serviceDescriptionAttributeFacets,
        //    IList<int> tireSizeAttributeFacets, IList<int> tireRimSizeAttributeFacets)
        //{

        //    switch (searchModel.PF)
        //    {
        //        case PrimaryFilterEnum.Category:
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));
        //            // originalPriceRangeIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "pricerange").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.Manufacturer:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));

        //            // originalPriceRangeIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "pricerange").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.PriceRange:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TirePerformance:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireLoad:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireSpeed:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireTreadType:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireSidewall:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireLoadRange:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireUtqg:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireServiceDescription:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireSize:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireRimSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "rim").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //        case PrimaryFilterEnum.TireRimSize:
        //            originalCategoryIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "partid").Value.Select(i => int.Parse(i.Key)));
        //            originalManufacturerIds = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "brandid").Value.Select(i => int.Parse(i.Key)));

        //            // Tire attributes
        //            performanceAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "performance").Value.Select(i => int.Parse(i.Key)));
        //            tireLoadAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tireload").Value.Select(i => int.Parse(i.Key)));
        //            tireSpeedAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tirespeed").Value.Select(i => int.Parse(i.Key)));
        //            treadTypeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "treadtype").Value.Select(i => int.Parse(i.Key)));
        //            sidewallAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "sidewall").Value.Select(i => int.Parse(i.Key)));
        //            loadRangeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "loadrange").Value.Select(i => int.Parse(i.Key)));
        //            utqgAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "utqg").Value.Select(i => int.Parse(i.Key)));
        //            serviceDescriptionAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "servicedesc").Value.Select(i => int.Parse(i.Key)));
        //            tireSizeAttributeFacets = new List<int>(products.FacetFields.FirstOrDefault(i => i.Key == "tiresize").Value.Select(i => int.Parse(i.Key)));
        //            break;
        //    }
        //}

        //private void GetOriginalFacetWithSelectedFilter(FilterSearchModel searchModel, Core.Domain.Solr.SearchModel solrSearchModel)
        //{
        //    switch (searchModel.PF)
        //    {
        //        case PrimaryFilterEnum.Category:
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.Manufacturer:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.PriceRange:
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TirePerformance:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireLoad:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireSpeed:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireRunFlat:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireSidewall:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireLoadRange:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireUtqg:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireServiceDescription:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireSize:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //        case PrimaryFilterEnum.TireRimSize:
        //            solrSearchModel.Brands = searchModel.SelectedManufacturerIdsArray.ToArray();
        //            solrSearchModel.Categories = searchModel.SelectedCategoryIdsArray.ToArray();
        //            solrSearchModel.Prices = searchModel.SelectedPriceRangeIdsArray.ToArray();
        //            solrSearchModel.PerformanceAttributes = searchModel.SelectedPerformanceAttributes.ToArray();
        //            solrSearchModel.TireLoadAttributes = searchModel.SelectedTireLoadAttributes.ToArray();
        //            solrSearchModel.TireSpeedAttributes = searchModel.SelectedTireSpeedAttributes.ToArray();
        //            solrSearchModel.TreadTypeAttributes = searchModel.SelectedTreadTypeAttributes.ToArray();
        //            solrSearchModel.SidewallAttributes = searchModel.SelectedSidewallAttributes.ToArray();
        //            solrSearchModel.LoadRangeAttributes = searchModel.SelectedLoadRangeAttributes.ToArray();
        //            solrSearchModel.UtqgAttributes = searchModel.SelectedUtqgAttributes.ToArray();
        //            solrSearchModel.ServiceDescriptionAttributes = searchModel.SelectedServiceDescriptionAttributes.ToArray();
        //            solrSearchModel.TireSizeAttributes = searchModel.SelectedTireSizeAttributes.ToArray();
        //            solrSearchModel.TireRimSizeAttributes = searchModel.SelectedTireRimSizeAttributes.ToArray();
        //            break;
        //    }

        //}

        //private void PrepareKeywordParts(string searchTerms, out List<string> keywordParts, out List<string> keywordPartsClean)
        //{
        //    keywordParts = searchTerms.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    keywordPartsClean = keywordParts.Select(i => i.ToAlphaNumeric()).Where(i => !string.IsNullOrEmpty(i)).ToList();
        //}

        //[NonAction]
        //private void FindAndSetVehicle(ref string searchTerms, FilterSearchModel searchModel)
        //{
        //    List<string> keywordParts;
        //    List<string> keywordPartsClean;
        //    searchModel.V = new FilterSearchModel.FilterVehicle();

        //    this.PrepareKeywordParts(searchTerms, out keywordParts, out keywordPartsClean);

        //    string makeName, modelName;
        //    var makeId = this.FindMakeId(ref searchTerms, keywordParts, keywordPartsClean, out makeName);
        //    searchModel.V.FullName = makeName;
        //    var modelId = this.FindModelId(ref searchTerms, keywordParts, keywordPartsClean, makeId, out modelName);
        //    searchModel.V.FullName = $"{searchModel.V.FullName}{(!string.IsNullOrEmpty(modelName) ? $" {modelName}" : string.Empty)}";
        //    int? yearId = null;
        //    if (makeId.HasValue || modelId.HasValue)
        //    {
        //        yearId = this.FindYearId(ref searchTerms, keywordParts, keywordPartsClean, makeId, modelId);
        //        searchModel.V.FullName = $"{searchModel.V.FullName} {yearId?.ToString() ?? string.Empty}".Trim();
        //        if (makeId.HasValue && modelId.HasValue && yearId.HasValue)
        //        {
        //            var baseVehicle = this.vehicleService.GetBaseVehicle(yearId.Value, makeId.Value, modelId.Value);
        //            if (baseVehicle != null)
        //            {
        //                searchModel.V.BaseVehicleId = baseVehicle.Id;
        //            }
        //        }
        //    }

        //    if (searchModel.V.BaseVehicleId == 0)
        //    {
        //        searchModel.V.Make = makeId ?? 0;
        //        searchModel.V.Model = modelId ?? 0;
        //        searchModel.V.Year = yearId ?? 0;
        //    }
        //}

        //[NonAction]
        //private List<int> FindAndSetCategories(string searchTerms)
        //{
        //    var categories = this.solrService.GetCategories(searchTerms, this.storeContext.CurrentStore.Id);
        //    return categories.Select(i => i.Id).ToList();
        //}

        //[NonAction]
        //private List<int> FindAndSetBrands(ref string searchTerms)
        //{
        //    var searchBrands = new List<int>();
        //    var searchTermsClear = searchTerms.ToAlphaNumeric(keepSpaces: true);
        //    var allBrands = this.staticCacheManager.Get(MANUFACTURERS_ALL_KEY, () => this.manufacturerService.GetAllManufacturers(showManufacturersWithProducts: true))
        //        .Select(i => new { Id = i.Id, Name = i.Name, NameClear = i.Name.ToAlphaNumeric(keepSpaces: true) });

        //    var matchedBrands = allBrands.Where(i => searchTermsClear.Contains(i.NameClear) || i.NameClear.Contains(searchTermsClear)).ToList();

        //    if (matchedBrands.Any())
        //    {
        //        searchBrands = matchedBrands.Select(i => i.Id).ToList();

        //        List<string> keywordParts;
        //        List<string> keywordPartsClean;
        //        this.PrepareKeywordParts(searchTerms, out keywordParts, out keywordPartsClean);

        //        var brandWordsClear = matchedBrands.SelectMany(i => i.NameClear.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)).Distinct();
        //        foreach (var brandWordClear in brandWordsClear)
        //        {
        //            searchTerms = this.RemoveKeywordPart(keywordParts, keywordPartsClean, brandWordClear);
        //        }
        //    }

        //    return searchBrands;
        //}

        //[NonAction]
        //private int? FindMakeId(ref string searchTerms, IList<string> keywordParts, IList<string> keywordPartsClean, out string makeName)
        //{
        //    makeName = string.Empty;
        //    var makes = this.vehicleService.GetMakes().Where(i => i.IsActiveForSolrKeywordSearch).Select(i => new { Id = i.Id, Name = i.Name, NameClear = i.Name.ToAlphaNumeric() });
        //    var make = makes.FirstOrDefault(i => keywordPartsClean.Contains(i.NameClear));

        //    if (make != null)
        //    {
        //        searchTerms = this.RemoveKeywordPart(keywordParts, keywordPartsClean, make.NameClear);
        //        makeName = make.Name;
        //    }

        //    return make?.Id;
        //}

        //[NonAction]
        //private int? FindModelId(ref string searchTerms, IList<string> keywordParts, IList<string> keywordPartsClean, int? makeId, out string modelName)
        //{
        //    modelName = string.Empty;
        //    if (string.IsNullOrEmpty(searchTerms))
        //    {
        //        return null;
        //    }

        //    int digits;
        //    var model = makeId.HasValue && makeId.Value > 0
        //        ? this.vehicleService.GetModels(makeId.Value)
        //            .Where(i => i.IsActiveForSolrKeywordSearch)
        //            .Select(i => new { i.Id, i.Name, NameClear = i.Name.ToAlphaNumeric() })
        //            .FirstOrDefault(i => keywordPartsClean.Where(k => !int.TryParse(k, out digits)).Contains(i.NameClear))
        //        : this.vehicleService.GetModels()
        //            .Where(i => i.IsActiveForSolrKeywordSearch)
        //            .Select(i => new { i.Id, i.Name, NameClear = i.Name.ToAlphaNumeric() })
        //            .FirstOrDefault(i => keywordPartsClean.Contains(i.NameClear));

        //    if (model != null)
        //    {
        //        searchTerms = this.RemoveKeywordPart(keywordParts, keywordPartsClean, model.NameClear);
        //        modelName = model.Name;
        //    }

        //    return model?.Id;
        //}

        //[NonAction]
        //private int? FindYearId(ref string searchTerms, IList<string> keywordParts, IList<string> keywordPartsClean, int? makeId, int? modelId)
        //{
        //    if (!makeId.HasValue && !modelId.HasValue || string.IsNullOrEmpty(searchTerms))
        //    {
        //        return null;
        //    }

        //    int year;
        //    var match = Regex.Match(searchTerms, @"[ ]*[12][0-9]{3}[ ]*");
        //    if (!match.Success || !int.TryParse(match.Value, out year) || year < 1896 || year > DateTime.UtcNow.Year + 3)
        //    {
        //        return null;
        //    }

        //    IList<Year> years;
        //    if (makeId.HasValue && modelId.HasValue)
        //    {
        //        years = this.vehicleService.GetYears(makeId.Value, modelId.Value);
        //    }
        //    else if (makeId.HasValue)
        //    {
        //        years = this.vehicleService.GetYearsByMake(makeId.Value);
        //    }
        //    else
        //    {
        //        years = this.vehicleService.GetYearsByModel(modelId.Value);
        //    }

        //    if (years.Any(m => m.Id == year))
        //    {
        //        searchTerms = this.RemoveKeywordPart(keywordParts, keywordPartsClean, year.ToString());
        //        return year;
        //    }

        //    return null;
        //}

        //private string RemoveKeywordPart(IList<string> keywordParts, IList<string> keywordPartsClear, string termToRemoveClear)
        //{
        //    var index = keywordPartsClear.IndexOf(termToRemoveClear);
        //    if (index == -1)
        //    {
        //        return string.Join(" ", keywordParts);
        //    }

        //    keywordPartsClear.RemoveAt(index);
        //    keywordParts.RemoveAt(index);
        //    return !keywordParts.Any() ? string.Empty : string.Join(" ", keywordParts);
        //}

        //[NonAction]
        //private IEnumerable<int> GetChildCategoryIds(int parentCategoryId)
        //{
        //    return this.AllCategoriesCached.Where(m => m.Id == parentCategoryId).Select(m => m.Id).ToList();
        //}

        //[NonAction]
        //private IEnumerable<CustomProductOverviewModel> PrepareProductOverviewModels(
        //    SolrQueryResults<SolrProduct> products,
        //    IPagedList<Manufacturer> manufacturers,
        //    IList<SpecificationAttributeOption> specificationAttributeOptions,
        //    bool preparePriceModel = true, bool preparePictureModel = true,
        //    int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
        //    bool forceRedirectionAfterAddingToCart = false, bool showClubMembersPrices = false)
        //{
        //    return this.PrepareCustomProductOverviewModels(
        //        products,
        //        manufacturers,
        //        this.priceFormatter,
        //        this.localizationService,
        //        this.pictureService,
        //        this.productGroupService,
        //        this.staticCacheManager,
        //        this.webHelper,
        //        this.storeContext,
        //        this.productService,
        //        specificationAttributeOptions,
        //        this.specificationAttributeService,
        //        this.workContext,
        //        this.PrepareProductSpecificationModel,
        //        preparePriceModel,
        //        preparePictureModel,
        //        forceRedirectionAfterAddingToCart,
        //        showClubMembersPrices);
        //}

        //[NonAction]
        //private void PrepareSortingOptions(PagingFilteringModel pagingFilteringModel)
        //{
        //    if (pagingFilteringModel == null)
        //        throw new ArgumentNullException("pagingFilteringModel");

        //    pagingFilteringModel.AllowProductSorting = this.catalogSettings.AllowProductSorting;
        //    if (pagingFilteringModel.AllowProductSorting)
        //    {
        //        foreach (Core.Domain.Vehicles.ProductSortingEnum enumValue in Enum.GetValues(typeof(Core.Domain.Vehicles.ProductSortingEnum)))
        //        {
        //            var sortValue = enumValue.GetLocalizedEnum(this.localizationService, this.workContext);
        //            pagingFilteringModel.AvailableSortOptions.Add(new SelectListItem
        //            {
        //                Text = sortValue,
        //                Value = ((int)enumValue).ToString(CultureInfo.InvariantCulture),
        //                Selected = enumValue == (Core.Domain.Vehicles.ProductSortingEnum)pagingFilteringModel.Sort
        //            });
        //        }
        //    }
        //}

        //[NonAction]
        //private static List<CheckBoxListItem> PrepareFilterCheckBoxList(List<Category> originalCategories, IEnumerable<int> availableCategoryIds, IEnumerable<int> selectedCategoryIds, int searchManufacturerId = 0, bool defaultSort = false)
        //{
        //    var list = new List<CheckBoxListItem>();

        //    if (originalCategories == null || availableCategoryIds == null)
        //        return list;


        //    var originalCategoriesSorted = new List<Category>();
        //    bool hasOrdering = false;
        //    if (availableCategoryIds.Count() >= 10)
        //    {
        //        if (!defaultSort)
        //        {
        //            hasOrdering = true;
        //            originalCategoriesSorted.AddRange(originalCategories.Where(originalCategory => availableCategoryIds.Contains(originalCategory.Id)).OrderBy(c => c.DisplayOrder).Take(5).OrderBy(c => c.Name));
        //            originalCategoriesSorted.AddRange(originalCategories.Where(originalCategory => availableCategoryIds.Contains(originalCategory.Id) && !originalCategoriesSorted.Contains(originalCategory)).OrderBy(c => c.Name));
        //        }
        //        else
        //        {
        //            originalCategoriesSorted.AddRange(originalCategories.Where(originalCategory => availableCategoryIds.Contains(originalCategory.Id)));
        //        }
        //    }
        //    else
        //    {
        //        originalCategoriesSorted.AddRange(originalCategories.Where(originalCategory => availableCategoryIds.Contains(originalCategory.Id)).OrderBy(c => c.Name));
        //    }


        //    for (int i = 0; i < originalCategoriesSorted.Count; i++)
        //    {
        //        list.Add(new CheckBoxListItem
        //        {
        //            Name = originalCategoriesSorted[i].Name,
        //            Value = originalCategoriesSorted[i].Id.ToString(CultureInfo.InvariantCulture),
        //            IsDisabled = availableCategoryIds.All(c => c != originalCategoriesSorted[i].Id),
        //            IsSelected = selectedCategoryIds.Any(c => c == originalCategoriesSorted[i].Id),
        //            RouteName = searchManufacturerId == 0 ? "Category" : "ManufacturerCategory",
        //            SeName = searchManufacturerId == 0 ? SeoExtensions.GetSeName(originalCategoriesSorted[i].Id, "Category", 0) : SeoExtensions.GetSeName(searchManufacturerId, originalCategoriesSorted[i].Id, "ManufacturerCategory", 0),
        //            IsTop = (hasOrdering & (i < 5))
        //        });
        //    }

        //    return list;
        //}

        //[NonAction]
        //private static List<CheckBoxListItem> PrepareFilterCheckBoxList(IEnumerable<Manufacturer> originalManufacturers, IEnumerable<int> availableManufacturerIds, IEnumerable<int> selectedManufacturerIds, int searchCategoryId = 0)
        //{
        //    var list = new List<CheckBoxListItem>();

        //    if (originalManufacturers == null || availableManufacturerIds == null)
        //        return list;

        //    list.AddRange(originalManufacturers.Where(originalManufacturer => availableManufacturerIds.Contains(originalManufacturer.Id)).Select(originalManufacturer => new CheckBoxListItem
        //    {
        //        Name = originalManufacturer.Name,
        //        Value = originalManufacturer.Id.ToString(CultureInfo.InvariantCulture),
        //        IsDisabled = availableManufacturerIds.All(i => i != originalManufacturer.Id),
        //        IsSelected = selectedManufacturerIds.Any(i => i == originalManufacturer.Id),
        //        RouteName = searchCategoryId == 0 ? "Manufacturer" : "CategoryManufacturer",
        //        SeName = searchCategoryId == 0 ? SeoExtensions.GetSeName(originalManufacturer.Id, "Manufacturer", 0) : SeoExtensions.GetSeName(searchCategoryId, originalManufacturer.Id, "CategoryManufacturer", 0)
        //    }));

        //    return list;
        //}

        //[NonAction]
        //private static List<CheckBoxListItem> PrepareFilterCheckBoxList(IEnumerable<Core.Domain.Vehicles.PriceRange> originalPriceRanges, IEnumerable<int> availablePriceRangeIds, IEnumerable<int> selectedPriceRangeIds)
        //{
        //    var list = new List<CheckBoxListItem>();

        //    if (originalPriceRanges == null || availablePriceRangeIds == null)
        //        return list;

        //    list.AddRange(originalPriceRanges.Where(originalPriceRange => availablePriceRangeIds.Contains(originalPriceRange.Id)).Select(originalPriceRange => new CheckBoxListItem
        //    {
        //        Name = string.Format("${0} - {1}", (int)originalPriceRange.MinPrice, (originalPriceRange.MaxPrice > 2000) ? "and up" : ((int)originalPriceRange.MaxPrice).ToString(CultureInfo.InvariantCulture)),
        //        Value = originalPriceRange.Id.ToString(CultureInfo.InvariantCulture),
        //        IsDisabled = availablePriceRangeIds.All(i => i != originalPriceRange.Id),
        //        IsSelected = selectedPriceRangeIds.Any(i => i == originalPriceRange.Id)
        //    }));

        //    return list;
        //}

        //[HttpPost]
        //public ActionResult GetTireSpecificationValues(int? sectionValue, int? aspectValue, int? rimValue, string target)
        //{
        //    var attributeValues = this.solrService.GetTireSpecification(sectionValue, aspectValue, rimValue, target, this.storeContext.CurrentStore.Id);
        //    if (!attributeValues.Any())
        //    {
        //        return this.Json(new { error = "Could not get specification attribute values." });
        //    }

        //    var specificationAttributes = this.staticCacheManager.Get(SPEC_ATTRIBUTES_ALL_KEY, CacheTime, () => this.specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttributeIds(this.attributeIDs));
        //    var specificationAttributeId = target == "section" ? sectionSpecificationAttributeId : target == "aspect" ? aspectSpecificationAttributeId : rimSpecificationAttributeId;
        //    var aspectSpecificationAttributeOptions = specificationAttributes.Where(m => m.SpecificationAttributeId == specificationAttributeId);
        //    if (aspectSpecificationAttributeOptions == null)
        //    {
        //        return this.Json(new { error = "Could not get specification attribute values." });
        //    }

        //    var model = aspectSpecificationAttributeOptions
        //        .ToList()
        //        .Join(attributeValues, a => a.Id, b => b, (a, b) => a)
        //        .Where(m => m.Name.IsDecimal())
        //        .Select(m => new { m.Id, Value = decimal.Parse(m.Name) })
        //        .GroupBy(m => m.Value)
        //        .Select(m => new { Id = m.First().Id, Value = m.Key })
        //        .OrderBy(m => m.Value)
        //        .Select(m => new { m.Id, Name = m.Value.ToString("##.##") })
        //        .ToList();

        //    return this.Json(model);
        //}

        //[NonAction]
        //private static List<CheckBoxListItem> PrepareFilterCheckBoxList(IEnumerable<SpecificationAttributeOption> original, IEnumerable<int> selected)
        //{
        //    var list = new List<CheckBoxListItem>();

        //    if (original == null || selected == null)
        //    {
        //        return list;
        //    }


        //    list.AddRange(original.Select(m => new CheckBoxListItem
        //    {
        //        Name = m.Name,
        //        Value = m.Id.ToString(CultureInfo.InvariantCulture),
        //        IsSelected = selected.Any(i => i == m.Id)
        //    }));

        //    return list;
        //}

        //[NonAction]
        //private List<BannerModel> PrepareBannerModel(int entityId, string entityType, string entityName)
        //{
        //    var banners = _bannerService.GetAuthorizeBanners(entityId, entityType);
        //    var bannerPictureModel = new List<BannerModel>();
        //    if (banners.Any())
        //    {
        //        //prepare picture model
        //        var bannerPictureCacheKey = string.Format(BANNER_PICTURE_MODEL_KEY, entityId, entityType, storeContext.CurrentStore.Id, string.Join(",", banners.Select(x => x.Id)));
        //        bannerPictureModel = staticCacheManager.Get(bannerPictureCacheKey, () =>
        //        {
        //            var bannerListModel = new List<BannerModel>();
        //            foreach (var banner in banners)
        //            {

        //                string bannerUrl = string.Empty;
        //                string mobileBannerUrl = string.Empty;

        //                if (!string.IsNullOrWhiteSpace(banner.BannerPicturePath))
        //                    bannerUrl = $"{this.webHelper.GetStoreLocation()}content/images/{BannerDefaults.BannerFolderName}/{banner.BannerPicturePath}";
        //                else
        //                {
        //                    var bannerPicture = pictureService.GetPictureById(banner.BannerPictureId);
        //                    bannerUrl = pictureService.GetPictureUrl(bannerPicture);
        //                }
        //                if (!string.IsNullOrWhiteSpace(banner.MobileBannerPicturePath))
        //                    mobileBannerUrl = $"{this.webHelper.GetStoreLocation()}content/images/{BannerDefaults.BannerFolderName}/{banner.MobileBannerPicturePath}";
        //                else
        //                {
        //                    var mobileBannerPicture = banner.MobileBannerPictureId > 0 ? pictureService.GetPictureById(banner.MobileBannerPictureId) : pictureService.GetPictureById(banner.BannerPictureId);
        //                    mobileBannerUrl = pictureService.GetPictureUrl(mobileBannerPicture);
        //                }

        //                var bannerModel = new BannerModel
        //                {
        //                    BannerImageUrl = bannerUrl,
        //                    MobileBannerImageUrl = mobileBannerUrl,
        //                    Title = string.Format(localizationService.GetResource("Media.Manufacturer.ImageLinkTitleFormat"), entityName),
        //                    AlternateText = string.Format(localizationService.GetResource("Media.Manufacturer.ImageAlternateTextFormat"), banner.AlterText),
        //                    StartDateTimeUtc = banner.StartDateTimeUtc,
        //                    EndDateTimeUtc = banner.EndDateTimeUtc,
        //                    Id = banner.Id,
        //                };

        //                bannerListModel.Add(bannerModel);
        //            }
        //            return bannerListModel;
        //        });
        //    }

        //    return bannerPictureModel;
        //}

        //#endregion
    }
}