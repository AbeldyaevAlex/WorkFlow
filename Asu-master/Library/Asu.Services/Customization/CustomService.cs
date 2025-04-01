using System.Data;
//using Kount.Ris;
using System.Transactions;
using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customization;
using System;
using System.Linq;
using System.Collections.Generic;
using Asu.Services.Messages;
using CartItem = Asu.Core.Domain.Customization.CartItem;

namespace Asu.Services.Customization
{
    using System.Configuration;

    using Core.Caching;
    using Data;
    using System.Web;
    using System.IO;
    using Logging;
    using Core.Domain.Orders;
    using Core;
    using Core.Domain.Media;

    using Tax;
    using Catalog;
    using Directory;
    using Media;
    using Core.Domain.Tax;

    using Asu.Core.Domain.Discounts;
    using Asu.Core.Domain.Stores;
    using Asu.Core.Domain.Vehicles;

    using Orders;
    using Asu.Services.Security;
    using Asu.Services.Stores;
    using Asu.Core.Domain.Returns;

    public sealed class CustomService : ICustomService
    {
        #region Constants

        private const string MANUFACTURERS_ALL_KEY = "WC.manufacturers.all";
        /// <summary>
        /// id - discountId
        /// </summary>
        private const string MANUFACTURERS_ALL_DISCOUNT_KEY = "WC.manufacturers.discount.all-{0}";
        private const string MANUFACTURER_FIRST_SYMBOLS_KEY = "WC.manufacturer.first.symbol.all";
        private const string MANUFACTURER_FIRST_SYMBOLS_DISCOUNT_KEY = "WC.manufacturer.first.symbol.discount";
        private const string MANUFACTURER_FIRST_ALPHABET_SYMBOLS_KEY = "WC.manufacturer.first.alphabet.symbol.all";
        private const string NUMERIC_AVAILABLE_SYMBOLS = "0123456789";

        private const string CART_PICTURE_MODEL_KEY = "Nop.pres.cart.picture-{0}-{1}-{2}-{3}-{4}-{5}";
        private const string CART_PICTURE_PATTERN_KEY = "Nop.pres.cart.picture";

        #endregion

        #region Fields

        private readonly ICacheManager cacheManager;
        private readonly ICustomHelper customHelper;
        private readonly IRepository<SignUpCoupon> signUpCouponRepository;
        private readonly IRepository<Manufacturer> manufacturerRepository;
        private readonly IRepository<AdditionalImage> additionalImageRepository;
        private readonly IRepository<GoogleImage> googleImageRepository;
        private readonly IRepository<QueuedMessagesSendTaskLocker> queuedMessagesSendTaskLockerRepository;
        private readonly IRepository<OrderShippedEmailSendTaskLocker> orderShippedEmailSendTaskLockerRepository;
        private readonly IRepository<ProductCashRebate> productCashRebateRepository;
        private readonly IRepository<OrderProductWithRebates> orderProductWithRebatesRepository;
        private readonly IRepository<OrderWithRebates> orderWithRebatesRepository;
        private readonly IRepository<OrderWithRebatesNotification> orderWithRebatesNotificationRepository;
        private readonly IRepository<OrderProductToReview> orderProductToReviewRepository;
        private readonly IRepository<ProductReviewCustomerNotification> productReviewCustomerNotificationRepository;
        private readonly IRepository<OrderShipmentEta> orderShipmentEtaRepository;
        private readonly IRepository<OrderEtaNotification> orderEtaNotificationRepository;
        private readonly IRepository<Locker> lockerRepository;
        private readonly IRepository<OrderExtra> orderExtraRepository;
        private readonly IRepository<StoreMapping> storeMappingRepository;
        private readonly IRepository<ProductManufacturer> productManufacturerRepository;
        private readonly IRepository<Discount> discountRepository;
        private readonly IRepository<BackInStockSubscription> backInStockSubscriptionRepository;
        private readonly IRepository<EbayMarketplaceAccountDeletionNotification> ebayMarketplaceAccountDeletionNotificationRepository;
        private readonly IDbContext dbContext;
        private readonly IDataProvider dataProvider;
        private readonly ILogger logger;

