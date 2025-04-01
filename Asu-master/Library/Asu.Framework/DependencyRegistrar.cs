using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Asu.Core;
using Asu.Core.Caching;
using Asu.Core.Configuration;
using Asu.Core.Data;
using Asu.Core.Fakes;
using Asu.Core.Infrastructure;
using Asu.Core.Infrastructure.DependencyManagement;
using Asu.Core.Plugins;
using Asu.Data;
using Asu.Services.Affiliates;
using Asu.Services.Authentication;
using Asu.Services.Authentication.External;
using Asu.Services.Blogs;
using Asu.Services.Catalog;
using Asu.Services.Cms;
using Asu.Services.Common;
using Asu.Services.Configuration;
using Asu.Services.Customers;
using Asu.Services.Directory;
using Asu.Services.Discounts;
using Asu.Services.Events;
using Asu.Services.ExportImport;
using Asu.Services.Forums;
//using Asu.Services.Helpers;
using Asu.Services.Installation;
using Asu.Services.Localization;
using Asu.Services.Logging;
using Asu.Services.Media;
using Asu.Services.Messages;
using Asu.Services.News;
using Asu.Services.Orders;
using Asu.Services.Payments;
using Asu.Services.Polls;
using Asu.Services.Security;
using Asu.Services.Seo;
using Asu.Services.Shipping;
using Asu.Services.Stores;
using Asu.Services.Tasks;
using Asu.Services.Tax;
using Asu.Services.Topics;
using Asu.Services.Vehicles;
using Asu.Services.Vendors;
//using Nop.Web.Framework.Mvc.Routes;
//using Nop.Web.Framework.Themes;
//using Nop.Web.Framework.UI;
using Asu.Services.Customization;
using Asu.Services;
//using Nop.Services.Solr;

namespace Asu.Framework
{
    using Asu.Services.BannerPicture;
    using Asu.Services.Blogs;
    using Asu.Services.Cms;
    using Asu.Services.Common;
    using Asu.Services.ExportImport;
    using Asu.Services.Installation;
    using Asu.Services.News;
    using Asu.Services.Polls;
    //using AutofacContrib.SolrNet;
    //using AutofacContrib.SolrNet.Config;
    //using AutofacContrib.SolrNetCloud;
    //using Asu.Core.Domain.Solr;
    using Asu.Services.SalesQuotes;
    using Asu.Services.Topics;
    using Asu.Services.Warranty;
    using Asu.Framework.Themes;
    using Asu.Services.SprPkp;
    using Asu.Services.CustomerServices;
    using Asu.Core.CustomerAsu;
    using Asu.Services.Helpers;
    using Asu.Framework;
    using Asu.Framework.Mvc.Routes;
    using Asu.Services;
    using Asu.Mapping.DocumentStatusService;
    using Asu.Services.UsersTasks;
    using Asu.Mapping.TTO;
    using Asu.Mapping.Skm;
    using Asu.Framework.UI;
    using Asu.Mapping.Malahit;
    using Asu.Mapping.Msi;
    using Asu.Mapping.Metrology;
    using Asu.Mapping.Work;
    using Autofac.Integration.WebApi;




    //using SolrNet;
    //using SolrNet.Cloud;
    //using SolrNet.Cloud.ZooKeeperClient;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            //user agent helper
            builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();

            #region WC

            // WC vehicle helper
            builder.RegisterType<VehicleHelper>().As<IVehicleHelper>().InstancePerLifetimeScope();
            // WC custom helper
            builder.RegisterType<CustomHelper>().As<ICustomHelper>().InstancePerLifetimeScope();
            // WC rating service
            builder.RegisterType<RatingService>().As<IRatingService>().InstancePerLifetimeScope();
            // WC Amazon task service
            //builder.RegisterType<AmazonPaymentsAdvancedOrderService>().As<IAmazonPaymentsAdvancedOrderService>().InstancePerLifetimeScope();
            // WC VehicleService
            builder.RegisterType<VehicleService>().As<IVehicleService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")).InstancePerLifetimeScope();
            // WC Custom Service
            builder.RegisterType<CustomService>().As<ICustomService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")).InstancePerLifetimeScope();
            //builder.RegisterType<GlimpseSecurityPolicy>().As<IRuntimePolicy>().InstancePerLifetimeScope();
            // WC Amazon Payments Service
            //builder.RegisterType<AmazonPaymentsService>().As<IAmazonPaymentsService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")).InstancePerLifetimeScope();


