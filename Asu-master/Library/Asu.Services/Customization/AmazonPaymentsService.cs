//using AmazonPaymentsAdvanced;
using Asu.Core;
using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Discounts;
using Asu.Core.Domain.Logging;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Shipping;
using Asu.Core.Domain.Tax;
using Asu.Services.Catalog;
using Asu.Services.Customers;
using Asu.Services.Directory;
using Asu.Services.Logging;
using Asu.Services.Orders;
using Asu.Services.Shipping;
using Asu.Services.Tax;
using Asu.Services.Common;
using Asu.Services.Discounts;
using Asu.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Catalog;
using Asu.Services.Authentication;
using Asu.Services.Messages;
using Asu.Services.Localization;
using System.Web;
using Asu.Core.Data;
using System.Globalization;
using Asu.Core.Domain.Localization;
using Asu.Core.Domain.Payments;
using Asu.Services.Security;
using Asu.Services.Affiliates;
using Asu.Core.Domain.Directory;
using Asu.Services.Events;
using System.Web.Security;

namespace Asu.Services.Customization
{
    using Asu.Services.SalesQuotes;

  //  public sealed class AmazonPaymentsService : IAmazonPaymentsService
  //  {
  //      #region Ctor

  //      public AmazonPaymentsService(IRepository<AmazonPaymentAdvanced> amazonPaymentAdvancedRepository,
  //          ICountryService countryService,
  //          IStateProvinceService stateProvinceService,
  //          ILogger logger,
  //          IWorkContext workContext,
  //          IShippingService shippingService,
  //          IStoreContext storeContext,
  //          ICustomerService customerService,
  //          ITaxService taxService,
  //          ICurrencyService currencyService,
  //          IOrderTotalCalculationService orderTotalCalculationService,
  //          IGenericAttributeService genericAttributeService,
  //          IPaymentService paymentService,
  //          IGiftCardService giftCardService,
  //          IDiscountService discountService,
  //          IAuthenticationService authenticationService,
  //          ICustomerRegistrationService customerRegistrationService,
  //          IWorkflowMessageService workflowMessageService,
  //          IOrderService orderService,
  //          ILocalizationService localizationService,
  //          IOrderProcessingService orderProcessingService,
  //          IShoppingCartService shoppingCartService,
  //          IPriceCalculationService priceCalculationService,
  //          IEncryptionService encryptionService,
  //          ILanguageService languageService,
  //          IAffiliateService affiliateService,
  //          IProductService productService,
  //          IPdfService pdfService,
  //          ICustomerActivityService customerActivityService,
  //          IPriceFormatter priceFormatter,
  //          ICheckoutAttributeFormatter checkoutAttributeFormatter,
  //          IProductAttributeFormatter productAttributeFormatter,
  //          IProductAttributeParser productAttributeParser,
  //          IEventPublisher eventPublisher,
  //          IWebHelper webHelper,
  //          ShoppingCartSettings shoppingCartSettings,
  //          RewardPointsSettings rewardPointsSettings,
  //          CatalogSettings catalogSettings,
  //          CustomerSettings customerSettings,
  //          OrderSettings orderSettings,
  //          TaxSettings taxSettings,
  //          CurrencySettings currencySettings,
  //          ShippingSettings shippingSettings,
  //          PaymentSettings paymentSettings,
  //          LocalizationSettings localizationSettings,
  //          HttpContextBase httpContext,
		//	AmazonPaymentSettings amazonPaymentSettings,
  //          ISalesQuoteService salesQuoteService,
  //          IShippingInsuranceService shippingInsuranceService)
		//{
  //          this.countryService = countryService;
  //          this.stateProvinceService = stateProvinceService;
  //          this.logger = logger;
  //          this.workContext = workContext;
  //          this.shippingService = shippingService;
  //          this.storeContext = storeContext;
  //          this.customerService = customerService;
  //          this.shoppingCartSettings = shoppingCartSettings;
  //          this.taxService = taxService;
  //          this.currencyService = currencyService;
  //          this.priceFormatter = priceFormatter;
  //          this.orderTotalCalculationService = orderTotalCalculationService;
  //          this.genericAttributeService = genericAttributeService;
  //          this.paymentService = paymentService;
  //          this.giftCardService = giftCardService;
  //          this.rewardPointsSettings = rewardPointsSettings;
  //          this.catalogSettings = catalogSettings;
  //          this.discountService = discountService;
  //          this.authenticationService = authenticationService;
  //          this.customerSettings = customerSettings;
  //          this.customerRegistrationService = customerRegistrationService;
  //          this.workflowMessageService = workflowMessageService;
  //          this.orderSettings = orderSettings;
  //          this.orderService = orderService;
  //          this.localizationService = localizationService;
  //          this.orderProcessingService = orderProcessingService;
  //          this.httpContext = httpContext;
  //          this.amazonPaymentAdvancedRepository = amazonPaymentAdvancedRepository;
  //          this.shoppingCartService = shoppingCartService;
  //          this.taxSettings = taxSettings;
  //          this.priceCalculationService = priceCalculationService;
  //          this.encryptionService = encryptionService;
  //          this.languageService = languageService;
  //          this.affiliateService = affiliateService;
  //          this.currencySettings = currencySettings;
  //          this.checkoutAttributeFormatter = checkoutAttributeFormatter;
  //          this.shippingSettings = shippingSettings;
  //          this.paymentSettings = paymentSettings;
  //          this.webHelper = webHelper;
  //          this.productAttributeFormatter = productAttributeFormatter;
  //          this.productAttributeParser = productAttributeParser;
  //          this.productService = productService;
  //          this.localizationSettings = localizationSettings;
  //          this.pdfService = pdfService;
  //          this.customerActivityService = customerActivityService;
  //          this.eventPublisher = eventPublisher;
  //          this.amazonPaymentSettings = amazonPaymentSettings;
  //          this.salesQuoteService = salesQuoteService;
  //          this.shippingInsuranceService = shippingInsuranceService;
  //      }

  //      #endregion

  //      #region Fields

  //      private readonly AmazonPaymentSettings amazonPaymentSettings;
  //      private readonly IRepository<AmazonPaymentAdvanced> amazonPaymentAdvancedRepository;
  //      private readonly ICountryService countryService;
  //      private readonly IStateProvinceService stateProvinceService;
  //      private readonly ILogger logger;
  //      private readonly IWorkContext workContext;
  //      private readonly IShippingService shippingService;
  //      private readonly IStoreContext storeContext;
  //      private readonly ICustomerService customerService;
  //      private readonly ITaxService taxService;
  //      private readonly ICurrencyService currencyService;
  //      private readonly IOrderTotalCalculationService orderTotalCalculationService;
  //      private readonly IGenericAttributeService genericAttributeService;
  //      private readonly IPaymentService paymentService;
  //      private readonly IGiftCardService giftCardService;
  //      private readonly IDiscountService discountService;
  //      private readonly IAuthenticationService authenticationService;
  //      private readonly ICustomerRegistrationService customerRegistrationService;
  //      private readonly IWorkflowMessageService workflowMessageService;
  //      private readonly IOrderService orderService;
  //      private readonly ILocalizationService localizationService;
  //      private readonly IOrderProcessingService orderProcessingService;
  //      private readonly IShoppingCartService shoppingCartService;
  //      private readonly IPriceCalculationService priceCalculationService;
  //      private readonly IEncryptionService encryptionService;
  //      private readonly ILanguageService languageService;
  //      private readonly IAffiliateService affiliateService;
  //      private readonly IProductService productService;
  //      private readonly IPdfService pdfService;
  //      private readonly ICustomerActivityService customerActivityService;

  //      private readonly IPriceFormatter priceFormatter;
  //      private readonly ICheckoutAttributeFormatter checkoutAttributeFormatter;
  //      private readonly IProductAttributeFormatter productAttributeFormatter;
  //      private readonly IProductAttributeParser productAttributeParser;
  //      private readonly IEventPublisher eventPublisher;
  //      private readonly IWebHelper webHelper;

  //      private readonly ShoppingCartSettings shoppingCartSettings;
  //      private readonly RewardPointsSettings rewardPointsSettings;
  //      private readonly CatalogSettings catalogSettings;
  //      private readonly CustomerSettings customerSettings;
  //      private readonly OrderSettings orderSettings;
  //      private readonly TaxSettings taxSettings;
  //      private readonly CurrencySettings currencySettings;
  //      private readonly ShippingSettings shippingSettings;
  //      private readonly PaymentSettings paymentSettings;
  //      private readonly LocalizationSettings localizationSettings;

  //      private readonly HttpContextBase httpContext;
  //      private readonly ISalesQuoteService salesQuoteService;
  //      private readonly IShippingInsuranceService shippingInsuranceService;

  //      #endregion

  //      #region Public Methods

  //      public bool CheckAuthorizeStatus(OffAmazonPaymentsService.Model.Status authorizeStatus, out string message)
  //      {
  //          message = string.Empty;
  //          if (authorizeStatus.State == OffAmazonPaymentsService.Model.PaymentStatus.DECLINED && authorizeStatus.ReasonCode.ToUpper() == "INVALIDPAYMENTMETHOD")
  //          {
  //              message = "We're sorry, but authorize transaction was declined: Invalid Payment Method. Please contact us for assistance";
  //              return false;
  //          }

  //          if (authorizeStatus.State == OffAmazonPaymentsService.Model.PaymentStatus.CLOSED && authorizeStatus.ReasonCode.ToUpper() == "EXPIREDUNUSED")
  //          {
  //              message = "We're sorry, but authorize transaction expires. Please contact us for assistance.";
  //              return false;
  //          }

  //          if (authorizeStatus.State == OffAmazonPaymentsService.Model.PaymentStatus.CLOSED && authorizeStatus.ReasonCode.ToUpper() == "AMAZONCLOSED")
  //          {
  //              message = "We're sorry, but authorize transaction closed. Please contact us for assistance.";
  //              return false;
  //          }

  //          if (authorizeStatus.State == OffAmazonPaymentsService.Model.PaymentStatus.DECLINED && authorizeStatus.ReasonCode.ToUpper() == "TRANSACTIONTTIMEDOUT")
  //          {
  //              message = "We're sorry, but authorize transaction timed out. Please contact us for assistance.";
  //              return false;
  //          }

  //          return true;
  //      }

  //      public AmazonPlaceOrderResult PlaceAmazonOrder(List<ShoppingCartItem> cart, string orderReferenceId, string selectedMethod)
  //      {
  //          var errorMessage = string.Empty;
  //          var amazonOrderResult = new AmazonPlaceOrderResult();

  //          var shippingAddress = workContext.CurrentCustomer.ShippingAddress;

  //          if (shippingAddress == null)
  //          {
  //              errorMessage = "Your Amazon shipping address is empty.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var shippingOptions = PrepareShippingMethodList(cart, shippingAddress);
  //          if (shippingOptions == null || shippingOptions.ShippingMethods == null || shippingOptions.ShippingMethods.Count == 0)
  //          {
  //              errorMessage = "There are no shipping methods for this order.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var selectedShippingOption = shippingOptions.ShippingMethods.FirstOrDefault(sm => sm.Name == selectedMethod)?.ShippingOption;
  //          if (selectedShippingOption == null)
  //          {
  //              errorMessage = "There are no shipping method as selected.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var customAttributes = string.Empty;
  //          //try to find an address with the same values (don't duplicate records)
  //          var address = workContext.CurrentCustomer.Addresses.ToList().FindAddress(
  //              shippingAddress.FirstName, shippingAddress.LastName, shippingAddress.PhoneNumber,
  //              shippingAddress.Email, shippingAddress.FaxNumber, shippingAddress.Company,
  //              shippingAddress.Address1, shippingAddress.Address2, shippingAddress.City,
  //              shippingAddress.StateProvinceId, shippingAddress.ZipPostalCode, shippingAddress.CountryId, customAttributes);
  //          if (address == null)
  //          {
  //              address = shippingAddress;
  //              address.CreatedOnUtc = DateTime.UtcNow;
  //              //little hack here (TODO: find a better solution)
  //              //EF does not load navigation properties for newly created entities (such as this "Address").
  //              //we have to load them manually 
  //              //otherwise, "Country" property of "Address" entity will be null in shipping rate computation methods
  //              if (address.CountryId.HasValue)
  //                  address.Country = countryService.GetCountryById(address.CountryId.Value);
  //              if (address.StateProvinceId.HasValue)
  //                  address.StateProvince = stateProvinceService.GetStateProvinceById(address.StateProvinceId.Value);