        private readonly IWorkContext workContext;
        private readonly ITaxService taxService;
        private readonly IPriceCalculationService priceCalculationService;
        private readonly ICurrencyService currencyService;
        private readonly MediaSettings mediaSettings;
        private readonly IPictureService pictureService;
        private readonly IWebHelper webHelper;
        private readonly IProductAttributeParser productAttributeParser;
        private readonly IStoreContext storeContext;
        private readonly TaxSettings taxSettings;
        private readonly IOrderTotalCalculationService orderTotalCalculationService;
        private readonly IWorkflowMessageService workflowMessageService;
        private readonly IRepository<Product> _productRepository;
        private readonly CatalogSettings catalogSettings;
        private readonly IRepository<ProductExtra> _productExtraRepository;
        private readonly IProductService _prouctService;

        #endregion

        #region Ctor

        public CustomService(ICacheManager cacheManager,
            ICustomHelper customHelper,
            IRepository<SignUpCoupon> signUpCouponRepository,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<AdditionalImage> additionalImageRepository,
            IRepository<GoogleImage> googleImageRepository,
            IRepository<QueuedMessagesSendTaskLocker> queuedMessagesSendTaskLockerRepository,
            IRepository<OrderShippedEmailSendTaskLocker> orderShippedEmailSendTaskLockerRepository,
            IRepository<ProductCashRebate> productCashRebateRepository,
            IRepository<OrderProductWithRebates> orderProductWithRebatesRepository,
            IRepository<OrderWithRebates> orderWithRebatesRepository,
            IRepository<OrderWithRebatesNotification> orderWithRebatesNotificationRepository,
            IRepository<OrderProductToReview> orderProductToReviewRepository,
            IRepository<ProductReviewCustomerNotification> productReviewCustomerNotificationRepository,
            IRepository<OrderShipmentEta> orderShipmentEtaRepository,
            IRepository<OrderEtaNotification> orderEtaNotificationRepository,
            IRepository<Locker> lockerRepository,
            IRepository<OrderExtra> orderExtraRepository,
            IRepository<ProductManufacturer> productManufacturer,
            IRepository<StoreMapping> storeMapping,
            IRepository<ProductExtra> _productExtraRepository,
            IRepository<Product> productRepository,
            IRepository<Discount> discountRepository,
            IRepository<BackInStockSubscription> backInStockSubscriptionRepository,
            IDbContext dbContext,
            IDataProvider dataProvider,
            ILogger logger,
            IWorkContext workContext,
            ITaxService taxService,
            IPriceCalculationService priceCalculationService,
            ICurrencyService currencyService,
            MediaSettings mediaSettings,
            IPictureService pictureService,
            IWebHelper webHelper,
            IProductAttributeParser productAttributeParser,
            IStoreContext storeContext,
            TaxSettings taxSettings,
            IOrderTotalCalculationService orderTotalCalculationService,
            IWorkflowMessageService workflowMessageService,
            IProductService prouctService,
            CatalogSettings catalogSettings,
            IRepository<EbayMarketplaceAccountDeletionNotification> ebayMarketplaceAccountDeletionNotificationRepository)
        {
            this.cacheManager = cacheManager;
            this.customHelper = customHelper;
            this.signUpCouponRepository = signUpCouponRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.additionalImageRepository = additionalImageRepository;
            this.googleImageRepository = googleImageRepository;
            this.queuedMessagesSendTaskLockerRepository = queuedMessagesSendTaskLockerRepository;
            this.orderShippedEmailSendTaskLockerRepository = orderShippedEmailSendTaskLockerRepository;
            this.productCashRebateRepository = productCashRebateRepository;
            this.orderProductWithRebatesRepository = orderProductWithRebatesRepository;
            this.orderWithRebatesRepository = orderWithRebatesRepository;
            this.orderWithRebatesNotificationRepository = orderWithRebatesNotificationRepository;
            this.orderProductToReviewRepository = orderProductToReviewRepository;
            this.productReviewCustomerNotificationRepository = productReviewCustomerNotificationRepository;
            this.orderShipmentEtaRepository = orderShipmentEtaRepository;
            this.orderEtaNotificationRepository = orderEtaNotificationRepository;
            this.lockerRepository = lockerRepository;
            this.orderExtraRepository = orderExtraRepository;
            this.discountRepository = discountRepository;
            this.backInStockSubscriptionRepository = backInStockSubscriptionRepository;
            this.dbContext = dbContext;
            this.dataProvider = dataProvider;
            this.logger = logger;
            this.productManufacturerRepository = productManufacturer;

            this.workContext = workContext;
            this.taxService = taxService;
            this.priceCalculationService = priceCalculationService;
            this.currencyService = currencyService;
            this.mediaSettings = mediaSettings;
            this.pictureService = pictureService;
            this.webHelper = webHelper;
            this.productAttributeParser = productAttributeParser;
            this.storeContext = storeContext;
            this.taxSettings = taxSettings;
            this.orderTotalCalculationService = orderTotalCalculationService;
            this.workflowMessageService = workflowMessageService;
            this.catalogSettings = catalogSettings;
            this.storeMappingRepository = storeMapping;
            this._productExtraRepository = _productExtraRepository;
            this._prouctService = prouctService;
            this._productRepository = productRepository;
            this.ebayMarketplaceAccountDeletionNotificationRepository = ebayMarketplaceAccountDeletionNotificationRepository;
        }