            // Solr Cloud 
            //var solrProvider = new SolrCloudStateProvider(ConfigurationManager.AppSettings["ZKConnectionString"]);

            //builder.Register<ISolrBasicOperations<SolrCategory>>(
            //container => new SolrCloudBasicOperations<SolrCategory>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["BoatplicityCategory"],
            //        true));

            //builder.Register<ISolrBasicReadOnlyOperations<SolrCategory>>(
            //    container => new SolrCloudBasicOperations<SolrCategory>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["BoatplicityCategory"],
            //        true));

            //builder.Register<ISolrOperations<SolrCategory>>(
            //    container => new SolrCloudOperations<SolrCategory>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["BoatplicityCategory"],
            //        true));

            //builder.Register<ISolrReadOnlyOperations<SolrCategory>>(
            //    container => new SolrCloudOperations<SolrCategory>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["BoatplicityCategory"],
            //        true));


            ////product
            //builder.Register<ISolrBasicOperations<SolrProduct>>(
            //container => new SolrCloudBasicOperations<SolrProduct>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["Boatplicity"],
            //        true));

            //builder.Register<ISolrBasicReadOnlyOperations<SolrProduct>>(
            //    container => new SolrCloudBasicOperations<SolrProduct>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["Boatplicity"],
            //        true));

            //builder.Register<ISolrOperations<SolrProduct>>(
            //    container => new SolrCloudOperations<SolrProduct>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["Boatplicity"],
            //        true));

            //builder.Register<ISolrReadOnlyOperations<SolrProduct>>(
            //    container => new SolrCloudOperations<SolrProduct>(
            //        solrProvider,
            //        container.Resolve<ISolrOperationsProvider>(),
            //        ConfigurationManager.AppSettings["Boatplicity"],
            //        true));



            //builder.RegisterModule(new SolrNetCloudModule(solrProvider, ConfigurationManager.AppSettings["BoatplicityCategory"]));
            //builder.RegisterModule(new SolrNetCloudModule(solrProvider, ConfigurationManager.AppSettings["Boatplicity"]));

            ////builder.RegisterModule(new SolrNetModule(cores));
            //builder.RegisterType<SolrService>().As<ISolrService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")).InstancePerLifetimeScope();
            // WC Freshdesk Ticket Service
            builder.RegisterType<FreshdeskTicketService>().As<IFreshdeskTicketService>().InstancePerLifetimeScope();
            // WC Google Tag Manager Service
            builder.RegisterType<GoogleTagManagerService>().As<IGoogleTagManagerService>().InstancePerRequest();
            // WC Return Service
            builder.RegisterType<ReturnService>().As<IReturnService>().InstancePerRequest();
            // WC. ShopperApproved Revews Service
            builder.RegisterType<ShopperApprovedRevewsService>().As<IShopperApprovedReviewsService>().InstancePerLifetimeScope();
            // WC. Digital Data Service
            builder.RegisterType<DigitalDataService>().As<IDigitalDataService>().InstancePerLifetimeScope();
            // WC. ProductGroup Service
            builder.RegisterType<ProductGroupService>().As<IProductGroupService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")).InstancePerLifetimeScope();

            #endregion