  //              //other null validations
  //              if (address.CountryId == 0)
  //                  address.CountryId = null;
  //              if (address.StateProvinceId == 0)
  //                  address.StateProvinceId = null;
  //              workContext.CurrentCustomer.Addresses.Add(address);
  //          }
  //          workContext.CurrentCustomer.ShippingAddress = address;
  //          workContext.CurrentCustomer.BillingAddress = address;
  //          customerService.UpdateCustomer(workContext.CurrentCustomer);

  //          genericAttributeService.SaveAttribute(workContext.CurrentCustomer,
  //                      SystemCustomerAttributeNames.SelectedShippingOption,
  //                      selectedShippingOption,
  //                      storeContext.CurrentStore.Id);

  //          string message;
  //          var orderAmount = orderTotalCalculationService.GetShoppingCartTotal(cart, false, false);    // TODO: check payment additional fees (third parameter = false)
  //          if (!orderAmount.HasValue)
  //          {
  //              errorMessage = "Impossible to calculate order amount. Please try again and contact us.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          //model
  //          Order order;
  //          try
  //          {
  //              var processPaymentRequest = new ProcessPaymentRequest();

  //              //prevent 2 orders being placed within an X seconds time frame
  //              if (!IsMinimumOrderPlacementIntervalValid(workContext.CurrentCustomer))
  //                  throw new Exception(localizationService.GetResource("Checkout.MinOrderPlacementInterval"));

  //              //place order
  //              processPaymentRequest.StoreId = storeContext.CurrentStore.Id;
  //              processPaymentRequest.CustomerId = workContext.CurrentCustomer.Id;
  //              processPaymentRequest.PaymentMethodSystemName = "Payments.Amazon";
  //              processPaymentRequest.IsRecurringPayment = false;
  //              var placeOrderResult = PlaceOrder(processPaymentRequest);
  //              if (placeOrderResult.Success)
  //              {
  //                  httpContext.Session["OrderPaymentInfo"] = null;
  //                  order = placeOrderResult.PlacedOrder;
  //              }
  //              else
  //              {
  //                  foreach (var error in placeOrderResult.Errors)
  //                      errorMessage += string.Format("{0}\r\n", error);
  //                  logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //                  amazonOrderResult.ErrorMessage = errorMessage;
  //                  amazonOrderResult.IsSuccess = false;
  //                  return amazonOrderResult;
  //              }
  //          }
  //          catch (Exception exc)
  //          {
  //              logger.Warning(exc.Message, exc, workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = exc.Message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // Create Record About Order in 'AmazonPaymentsAdvanced' table
  //          CreateRecordAboutOrder(orderReferenceId, orderAmount, order);

            
  //          var service = new AmazonPaymentsAdvancedService(
  //              this.amazonPaymentSettings.ApplicationName,
  //              this.amazonPaymentSettings.ApplicationVersion,
  //              this.amazonPaymentSettings.Region,
  //              this.amazonPaymentSettings.MerchantId,
  //              this.amazonPaymentSettings.AccessKey,
  //              this.amazonPaymentSettings.SecretAccessKey,
  //              this.amazonPaymentSettings.Environment,
  //              this.amazonPaymentSettings.ClientId,
  //              this.amazonPaymentSettings.WidgetUrl,
  //              this.amazonPaymentSettings.CertCn,
  //              this.amazonPaymentSettings.ServiceUrl,
  //              orderReferenceId);

  //          // SetOrderReferenceDetails
            