        #endregion

        #region Public Methods

        public void AddWelcomeCookie()
        {
            this.customHelper.AddToCookie("AP_WelcomeCouponSignUp", "1", DateTime.UtcNow.AddHours(24 * 14));
        }

        public void AddSignCouponNotificationToDb(string email)
        {
            this.signUpCouponRepository.Insert(new SignUpCoupon(email));
        }

        public List<string> GetAllManufacturerFirstSymbols()
        {
            return this.cacheManager.Get(MANUFACTURER_FIRST_SYMBOLS_KEY, () =>
            {
                var productIdsCache = this._prouctService.GetProductIds();

                var query = this.manufacturerRepository.TableNoTracking;               

                var result = from man in query
                        join mp in this.productManufacturerRepository.TableNoTracking on man.Id equals mp.ManufacturerId
                            join p in productIdsCache on mp.ProductId equals p
                            where !man.Deleted && man.Published
                            select man.Name.Substring(0, 1);

                return result.Distinct().OrderBy(s => s).ToList();
            });
        }

        public List<string> GetAllManufacturerFirstSymbolsByDiscount(int discountId)
        {
            return this.cacheManager.Get(MANUFACTURER_FIRST_SYMBOLS_DISCOUNT_KEY, () =>
            {
                var productIdsCache = this._prouctService.GetProductIds();

                var query = this.manufacturerRepository.TableNoTracking;

                var result = from man in query
                             join mp in this.productManufacturerRepository.TableNoTracking on man.Id equals mp.ManufacturerId
                             //join p in productIdsCache on mp.ProductId equals p
                             join d in this.discountRepository.TableNoTracking on discountId equals d.Id
                             where !man.Deleted && man.Published && d.AppliedToProducts.Select(r => r.Id).Contains(mp.ProductId)
                             select man.Name.Substring(0, 1);

                return result.Distinct().OrderBy(s => s).ToList();
            });
        }

        public List<string> GetAllManufacturerFirstAlphabetSymbols()
        {
            return this.cacheManager.Get(MANUFACTURER_FIRST_ALPHABET_SYMBOLS_KEY, () =>
            {
                string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                return this.GetAllManufacturerFirstSymbols().Except(numbers).ToList();
            });
        }

        public List<Manufacturer> GetAllManufacturersWithFirstSymbol(string firstSymbol, bool isSymbolNumeric = false)
        {
            if (string.IsNullOrEmpty(firstSymbol))
            {
                return new List<Manufacturer>();
            }

            var productIdsCache = this._prouctService.GetProductIds();

            var query = from a in this.manufacturerRepository.TableNoTracking
                        join b in this.productManufacturerRepository.TableNoTracking on a.Id equals b.ManufacturerId
                        join p in productIdsCache on b.ProductId equals p
                        where !a.Deleted && a.Published
                        select a;

            query = !isSymbolNumeric
                        ? query.Where(m => m.Name.ToLower().StartsWith(firstSymbol.ToLower().Trim())).Select(m => m)
                        : query.Where(m => NUMERIC_AVAILABLE_SYMBOLS.Contains(m.Name.ToLower().Substring(0, 1))).Select(m => m);

            return query.Distinct().OrderBy(man => man.Name).ToList();
        }