            builder.RegisterType<DbHelper>().As<IDbHelper>().InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //Apicontrollers
            builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray());

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();


            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new NopObjectContext(dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<IDbContext>(c => new NopObjectContext(dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();
            }


            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //plugins
            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerLifetimeScope();


            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            //store context
            builder.RegisterType<WebStoreContext>().As<IStoreContext>().InstancePerLifetimeScope();

            //services
            builder.RegisterType<BackInStockSubscriptionService>().As<IBackInStockSubscriptionService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<WcCategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<CompareProductsService>().As<ICompareProductsService>().InstancePerLifetimeScope();
            builder.RegisterType<RecentlyViewedProductsService>().As<IRecentlyViewedProductsService>().InstancePerLifetimeScope();
            builder.RegisterType<ManufacturerService>().As<IManufacturerService>().InstancePerLifetimeScope();
            builder.RegisterType<BannerService>().As<IBannerService>().InstancePerLifetimeScope();
            builder.RegisterType<ManufacturerService>().As<IManufacturerService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")).InstancePerLifetimeScope();
            builder.RegisterType<PriceFormatter>().As<IPriceFormatter>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeFormatter>().As<IProductAttributeFormatter>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeParser>().As<IProductAttributeParser>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeService>().As<IProductAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<CopyProductService>().As<ICopyProductService>().InstancePerLifetimeScope();
            builder.RegisterType<SpecificationAttributeService>().As<ISpecificationAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductTemplateService>().As<IProductTemplateService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryTemplateService>().As<ICategoryTemplateService>().InstancePerLifetimeScope();
            builder.RegisterType<ManufacturerTemplateService>().As<IManufacturerTemplateService>().InstancePerLifetimeScope();
            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<ProductTagService>().As<IProductTagService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<AddressAttributeFormatter>().As<IAddressAttributeFormatter>().InstancePerLifetimeScope();
            builder.RegisterType<AddressAttributeParser>().As<IAddressAttributeParser>().InstancePerLifetimeScope();
            builder.RegisterType<AddressAttributeService>().As<IAddressAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateService>().As<IAffiliateService>().InstancePerLifetimeScope();
            builder.RegisterType<VendorService>().As<IVendorService>().InstancePerLifetimeScope();
            builder.RegisterType<SearchTermService>().As<ISearchTermService>().InstancePerLifetimeScope();
            builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<FulltextService>().As<IFulltextService>().InstancePerLifetimeScope();
            builder.RegisterType<MaintenanceService>().As<IMaintenanceService>().InstancePerLifetimeScope();

            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();

            

            

            builder.RegisterType<CustomerAttributeParser>().As<ICustomerAttributeParser>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerAttributeService>().As<ICustomerAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRegistrationService>().As<ICustomerRegistrationService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerReportService>().As<ICustomerReportService>().InstancePerLifetimeScope();

            builder.RegisterType<WarrantyService>().As<IWarrantyService>().InstancePerLifetimeScope();
            builder.RegisterType<SalesQuoteService>().As<ISalesQuoteService>().InstancePerLifetimeScope();

            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<PermissionService>().As<IPermissionService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<AclService>().As<IAclService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<PriceCalculationService>().As<IPriceCalculationService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<GeoLookupService>().As<IGeoLookupService>().InstancePerLifetimeScope();
            builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope();
            builder.RegisterType<MeasureService>().As<IMeasureService>().InstancePerLifetimeScope();
            builder.RegisterType<StateProvinceService>().As<IStateProvinceService>().InstancePerLifetimeScope();

            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();
            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<StoreMappingService>().As<IStoreMappingService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<DiscountService>().As<IDiscountService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomDiscountService>().As<ICustomDiscountService>().InstancePerLifetimeScope();


            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<SettingService>().As<ISettingService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());

            //pass MemoryCacheManager as cacheManager (cache locales between requests)
            builder.RegisterType<LocalizationService>().As<ILocalizationService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();

            //pass MemoryCacheManager as cacheManager (cache locales between requests)
            builder.RegisterType<LocalizedEntityService>().As<ILocalizedEntityService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();

            builder.RegisterType<DownloadService>().As<IDownloadService>().InstancePerLifetimeScope();
            builder.RegisterType<WcPictureService>().As<IPictureService>().InstancePerLifetimeScope();

            builder.RegisterType<MessageTemplateService>().As<IMessageTemplateService>().InstancePerLifetimeScope();
            builder.RegisterType<QueuedEmailService>().As<IQueuedEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<SendGridMessageTemplateService>().As<ISendGridMessageTemplateService>().InstancePerLifetimeScope();
            builder.RegisterType<QueuedEmailSendGridService>().As<IQueuedEmailSendGridService>().InstancePerLifetimeScope();
            builder.RegisterType<ManualOrderService>().As<IManualOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<NewsLetterSubscriptionService>().As<INewsLetterSubscriptionService>().InstancePerLifetimeScope();
            builder.RegisterType<CampaignService>().As<ICampaignService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailAccountService>().As<IEmailAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkflowMessageService>().As<IWorkflowMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageTokenProvider>().As<IMessageTokenProvider>().InstancePerLifetimeScope();
            builder.RegisterType<Tokenizer>().As<ITokenizer>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<KlaviyoService>().As<IKlaviyoService>().InstancePerLifetimeScope();

            builder.RegisterType<CheckoutAttributeFormatter>().As<ICheckoutAttributeFormatter>().InstancePerLifetimeScope();
            builder.RegisterType<CheckoutAttributeParser>().As<ICheckoutAttributeParser>().InstancePerLifetimeScope();
            builder.RegisterType<CheckoutAttributeService>().As<ICheckoutAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<GiftCardService>().As<IGiftCardService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderReportService>().As<IOrderReportService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderProcessingService>().As<IOrderProcessingService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomOrderTotalCalculationService>().As<IOrderTotalCalculationService>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRecommendationService>().As<IProductRecommendationService>().InstancePerLifetimeScope();
            builder.RegisterType<ShippingInsuranceService>().As<IShippingInsuranceService>().InstancePerLifetimeScope();
            builder.RegisterType<ReturnExtensionService>().As<IReturnExtensionService>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();

            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();


            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<UrlRecordService>().As<IUrlRecordService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<ShipmentService>().As<IShipmentService>().InstancePerLifetimeScope();
            builder.RegisterType<ShippingService>().As<IShippingService>().InstancePerLifetimeScope();

            builder.RegisterType<TaxCategoryService>().As<ITaxCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<TaxService>().As<ITaxService>().InstancePerLifetimeScope();
            builder.RegisterType<TaxCategoryService>().As<ITaxCategoryService>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<CustomerActivityService>().As<ICustomerActivityService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseFastInstallationService"]) &&
                Convert.ToBoolean(ConfigurationManager.AppSettings["UseFastInstallationService"]))
            {
                builder.RegisterType<SqlFileInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            }

            builder.RegisterType<ForumService>().As<IForumService>().InstancePerLifetimeScope();

            builder.RegisterType<PollService>().As<IPollService>().InstancePerLifetimeScope();
            builder.RegisterType<BlogService>().As<IBlogService>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetService>().As<IWidgetService>().InstancePerLifetimeScope();
            builder.RegisterType<TopicService>().As<ITopicService>().InstancePerLifetimeScope();
            builder.RegisterType<NewsService>().As<INewsService>().InstancePerLifetimeScope();


            //Блок регистрации сервисов Asu.Avia
            ///MSI
            builder.RegisterType<SprPkpService>().As<ISprPkpService>().InstancePerLifetimeScope();
            builder.RegisterType<MalahitHelpers>().As<IMalahitHelpers>().InstancePerLifetimeScope();


            //Блок регистрации сервисов Asu.Avia
            ///TTO
            builder.RegisterType<TtoService>().As<ITtoService>().InstancePerLifetimeScope();

            //Блок регистрации сервисов Asu.Avia
            ///SKM
            builder.RegisterType<DirectoryOfMaterialCodifiersService>().As<IDirectoryOfMaterialCodifiersService>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentStatusService>().As<IDocumentStatus>().InstancePerLifetimeScope();          
            builder.RegisterType<UslSkmService>().As<IUslSkmService>().InstancePerLifetimeScope();
            builder.RegisterType<GostMaterService>().As<IGostMaterService>().InstancePerLifetimeScope();
            builder.RegisterType<NmMaterService>().As<INmMaterService>().InstancePerLifetimeScope();
            builder.RegisterType<MarkaMaterialService>().As<IMarkaMaterialService>().InstancePerLifetimeScope();
            builder.RegisterType<OgtService>().As<IOgtService>().InstancePerLifetimeScope();
            builder.RegisterType<GrMaterService>().As<IGrMaterService>().InstancePerLifetimeScope();
            builder.RegisterType<MemoAddingMaterialCodeService>().As<IMemoAddingMaterialCode>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfMeasurementService>().As<IUnitOfMeasurementService>().InstancePerLifetimeScope();
            builder.RegisterType<SprKgrService>().As<ISprKgrService>().InstancePerLifetimeScope();
            builder.RegisterType<OtsService>().As<IOtsService>().InstancePerLifetimeScope();
            builder.RegisterType<SprCenMaterService>().As<ISprCenMaterService>().InstancePerLifetimeScope();
            builder.RegisterType<SkmHelper>().As<ISkmHelper>().InstancePerLifetimeScope();
            builder.RegisterType<SprPrKmService>().As<ISprPrKmService>().InstancePerLifetimeScope();
            

            //Блок регистрации сервисов Asu.Avia
            ///Msi
            builder.RegisterType<SprTemService>().As<ISprTemService>().InstancePerLifetimeScope();
            builder.RegisterType<SprPerizdService>().As<ISprPerizdService>().InstancePerLifetimeScope();
            builder.RegisterType<SprMashService>().As<ISprMashService>().InstancePerLifetimeScope();
            builder.RegisterType<DerIzdService>().As<IDerIzdService>().InstancePerLifetimeScope();
            builder.RegisterType<TreeProductService>().As<ITreeProductService>().InstancePerLifetimeScope();
            builder.RegisterType<srez_sostoyanieService>().As<Isrez_sostoyanieService>().InstancePerLifetimeScope();

            //Блок регистрации сервисов Asu.Avia
            ///Metrology
            builder.RegisterType<AccuracyClassService>().As<IAccuracyClassService>().InstancePerLifetimeScope();
            builder.RegisterType<MetrologyHelper>().As<IMetrologyHelper>().InstancePerLifetimeScope();
            builder.RegisterType<MetrologyService>().As<IMetrologyService>().InstancePerLifetimeScope();
            builder.RegisterType<RodPoverkService>().As<IRodPoverkService>().InstancePerLifetimeScope();
            builder.RegisterType<PodgrPribService>().As<IPodgrPribService>().InstancePerLifetimeScope();
            builder.RegisterType<NaznPribService>().As<INaznPribService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkShopService>().As<IWorkShopService>().InstancePerLifetimeScope();
            builder.RegisterType<PeriodPoverkService>().As<IPeriodPoverkService>().InstancePerLifetimeScope();
            builder.RegisterType<TipPribService>().As<ITipPribService>().InstancePerLifetimeScope();
            builder.RegisterType<KonservService>().As<IKonservService>().InstancePerLifetimeScope();

            //Блок регистрации сервисов Asu.Avia
            ///DirectiveWork
            builder.RegisterType<DirectiveWorkService>().As<IDirectiveWorkService>().InstancePerLifetimeScope();
            


            builder.RegisterType<UserTaskService>().As<IUserTaskService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository<ApplicationUser>>().InstancePerLifetimeScope();
            //


            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            builder.RegisterType<SitemapGenerator>().As<ISitemapGenerator>().InstancePerLifetimeScope();
            ///////////////
            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();
            ///////////////
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();

            builder.RegisterType<ExportManager>().As<IExportManager>().InstancePerLifetimeScope();
            builder.RegisterType<ImportManager>().As<IImportManager>().InstancePerLifetimeScope();
            builder.RegisterType<PdfService>().As<IPdfService>().InstancePerLifetimeScope();
            builder.RegisterType<ThemeProvider>().As<IThemeProvider>().InstancePerLifetimeScope();
            builder.RegisterType<ThemeContext>().As<IThemeContext>().InstancePerLifetimeScope();


            builder.RegisterType<ExternalAuthorizer>().As<IExternalAuthorizer>().InstancePerLifetimeScope();
            builder.RegisterType<OpenAuthenticationService>().As<IOpenAuthenticationService>().InstancePerLifetimeScope();
            //builder.RegisterType<EbayEventNotificationService>().As<IEbayEventNotificationService>().InstancePerLifetimeScope();


            //builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();

            //Register event consumers
            var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            foreach (var consumer in consumers)
            {
                builder.RegisterType(consumer)
                    .As(consumer.FindInterfaces((type, criteria) =>
                    {
                        var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                        return isMatch;
                    }, typeof(IConsumer<>)))
                    .InstancePerLifetimeScope();
            }
            builder.RegisterType<EventPublisher>().As<IEventPublisher>().SingleInstance();
            builder.RegisterType<SubscriptionService>().As<ISubscriptionService>().SingleInstance();
        }

        public int Order
        {
            get { return 0; }
        }
    }


    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    var currentStoreId = c.Resolve<IStoreContext>().CurrentStore.Id;
                    //uncomment the code below if you want load settings per store only when you have two stores installed.
                    //var currentStoreId = c.Resolve<IStoreService>().GetAllStores().Count > 1
                    //    c.Resolve<IStoreContext>().CurrentStore.Id : 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>(currentStoreId);
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }

}