  //          var orderReferenceResponse = service.SetOrderReferenceDetails(orderAmount.ToString(), order.Id.ToString(CultureInfo.InvariantCulture), this.storeContext.CurrentStore.Name, out message);
  //          if (!string.IsNullOrWhiteSpace(message))
  //          {
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          if (!orderReferenceResponse.IsSetResponseMetadata()
  //              || !orderReferenceResponse.IsSetSetOrderReferenceDetailsResult()
  //              || !orderReferenceResponse.SetOrderReferenceDetailsResult.IsSetOrderReferenceDetails()
  //              || !orderReferenceResponse.SetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetOrderReferenceStatus())
  //          {
  //              errorMessage = "Error when Set Order Reference Details. Please contact us.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var orderReferenceStatus = orderReferenceResponse.SetOrderReferenceDetailsResult.OrderReferenceDetails.OrderReferenceStatus;
  //          if (string.IsNullOrEmpty(orderReferenceStatus.State)
  //              || orderReferenceStatus.State.ToUpper() == "CLOSED"
  //              || (!string.IsNullOrEmpty(orderReferenceStatus.ReasonCode) && orderReferenceStatus.ReasonCode.ToUpper() == "AMAZONCLOSED"))
  //          {
  //              errorMessage = "We're sorry, but there's a problem when Set Order Reference Details. Please contact us for assistance";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // ConfirmOrderReference
  //          service.ConfirmOrderReferenceObject(out message);
  //          if (!string.IsNullOrWhiteSpace(message))
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + message, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // GetOrderReferenceDetails
  //          var orderReferenceDetailResponse = service.GetOrderReferenceDetails(out message);
  //          if (!orderReferenceDetailResponse.IsSetGetOrderReferenceDetailsResult()
  //              || !orderReferenceDetailResponse.GetOrderReferenceDetailsResult.IsSetOrderReferenceDetails()
  //              || !string.IsNullOrWhiteSpace(message))
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + message, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          if (!orderReferenceDetailResponse.GetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetOrderReferenceStatus())
  //          {
  //              errorMessage = "OrderReference status empty. Please try again or contact us.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var orderReferenceDetailsStatus = orderReferenceDetailResponse.GetOrderReferenceDetailsResult.OrderReferenceDetails.OrderReferenceStatus;
  //          if (string.IsNullOrEmpty(orderReferenceDetailsStatus.State)
  //              || orderReferenceDetailsStatus.State.ToUpper() == "CLOSED"
  //              || (!string.IsNullOrEmpty(orderReferenceDetailsStatus.ReasonCode) && orderReferenceDetailsStatus.ReasonCode.ToUpper() == "AMAZONCLOSED"))
  //          {
  //              errorMessage = "We're sorry, but there's a problem when Get Order Reference Details. Please contact us for assistance.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // GetOrder from Autoplicity Database
  //          order.AuthorizationTransactionId = orderReferenceId;
  //          orderService.UpdateOrder(order);

  //          string state = orderReferenceDetailResponse.GetOrderReferenceDetailsResult.OrderReferenceDetails.OrderReferenceStatus.State;
  //          UpdateOrderReferenceStatus(orderReferenceId, state, message);

  //          var authorizeActionResponse = service.AuthorizeAction(orderAmount.ToString(), false, out message);
  //          if (authorizeActionResponse == null || !authorizeActionResponse.IsSetAuthorizeResult()
  //              || !authorizeActionResponse.AuthorizeResult.IsSetAuthorizationDetails()
  //              || !authorizeActionResponse.AuthorizeResult.AuthorizationDetails.IsSetAuthorizationStatus()
  //              || !authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.IsSetState())
  //          {
  //              errorMessage = message + ". Error when Authorize Amazon order. Please try again.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var authorizeStatus = authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus;
  //          if (!CheckAuthorizeStatus(authorizeStatus, out message))
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + message, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          UpdateAuthorizeStatus(orderReferenceId, authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AmazonAuthorizationId, authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.State.ToString(), message);

  //          if (authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.State != OffAmazonPaymentsService.Model.PaymentStatus.PENDING)
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + string.Format("CBA_new btnCheckout_OnClick() - status of order id {0} is {1}",
  //                  authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AmazonAuthorizationId,
  //                  authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.State), new Exception("Amazon Order State"), workContext.CurrentCustomer);
  //          }

  //          amazonOrderResult.ErrorMessage = string.Empty;
  //          amazonOrderResult.IsSuccess = true;
  //          amazonOrderResult.PlacedOrder = order;
  //          return amazonOrderResult;
  //      }

  //      public AmazonPlaceOrderResult PlaceAmazonOrder(List<ShoppingCartItem> cart, string orderReferenceId, string addressConsentToken, string selectedMethod)
  //      {
  //          var errorMessage = string.Empty;
  //          var amazonOrderResult = new AmazonPlaceOrderResult();

  //          var shippingAddress = GetSelectedAddress(orderReferenceId, addressConsentToken);

  //          if (shippingAddress == null)
  //          {
  //              errorMessage = "Your Amazon shipping address is empty.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var shippingOptions = PrepareShippingMethodList(cart, shippingAddress);
  //          if (shippingOptions == null || shippingOptions.ShippingMethods == null || shippingOptions.ShippingMethods.Count == 0)
  //          {
  //              errorMessage = "There are no shipping methods for this order.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var selectedShippingOption = shippingOptions.ShippingMethods.FirstOrDefault(sm => sm.Name == selectedMethod).ShippingOption;
  //          if (selectedShippingOption == null)
  //          {
  //              errorMessage = "There are no shipping method as selected.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var customAttributes = string.Empty;
  //          //try to find an address with the same values (don't duplicate records)
  //          var address = workContext.CurrentCustomer.Addresses.ToList().FindAddress(
  //              shippingAddress.FirstName, shippingAddress.LastName, shippingAddress.PhoneNumber,
  //              shippingAddress.Email, shippingAddress.FaxNumber, shippingAddress.Company,
  //              shippingAddress.Address1, shippingAddress.Address2, shippingAddress.City,
  //              shippingAddress.StateProvinceId, shippingAddress.ZipPostalCode, shippingAddress.CountryId, customAttributes);
  //          if (address == null)
  //          {
  //              address = shippingAddress;
  //              address.CreatedOnUtc = DateTime.UtcNow;
  //              //little hack here (TODO: find a better solution)
  //              //EF does not load navigation properties for newly created entities (such as this "Address").
  //              //we have to load them manually 
  //              //otherwise, "Country" property of "Address" entity will be null in shipping rate computation methods
  //              if (address.CountryId.HasValue)
  //                  address.Country = countryService.GetCountryById(address.CountryId.Value);
  //              if (address.StateProvinceId.HasValue)
  //                  address.StateProvince = stateProvinceService.GetStateProvinceById(address.StateProvinceId.Value);

  //              //other null validations
  //              if (address.CountryId == 0)
  //                  address.CountryId = null;
  //              if (address.StateProvinceId == 0)
  //                  address.StateProvinceId = null;
  //              workContext.CurrentCustomer.Addresses.Add(address);
  //          }
  //          workContext.CurrentCustomer.ShippingAddress = address;
  //          workContext.CurrentCustomer.BillingAddress = address;
  //          customerService.UpdateCustomer(workContext.CurrentCustomer);

  //          genericAttributeService.SaveAttribute(workContext.CurrentCustomer,
  //                      SystemCustomerAttributeNames.SelectedShippingOption,
  //                      selectedShippingOption,
  //                      storeContext.CurrentStore.Id);


  //          string message;
  //          var orderAmount = orderTotalCalculationService.GetShoppingCartTotal(cart, false, false);    // TODO: check payment additional fees (third parameter = false)
  //          if (!orderAmount.HasValue)
  //          {
  //              errorMessage = "Impossible to calculate order amount. Please try again and contact us.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          //model
  //          Order order;
  //          try
  //          {
  //              var processPaymentRequest = new ProcessPaymentRequest();

  //              //prevent 2 orders being placed within an X seconds time frame
  //              if (!IsMinimumOrderPlacementIntervalValid(workContext.CurrentCustomer))
  //                  throw new Exception(localizationService.GetResource("Checkout.MinOrderPlacementInterval"));

  //              //place order
  //              processPaymentRequest.StoreId = storeContext.CurrentStore.Id;
  //              processPaymentRequest.CustomerId = workContext.CurrentCustomer.Id;
  //              processPaymentRequest.PaymentMethodSystemName = "Payments.Amazon";
  //              processPaymentRequest.IsRecurringPayment = false;
  //              var placeOrderResult = PlaceOrder(processPaymentRequest);
  //              if (placeOrderResult.Success)
  //              {
  //                  httpContext.Session["OrderPaymentInfo"] = null;
  //                  order = placeOrderResult.PlacedOrder;
  //              }
  //              else
  //              {
  //                  foreach (var error in placeOrderResult.Errors)
  //                      errorMessage += string.Format("{0}\r\n", error);
  //                  logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //                  amazonOrderResult.ErrorMessage = errorMessage;
  //                  amazonOrderResult.IsSuccess = false;
  //                  return amazonOrderResult;
  //              }
  //          }
  //          catch (Exception exc)
  //          {
  //              logger.Warning(exc.Message, exc, workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = exc.Message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // Create Record About Order in 'AmazonPaymentsAdvanced' table
  //          CreateRecordAboutOrder(orderReferenceId, orderAmount, order);

            
  //          var service = new AmazonPaymentsAdvancedService(
  //              this.amazonPaymentSettings.ApplicationName,
  //              this.amazonPaymentSettings.ApplicationVersion,
  //              this.amazonPaymentSettings.Region,
  //              this.amazonPaymentSettings.MerchantId,
  //              this.amazonPaymentSettings.AccessKey,
  //              this.amazonPaymentSettings.SecretAccessKey,
  //              this.amazonPaymentSettings.Environment,
  //              this.amazonPaymentSettings.ClientId,
  //              this.amazonPaymentSettings.WidgetUrl,
  //              this.amazonPaymentSettings.CertCn,
  //              this.amazonPaymentSettings.ServiceUrl,
  //              orderReferenceId);

  //          // SetOrderReferenceDetails
            
  //          var orderReferenceResponse = service.SetOrderReferenceDetails(orderAmount.ToString(), order.Id.ToString(CultureInfo.InvariantCulture), this.storeContext.CurrentStore.Name, out message);
  //          if (!string.IsNullOrWhiteSpace(message))
  //          {
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          if (!orderReferenceResponse.IsSetResponseMetadata()
  //              || !orderReferenceResponse.IsSetSetOrderReferenceDetailsResult()
  //              || !orderReferenceResponse.SetOrderReferenceDetailsResult.IsSetOrderReferenceDetails()
  //              || !orderReferenceResponse.SetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetOrderReferenceStatus())
  //          {
  //              errorMessage = "Error when Set Order Reference Details. Please contact us.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var orderReferenceStatus = orderReferenceResponse.SetOrderReferenceDetailsResult.OrderReferenceDetails.OrderReferenceStatus;
  //          if (string.IsNullOrEmpty(orderReferenceStatus.State)
  //              || orderReferenceStatus.State.ToUpper() == "CLOSED"
  //              || (!string.IsNullOrEmpty(orderReferenceStatus.ReasonCode) && orderReferenceStatus.ReasonCode.ToUpper() == "AMAZONCLOSED"))
  //          {
  //              errorMessage = "We're sorry, but there's a problem when Set Order Reference Details. Please contact us for assistance";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // ConfirmOrderReference
  //          service.ConfirmOrderReferenceObject(out message);
  //          if (!string.IsNullOrWhiteSpace(message))
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + message, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // GetOrderReferenceDetails
  //          var orderReferenceDetailResponse = service.GetOrderReferenceDetails(addressConsentToken, out message);
  //          if (!orderReferenceDetailResponse.IsSetGetOrderReferenceDetailsResult()
  //              || !orderReferenceDetailResponse.GetOrderReferenceDetailsResult.IsSetOrderReferenceDetails()
  //              || !string.IsNullOrWhiteSpace(message))
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + message, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          if (!orderReferenceDetailResponse.GetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetOrderReferenceStatus())
  //          {
  //              errorMessage = "OrderReference status empty. Please try again or contact us.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var orderReferenceDetailsStatus = orderReferenceDetailResponse.GetOrderReferenceDetailsResult.OrderReferenceDetails.OrderReferenceStatus;
  //          if (string.IsNullOrEmpty(orderReferenceDetailsStatus.State)
  //              || orderReferenceDetailsStatus.State.ToUpper() == "CLOSED"
  //              || (!string.IsNullOrEmpty(orderReferenceDetailsStatus.ReasonCode) && orderReferenceDetailsStatus.ReasonCode.ToUpper() == "AMAZONCLOSED"))
  //          {
  //              errorMessage = "We're sorry, but there's a problem when Get Order Reference Details. Please contact us for assistance.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          // GetOrder from Autoplicity Database
  //          order.AuthorizationTransactionId = orderReferenceId;
  //          orderService.UpdateOrder(order);

  //          string state = orderReferenceDetailResponse.GetOrderReferenceDetailsResult.OrderReferenceDetails.OrderReferenceStatus.State;
  //          UpdateOrderReferenceStatus(orderReferenceId, state, message);

  //          var authorizeActionResponse = service.AuthorizeAction(orderAmount.ToString(), false, out message);
  //          if (authorizeActionResponse == null || !authorizeActionResponse.IsSetAuthorizeResult()
  //              || !authorizeActionResponse.AuthorizeResult.IsSetAuthorizationDetails()
  //              || !authorizeActionResponse.AuthorizeResult.AuthorizationDetails.IsSetAuthorizationStatus()
  //              || !authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.IsSetState())
  //          {
  //              errorMessage = message + ". Error when Authorize Amazon order. Please try again.";
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = errorMessage;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          var authorizeStatus = authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus;
  //          if (!CheckAuthorizeStatus(authorizeStatus, out message))
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + message, new Exception(), workContext.CurrentCustomer);
  //              amazonOrderResult.ErrorMessage = message;
  //              amazonOrderResult.IsSuccess = false;
  //              return amazonOrderResult;
  //          }

  //          UpdateAuthorizeStatus(orderReferenceId, authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AmazonAuthorizationId, authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.State.ToString(), message);

  //          if (authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.State != OffAmazonPaymentsService.Model.PaymentStatus.PENDING)
  //          {
  //              logger.Warning("Amazon service PlaceAmazonOrder() Amazon - " + string.Format("CBA_new btnCheckout_OnClick() - status of order id {0} is {1}",
  //                  authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AmazonAuthorizationId,
  //                  authorizeActionResponse.AuthorizeResult.AuthorizationDetails.AuthorizationStatus.State), new Exception("Amazon Order State"), workContext.CurrentCustomer);
  //          }

  //          amazonOrderResult.ErrorMessage = string.Empty;
  //          amazonOrderResult.IsSuccess = true;
  //          amazonOrderResult.PlacedOrder = order;
  //          return amazonOrderResult;
  //      }

  //      public bool LoginByAmazon(string orderReferenceId, string addressConsentToken, out string errorMessage)
  //      {
  //          errorMessage = string.Empty;

  //          if (workContext.CurrentCustomer.IsGuest())
  //          {
  //              if (string.IsNullOrEmpty(orderReferenceId))
  //                  return false;

  //              string email = string.Empty,
  //                  firstName = string.Empty,
  //                  lastName = string.Empty,
  //                  amazonAuthorizationId = string.Empty;

  //              try
  //              {
                    
  //                  var service = new AmazonPaymentsAdvancedService(
  //                      this.amazonPaymentSettings.ApplicationName,
  //                      this.amazonPaymentSettings.ApplicationVersion,
  //                      this.amazonPaymentSettings.Region,
  //                      this.amazonPaymentSettings.MerchantId,
  //                      this.amazonPaymentSettings.AccessKey,
  //                      this.amazonPaymentSettings.SecretAccessKey,
  //                      this.amazonPaymentSettings.Environment,
  //                      this.amazonPaymentSettings.ClientId,
  //                      this.amazonPaymentSettings.WidgetUrl,
  //                      this.amazonPaymentSettings.CertCn,
  //                      this.amazonPaymentSettings.ServiceUrl,
  //                      orderReferenceId);

  //                  var details = service.GetOrderReferenceDetails(addressConsentToken, out errorMessage);

  //                  if (!string.IsNullOrEmpty(errorMessage))
  //                      logger.Warning("CBA service LoginByAmazon() - " + errorMessage, new Exception(), workContext.CurrentCustomer);

  //                  if (details == null
  //                      || !details.IsSetGetOrderReferenceDetailsResult()
  //                      || !details.GetOrderReferenceDetailsResult.IsSetOrderReferenceDetails())
  //                  {
  //                      logger.Warning("CBA service LoginByAmazon() - don't have details.", new Exception(), workContext.CurrentCustomer);
  //                      return false;
  //                  }

  //                  if (details.GetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetBuyer())
  //                  {
  //                      var buyer = details.GetOrderReferenceDetailsResult.OrderReferenceDetails.Buyer;
  //                      if (!string.IsNullOrEmpty(buyer.Email))
  //                      {
  //                          email = buyer.Email;
  //                      }
  //                      else
  //                      {
  //                          errorMessage = "Amazon email was null or empty.";
  //                          logger.Warning("CBA service LoginByAmazon() Amazon - " + errorMessage, new Exception(errorMessage), workContext.CurrentCustomer);
  //                          return false;
  //                      }

  //                      if (!string.IsNullOrEmpty(buyer.Name))
  //                      {
  //                          var nameParts = buyer.Name.Split(' ');
  //                          if (nameParts.Length > 1)
  //                          {
  //                              firstName = nameParts[0];
  //                              for (int i = 1; i < nameParts.Length; i++)
  //                              {
  //                                  lastName += nameParts[i] + " ";
  //                              }
  //                              lastName = lastName.Trim();
  //                          }
  //                          else
  //                          {
  //                              firstName = buyer.Name;
  //                          }
  //                      }
  //                  }
  //                  else
  //                  {
  //                      logger.Warning("CBA service LoginByAmazon() - the buyer is not set", new Exception(), workContext.CurrentCustomer);
  //                      return false;
  //                  }
  //              }
  //              catch (Exception ex)
  //              {
  //                  logger.Warning("CBA service LoginByAmazon() Amazon - " + ex.Message, ex, workContext.CurrentCustomer);
  //                  errorMessage = "There was some problem with Amazon data.";
  //                  return false;
  //              }

  //              if (string.IsNullOrEmpty(email))
  //              {
  //                  errorMessage = "Amazon email was null or empty.";
  //                  logger.Warning("CBA service LoginByAmazon() - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //                  return false;
  //              }

  //              var customer = customerService.GetCustomerByEmail(email);
  //              if (customer == null)
  //              {
  //                  var registerResult = RegisterNewCustomer(email, firstName, lastName);
  //                  if (registerResult.Errors.Count != 0)
  //                  {
  //                      foreach(var error in registerResult.Errors)
  //                          errorMessage += string.Format("||{0}||\r\n", error);
  //                      logger.Warning("CBA service LoginByAmazon() - " + errorMessage, new Exception(), workContext.CurrentCustomer);
  //                      return false;
  //                  }
  //                  workContext.CurrentCustomer = registerResult.NewCustomer;
  //              }
  //              else
  //              {
  //                  //migrate shopping cart
  //                  if (customer.HasShoppingCartItems)
  //                  {
  //                      while(customer.HasShoppingCartItems)
  //                          shoppingCartService.DeleteShoppingCartItem(customer.ShoppingCartItems.FirstOrDefault());
  //                  }

  //                  shoppingCartService.MigrateShoppingCart(workContext.CurrentCustomer, customer, true);
  //                  authenticationService.SignIn(customer, true);
  //                  workContext.CurrentCustomer = customer;
  //              }
  //          }

  //          return true;
  //      }

  //      public Address GetSelectedAddress(string orderReferenceId, string addressConsentToken)
  //      {
  //          var amazonAddress = new Address();
  //          try
  //          {
                
  //              var service = new AmazonPaymentsAdvancedService(
  //                  this.amazonPaymentSettings.ApplicationName,
  //                  this.amazonPaymentSettings.ApplicationVersion,
  //                  this.amazonPaymentSettings.Region,
  //                  this.amazonPaymentSettings.MerchantId,
  //                  this.amazonPaymentSettings.AccessKey,
  //                  this.amazonPaymentSettings.SecretAccessKey,
  //                  this.amazonPaymentSettings.Environment,
  //                  this.amazonPaymentSettings.ClientId,
  //                  this.amazonPaymentSettings.WidgetUrl,
  //                  this.amazonPaymentSettings.CertCn,
  //                  this.amazonPaymentSettings.ServiceUrl,
  //                  orderReferenceId);
  //              string errorMessage;
  //              var details = service.GetOrderReferenceDetails(addressConsentToken, out errorMessage);
  //              if(!string.IsNullOrEmpty(errorMessage))
  //                  logger.Warning("CBA service GetSelectedAddress() - " + errorMessage, new Exception(errorMessage), workContext.CurrentCustomer);

  //              if (details == null
  //                  || !details.IsSetGetOrderReferenceDetailsResult()
  //                  || !details.GetOrderReferenceDetailsResult.IsSetOrderReferenceDetails()
  //                  || !details.GetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetDestination()
  //                  || !details.GetOrderReferenceDetailsResult.OrderReferenceDetails.Destination.IsSetPhysicalDestination())
  //              {
  //                  logger.Warning("CBA service LoginByAmazon() - we don't have details or destination.", new Exception(), workContext.CurrentCustomer);
  //                  return null;
  //              }

  //              var destination = details.GetOrderReferenceDetailsResult.OrderReferenceDetails.Destination.PhysicalDestination;
                
  //              amazonAddress.Email = $"noreply@{this.storeContext.CurrentStore.Name.ToLowerInvariant()}";
  //              amazonAddress.PhoneNumber = destination.Phone;
  //              amazonAddress.ZipPostalCode = destination.PostalCode;
  //              amazonAddress.Address1 = destination.AddressLine1;
  //              amazonAddress.Address2 = destination.AddressLine2;
  //              amazonAddress.City = destination.City;
  //              amazonAddress.CountryId = countryService.GetCountryByTwoLetterIsoCode(destination.CountryCode).Id;
  //              amazonAddress.Country = countryService.GetCountryById(amazonAddress.CountryId ?? 0);
  //              amazonAddress.Country.TwoLetterIsoCode = destination.CountryCode;

  //              var state = stateProvinceService.GetStateProvinceByAbbreviation(destination.StateOrRegion, amazonAddress.CountryId);
  //              if (state == null)
  //                  state = stateProvinceService.GetStateProvinces().Where(sp => sp.Name.ToLower() == destination.StateOrRegion.ToLower()).First();
  //              if (state != null)
  //              {
  //                  amazonAddress.StateProvinceId = state.Id;
  //              }


  //              // log info 
  //              var phone = string.Empty;
  //              var name = string.Empty;
  //              if (details.GetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetBillingAddress() && details.GetOrderReferenceDetailsResult.OrderReferenceDetails.BillingAddress.IsSetPhysicalAddress())
  //              {
  //                  name = details.GetOrderReferenceDetailsResult.OrderReferenceDetails.BillingAddress.PhysicalAddress.Name;
  //                  phone = details.GetOrderReferenceDetailsResult.OrderReferenceDetails.BillingAddress.PhysicalAddress.Phone;
  //              }

  //              if (details.GetOrderReferenceDetailsResult.OrderReferenceDetails.IsSetBuyer())
  //              {
  //                  var buyer = details.GetOrderReferenceDetailsResult.OrderReferenceDetails.Buyer;
  //                  if (!string.IsNullOrEmpty(buyer.Email))
  //                  {
  //                      amazonAddress.Email = buyer.Email;
  //                  }

  //                  if (!string.IsNullOrEmpty(buyer.Name))
  //                  {
  //                      var nameParts = buyer.Name.Split(' ');
  //                      if (nameParts.Length > 1)
  //                      {
  //                          amazonAddress.FirstName = nameParts[0];
  //                          for (int i = 1; i < nameParts.Length; i++)
  //                          {
  //                              amazonAddress.LastName = nameParts[i] + " ";
  //                          }
  //                          amazonAddress.LastName = amazonAddress.LastName.Trim();
  //                      }
  //                      else
  //                      {
  //                          amazonAddress.FirstName = buyer.Name;
  //                      }
  //                  }

  //                  if (!string.IsNullOrEmpty(buyer.Phone) && string.IsNullOrWhiteSpace(amazonAddress.PhoneNumber))
  //                  {
  //                      amazonAddress.PhoneNumber = buyer.Phone;
  //                  }
  //              }

  //              logger.InsertLog(LogLevel.Information, string.Format("Amazon Address #{0}", orderReferenceId), string.Format("destination name: {0}; phone: {1}; name: {2}", destination.Name, phone, name));
  //          }
  //          catch (Exception ex)
  //          {
  //              logger.Warning("CBA service GetSelectedAddress() - overall exception - " + ex.Message, ex, workContext.CurrentCustomer);
  //          }

  //          return amazonAddress;
  //      }

  //      public AmazonShippingMethodSet PrepareShippingMethodList(IList<ShoppingCartItem> cart, Address shippingAddress)
  //      {
  //          var model = new AmazonShippingMethodSet();

  //          var getShippingOptionResponse = shippingService.GetShippingOptions(cart, shippingAddress, "", storeContext.CurrentStore.Id);
  //          if (getShippingOptionResponse.Success)
  //          {
  //              foreach (var shippingOption in getShippingOptionResponse.ShippingOptions)
  //              {
  //                  var soModel = new AmazonShippingMethodSet.ShippingMethodModel
  //                  {
  //                      Name = shippingOption.Name,
  //                      Description = shippingOption.Description,
  //                      ShippingRateComputationMethodSystemName = shippingOption.ShippingRateComputationMethodSystemName,
  //                      ShippingOption = shippingOption,
  //                  };

  //                  //adjust rate
  //                  Discount appliedDiscount;
  //                  decimal discountAmount;
  //                  var shippingTotal = orderTotalCalculationService.AdjustShippingRate(shippingOption.Rate, cart, out discountAmount, out appliedDiscount);

  //                  decimal rateBase = taxService.GetShippingPrice(shippingTotal + discountAmount, workContext.CurrentCustomer);
  //                  decimal rate = currencyService.ConvertFromPrimaryStoreCurrency(rateBase, workContext.WorkingCurrency);
  //                  soModel.Fee = priceFormatter.FormatShippingPrice(rate, true);

  //                  model.ShippingMethods.Add(soModel);
  //              }

  //              var shippingOptionToSelect = model.ShippingMethods.FirstOrDefault();
  //              if (shippingOptionToSelect != null)
  //              {
  //                  shippingOptionToSelect.Selected = true;
  //              }
  //          }
  //          else
  //          {
  //              foreach (var error in getShippingOptionResponse.Errors)
  //                  model.Warnings.Add(error);
  //          }

  //          return model;
  //      }

  //      #endregion

  //      #region Private Methods

  //      private string GeneratePassword()
  //      {
  //          return Membership.GeneratePassword(12, 5);
  //      }

  //      private AmazonRegisterResult RegisterNewCustomer(string email, string firstName, string lastName = null)
  //      {
  //          var amazonRegisterResult = new AmazonRegisterResult();

  //          if (string.IsNullOrEmpty(email))
  //              return new AmazonRegisterResult();

  //          if (workContext.CurrentCustomer.IsRegistered())
  //          {
  //              //Already registered customer. 
  //              authenticationService.SignOut();

  //              //Save a new record
  //              workContext.CurrentCustomer = customerService.InsertGuestCustomer();
  //          }
  //          var customer = workContext.CurrentCustomer;
  //          var password = GeneratePassword();

  //          bool isApproved = customerSettings.UserRegistrationType == UserRegistrationType.Standard;
  //          var registrationRequest = new CustomerRegistrationRequest(customer, email,
  //              email, password, customerSettings.DefaultPasswordFormat, isApproved);
  //          var registrationResult = customerRegistrationService.RegisterCustomer(registrationRequest);
  //          if (registrationResult.Success)
  //          {

  //              genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, string.IsNullOrEmpty(firstName) ? string.Empty : firstName);
  //              genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, string.IsNullOrEmpty(lastName) ? string.Empty : lastName);

  //              //login customer now
  //              if (isApproved)
  //                  authenticationService.SignIn(customer, true);

  //              /*
  //              switch (customerSettings.UserRegistrationType)
  //              {
  //                  case UserRegistrationType.EmailValidation:
  //                      {
  //                          //email validation message
  //                          genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken, Guid.NewGuid().ToString());
  //                          workflowMessageService.SendCustomerEmailValidationMessage(customer, workContext.WorkingLanguage.Id);
  //                          break;
  //                      }
  //                  case UserRegistrationType.Standard:
  //                      {
  //                          //send customer welcome message
  //                          workflowMessageService.SendCustomerWelcomeMessage(customer, workContext.WorkingLanguage.Id);
  //                          break;
  //                      }
  //                  default:
  //                      {
  //                          break;
  //                      }
  //              }
  //               * */
  //          }

  //          //errors
  //          foreach (var error in registrationResult.Errors)
  //              amazonRegisterResult.Errors.Add(error);

  //          amazonRegisterResult.NotEncodedPassword = password;
  //          amazonRegisterResult.NewCustomer = customer;

  //          return amazonRegisterResult;
  //      }

  //      private bool IsMinimumOrderPlacementIntervalValid(Customer customer)
  //      {
  //          //prevent 2 orders being placed within an X seconds time frame
  //          if (orderSettings.MinimumOrderPlacementInterval == 0)
  //              return true;

  //          var lastOrder = orderService.SearchOrders(storeId: storeContext.CurrentStore.Id,
  //              customerId: workContext.CurrentCustomer.Id, pageSize: 1)
  //              .FirstOrDefault();
  //          if (lastOrder == null)
  //              return true;

  //          var interval = DateTime.UtcNow - lastOrder.CreatedOnUtc;
  //          return interval.TotalSeconds > orderSettings.MinimumOrderPlacementInterval;
  //      }

  //      /// <summary>
  //      /// Places an order
  //      /// </summary>
  //      /// <param name="processPaymentRequest">Process payment request</param>
  //      /// <returns>Place order result</returns>
  //      private PlaceOrderResult PlaceOrder(ProcessPaymentRequest processPaymentRequest)
  //      {
  //          //think about moving functionality of processing recurring orders (after the initial order was placed) to ProcessNextRecurringPayment() method
  //          if (processPaymentRequest == null)
  //              throw new ArgumentNullException("processPaymentRequest");

  //          if (processPaymentRequest.OrderGuid == Guid.Empty)
  //              processPaymentRequest.OrderGuid = Guid.NewGuid();

  //          var result = new PlaceOrderResult();
  //          try
  //          {
  //              #region Order details (customer, addresses, totals)

  //              //Recurring orders. Load initial order
  //              Order initialOrder = orderService.GetOrderById(processPaymentRequest.InitialOrderId);
  //              if (processPaymentRequest.IsRecurringPayment)
  //              {
  //                  if (initialOrder == null)
  //                      throw new ArgumentException("Initial order is not set for recurring payment");

  //                  processPaymentRequest.PaymentMethodSystemName = initialOrder.PaymentMethodSystemName;
  //              }

  //              //customer
  //              var customer = customerService.GetCustomerById(processPaymentRequest.CustomerId);
  //              if (customer == null)
  //                  throw new ArgumentException("Customer is not set");

  //              //affilites
  //              int affiliateId = 0;
  //              var affiliate = affiliateService.GetAffiliateById(customer.AffiliateId);
  //              if (affiliate != null && affiliate.Active && !affiliate.Deleted)
  //                  affiliateId = affiliate.Id;

  //              //customer currency
  //              string customerCurrencyCode = "";
  //              decimal customerCurrencyRate;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  var currencyTmp = currencyService.GetCurrencyById(customer.GetAttribute<int>(SystemCustomerAttributeNames.CurrencyId, processPaymentRequest.StoreId));
  //                  var customerCurrency = (currencyTmp != null && currencyTmp.Published) ? currencyTmp : workContext.WorkingCurrency;
  //                  customerCurrencyCode = customerCurrency.CurrencyCode;
  //                  var primaryStoreCurrency = currencyService.GetCurrencyById(currencySettings.PrimaryStoreCurrencyId);
  //                  customerCurrencyRate = customerCurrency.Rate / primaryStoreCurrency.Rate;
  //              }
  //              else
  //              {
  //                  customerCurrencyCode = initialOrder.CustomerCurrencyCode;
  //                  customerCurrencyRate = initialOrder.CurrencyRate;
  //              }
  //              //customer language
  //              Language customerLanguage;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  customerLanguage = languageService.GetLanguageById(customer.GetAttribute<int>(
  //                      SystemCustomerAttributeNames.LanguageId, processPaymentRequest.StoreId));
  //              }
  //              else
  //              {
  //                  customerLanguage = languageService.GetLanguageById(initialOrder.CustomerLanguageId);
  //              }
  //              if (customerLanguage == null || !customerLanguage.Published)
  //                  customerLanguage = workContext.WorkingLanguage;

  //              //check whether customer is guest
  //              if (customer.IsGuest() && !orderSettings.AnonymousCheckoutAllowed)
  //                  throw new NopException("Anonymous checkout is not allowed");

  //              //billing address
  //              Address billingAddress;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  if (customer.BillingAddress == null)
  //                      throw new NopException("Billing address is not provided");

  //                  if (!CommonHelper.IsValidEmail(customer.BillingAddress.Email))
  //                      throw new NopException("Email is not valid");

  //                  //clone billing address
  //                  billingAddress = (Address)customer.BillingAddress.Clone();
  //                  if (billingAddress.Country != null && !billingAddress.Country.AllowsBilling)
  //                      throw new NopException(string.Format("Country '{0}' is not allowed for billing", billingAddress.Country.Name));
  //              }
  //              else
  //              {
  //                  if (initialOrder.BillingAddress == null)
  //                      throw new NopException("Billing address is not available");

  //                  //clone billing address
  //                  billingAddress = (Address)initialOrder.BillingAddress.Clone();
  //                  if (billingAddress.Country != null && !billingAddress.Country.AllowsBilling)
  //                      throw new NopException(string.Format("Country '{0}' is not allowed for billing", billingAddress.Country.Name));
  //              }

  //              //checkout attributes
  //              string checkoutAttributeDescription, checkoutAttributesXml;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  checkoutAttributesXml = customer.GetAttribute<string>(SystemCustomerAttributeNames.CheckoutAttributes, processPaymentRequest.StoreId);
  //                  checkoutAttributeDescription = checkoutAttributeFormatter.FormatAttributes(checkoutAttributesXml, customer);
  //              }
  //              else
  //              {
  //                  checkoutAttributesXml = initialOrder.CheckoutAttributesXml;
  //                  checkoutAttributeDescription = initialOrder.CheckoutAttributeDescription;
  //              }

  //              //load and validate customer shopping cart
  //              IList<ShoppingCartItem> cart = null;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  //load shopping cart
  //                  cart = customer.ShoppingCartItems
  //                      .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
  //                      .LimitPerStore(processPaymentRequest.StoreId)
  //                      .ToList();

  //                  if (cart.Count == 0)
  //                      throw new NopException("Cart is empty");

  //                  //validate the entire shopping cart
  //                  var warnings = shoppingCartService.GetShoppingCartWarnings(cart,
  //                      checkoutAttributesXml,
  //                      true);
  //                  if (warnings.Count > 0)
  //                  {
  //                      var warningsSb = new StringBuilder();
  //                      foreach (string warning in warnings)
  //                      {
  //                          warningsSb.Append(warning);
  //                          warningsSb.Append(";");
  //                      }
  //                      throw new NopException(warningsSb.ToString());
  //                  }

  //                  //validate individual cart items
  //                  foreach (var sci in cart)
  //                  {
  //                      var sciWarnings = shoppingCartService.GetShoppingCartItemWarnings(customer, sci.ShoppingCartType,
  //                          sci.Product, processPaymentRequest.StoreId, sci.AttributesXml,
  //                          sci.CustomerEnteredPrice, sci.Quantity, false);
  //                      if (sciWarnings.Count > 0)
  //                      {
  //                          var warningsSb = new StringBuilder();
  //                          foreach (string warning in sciWarnings)
  //                          {
  //                              warningsSb.Append(warning);
  //                              warningsSb.Append(";");
  //                          }
  //                          throw new NopException(warningsSb.ToString());
  //                      }
  //                  }
  //              }

  //              //min totals validation
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  bool minOrderSubtotalAmountOk = orderProcessingService.ValidateMinOrderSubtotalAmount(cart);
  //                  if (!minOrderSubtotalAmountOk)
  //                  {
  //                      decimal minOrderSubtotalAmount = currencyService.ConvertFromPrimaryStoreCurrency(orderSettings.MinOrderSubtotalAmount, workContext.WorkingCurrency);
  //                      throw new NopException(string.Format(localizationService.GetResource("Checkout.MinOrderSubtotalAmount"), priceFormatter.FormatPrice(minOrderSubtotalAmount, true, false)));
  //                  }
  //                  bool minOrderTotalAmountOk = orderProcessingService.ValidateMinOrderTotalAmount(cart);
  //                  if (!minOrderTotalAmountOk)
  //                  {
  //                      decimal minOrderTotalAmount = currencyService.ConvertFromPrimaryStoreCurrency(orderSettings.MinOrderTotalAmount, workContext.WorkingCurrency);
  //                      throw new NopException(string.Format(localizationService.GetResource("Checkout.MinOrderTotalAmount"), priceFormatter.FormatPrice(minOrderTotalAmount, true, false)));
  //                  }
  //              }

  //              //tax display type
  //              var customerTaxDisplayType = TaxDisplayType.IncludingTax;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  if (taxSettings.AllowCustomersToSelectTaxDisplayType)
  //                      customerTaxDisplayType = (TaxDisplayType)customer.GetAttribute<int>(SystemCustomerAttributeNames.TaxDisplayTypeId, processPaymentRequest.StoreId);
  //                  else
  //                      customerTaxDisplayType = taxSettings.TaxDisplayType;
  //              }
  //              else
  //              {
  //                  customerTaxDisplayType = initialOrder.CustomerTaxDisplayType;
  //              }

  //              //applied discount (used to store discount usage history)
  //              var appliedDiscounts = new List<Discount>();

  //              //sub total
  //              decimal orderSubTotalInclTax, orderSubTotalExclTax;
  //              decimal orderSubTotalDiscountInclTax = 0, orderSubTotalDiscountExclTax = 0;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  //sub total (incl tax)
  //                  decimal orderSubTotalDiscountAmount1;
  //                  Discount orderSubTotalAppliedDiscount1;
  //                  decimal subTotalWithoutDiscountBase1;
  //                  decimal subTotalWithDiscountBase1;
  //                  orderTotalCalculationService.GetShoppingCartSubTotal(cart,
  //                      true, out orderSubTotalDiscountAmount1, out orderSubTotalAppliedDiscount1,
  //                      out subTotalWithoutDiscountBase1, out subTotalWithDiscountBase1);
  //                  orderSubTotalInclTax = subTotalWithoutDiscountBase1;
  //                  orderSubTotalDiscountInclTax = orderSubTotalDiscountAmount1;

  //                  //discount history
  //                  if (orderSubTotalAppliedDiscount1 != null && !appliedDiscounts.ContainsDiscount(orderSubTotalAppliedDiscount1))
  //                      appliedDiscounts.Add(orderSubTotalAppliedDiscount1);

  //                  //sub total (excl tax)
  //                  decimal orderSubTotalDiscountAmount2;
  //                  Discount orderSubTotalAppliedDiscount2;
  //                  decimal subTotalWithoutDiscountBase2;
  //                  decimal subTotalWithDiscountBase2;
  //                  orderTotalCalculationService.GetShoppingCartSubTotal(cart,
  //                      false, out orderSubTotalDiscountAmount2, out orderSubTotalAppliedDiscount2,
  //                      out subTotalWithoutDiscountBase2, out subTotalWithDiscountBase2);
  //                  orderSubTotalExclTax = subTotalWithoutDiscountBase2;
  //                  orderSubTotalDiscountExclTax = orderSubTotalDiscountAmount2;
  //              }
  //              else
  //              {
  //                  orderSubTotalInclTax = initialOrder.OrderSubtotalInclTax;
  //                  orderSubTotalExclTax = initialOrder.OrderSubtotalExclTax;
  //              }


  //              //shipping info
  //              bool shoppingCartRequiresShipping = false;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  shoppingCartRequiresShipping = cart.RequiresShipping();
  //              }
  //              else
  //              {
  //                  shoppingCartRequiresShipping = initialOrder.ShippingStatus != ShippingStatus.ShippingNotRequired;
  //              }
  //              Address shippingAddress = null;
  //              string shippingMethodName = "", shippingRateComputationMethodSystemName = "";
  //              bool pickUpInStore = false;
  //              decimal? originalShippingRate = null;
  //              if (shoppingCartRequiresShipping)
  //              {
  //                  if (!processPaymentRequest.IsRecurringPayment)
  //                  {
  //                      pickUpInStore = shippingSettings.AllowPickUpInStore &&
  //                          customer.GetAttribute<bool>(SystemCustomerAttributeNames.SelectedPickUpInStore, processPaymentRequest.StoreId);

  //                      if (!pickUpInStore)
  //                      {
  //                          if (customer.ShippingAddress == null)
  //                              throw new NopException("Shipping address is not provided");

  //                          if (!CommonHelper.IsValidEmail(customer.ShippingAddress.Email))
  //                              throw new NopException("Email is not valid");

  //                          //clone shipping address
  //                          shippingAddress = (Address)customer.ShippingAddress.Clone();
  //                          if (shippingAddress.Country != null && !shippingAddress.Country.AllowsShipping)
  //                          {
  //                              throw new NopException(string.Format("Country '{0}' is not allowed for shipping", shippingAddress.Country.Name));
  //                          }
  //                      }

  //                      var shippingOption = customer.GetAttribute<ShippingOption>(SystemCustomerAttributeNames.SelectedShippingOption, processPaymentRequest.StoreId);
  //                      if (shippingOption != null)
  //                      {
  //                          shippingMethodName = shippingOption.Name;
  //                          shippingRateComputationMethodSystemName = shippingOption.ShippingRateComputationMethodSystemName;
  //                          originalShippingRate = shippingOption.OriginalRate;
  //                      }
  //                  }
  //                  else
  //                  {
  //                      pickUpInStore = initialOrder.PickUpInStore;

  //                      if (!pickUpInStore)
  //                      {
  //                          if (initialOrder.ShippingAddress == null)
  //                              throw new NopException("Shipping address is not available");

  //                          //clone shipping address
  //                          shippingAddress = (Address)initialOrder.ShippingAddress.Clone();
  //                          if (shippingAddress.Country != null && !shippingAddress.Country.AllowsShipping)
  //                          {
  //                              throw new NopException(string.Format("Country '{0}' is not allowed for shipping", shippingAddress.Country.Name));
  //                          }
  //                      }

  //                      shippingMethodName = initialOrder.ShippingMethod;
  //                      shippingRateComputationMethodSystemName = initialOrder.ShippingRateComputationMethodSystemName;
  //                  }
  //              }


  //              //shipping total
  //              decimal? orderShippingTotalInclTax, orderShippingTotalExclTax = null;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  decimal taxRate;
  //                  Discount shippingTotalDiscount;
  //                  orderShippingTotalInclTax = orderTotalCalculationService.GetShoppingCartShippingTotal(cart, true, out taxRate, out shippingTotalDiscount);
  //                  orderShippingTotalExclTax = orderTotalCalculationService.GetShoppingCartShippingTotal(cart, false);
  //                  if (!orderShippingTotalInclTax.HasValue || !orderShippingTotalExclTax.HasValue)
  //                      throw new NopException("Shipping total couldn't be calculated");

  //                  if (shippingTotalDiscount != null && !appliedDiscounts.ContainsDiscount(shippingTotalDiscount))
  //                      appliedDiscounts.Add(shippingTotalDiscount);
  //              }
  //              else
  //              {
  //                  orderShippingTotalInclTax = initialOrder.OrderShippingInclTax;
  //                  orderShippingTotalExclTax = initialOrder.OrderShippingExclTax;
  //              }


  //              //payment total
  //              decimal paymentAdditionalFeeInclTax, paymentAdditionalFeeExclTax;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  decimal paymentAdditionalFee = paymentService.GetAdditionalHandlingFee(cart, processPaymentRequest.PaymentMethodSystemName);
  //                  paymentAdditionalFeeInclTax = taxService.GetPaymentMethodAdditionalFee(paymentAdditionalFee, true, customer);
  //                  paymentAdditionalFeeExclTax = taxService.GetPaymentMethodAdditionalFee(paymentAdditionalFee, false, customer);
  //              }
  //              else
  //              {
  //                  paymentAdditionalFeeInclTax = initialOrder.PaymentMethodAdditionalFeeInclTax;
  //                  paymentAdditionalFeeExclTax = initialOrder.PaymentMethodAdditionalFeeExclTax;
  //              }


  //              //tax total
  //              decimal orderTaxTotal = decimal.Zero;
  //              string vatNumber = "", taxRates = "";
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  //tax amount
  //                  SortedDictionary<decimal, decimal> taxRatesDictionary;
  //                  orderTaxTotal = orderTotalCalculationService.GetTaxTotal(cart, out taxRatesDictionary);

  //                  //VAT number
  //                  var customerVatStatus = (VatNumberStatus)customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId);
  //                  if (taxSettings.EuVatEnabled && customerVatStatus == VatNumberStatus.Valid)
  //                      vatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);

  //                  //tax rates
  //                  foreach (var kvp in taxRatesDictionary)
  //                  {
  //                      var taxRate = kvp.Key;
  //                      var taxValue = kvp.Value;
  //                      taxRates += string.Format("{0}:{1};   ", taxRate.ToString(CultureInfo.InvariantCulture), taxValue.ToString(CultureInfo.InvariantCulture));
  //                  }
  //              }
  //              else
  //              {
  //                  orderTaxTotal = initialOrder.OrderTax;
  //                  //VAT number
  //                  vatNumber = initialOrder.VatNumber;
  //              }


  //              //order total (and applied discounts, gift cards, reward points)
  //              decimal? orderTotal = null;
  //              decimal orderDiscountAmount = decimal.Zero;
  //              List<AppliedGiftCard> appliedGiftCards = null;
  //              int redeemedRewardPoints = 0;
  //              decimal redeemedRewardPointsAmount = decimal.Zero;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  Discount orderAppliedDiscount;
  //                  orderTotal = orderTotalCalculationService.GetShoppingCartTotal(cart,
  //                      out orderDiscountAmount, out orderAppliedDiscount, out appliedGiftCards,
  //                      out redeemedRewardPoints, out redeemedRewardPointsAmount);
  //                  if (!orderTotal.HasValue)
  //                      throw new NopException("Order total couldn't be calculated");

  //                  //discount history
  //                  if (orderAppliedDiscount != null && !appliedDiscounts.ContainsDiscount(orderAppliedDiscount))
  //                      appliedDiscounts.Add(orderAppliedDiscount);
  //              }
  //              else
  //              {
  //                  orderDiscountAmount = initialOrder.OrderDiscount;
  //                  orderTotal = initialOrder.OrderTotal;
  //              }
  //              processPaymentRequest.OrderTotal = orderTotal.Value;

  //              #endregion

  //              #region Payment workflow

  //              //skip payment workflow if order total equals zero
  //              bool skipPaymentWorkflow = orderTotal.Value == decimal.Zero;

  //              //payment workflow
  //              if (!skipPaymentWorkflow)
  //              {
  //                  var paymentMethod = paymentService.LoadPaymentMethodBySystemName(processPaymentRequest.PaymentMethodSystemName);
  //                  if (paymentMethod == null)
  //                      throw new NopException("Payment method couldn't be loaded");

  //                  //ensure that payment method is active
  //                  if (!paymentMethod.IsPaymentMethodActive(paymentSettings))
  //                      throw new NopException("Payment method is not active");
  //              }
  //              else
  //                  processPaymentRequest.PaymentMethodSystemName = "";

  //              //recurring or standard shopping cart?
  //              bool isRecurringShoppingCart = false;
  //              if (!processPaymentRequest.IsRecurringPayment)
  //              {
  //                  isRecurringShoppingCart = cart.IsRecurring();
  //                  if (isRecurringShoppingCart)
  //                  {
  //                      int recurringCycleLength;
  //                      RecurringProductCyclePeriod recurringCyclePeriod;
  //                      int recurringTotalCycles;
  //                      string recurringCyclesError = cart.GetRecurringCycleInfo(localizationService,
  //                          out recurringCycleLength, out recurringCyclePeriod, out recurringTotalCycles);
  //                      if (!string.IsNullOrEmpty(recurringCyclesError))
  //                          throw new NopException(recurringCyclesError);
  //                      processPaymentRequest.RecurringCycleLength = recurringCycleLength;
  //                      processPaymentRequest.RecurringCyclePeriod = recurringCyclePeriod;
  //                      processPaymentRequest.RecurringTotalCycles = recurringTotalCycles;
  //                  }
  //              }
  //              else
  //                  isRecurringShoppingCart = true;


  //              //process payment
  //              ProcessPaymentResult processPaymentResult = null;
  //              if (!skipPaymentWorkflow)
  //              {
  //                  if (!processPaymentRequest.IsRecurringPayment)
  //                  {
  //                      if (isRecurringShoppingCart)
  //                      {
  //                          //recurring cart
  //                          var recurringPaymentType = paymentService.GetRecurringPaymentType(processPaymentRequest.PaymentMethodSystemName);
  //                          switch (recurringPaymentType)
  //                          {
  //                              case RecurringPaymentType.NotSupported:
  //                                  throw new NopException("Recurring payments are not supported by selected payment method");
  //                              case RecurringPaymentType.Manual:
  //                              case RecurringPaymentType.Automatic:
  //                                  processPaymentResult = paymentService.ProcessRecurringPayment(processPaymentRequest);
  //                                  break;
  //                              default:
  //                                  throw new NopException("Not supported recurring payment type");
  //                          }
  //                      }
  //                      else
  //                      {
  //                          //standard cart
  //                          processPaymentResult = paymentService.ProcessPayment(processPaymentRequest);
  //                      }
  //                  }
  //                  else
  //                  {
  //                      if (isRecurringShoppingCart)
  //                      {
  //                          //Old credit card info
  //                          processPaymentRequest.CreditCardType = initialOrder.AllowStoringCreditCardNumber ? encryptionService.DecryptText(initialOrder.CardType) : "";
  //                          processPaymentRequest.CreditCardName = initialOrder.AllowStoringCreditCardNumber ? encryptionService.DecryptText(initialOrder.CardName) : "";
  //                          processPaymentRequest.CreditCardNumber = initialOrder.AllowStoringCreditCardNumber ? encryptionService.DecryptText(initialOrder.CardNumber) : "";
  //                          //MaskedCreditCardNumber 
  //                          processPaymentRequest.CreditCardCvv2 = initialOrder.AllowStoringCreditCardNumber ? encryptionService.DecryptText(initialOrder.CardCvv2) : "";
  //                          try
  //                          {
  //                              processPaymentRequest.CreditCardExpireMonth = initialOrder.AllowStoringCreditCardNumber ? Convert.ToInt32(encryptionService.DecryptText(initialOrder.CardExpirationMonth)) : 0;
  //                              processPaymentRequest.CreditCardExpireYear = initialOrder.AllowStoringCreditCardNumber ? Convert.ToInt32(encryptionService.DecryptText(initialOrder.CardExpirationYear)) : 0;
  //                          }
  //                          catch { }

  //                          var recurringPaymentType = paymentService.GetRecurringPaymentType(processPaymentRequest.PaymentMethodSystemName);
  //                          switch (recurringPaymentType)
  //                          {
  //                              case RecurringPaymentType.NotSupported:
  //                                  throw new NopException("Recurring payments are not supported by selected payment method");
  //                              case RecurringPaymentType.Manual:
  //                                  processPaymentResult = paymentService.ProcessRecurringPayment(processPaymentRequest);
  //                                  break;
  //                              case RecurringPaymentType.Automatic:
  //                                  //payment is processed on payment gateway site
  //                                  processPaymentResult = new ProcessPaymentResult();
  //                                  break;
  //                              default:
  //                                  throw new NopException("Not supported recurring payment type");
  //                          }
  //                      }
  //                      else
  //                      {
  //                          throw new NopException("No recurring products");
  //                      }
  //                  }
  //              }
  //              else
  //              {
  //                  //payment is not required
  //                  if (processPaymentResult == null)
  //                      processPaymentResult = new ProcessPaymentResult();
  //                  processPaymentResult.NewPaymentStatus = PaymentStatus.Paid;
  //              }

  //              if (processPaymentResult == null)
  //                  throw new NopException("processPaymentResult is not available");

  //              #endregion

  //              var isInsuranceApplied = this.shippingInsuranceService.IsShowInsurance() && this.shippingInsuranceService.IsInsuranceApplied(cart);
  //              if (processPaymentResult.Success)
  //              {

  //                  //save order in data storage
  //                  //uncomment this line to support transactions
  //                  //using (var scope = new System.Transactions.TransactionScope())
  //                  {
  //                      #region Save order details

  //                      // Always Unpaid
  //                      processPaymentResult.NewPaymentStatus = PaymentStatus.Pending;

  //                      var shippingStatus = ShippingStatus.NotYetShipped;
  //                      if (!shoppingCartRequiresShipping)
  //                          shippingStatus = ShippingStatus.ShippingNotRequired;

  //                      var order = new Order
  //                      {
  //                          StoreId = processPaymentRequest.StoreId,
  //                          OrderGuid = processPaymentRequest.OrderGuid,
  //                          CustomerId = customer.Id,
  //                          CustomerLanguageId = customerLanguage.Id,
  //                          CustomerTaxDisplayType = customerTaxDisplayType,
  //                          CustomerIp = webHelper.GetCurrentIpAddress(),
  //                          OrderSubtotalInclTax = orderSubTotalInclTax,
  //                          OrderSubtotalExclTax = orderSubTotalExclTax,
  //                          OrderSubTotalDiscountInclTax = orderSubTotalDiscountInclTax,
  //                          OrderSubTotalDiscountExclTax = orderSubTotalDiscountExclTax,
  //                          OrderShippingInclTax = orderShippingTotalInclTax.Value,
  //                          OrderShippingExclTax = orderShippingTotalExclTax.Value,
  //                          PaymentMethodAdditionalFeeInclTax = paymentAdditionalFeeInclTax,
  //                          PaymentMethodAdditionalFeeExclTax = paymentAdditionalFeeExclTax,
  //                          TaxRates = taxRates,
  //                          OrderTax = orderTaxTotal,
  //                          OrderTotal = orderTotal.Value,
  //                          RefundedAmount = decimal.Zero,
  //                          OrderDiscount = orderDiscountAmount,
  //                          CheckoutAttributeDescription = checkoutAttributeDescription,
  //                          CheckoutAttributesXml = checkoutAttributesXml,
  //                          CustomerCurrencyCode = customerCurrencyCode,
  //                          CurrencyRate = customerCurrencyRate,
  //                          AffiliateId = affiliateId,
  //                          OrderStatus = OrderStatus.Pending,
  //                          AllowStoringCreditCardNumber = processPaymentResult.AllowStoringCreditCardNumber,
  //                          CardType = processPaymentResult.AllowStoringCreditCardNumber ? encryptionService.EncryptText(processPaymentRequest.CreditCardType) : string.Empty,
  //                          CardName = processPaymentResult.AllowStoringCreditCardNumber ? encryptionService.EncryptText(processPaymentRequest.CreditCardName) : string.Empty,
  //                          CardNumber = processPaymentResult.AllowStoringCreditCardNumber ? encryptionService.EncryptText(processPaymentRequest.CreditCardNumber) : string.Empty,
  //                          MaskedCreditCardNumber = encryptionService.EncryptText(paymentService.GetMaskedCreditCardNumber(processPaymentRequest.CreditCardNumber)),
  //                          CardCvv2 = processPaymentResult.AllowStoringCreditCardNumber ? encryptionService.EncryptText(processPaymentRequest.CreditCardCvv2) : string.Empty,
  //                          CardExpirationMonth = processPaymentResult.AllowStoringCreditCardNumber ? encryptionService.EncryptText(processPaymentRequest.CreditCardExpireMonth.ToString()) : string.Empty,
  //                          CardExpirationYear = processPaymentResult.AllowStoringCreditCardNumber ? encryptionService.EncryptText(processPaymentRequest.CreditCardExpireYear.ToString()) : string.Empty,
  //                          PaymentMethodSystemName = processPaymentRequest.PaymentMethodSystemName,
  //                          AuthorizationTransactionId = processPaymentResult.AuthorizationTransactionId,
  //                          AuthorizationTransactionCode = processPaymentResult.AuthorizationTransactionCode,
  //                          AuthorizationTransactionResult = processPaymentResult.AuthorizationTransactionResult,
  //                          CaptureTransactionId = processPaymentResult.CaptureTransactionId,
  //                          CaptureTransactionResult = processPaymentResult.CaptureTransactionResult,
  //                          SubscriptionTransactionId = processPaymentResult.SubscriptionTransactionId,
  //                          PurchaseOrderNumber = processPaymentRequest.PurchaseOrderNumber,
  //                          PaymentStatus = processPaymentResult.NewPaymentStatus,
  //                          PaidDateUtc = null,
  //                          BillingAddress = billingAddress,
  //                          ShippingAddress = shippingAddress,
  //                          ShippingStatus = shippingStatus,
  //                          ShippingMethod = shippingMethodName,
  //                          PickUpInStore = pickUpInStore,
  //                          ShippingRateComputationMethodSystemName = shippingRateComputationMethodSystemName,
  //                          CustomValuesXml = processPaymentRequest.SerializeCustomValues(),
  //                          VatNumber = vatNumber,
  //                          CreatedOnUtc = DateTime.UtcNow,
  //                          Deleted = true
  //                      };

  //                      #region Cancel test orders (which uses 99% off coupon)

  //                      if (appliedDiscounts.Any(i => i.Id == 4))
  //                      {
  //                          order.OrderStatus = OrderStatus.Cancelled;
  //                      }

  //                      #endregion

  //                      orderService.InsertOrder(order);

  //                      result.PlacedOrder = order;

  //                      if (originalShippingRate.HasValue)
  //                      {
  //                          this.shippingService.InsertOriginalShippingRate(new OriginalShippingRate
  //                          {
  //                              OrderId = order.Id,
  //                              Value = originalShippingRate.Value,
  //                          });
  //                      }

  //                      if (!processPaymentRequest.IsRecurringPayment)
  //                      {
  //                          //move shopping cart items to order items
  //                          foreach (var sc in cart)
  //                          {
  //                              //prices
  //                              decimal taxRate;
  //                              Discount scDiscount;
  //                              decimal discountAmount;
  //                              decimal scUnitPrice = priceCalculationService.GetUnitPrice(sc);
  //                              decimal scSubTotal = priceCalculationService.GetSubTotal(sc, true, out discountAmount, out scDiscount);
  //                              decimal scUnitPriceInclTax = taxService.GetProductPrice(sc.Product, scUnitPrice, true, customer, out taxRate);
  //                              decimal scUnitPriceExclTax = taxService.GetProductPrice(sc.Product, scUnitPrice, false, customer, out taxRate);
  //                              decimal scSubTotalInclTax = taxService.GetProductPrice(sc.Product, scSubTotal, true, customer, out taxRate);
  //                              decimal scSubTotalExclTax = taxService.GetProductPrice(sc.Product, scSubTotal, false, customer, out taxRate);

  //                              decimal discountAmountInclTax = taxService.GetProductPrice(sc.Product, discountAmount, true, customer, out taxRate);
  //                              decimal discountAmountExclTax = taxService.GetProductPrice(sc.Product, discountAmount, false, customer, out taxRate);
  //                              if (scDiscount != null && !appliedDiscounts.ContainsDiscount(scDiscount))
  //                                  appliedDiscounts.Add(scDiscount);

  //                              //attributes
  //                              string attributeDescription = productAttributeFormatter.FormatAttributes(sc.Product, sc.AttributesXml, customer);

  //                              var itemWeight = shippingService.GetShoppingCartItemWeight(sc);

  //                              //save order item
  //                              var orderItem = new OrderItem
  //                              {
  //                                  OrderItemGuid = Guid.NewGuid(),
  //                                  Order = order,
  //                                  ProductId = sc.ProductId,
  //                                  UnitPriceInclTax = scUnitPriceInclTax,
  //                                  UnitPriceExclTax = scUnitPriceExclTax,
  //                                  PriceInclTax = scSubTotalInclTax,
  //                                  PriceExclTax = scSubTotalExclTax,
  //                                  OriginalProductCost = priceCalculationService.GetProductCost(sc.Product, sc.AttributesXml),
  //                                  AttributeDescription = attributeDescription,
  //                                  AttributesXml = sc.AttributesXml,
  //                                  Quantity = sc.Quantity,
  //                                  DiscountAmountInclTax = discountAmountInclTax,
  //                                  DiscountAmountExclTax = discountAmountExclTax,
  //                                  DownloadCount = 0,
  //                                  IsDownloadActivated = false,
  //                                  LicenseDownloadId = 0,
  //                                  ItemWeight = itemWeight,
  //                              };
  //                              order.OrderItems.Add(orderItem);
  //                              orderService.UpdateOrder(order);

  //                              //gift cards
  //                              if (sc.Product.IsGiftCard)
  //                              {
  //                                  string giftCardRecipientName, giftCardRecipientEmail,
  //                                      giftCardSenderName, giftCardSenderEmail, giftCardMessage;
  //                                  productAttributeParser.GetGiftCardAttribute(sc.AttributesXml,
  //                                      out giftCardRecipientName, out giftCardRecipientEmail,
  //                                      out giftCardSenderName, out giftCardSenderEmail, out giftCardMessage);

  //                                  for (int i = 0; i < sc.Quantity; i++)
  //                                  {
  //                                      var gc = new GiftCard
  //                                      {
  //                                          GiftCardType = sc.Product.GiftCardType,
  //                                          PurchasedWithOrderItem = orderItem,
  //                                          Amount = scUnitPriceExclTax,
  //                                          IsGiftCardActivated = false,
  //                                          GiftCardCouponCode = giftCardService.GenerateGiftCardCode(),
  //                                          RecipientName = giftCardRecipientName,
  //                                          RecipientEmail = giftCardRecipientEmail,
  //                                          SenderName = giftCardSenderName,
  //                                          SenderEmail = giftCardSenderEmail,
  //                                          Message = giftCardMessage,
  //                                          IsRecipientNotified = false,
  //                                          CreatedOnUtc = DateTime.UtcNow
  //                                      };
  //                                      giftCardService.InsertGiftCard(gc);
  //                                  }
  //                              }

  //                              //inventory
  //                              productService.AdjustInventory(sc.Product, -sc.Quantity, sc.AttributesXml);
  //                          }

  //                          //clear shopping cart
  //                          cart.ToList().ForEach(sci => shoppingCartService.DeleteShoppingCartItem(sci, false));
  //                      }
  //                      else
  //                      {
  //                          //recurring payment
  //                          var initialOrderItems = initialOrder.OrderItems;
  //                          foreach (var orderItem in initialOrderItems)
  //                          {
  //                              //save item
  //                              var newOrderItem = new OrderItem
  //                              {
  //                                  OrderItemGuid = Guid.NewGuid(),
  //                                  Order = order,
  //                                  ProductId = orderItem.ProductId,
  //                                  UnitPriceInclTax = orderItem.UnitPriceInclTax,
  //                                  UnitPriceExclTax = orderItem.UnitPriceExclTax,
  //                                  PriceInclTax = orderItem.PriceInclTax,
  //                                  PriceExclTax = orderItem.PriceExclTax,
  //                                  OriginalProductCost = orderItem.OriginalProductCost,
  //                                  AttributeDescription = orderItem.AttributeDescription,
  //                                  AttributesXml = orderItem.AttributesXml,
  //                                  Quantity = orderItem.Quantity,
  //                                  DiscountAmountInclTax = orderItem.DiscountAmountInclTax,
  //                                  DiscountAmountExclTax = orderItem.DiscountAmountExclTax,
  //                                  DownloadCount = 0,
  //                                  IsDownloadActivated = false,
  //                                  LicenseDownloadId = 0,
  //                                  ItemWeight = orderItem.ItemWeight,
  //                              };
  //                              order.OrderItems.Add(newOrderItem);
  //                              orderService.UpdateOrder(order);

  //                              //gift cards
  //                              if (orderItem.Product.IsGiftCard)
  //                              {
  //                                  string giftCardRecipientName, giftCardRecipientEmail,
  //                                      giftCardSenderName, giftCardSenderEmail, giftCardMessage;
  //                                  productAttributeParser.GetGiftCardAttribute(orderItem.AttributesXml,
  //                                      out giftCardRecipientName, out giftCardRecipientEmail,
  //                                      out giftCardSenderName, out giftCardSenderEmail, out giftCardMessage);

  //                                  for (int i = 0; i < orderItem.Quantity; i++)
  //                                  {
  //                                      var gc = new GiftCard
  //                                      {
  //                                          GiftCardType = orderItem.Product.GiftCardType,
  //                                          PurchasedWithOrderItem = newOrderItem,
  //                                          Amount = orderItem.UnitPriceExclTax,
  //                                          IsGiftCardActivated = false,
  //                                          GiftCardCouponCode = giftCardService.GenerateGiftCardCode(),
  //                                          RecipientName = giftCardRecipientName,
  //                                          RecipientEmail = giftCardRecipientEmail,
  //                                          SenderName = giftCardSenderName,
  //                                          SenderEmail = giftCardSenderEmail,
  //                                          Message = giftCardMessage,
  //                                          IsRecipientNotified = false,
  //                                          CreatedOnUtc = DateTime.UtcNow
  //                                      };
  //                                      giftCardService.InsertGiftCard(gc);
  //                                  }
  //                              }

  //                              //inventory
  //                              productService.AdjustInventory(orderItem.Product, -orderItem.Quantity, orderItem.AttributesXml);
  //                          }
  //                      }

  //                      if (order.OrderStatus == OrderStatus.Pending)
  //                      {
  //                          order.OrderStatus = OrderStatus.Processing;
  //                          orderService.UpdateOrder(order);
  //                      }

  //                      if (this.storeContext.CurrentStore.Id == (int)NopStore.Autoplicity || this.storeContext.CurrentStore.Id == (int)NopStore.Thmotorsports)
  //                      {
  //                          this.salesQuoteService.UpdatePaidQuote(order);
  //                      }

  //                      //discount usage history
  //                      if (!processPaymentRequest.IsRecurringPayment)
  //                          foreach (var discount in appliedDiscounts)
  //                          {
  //                              var duh = new DiscountUsageHistory
  //                              {
  //                                  Discount = discount,
  //                                  Order = order,
  //                                  CreatedOnUtc = DateTime.UtcNow
  //                              };
  //                              discountService.InsertDiscountUsageHistory(duh);
  //                          }

  //                      //gift card usage history
  //                      if (!processPaymentRequest.IsRecurringPayment)
  //                          if (appliedGiftCards != null)
  //                              foreach (var agc in appliedGiftCards)
  //                              {
  //                                  decimal amountUsed = agc.AmountCanBeUsed;
  //                                  var gcuh = new GiftCardUsageHistory
  //                                  {
  //                                      GiftCard = agc.GiftCard,
  //                                      UsedWithOrder = order,
  //                                      UsedValue = amountUsed,
  //                                      CreatedOnUtc = DateTime.UtcNow
  //                                  };
  //                                  agc.GiftCard.GiftCardUsageHistory.Add(gcuh);
  //                                  giftCardService.UpdateGiftCard(agc.GiftCard);
  //                              }

  //                      //reward points history
  //                      if (redeemedRewardPointsAmount > decimal.Zero)
  //                      {
  //                          customer.AddRewardPointsHistoryEntry(-redeemedRewardPoints,
  //                              string.Format(localizationService.GetResource("RewardPoints.Message.RedeemedForOrder", order.CustomerLanguageId), order.Id),
  //                              order,
  //                              redeemedRewardPointsAmount);
  //                          customerService.UpdateCustomer(customer);
  //                      }

  //                      //recurring orders
  //                      if (!processPaymentRequest.IsRecurringPayment && isRecurringShoppingCart)
  //                      {
  //                          //create recurring payment (the first payment)
  //                          var rp = new RecurringPayment
  //                          {
  //                              CycleLength = processPaymentRequest.RecurringCycleLength,
  //                              CyclePeriod = processPaymentRequest.RecurringCyclePeriod,
  //                              TotalCycles = processPaymentRequest.RecurringTotalCycles,
  //                              StartDateUtc = DateTime.UtcNow,
  //                              IsActive = true,
  //                              CreatedOnUtc = DateTime.UtcNow,
  //                              InitialOrder = order,
  //                          };
  //                          orderService.InsertRecurringPayment(rp);


  //                          var recurringPaymentType = paymentService.GetRecurringPaymentType(processPaymentRequest.PaymentMethodSystemName);
  //                          switch (recurringPaymentType)
  //                          {
  //                              case RecurringPaymentType.NotSupported:
  //                                  {
  //                                      //not supported
  //                                  }
  //                                  break;
  //                              case RecurringPaymentType.Manual:
  //                                  {
  //                                      //first payment
  //                                      var rph = new RecurringPaymentHistory
  //                                      {
  //                                          RecurringPayment = rp,
  //                                          CreatedOnUtc = DateTime.UtcNow,
  //                                          OrderId = order.Id,
  //                                      };
  //                                      rp.RecurringPaymentHistory.Add(rph);
  //                                      orderService.UpdateRecurringPayment(rp);
  //                                  }
  //                                  break;
  //                              case RecurringPaymentType.Automatic:
  //                                  {
  //                                      //will be created later (process is automated)
  //                                  }
  //                                  break;
  //                              default:
  //                                  break;
  //                          }
  //                      }

  //                      #endregion

  //                      #region Notifications & notes

  //                      //notes, messages
  //                      if (workContext.OriginalCustomerIfImpersonated != null)
  //                      {
  //                          //this order is placed by a store administrator impersonating a customer
  //                          order.OrderNotes.Add(new OrderNote
  //                          {
  //                              Note = string.Format("Order placed by a store owner ('{0}'. ID = {1}) impersonating the customer.",
  //                                  workContext.OriginalCustomerIfImpersonated.Email, workContext.OriginalCustomerIfImpersonated.Id),
  //                              DisplayToCustomer = false,
  //                              CreatedOnUtc = DateTime.UtcNow
  //                          });
  //                          orderService.UpdateOrder(order);
  //                      }
  //                      else
  //                      {
  //                          order.OrderNotes.Add(new OrderNote
  //                          {
  //                              Note = "Order placed",
  //                              DisplayToCustomer = false,
  //                              CreatedOnUtc = DateTime.UtcNow
  //                          });
  //                          orderService.UpdateOrder(order);
  //                      }


  //                      //send email notifications
  //                      /*int orderPlacedStoreOwnerNotificationQueuedEmailId = workflowMessageService.SendOrderPlacedStoreOwnerNotification(order, localizationSettings.DefaultAdminLanguageId);
  //                      if (orderPlacedStoreOwnerNotificationQueuedEmailId > 0)
  //                      {
  //                          order.OrderNotes.Add(new OrderNote
  //                          {
  //                              Note = string.Format("\"Order placed\" email (to store owner) has been queued. Queued email identifier: {0}.", orderPlacedStoreOwnerNotificationQueuedEmailId),
  //                              DisplayToCustomer = false,
  //                              CreatedOnUtc = DateTime.UtcNow
  //                          });
  //                          orderService.UpdateOrder(order);
  //                      }

  //                      var orderPlacedAttachmentFilePath = orderSettings.AttachPdfInvoiceToOrderPlacedEmail ?
  //                          pdfService.PrintOrderToPdf(order, 0) : null;
  //                      var orderPlacedAttachmentFileName = orderSettings.AttachPdfInvoiceToOrderPlacedEmail ?
  //                          "order.pdf" : null;
  //                      int orderPlacedCustomerNotificationQueuedEmailId = workflowMessageService
  //                          .SendOrderPlacedCustomerNotification(order, order.CustomerLanguageId, orderPlacedAttachmentFilePath, orderPlacedAttachmentFileName);
  //                      if (orderPlacedCustomerNotificationQueuedEmailId > 0)
  //                      {
  //                          order.OrderNotes.Add(new OrderNote
  //                          {
  //                              Note = string.Format("\"Order placed\" email (to customer) has been queued. Queued email identifier: {0}.", orderPlacedCustomerNotificationQueuedEmailId),
  //                              DisplayToCustomer = false,
  //                              CreatedOnUtc = DateTime.UtcNow
  //                          });
  //                          orderService.UpdateOrder(order);
  //                      }*/

  //                      /*var vendors = GetVendorsInOrder(order);
  //                      foreach (var vendor in vendors)
  //                      {
  //                          int orderPlacedVendorNotificationQueuedEmailId = _workflowMessageService.SendOrderPlacedVendorNotification(order, vendor, order.CustomerLanguageId);
  //                          if (orderPlacedVendorNotificationQueuedEmailId > 0)
  //                          {
  //                              order.OrderNotes.Add(new OrderNote
  //                              {
  //                                  Note = string.Format("\"Order placed\" email (to vendor) has been queued. Queued email identifier: {0}.", orderPlacedVendorNotificationQueuedEmailId),
  //                                  DisplayToCustomer = false,
  //                                  CreatedOnUtc = DateTime.UtcNow
  //                              });
  //                              _orderService.UpdateOrder(order);
  //                          }
  //                      }

  //                      */
  //                      //check order status
  //                      orderProcessingService.CheckOrderStatus(order);

  //                      //reset checkout data
  //                      if (!processPaymentRequest.IsRecurringPayment)
  //                          customerService.ResetCheckoutData(customer, processPaymentRequest.StoreId, clearCouponCodes: true, clearCheckoutAttributes: true);

  //                      if (!processPaymentRequest.IsRecurringPayment)
  //                      {
  //                          customerActivityService.InsertActivity(
  //                              "PublicStore.PlaceOrder",
  //                              localizationService.GetResource("ActivityLog.PublicStore.PlaceOrder"),
  //                              order.Id);
  //                      }

  //                      //uncomment this line to support transactions
  //                      //scope.Complete();

  //                      //raise event       
  //                      eventPublisher.PublishOrderPlaced(order);

  //                      /*if (order.PaymentStatus == PaymentStatus.Paid)
  //                      {
  //                          ProcessOrderPaid(order);
  //                      }*/
  //                      #endregion

  //                      #region WC - Delete overrided Prices

  //                      if (customer.IsAdmin())
  //                      {
  //                          try
  //                          {
  //                              var adminShippingAttribute = workContext.CurrentCustomer.GetAttribute("AdminShipping", genericAttributeService, 1);
  //                              if (adminShippingAttribute != null)
  //                              {
  //                                  genericAttributeService.DeleteAttribute(adminShippingAttribute);
  //                              }

  //                              foreach (var item in order.OrderItems)
  //                              {
  //                                  var adminPriceAttribute = workContext.CurrentCustomer.GetAttribute(string.Format("AdminProductPrice-{0}", item.ProductId), genericAttributeService, 1);
  //                                  if (adminPriceAttribute != null)
  //                                  {
  //                                      genericAttributeService.DeleteAttribute(adminPriceAttribute);
  //                                  }
  //                              }
  //                          }
  //                          catch (Exception)
  //                          {
  //                          }
  //                      }

  //                      #endregion
  //                  }
  //              }
  //              else
  //              {
  //                  foreach (var paymentError in processPaymentResult.Errors)
  //                      result.AddError(string.Format("Payment error: {0}", paymentError));
  //              }
  //          }
  //          catch (Exception exc)
  //          {
  //              logger.Error(exc.Message, exc);
  //              result.AddError(exc.Message);
  //          }

  //          #region Process errors

  //          string error = "";
  //          for (int i = 0; i < result.Errors.Count; i++)
  //          {
  //              error += string.Format("Error {0}: {1}", i + 1, result.Errors[i]);
  //              if (i != result.Errors.Count - 1)
  //                  error += ". ";
  //          }
  //          if (!String.IsNullOrEmpty(error))
  //          {
  //              //log it
  //              string logError = string.Format("Error while placing order. {0}", error);
  //              logger.Error(logError);
  //          }

  //          #endregion

  //          return result;
  //      }

  //      private void CreateRecordAboutOrder(string orderReferenceId, decimal? orderAmount, Order order)
  //      {
  //          if (orderAmount == null)
  //          {
  //              string errorMessage = "The order amount is null.";
  //              logger.Warning("CBA service CreateRecordAboutOrder() - " + errorMessage, new Exception(errorMessage), workContext.CurrentCustomer);
  //          }

  //          var amazonPaymentRecord = new AmazonPaymentAdvanced() 
  //          { 
  //              Customer = workContext.CurrentCustomer,
  //              Order = order,
  //              OrderReferenceId = orderReferenceId,
  //              OrderAmount = orderAmount ?? 0,
  //              CreatedOn = DateTime.UtcNow,
  //              UpdatedOn = DateTime.UtcNow
  //          };

  //          try
  //          {
  //              amazonPaymentAdvancedRepository.Insert(amazonPaymentRecord);
  //          }
  //          catch (Exception exc)
  //          {
  //              logger.Warning("Amazon service CreateRecordAboutOrder() Amazon. " + exc.Message, exc, workContext.CurrentCustomer);
  //          }
  //      }

  //      private void UpdateOrderReferenceStatus(string orderReferenceId, string status, string error)
  //      {
  //          var amazonPaymentRecords = amazonPaymentAdvancedRepository.Table.Where(ap => ap.OrderReferenceId == orderReferenceId).ToList();
  //          foreach (var apr in amazonPaymentRecords)
  //          {
  //              apr.OrderReferenceStatus = status;
  //              apr.LastError = error;
  //              apr.UpdatedOn = DateTime.UtcNow;
  //              amazonPaymentAdvancedRepository.Update(apr);
  //          }
  //      }

  //      private void UpdateAuthorizeStatus(string orderReferenceId, string amazonAuthorizationId, string status, string error)
  //      {
  //          var amazonPaymentRecords = amazonPaymentAdvancedRepository.Table.Where(ap => ap.OrderReferenceId == orderReferenceId).ToList();
  //          foreach (var apr in amazonPaymentRecords)
  //          {
  //              apr.AmazonAuthorizationId = amazonAuthorizationId;
  //              apr.AuthorizeStatus = status;
  //              apr.LastError = error;
  //              apr.UpdatedOn = DateTime.UtcNow;
  //              amazonPaymentAdvancedRepository.Update(apr);
  //          }
  //      }
  //      #endregion
  //  }
}