        public List<Manufacturer> GetAllManufacturersByDiscount(int discountId)
        {
            var productIdsCache = this._prouctService.GetProductIds();

            var manufactorers = cacheManager.Get(string.Format(MANUFACTURERS_ALL_DISCOUNT_KEY, discountId), (DateTime.Now.AddDays(1) - DateTime.Now).Minutes, () =>
            {
                var query = from a in this.manufacturerRepository.TableNoTracking
                            join b in this.productManufacturerRepository.TableNoTracking on a.Id equals b.ManufacturerId
                            //join p in productIdsCache on b.ProductId equals p
                            join d in this.discountRepository.TableNoTracking on discountId equals d.Id
                            where !a.Deleted && a.Published && d.AppliedToProducts.Select(r => r.Id).Contains(b.ProductId)
                            select a;

                return query.Distinct().OrderBy(man => man.Name).ToList();
            });
            return manufactorers;
        }


        public List<Manufacturer> GetAllManufacturersByDiscountWithFirstSymbol(string firstSymbol, int discountId, bool isSymbolNumeric = false)
        {
            if (string.IsNullOrEmpty(firstSymbol))
            {
                return GetAllManufacturersByDiscount(discountId);
            }

            var productIdsCache = this._prouctService.GetProductIds();

            var query = from a in this.manufacturerRepository.TableNoTracking
                        join b in this.productManufacturerRepository.TableNoTracking on a.Id equals b.ManufacturerId
                        //join p in productIdsCache on b.ProductId equals p
                        join d in this.discountRepository.TableNoTracking on discountId equals d.Id
                        where !a.Deleted && a.Published && d.AppliedToProducts.Select(r => r.Id).Contains(b.ProductId)
                        select a;

            query = !isSymbolNumeric
                        ? query.Where(m => m.Name.ToLower().StartsWith(firstSymbol.ToLower().Trim())).Select(m => m)
                        : query.Where(m => NUMERIC_AVAILABLE_SYMBOLS.Contains(m.Name.ToLower().Substring(0, 1))).Select(m => m);

            return query.Distinct().OrderBy(man => man.Name).ToList();
        }

        public bool CheckSymbolForNumeric(string symbol)
        {
            if (string.IsNullOrEmpty(symbol) || symbol.Length > 1)
                return false;

            return NUMERIC_AVAILABLE_SYMBOLS.Contains(symbol);
        }

        public List<int> GetAllChildCategoriesIdList(int parentCategoryId)
        {
            var pCategoryId = this.dataProvider.GetParameter();
            pCategoryId.ParameterName = "categoryId";
            pCategoryId.Value = parentCategoryId;
            pCategoryId.DbType = DbType.Int32;

            var subCategories = this.dbContext.SqlQuery<int>("SELECT * FROM WC_GetAllSubcategories(@categoryId)", pCategoryId);
            return subCategories.ToList();
        }

        public List<SolrCategory> GetAllCategories()
        {
            return this.dbContext.SqlQuery<SolrCategory>("SELECT * FROM WC_GetAllCategories()").ToList();
        }

        public string GetProductAdditionalImageName(int productId)
        {
            var query = from img in this.additionalImageRepository.Table
                        where img.ProductId == productId
                        select img.PictureName;

            return query.FirstOrDefault();
        }

        public string GetProductGoogleImageName(int productId)
        {
            var query = from img in this.googleImageRepository.Table
                        where img.ProductId == productId
                        select img.PicturePath;

            return query.FirstOrDefault();
        }

        public byte[] GetAdditionalImageForProduct(int productId)
        {
            var pictureVirtualPath = this.GetProductAdditionalImageName(productId);
            var directoryPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ImageLoaderVirtualPath"]);

            if (string.IsNullOrEmpty(pictureVirtualPath))
            {
                pictureVirtualPath = $"{this.storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
                directoryPath = HttpContext.Current.Server.MapPath("~/content/images/");
            }

            pictureVirtualPath = pictureVirtualPath.Replace("/", "\\");

            if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                throw new Exception("Directory does not exist: " + directoryPath);
            }

            var picturePath = Path.Combine(directoryPath, pictureVirtualPath);
            if (!File.Exists(picturePath))
            {
                throw new Exception("File does not exist: " + picturePath);
            }

            var data = File.ReadAllBytes(picturePath);
            if (data.Length == 0)
            {
                throw new Exception("File data.Length == 0. File: " + picturePath);
            }

            return data;
        }

        public bool GetProductCashRebateAmount(int productId, out decimal rebateAmount)
        {
            var query = from pcr in this.productCashRebateRepository.Table
                        where pcr.ProductId == productId
                        select pcr.RebateAmount;

            rebateAmount = query.FirstOrDefault();
            return rebateAmount > 0;
        }

        public void SaveProductCashRebates(Order order)
        {
            if (order == null)
                return;

            try
            {
                var orderWithRebates = new List<OrderProductWithRebates>();

                foreach (var orderItem in order.OrderItems)
                {
                    int productId = orderItem.ProductId;

                    if (IsOrderItemWithRebateExistInDb(orderItem.Id))
                        return;

                    decimal rebateAmount;
                    if (!GetProductCashRebateAmount(productId, out rebateAmount))
                        continue;

                    orderWithRebates.Add(
                        new OrderProductWithRebates
                        {
                            ProductId = orderItem.ProductId,
                            OrderProductVariantId = orderItem.Id,
                            RebateAmount = rebateAmount
                        });
                }

                SaveOrderWithRebates(orderWithRebates);
            }
            catch (Exception ex)
            {
                logger.Error("CustomService SaveProductCashRebates() - " + ex.Message, ex);
            }
        }

        public IList<OrderWithRebates> GetOrdersWithRebates()
        {
            var query = from o in this.orderWithRebatesRepository.Table orderby o.OrderId descending select o;
            return query.ToList();
        }

        public void NotifyOrderWithRebatesCustomer(OrderWithRebates orderWithRebates)
        {
            this.workflowMessageService.SendOrderWithRebatesCustomerNotification(orderWithRebates, this.workContext.WorkingLanguage.Id);
        }

        public void InsertOrderWithRebatesNotification(OrderWithRebatesNotification notification)
        {
            this.orderWithRebatesNotificationRepository.Insert(notification);
        }

        public IList<OrderProductToReview> GetOrderProductsToReview(int count = 0)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.Serializable
            }))
            {

                var query = from o in this.orderProductToReviewRepository.Table
                            where o.ProductId.HasValue && !string.IsNullOrEmpty(o.ProductName) && !string.IsNullOrEmpty(o.Email) && !string.IsNullOrEmpty(o.CustomerFullName)
                            orderby o.OrderId descending
                            select o;

                var entities = count > 0 ? query.Take(count).ToList() : query.ToList();
                scope.Complete();
                return entities;
            }
        }

        public void NotifyProductReviewCustomer(OrderProductToReview orderProductToReview)
        {
            this.workflowMessageService.SendProductReviewCustomerNotification(orderProductToReview, this.workContext.WorkingLanguage.Id);
        }

        public void InsertProductReviewCustomerNotification(ProductReviewCustomerNotification notification)
        {
            this.productReviewCustomerNotificationRepository.Insert(notification);
        }

        public IList<OrderShipmentEta> GetOrderShipmentEta()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.Serializable
            }))
            {

                var query = from o in this.orderShipmentEtaRepository.Table
                            orderby o.OrderId
                            select o;

                var entities = query.ToList();
                scope.Complete();
                return entities;
            }
        }

        public void NotifyOrderShipmentEtaCustomer(OrderShipmentEta orderShipmentEta)
        {
            this.workflowMessageService.SendOrderShipmentEtaNotification(orderShipmentEta, this.workContext.WorkingLanguage.Id);
        }

        public void InsertOrderShipmentEtaNotification(OrderEtaNotification notification)
        {
            this.orderEtaNotificationRepository.Insert(notification);
        }

        public void InsertOrderReviewNotification(Order order)
        {
            this.workflowMessageService.SendOrderReviewNotification(order, this.workContext.WorkingLanguage.Id);
        }

        public Locker GetLocker(string lockerName)
        {
            using (var transaction = new TransactionScope())
            {
                var query = from locker in this.lockerRepository.Table
                            where locker.Name.ToLower() == lockerName.ToLower()
                            select locker;

                transaction.Complete();
                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// Check is operation has been locked
        /// </summary>
        /// <param name="lockerName">Name of locker</param>
        /// <param name="maxTimeoutSeconds">Max timeout when we can say that previous operation hangs or down</param>
        /// <returns></returns>
        public bool IsLocked(string lockerName, int maxTimeoutSeconds)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                var query = from lockerTable in this.lockerRepository.Table
                            where lockerTable.Name.ToLower() == lockerName.ToLower()
                            select lockerTable;

                var locker = query.FirstOrDefault();
                if (locker == null)
                {
                    this.lockerRepository.Insert(new Locker { Name = lockerName, IsLocked = true, UpdatedOnUtc = DateTime.UtcNow });
                    transaction.Complete();
                    return true;
                }

                //if hangs
                if (locker.IsLocked && (DateTime.UtcNow - locker.UpdatedOnUtc).TotalSeconds > maxTimeoutSeconds)
                {
                    locker.UpdatedOnUtc = DateTime.UtcNow;
                    locker.IsLocked = false;
                    this.lockerRepository.Update(locker);
                    transaction.Complete();
                    return false;
                }

                transaction.Complete();
                return locker.IsLocked;
            }
        }

        public void SetLocked(string lockerName)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                var query = from locker in this.lockerRepository.Table
                            where locker.Name.ToLower() == lockerName.ToLower()
                            select locker;

                var entity = query.FirstOrDefault();
                if (entity != null)
                {
                    entity.IsLocked = true;
                    entity.UpdatedOnUtc = DateTime.UtcNow;
                    this.lockerRepository.Update(entity);
                }
                //else
                //{
                //    this.lockerRepository.Insert(new Locker { Name = lockerName, IsLocked = true, UpdatedOnUtc = DateTime.UtcNow });
                //}

                transaction.Complete();
            }
        }

        public bool SetLockedIfUnlocked(string lockerName, int maxTimeoutSeconds)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                var query = from lockerTable in this.lockerRepository.Table
                            where lockerTable.Name.ToLower() == lockerName.ToLower()
                            select lockerTable;

                var locker = query.FirstOrDefault();
                //if (locker == null)
                //{
                //    this.lockerRepository.Insert(new Locker { Name = lockerName, IsLocked = true, UpdatedOnUtc = DateTime.UtcNow });
                //    transaction.Complete();
                //    return true;
                //}

                //if hangs
                if (locker.IsLocked && (DateTime.UtcNow - locker.UpdatedOnUtc).TotalSeconds > maxTimeoutSeconds)
                {
                    locker.UpdatedOnUtc = DateTime.UtcNow;
                    locker.IsLocked = true;
                    this.lockerRepository.Update(locker);
                    transaction.Complete();
                    return true;
                }

                if (locker.IsLocked)
                {
                    transaction.Complete();
                    return false;
                }

                locker.IsLocked = true;
                locker.UpdatedOnUtc = DateTime.UtcNow;
                this.lockerRepository.Update(locker);

                transaction.Complete();
                return true;
            }
        }

        public void SetUnlocked(string lockerName)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                var query = from locker in this.lockerRepository.Table
                            where locker.Name.ToLower() == lockerName.ToLower()
                            select locker;

                var entity = query.FirstOrDefault();
                if (entity != null)
                {
                    entity.IsLocked = false;
                    entity.UpdatedOnUtc = DateTime.UtcNow;
                    this.lockerRepository.Update(entity);
                }
                //else
                //{
                //    this.lockerRepository.Insert(new Locker { Name = lockerName, IsLocked = false, UpdatedOnUtc = DateTime.UtcNow });
                //}

                transaction.Complete();
            }
        }

        public void InsertOrderExtra(OrderExtra orderExtra)
        {
            this.orderExtraRepository.Insert(orderExtra);
        }

        public void UpdateOrderExtra(OrderExtra orderExtra)
        {
            this.orderExtraRepository.Update(orderExtra);
        }

        public OrderExtra GetOrderExtra(int orderId)
        {
            var query = from oe in this.orderExtraRepository.Table where oe.OrderId == orderId select oe;
            return query.FirstOrDefault();
        }

        /*public void CallKountService(int? orderId, string shippingMethodName, Customer customer, ProcessPaymentRequest processPaymentRequest, ProcessPaymentResult paymentResult)
        {
            var paymentMethod = GetPaymentMethod(processPaymentRequest.PaymentMethodSystemName);
            if (paymentMethod == PaymentMethod.Unknown)
            {
                return;
            }

            try
            {
                int? kountScore;
                string kountResponse;
                this.KountAuthorization(orderId, shippingMethodName, customer, processPaymentRequest, paymentResult, out kountScore, out kountResponse);

                if (orderId.HasValue)
                {
                    var orderExtra = this.GetOrderExtra(orderId.Value);
                    if (orderExtra == null)
                    {
                        orderExtra = new OrderExtra
                        {
                            OrderId = orderId.Value,
                            KountScore = kountScore,
                            KountResponse = kountResponse
                        };

                        this.InsertOrderExtra(orderExtra);
                    }
                    else
                    {
                        orderExtra.KountScore = kountScore;
                        orderExtra.KountResponse = kountResponse;
                        this.UpdateOrderExtra(orderExtra);
                    }
                }
                
            }
            catch(Exception ex)
            {
                this.logger.InsertLog(LogLevel.Error, "Kount. " + ex.Message, ex.StackTrace);
            }
        }*/

        public bool PrepareSearchThumbPicture(int pictureId)
        {
            try
            {
                var imageUrl = pictureService.GetWidthHeightPictureUrl(pictureId, 233, 175, false, null, PictureType.Entity, false);
                if (string.IsNullOrEmpty(imageUrl))
                    return false;
            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }

        public void InsertEbayOrderDeliveryNotification(CrmSalesOrder order)
        {
            this.workflowMessageService.SendEbayOrderDeliveryNotification(order);
        }

        #endregion

        #region Private Methods

        private bool IsOrderItemWithRebateExistInDb(int orderItemId)
        {
            var query = from owr in this.orderProductWithRebatesRepository.Table
                        where owr.OrderProductVariantId == orderItemId
                        select owr.OrderProductVariantId;

            return query.Any();
        }

        private void SaveOrderWithRebates(List<OrderProductWithRebates> orderRebates)
        {
            this.orderProductWithRebatesRepository.Insert(orderRebates);
        }

        /*private void KountAuthorization(int? orderId, string shippingMethodName, Customer customer, ProcessPaymentRequest processPaymentRequest, ProcessPaymentResult paymentResult, out int? score, out string kountResponse)
        {
            if (HttpContext.Current.Session["Kount.SessionId"] == null)
            {
                score = null;
                kountResponse = null;
                this.logger.InsertLog(LogLevel.Error, "Kount.SessionId is NULL");
                return;
            }

            var sessionId = HttpContext.Current.Session["Kount.SessionId"].ToString();
            var inquiry = new Inquiry();
            inquiry.SetSessionId(sessionId);
            inquiry.SetTotal((int)(processPaymentRequest.OrderTotal * 100m));
            inquiry.SetEmail(customer.ShippingAddress.Email);
            inquiry.SetName(customer.GetFullName() == string.Empty ? string.Format("{0} {1}", customer.BillingAddress.FirstName, customer.BillingAddress.LastName).Trim() : customer.GetFullName());
            inquiry.SetUnique(customer.Id.ToString(CultureInfo.InvariantCulture));
            inquiry.SetEpoch((long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            inquiry.SetBillingAddress(customer.BillingAddress.Address1, customer.BillingAddress.Address2, customer.BillingAddress.City, customer.BillingAddress.StateProvince.Name, customer.BillingAddress.ZipPostalCode, customer.BillingAddress.Country.TwoLetterIsoCode);
            inquiry.SetShippingAddress(customer.ShippingAddress.Address1, customer.ShippingAddress.Address2, customer.ShippingAddress.City, customer.ShippingAddress.StateProvince.Name, customer.ShippingAddress.ZipPostalCode, customer.ShippingAddress.Country.TwoLetterIsoCode);
            inquiry.SetShippingName(string.Format("{0} {1}", customer.ShippingAddress.FirstName, customer.ShippingAddress.LastName).Trim());
            inquiry.SetShippingEmail(customer.ShippingAddress.Email);
            inquiry.SetWebsite(ConfigurationManager.AppSettings["Ris.MerchantWebsite"]);
            inquiry.SetIpAddress(this.webHelper.GetCurrentIpAddress());
            inquiry.SetUserAgent(HttpContext.Current.Request.UserAgent);
            

            if (paymentResult.Success)
            {
                if (orderId.HasValue)
                {
                    inquiry.SetOrderNumber(orderId.Value.ToString(CultureInfo.InvariantCulture));
                }

                inquiry.SetAuth('A');
                inquiry.SetMack('Y');
            }
            else
            {
                inquiry.SetAuth('D');
                inquiry.SetMack('N');
            }

            var paymentMethod = GetPaymentMethod(processPaymentRequest.PaymentMethodSystemName);
            if (paymentMethod == PaymentMethod.CreditCard)
            {
                if (!string.IsNullOrEmpty(processPaymentRequest.CreditCardNumber))
                {
                    inquiry.SetCardPayment(processPaymentRequest.CreditCardNumber);
                }

                if (processPaymentRequest.CreditCardExpireMonth > 0)
                {
                    inquiry.SetExpirationMonth(processPaymentRequest.CreditCardExpireMonth.ToString(CultureInfo.InvariantCulture));
                }

                if (processPaymentRequest.CreditCardExpireYear > 0)
                {
                    inquiry.SetExpirationYear(processPaymentRequest.CreditCardExpireYear.ToString(CultureInfo.InvariantCulture));
                }
            }
            else if (paymentMethod == PaymentMethod.PayPal)
            {
                if (!string.IsNullOrEmpty(paymentResult.PayerId))
                {
                    inquiry.SetPaypalPayment(paymentResult.PayerId);
                }
            }

            if (!string.IsNullOrEmpty(customer.BillingAddress.PhoneNumber))
            {
                inquiry.SetBillingPhoneNumber(customer.BillingAddress.PhoneNumber);
            }

            if (!string.IsNullOrEmpty(customer.ShippingAddress.PhoneNumber))
            {
                inquiry.SetShippingPhoneNumber(customer.ShippingAddress.PhoneNumber);
            }

            if (!string.IsNullOrEmpty(GetKountShipType(shippingMethodName)))
            {
                inquiry.SetShipType(GetKountShipType(shippingMethodName));  //Same Day = SD, Next Day = ND, Second Day = 2D, Standard = ST
            }

            var cart = customer.ShoppingCartItems.Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart).LimitPerStore(processPaymentRequest.StoreId).ToList();
            var items = new ArrayList();
            foreach (var item in cart)
            {
                var description = item.Product.ShortDescription ?? string.Empty;
                if (description.Length > 255)
                {
                    description = description.Substring(0, 255);
                }

                var name = item.Product.Name ?? string.Empty;
                if (name.Length > 255)
                {
                    name = name.Substring(0, 255);
                }

                items.Add(new Kount.Ris.CartItem(name, item.ProductId.ToString(CultureInfo.InvariantCulture), description, item.Quantity, (long)(item.Product.Price * 100m)));
            }

            // Set AVS data
            if (paymentMethod == PaymentMethod.CreditCard && !string.IsNullOrEmpty(paymentResult.AuthorizationTransactionResult))
            {
                var avsData = paymentResult.AuthorizationTransactionResult;
                string[] avsParts;
                if (avsData.Length >= 12 && avsData[11] == '#' && (avsParts = avsData.Substring(0, 11).Split(new[] { '|' })).Length == 6)
                {
                    inquiry.SetAvst(avsParts[0] == "Y" ? 'M' : 'N');
                    inquiry.SetAvsz(avsParts[1] == "Y" ? 'M' : 'N');
                }
            }

            inquiry.SetCart(items);
            var response = inquiry.GetResponse();
            if (response != null)
            {
                kountResponse = response.ToString();
                int kountScore;
                if (int.TryParse(response.GetScore(), out kountScore))
                {
                    score = kountScore;
                }
                else
                {
                    score = null;
                }
            }
            else
            {
                score = null;
                kountResponse = null;
                this.logger.InsertLog(LogLevel.Error, "Kount Response is NULL");
            }
        }

        private static string GetKountShipType(string shippingMethodName)
        {
            switch (shippingMethodName)
            {
                case "UPS 3-5 Day Ground":
                case "Ground":
                case "UPS Ground":
                case "UPS Ground shipping":
                case "UPS Standard":
                    return "ST";
                case "UPS 2nd Day Air":
                    return "2D";
                case "UPS Next Day Air Saver":
                    return "ND";
                default:
                    return string.Empty;
            }
        }*/

        private static PaymentMethod GetPaymentMethod(string paymentMethodSystemName)
        {
            switch (paymentMethodSystemName)
            {
                case "Payments.Payflow":
                    return PaymentMethod.CreditCard;
                case "Payments.PayPalExpressCheckout":
                    return PaymentMethod.PayPal;
                default:
                    return PaymentMethod.Unknown;
            }
        }

        public void InsertEbayMarketplaceAccountDeletionNotification(EbayMarketplaceAccountDeletionNotification notification)
        {
            this.ebayMarketplaceAccountDeletionNotificationRepository.Insert(notification);
        }

        #endregion
    }
}
