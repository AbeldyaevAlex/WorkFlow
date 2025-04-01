

using System.Threading.Tasks;
using Asu.Core.Domain.Customization;
using WebGrease.Css.Extensions;

namespace Asu.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Asu.Core;
    using Asu.Core.Caching;
    using Asu.Core.Domain.Catalog;
    using Asu.Core.Domain.Media;
    using Asu.Core.Domain.Vehicles;
    using Asu.Services.Catalog;
    using Asu.Services.Directory;
    using Asu.Services.Localization;
    using Asu.Services.Media;
    using Asu.Services.Security;
    using Asu.Services.Seo;
    using Asu.Services.Tax;
    using Asu.Web.Models.Catalog;
    using Asu.Web.Models.Media;
    using Asu.Web.Models.Vehicles;
    using Asu.Core.Domain.Common;
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.Returns;
    using Asu.Core.Domain.Shipping;
    //using Asu.Core.Domain.Solr;
    using Asu.Core.Domain.Stores;
    using Asu.Core.Domain.Tax;
    using Asu.Core.Domain.Warranty;
    using Asu.Services.Common;
    using Asu.Services.Customization;
    using Asu.Services.Helpers;
    using Asu.Services.Orders;
    using Asu.Services.Payments;
    using Asu.Services.Shipping;
    using Asu.Web.Models.Order;
    //using SolrNet;
    using Asu.Web.Models.Returns;
    using static Asu.Web.Models.Order.OrderDetailsModel;

    using Channel = Asu.Web.Models.Order.Channel;
    using Asu.Core.Domain.Customers;
    using Asu.Core.Infrastructure;
    //using SolrNet.Schema;
    using Asu.Services.Logging;
    using System.Diagnostics;

    public static class CustomControllerExtensions
    {
        private const string PRODUCT_OVERVIEW_PICTURE_MODEL_KEY = "Wc.prod.overview.pic.model-{0}";
        private const string PRODUCT_GROUP_OVERVIEW_PICTURE_MODEL_KEY = "Wc.prod.group.overview.pic.model-{0}";
        private const string PRODUCT_OVERVIEW_PICTURE_URL_KEY = "Wc.prod.overview.pic.url-{0}-250";
        private const string MANUFACTURER_OVERVIEW_PICTURE_MODEL_KEY = "Wc.manuf.overview.pic.model-{0}";
        private const string MANUFACTURER_OVERVIEW_PICTURE_URL_KEY = "Wc.manuf.overview.pic.url-{0}-128-30";
        private const string PRODUCT_OVERVIEW_SPECATTRIBUTE_MODEL_KEY = "Wc.prod.overview.specattribute.model-{0}-{1}-{2}";

        public static IEnumerable<CustomProductOverviewModel> PrepareCustomProductOverviewModels(this Controller controller,
            IWorkContext workContext,
            IStoreContext storeContext,
            ICategoryService categoryService,
            IProductService productService,
            ISpecificationAttributeService specificationAttributeService,
            IPriceCalculationService priceCalculationService,
            IPriceFormatter priceFormatter,
            IPermissionService permissionService,
            ILocalizationService localizationService,
            ITaxService taxService,
            ICurrencyService currencyService,
            IPictureService pictureService,
            IWebHelper webHelper,
            ICacheManager cacheManager,
            CatalogSettings catalogSettings,
            MediaSettings mediaSettings,
            IEnumerable<Product> products,
            IShippingService shippingService,
            bool preparePriceModel = true, bool preparePictureModel = true,
            int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
            bool forceRedirectionAfterAddingToCart = false, bool showClubMembersPrices = false)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            var storeId = storeContext.CurrentStore.Id;
            var models = new List<CustomProductOverviewModel>();
            foreach (var product in products)
            {
                var model = new CustomProductOverviewModel
                {
                    Id = product.Id,
                    Name = product.GetLocalized(x => x.Name),
                    SeName = product.GetSeName(),
                };

                var productExtra = product.ProductExtra;

                //hidden price
                model.IsPriceHidden = productExtra != null && productExtra.IsPriceHidden;

                //price
                if (preparePriceModel && !model.IsPriceHidden)
                {
                    #region Prepare product price

                    var priceModel = new ProductOverviewModel.ProductPriceModel
                    {
                        ForceRedirectionAfterAddingToCart = forceRedirectionAfterAddingToCart
                    };

                    //add to cart button
                    priceModel.DisableBuyButton = product.DisableBuyButton;

                    //add to wishlist button
                    priceModel.DisableWishlistButton = true;

                    //calculate for the maximum quantity (in case if we have tier prices)
                    priceModel.OldPrice = null;
                    if (product.CallForPrice)
                    {
                        //call for price
                        priceModel.Price = localizationService.GetResource("Products.CallForPrice");
                    }
                    else
                    {
                        priceModel.Price = priceFormatter.FormatPrice(priceCalculationService.GetFinalPrice(product, workContext.CurrentCustomer));
                    }

                    model.ProductPrice = priceModel;

                    #endregion
                }

                //picture
                if (preparePictureModel)
                {
                    #region Prepare product picture

                    //prepare picture model
                    var defaultProductPictureCacheKey = string.Format(PRODUCT_OVERVIEW_PICTURE_MODEL_KEY, product.Id);
                    model.DefaultPictureModel = cacheManager.Get(defaultProductPictureCacheKey, () =>
                    {
                        var picture = pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
                        var pictureModel = new PictureModel
                        {
                            ImageUrl = cacheManager.Get(string.Format(PRODUCT_OVERVIEW_PICTURE_URL_KEY, product.Id), () => pictureService.GetPictureUrl(picture, 250)),
                            Title = string.Format(localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), model.Name),
                            AlternateText = string.Format(localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), model.Name)
                        };
                        return pictureModel;
                    });

                    #endregion
                }

                //reviews
                model.ReviewOverviewModel = new CustomProductReviewOverviewModel
                {
                    ProductId = product.Id,
                    RatingScore = productExtra == null ? 0 : Math.Round(productExtra.RatingScore, 2),
                    RatingCount = productExtra == null ? 0 : productExtra.RatingCount
                };

                model.StockQuantity = product.StockQuantity;
                model.Sku = product.Sku;
                model.ManufacturerPartNumber = product.ManufacturerPartNumber;

                //manufacturer model
                var productManufacturer = product.ProductManufacturers.FirstOrDefault();
                if (productManufacturer != null)
                {
                    if (productManufacturer.Manufacturer != null)
                    {
                        var manufacturer = productManufacturer.Manufacturer;
                        model.Manufacturer.Name = manufacturer.Name;
                        model.Manufacturer.SeName = manufacturer.GetSeName();

                        #region Prepare Manufacturer logo

                        //prepare picture model
                        var manufacturerPictureCacheKey = string.Format(MANUFACTURER_OVERVIEW_PICTURE_MODEL_KEY, manufacturer.Id);
                        model.Manufacturer.Logo = cacheManager.Get(manufacturerPictureCacheKey, () =>
                        {
                            var picture = pictureService.GetPictureById(manufacturer.PictureId);
                            var pictureModel = new PictureModel
                            {
                                ImageUrl = cacheManager.Get(string.Format(MANUFACTURER_OVERVIEW_PICTURE_URL_KEY, manufacturer.Id), () => pictureService.GetWidthHeightPictureUrl(picture, 128, 30)),
                                Title = string.Format(localizationService.GetResource("Media.Manufacturer.ImageLinkTitleFormat"), model.Name),
                                AlternateText = string.Format(localizationService.GetResource("Media.Manufacturer.ImageAlternateTextFormat"), model.Name)
                            };
                            return pictureModel;
                        });

                        #endregion
                    }
                }

                //Specifications
                model.SpecificationAttributeModels = controller.PrepareProductSpecificationModel(workContext, specificationAttributeService, cacheManager, product.Id);

                //free shipping
                model.IsFreeShipping = shippingService.IsFreeShipping(product, storeId);

                models.Add(model);
            }

            return models;
        }

        public static List<ShipmentBriefModel> GetShipments(this Controller controller, CrmSalesOrder order, IDateTimeHelper dateTimeHelper, IShipmentService shipmentService)
        {
            var model = new List<ShipmentBriefModel>();
            var shipments = order?.PurchaseOrders.SelectMany(m => m?.Shipments).OrderBy(m => m.CreatedOn).ToList();
            if (shipments == null)
            {
                return model;
            }

            foreach (var shipment in shipments)
            {
                var shipmentModel = new ShipmentBriefModel
                {
                    Id = shipment.Id,
                    TrackingNumber = shipment.TrackingNumber,
                    TrackPackageAvailable = true,
                    CarrierUrl = shipmentService.GetCarrierUrl(shipment.Id),
                    CarrierName = shipment.ShippingService.Name,
                    Status = shipment.Events.OrderByDescending(e => e.TimeStamp).FirstOrDefault()?.Description
                };

                if (shipment.ShippedOn.HasValue)
                {
                    shipmentModel.ShippedDate = dateTimeHelper.ConvertToUserTime(shipment.ShippedOn.Value, DateTimeKind.Utc);
                }

                if (shipment.DeliveredOn.HasValue)
                {
                    shipmentModel.DeliveryDate = dateTimeHelper.ConvertToUserTime(shipment.DeliveredOn.Value, DateTimeKind.Utc);
                }

                if (shipment.EstimatedDeliveryDate.HasValue)
                {
                    shipmentModel.EstimateDeliveryDate = dateTimeHelper.ConvertToUserTime(shipment.EstimatedDeliveryDate.Value, DateTimeKind.Utc);
                }

                model.Add(shipmentModel);
            }

            return model;
        }

        [NonAction]
        public static OrderDetailsModel PrepareOrderDetailsModel(this Controller controller, Core.Domain.Orders.Order order,
            IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService,
            IWorkContext workContext,
            OrderSettings orderSettings,
            AddressSettings addressSettings,
            IAddressAttributeFormatter addressAttributeFormatter,
            ShippingSettings shippingSettings,
            IOrderService orderService,
            IPaymentService paymentService,
            TaxSettings taxSettings,
            ICurrencyService currencyService,
            IPriceFormatter priceFormatter,
            CatalogSettings catalogSettings,
            IProductAttributeParser productAttributeParser,
            MediaSettings mediaSettings,
            IDownloadService downloadService,
            ICacheManager cacheManager,
            IPictureService pictureService,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IShipmentService shipmentService,
            IOrderProcessingService orderProcessingService,
            IReturnService returnService,
            IShippingInsuranceService shippingInsuranceService)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var model = new OrderDetailsModel
            {
                Id = order.Id,
                CreatedOn = dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc),
                IsReOrderAllowed = orderSettings.IsReOrderAllowed,
                ShippingStatus = order.ShippingStatus.GetLocalizedEnum(localizationService, workContext),
                IsCancelled = order.OrderStatus == OrderStatus.Cancelled
            };

            //shipping info
            if (order.ShippingStatus != ShippingStatus.ShippingNotRequired)
            {
                model.IsShippable = true;
                model.PickUpInStore = order.PickUpInStore;
                if (!order.PickUpInStore)
                {
                    model.ShippingAddress.PrepareModel(
                        address: order.ShippingAddress,
                        excludeProperties: false,
                        addressSettings: addressSettings,
                        addressAttributeFormatter: addressAttributeFormatter);
                }

                model.ShippingMethod = order.ShippingMethod;
            }

            var crmOrderId = orderService.GetCrmOrderIdByReference(order.Id.ToString(), (int)storeContext.CurrentStore.GetStoreChannel());
            if (crmOrderId.HasValue)
            {
                var crmOrder = orderService.GetCrmOrder(crmOrderId.Value);
                model.Shipments = GetShipments(controller, crmOrder, dateTimeHelper, shipmentService);
                /*var returns = returnService.GetReturns(crmOrderId.Value);
                var returnedQty = returns.SelectMany(m => m.ReturnItems).Sum(m => m.Quantity);
                var qty = crmOrder.ThubOrder.OrderItems.Sum(m => m.Quantity);*/
                model.OrderStatus = string.IsNullOrEmpty(crmOrder.OrderStatusName)
                     ? order.OrderStatus.GetLocalizedEnum(localizationService, workContext)
                     : crmOrder.OrderStatusName == "Swap Cancel"
                         ? "Pending"
                         : (crmOrder.OrderStatusName.Contains("Swap")
                             ? crmOrder.OrderStatusName.Replace("Swap", string.Empty).Trim()
                             : crmOrder.OrderStatusName);
                if (model.OrderStatus == "Postponed")
                {
                    model.OrderStatus = "Progress";
                }
                if (crmOrder.OrderStatus.HasValue)
                {
                    model.IsCancelled = crmOrder.OrderStatus == CrmOrderStatus.Cancelled;
                }

                var customerId = workContext.CurrentCustomer.Id;
                var ignoreRmaWindow = customerId == 1364997339 || customerId == 72907744; // estevens@autoplicity.com  calahan@autoplicity.com
#if DEBUG
                model.IsReturnRequestAllowed = true;
#else
                model.IsReturnRequestAllowed = workContext.CurrentCustomer.IsAdmin() || (orderProcessingService.IsReturnRequestAllowed(order) && crmOrder.SalesOrderImportId.HasValue) || ignoreRmaWindow;
#endif
            }

            //billing info
            model.BillingAddress.PrepareModel(
                address: order.BillingAddress,
                excludeProperties: false,
                addressSettings: addressSettings,
                addressAttributeFormatter: addressAttributeFormatter);

            //VAT number
            model.VatNumber = order.VatNumber;

            //payment method
            var paymentMethod = paymentService.LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
            model.PaymentMethod = paymentMethod != null ? paymentMethod.GetLocalizedFriendlyName(localizationService, workContext.WorkingLanguage.Id) : order.PaymentMethodSystemName;
            model.PaymentMethodStatus = order.PaymentStatus.GetLocalizedEnum(localizationService, workContext);
            model.CanRePostProcessPayment = paymentService.CanRePostProcessPayment(order);

            //purchase order number
            //TODO: we have to find a better way to inject this information because it's related to a certain plugin
            if (paymentMethod != null && paymentMethod.PluginDescriptor.SystemName.Equals("Payments.PurchaseOrder", StringComparison.InvariantCultureIgnoreCase))
            {
                model.DisplayPurchaseOrderNumber = true;
                model.PurchaseOrderNumber = order.PurchaseOrderNumber;
            }

            var orderItems = orderService.GetAllOrderItems(order.Id, null, null, null, null, null, null);

            //order subtotal
            if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax && !taxSettings.ForceTaxExclusionFromOrderSubtotal)
            {
                //including tax

                //order subtotal
                var orderSubtotalInclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.OrderSubtotalInclTax, order.CurrencyRate);
                if (shippingInsuranceService.IsShowInsurance() && shippingInsuranceService.IsInsuranceApplied(orderItems))
                {
                    orderSubtotalInclTaxInCustomerCurrency -= shippingInsuranceService.GetInsuranceAmount();
                }

                model.OrderSubtotal = priceFormatter.FormatPrice(orderSubtotalInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, true);
               
                //discount (applied to order subtotal)
                var orderSubTotalDiscountInclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.OrderSubTotalDiscountInclTax, order.CurrencyRate);
                if (orderSubTotalDiscountInclTaxInCustomerCurrency > decimal.Zero)
                    model.OrderSubTotalDiscount = priceFormatter.FormatPrice(-orderSubTotalDiscountInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, true);
            }
            else
            {
                //excluding tax

                //order subtotal
                var orderSubtotalExclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.OrderSubtotalExclTax, order.CurrencyRate);
                if (shippingInsuranceService.IsShowInsurance() && shippingInsuranceService.IsInsuranceApplied(orderItems))
                {
                    orderSubtotalExclTaxInCustomerCurrency -= shippingInsuranceService.GetInsuranceAmount();
                }

                model.OrderSubtotal = priceFormatter.FormatPrice(orderSubtotalExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, false);
                //discount (applied to order subtotal)
                var orderSubTotalDiscountExclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.OrderSubTotalDiscountExclTax, order.CurrencyRate);
                if (orderSubTotalDiscountExclTaxInCustomerCurrency > decimal.Zero)
                    model.OrderSubTotalDiscount = priceFormatter.FormatPrice(-orderSubTotalDiscountExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, false);
            }

            if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
            {
                //including tax

                //order shipping
                var orderShippingInclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.OrderShippingInclTax, order.CurrencyRate);
                model.OrderShipping = priceFormatter.FormatShippingPrice(orderShippingInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, true);
                //payment method additional fee
                var paymentMethodAdditionalFeeInclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeInclTax, order.CurrencyRate);
                if (paymentMethodAdditionalFeeInclTaxInCustomerCurrency > decimal.Zero)
                    model.PaymentMethodAdditionalFee = priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, true);
            }
            else
            {
                //excluding tax

                //order shipping
                var orderShippingExclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.OrderShippingExclTax, order.CurrencyRate);
                model.OrderShipping = priceFormatter.FormatShippingPrice(orderShippingExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, false);
                //payment method additional fee
                var paymentMethodAdditionalFeeExclTaxInCustomerCurrency = currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeExclTax, order.CurrencyRate);
                if (paymentMethodAdditionalFeeExclTaxInCustomerCurrency > decimal.Zero)
                    model.PaymentMethodAdditionalFee = priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, false);
            }

            //tax
            var displayTax = true;
            var displayTaxRates = true;
            if (taxSettings.HideTaxInOrderSummary && order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
            {
                displayTax = false;
                displayTaxRates = false;
            }
            else
            {
                if (order.OrderTax == 0 && taxSettings.HideZeroTax)
                {
                    displayTax = false;
                    displayTaxRates = false;
                }
                else
                {
                    displayTaxRates = taxSettings.DisplayTaxRates && order.TaxRatesDictionary.Count > 0;
                    displayTax = !displayTaxRates;

                    var orderTaxInCustomerCurrency = currencyService.ConvertCurrency(order.OrderTax, order.CurrencyRate);
                    //TODO pass languageId to this.priceFormatter.FormatPrice
                    model.Tax = priceFormatter.FormatPrice(orderTaxInCustomerCurrency, true, order.CustomerCurrencyCode, false, workContext.WorkingLanguage);

                    foreach (var tr in order.TaxRatesDictionary)
                    {
                        model.TaxRates.Add(new Models.Order.OrderDetailsModel.TaxRate
                        {
                            Rate = priceFormatter.FormatTaxRate(tr.Key),
                            //TODO pass languageId to this.priceFormatter.FormatPrice
                            Value = priceFormatter.FormatPrice(currencyService.ConvertCurrency(tr.Value, order.CurrencyRate), true, order.CustomerCurrencyCode, false, workContext.WorkingLanguage),
                        });
                    }
                }
            }
            model.DisplayTaxRates = displayTaxRates;
            model.DisplayTax = displayTax;
            model.DisplayTaxShippingInfo = catalogSettings.DisplayTaxShippingInfoOrderDetailsPage;
            model.PricesIncludeTax = order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax;

            //discount (applied to order total)
            var orderDiscountInCustomerCurrency = currencyService.ConvertCurrency(order.OrderDiscount, order.CurrencyRate);
            if (orderDiscountInCustomerCurrency > decimal.Zero)
                model.OrderTotalDiscount = priceFormatter.FormatPrice(-orderDiscountInCustomerCurrency, true, order.CustomerCurrencyCode, false, workContext.WorkingLanguage);


            //gift cards
            foreach (var gcuh in order.GiftCardUsageHistory)
            {
                model.GiftCards.Add(new Models.Order.OrderDetailsModel.GiftCard
                {
                    CouponCode = gcuh.GiftCard.GiftCardCouponCode,
                    Amount = priceFormatter.FormatPrice(-(currencyService.ConvertCurrency(gcuh.UsedValue, order.CurrencyRate)), true, order.CustomerCurrencyCode, false, workContext.WorkingLanguage),
                });
            }

            //reward points           
            if (order.RedeemedRewardPointsEntry != null)
            {
                model.RedeemedRewardPoints = -order.RedeemedRewardPointsEntry.Points;
                model.RedeemedRewardPointsAmount = priceFormatter.FormatPrice(-(currencyService.ConvertCurrency(order.RedeemedRewardPointsEntry.UsedAmount, order.CurrencyRate)), true, order.CustomerCurrencyCode, false, workContext.WorkingLanguage);
            }

            //total
            var orderTotalInCustomerCurrency = currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
            model.OrderTotal = priceFormatter.FormatPrice(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, workContext.WorkingLanguage);

            //checkout attributes
            model.CheckoutAttributeInfo = order.CheckoutAttributeDescription;

            //order notes
            /*foreach (var orderNote in order.OrderNotes
                .Where(on => on.DisplayToCustomer)
                .OrderByDescending(on => on.CreatedOnUtc)
                .ToList())
            {
                model.OrderNotes.Add(new Models.Order.OrderDetailsModel.OrderNote
                {
                    Id = orderNote.Id,
                    HasDownload = orderNote.DownloadId > 0,
                    Note = orderNote.FormatOrderNoteText(),
                    CreatedOn = this.dateTimeHelper.ConvertToUserTime(orderNote.CreatedOnUtc, DateTimeKind.Utc)
                });
            }*/

            //purchased products
            model.ShowSku = catalogSettings.ShowProductSku;
            foreach (var orderItem in orderItems)
            {
                var orderItemModel = new OrderDetailsModel.OrderItemModel
                {
                    Id = orderItem.Id,
                    OrderItemGuid = orderItem.OrderItemGuid,
                    Sku = orderItem.Product.FormatSku(orderItem.AttributesXml, productAttributeParser),
                    ProductId = orderItem.Product.Id,
                    ProductName = orderItem.Product.GetLocalized(x => x.Name),
                    ProductSeName = orderItem.Product.GetSeName(),
                    Quantity = orderItem.Quantity,
                    IsInsurance = shippingInsuranceService.IsProductInsurance(orderItem.Product),
                    AttributeInfo = orderItem.AttributeDescription,
                    Picture = PrepareProductPictureModel(controller, orderItem.Product.Id, mediaSettings.CartThumbPictureSize, true, workContext, cacheManager, pictureService, storeContext, webHelper),
                };
                model.Items.Add(orderItemModel);

                //unit price, subtotal
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    //including tax
                    var unitPriceInclTaxInCustomerCurrency = currencyService.ConvertCurrency(orderItem.UnitPriceInclTax, order.CurrencyRate);
                    orderItemModel.UnitPrice = priceFormatter.FormatPrice(unitPriceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, true);

                    var priceInclTaxInCustomerCurrency = currencyService.ConvertCurrency(orderItem.PriceInclTax, order.CurrencyRate);
                    orderItemModel.SubTotal = priceFormatter.FormatPrice(priceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, true);
                }
                else
                {
                    //excluding tax
                    var unitPriceExclTaxInCustomerCurrency = currencyService.ConvertCurrency(orderItem.UnitPriceExclTax, order.CurrencyRate);
                    orderItemModel.UnitPrice = priceFormatter.FormatPrice(unitPriceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, false);

                    var priceExclTaxInCustomerCurrency = currencyService.ConvertCurrency(orderItem.PriceExclTax, order.CurrencyRate);
                    orderItemModel.SubTotal = priceFormatter.FormatPrice(priceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, workContext.WorkingLanguage, false);
                }

                //downloadable products
                if (downloadService.IsDownloadAllowed(orderItem))
                    orderItemModel.DownloadId = orderItem.Product.DownloadId;
                if (downloadService.IsLicenseDownloadAllowed(orderItem))
                    orderItemModel.LicenseId = orderItem.LicenseDownloadId.HasValue ? orderItem.LicenseDownloadId.Value : 0;
            }

            return model;
        }

        public static List<OrderDetailsModel.OrderNote> GetOrderNotes(this Controller controller, Core.Domain.Orders.Order order, IDateTimeHelper dateTimeHelper)
        {
            var orderNotes = order.OrderNotes.Where(on => on.DisplayToCustomer).OrderByDescending(on => on.CreatedOnUtc).ToList();
            var model = new List<OrderDetailsModel.OrderNote>();
            foreach (var orderNote in orderNotes)
            {
                model.Add(new OrderDetailsModel.OrderNote
                {
                    Id = orderNote.Id,
                    HasDownload = orderNote.DownloadId > 0,
                    Note = orderNote.FormatOrderNoteText(),
                    CreatedOn = dateTimeHelper.ConvertToUserTime(orderNote.CreatedOnUtc, DateTimeKind.Utc)
                });
            }

            return model;
        }

        //public static IEnumerable<CustomProductOverviewModel> PrepareCustomProductOverviewModels(
        //    this Controller controller,
        //    //SolrQueryResults<SolrProduct> products,
        //    IPagedList<Manufacturer> manufacturers,
        //    IPriceFormatter priceFormatter,
        //    ILocalizationService localizationService,
        //    IPictureService pictureService,
        //    IProductGroupService productGroupService,
        //    ICacheManager cacheManager,
        //    IWebHelper webHelper,
        //    IStoreContext storeContext,
        //    IProductService productService,
        //    IList<SpecificationAttributeOption> specificationAttributeOptions,
        //    ISpecificationAttributeService specificationAttributeService,
        //    IWorkContext workContext,
        //    Func<IWorkContext, ISpecificationAttributeService, ICacheManager, int, IList<ProductSpecificationModel>> getSpecifications,
        //    bool preparePriceModel = true,
        //    bool preparePictureModel = true,
        //    bool forceRedirectionAfterAddingToCart = false, 
        //    bool showClubMembersPrices = false)
        //{
        //    if (products == null)
        //        throw new ArgumentNullException("products");

        //    var models = new List<CustomProductOverviewModel>();

        //    if (products.Any())
        //    {
        //        var storeId = storeContext.CurrentStore.Id;

        //        var brandIds = products.Select(x => x.BrandId).Distinct().ToArray();

        //        var manufacturerEntities = manufacturers.Join(brandIds, a => a.Id, b => b, (a, b) => a);

        //        var pictureIds = manufacturerEntities.Select(x => x.PictureId).Distinct().ToArray();

        //        var pictureEntities = pictureService.GetPicturesByIds(pictureIds).ToList();
        //        var isClubMember = workContext.CurrentCustomer.IsClubMember();
        //        var prods = productService.GetProductsByIds(products.Select(p => p.Id).ToArray());
        //        foreach (var product in products)
        //        {
        //            var price = product.GetPrice(storeId, isClubMember);
        //            var clubmemberprice = product.GetClubMemberPrice(storeId, isClubMember);
        //            //var GroupClubPrice = productService.GetProductGroupClubPriceById(product.Id); // TODO: NOTE! 11/24/21
        //            var isShippingFromManufacturer = product.ShipsFromManufacturer;
        //            if (storeId == (int)NopStore.Thmotorsports)
        //            {
        //                if (product.IsManageInventoryMethodOverrided.HasValue && !product.ShipsFromManufacturer)
        //                {
        //                    isShippingFromManufacturer = true;
        //                }
        //            }

        //            var isThirdPartyApiGroup = product.IsGroup.HasValue && product.IsGroup.Value && (product.BrandId == 147 || product.BrandId == 12877);
        //            var model = new CustomProductOverviewModel
        //            {
        //                Id = isThirdPartyApiGroup ? product.Id - 200000000 : product.Id,
        //                Name = product.Name,
        //                Sku = product.Sku,
        //                ManufacturerPartNumber = product.Mpn,
        //                SeName = product.SeName,
        //                BrandSlug = product.BrandSlug,
        //                IsPriceHidden = product.IsPriceHidden,
        //                IsFreeShipping = product.GetFreeShippingValue(storeId),
        //                StockQuantity = product.Qty,
        //                IsShippingFromManufacturer = isShippingFromManufacturer, // isGroup ? product.ShipsFromManufacturer : product.ShipsFromManufacturer || productService.GetInventoryManageMethod(productEntity.Id, (ManageInventoryMethod)productEntity.ManageInventoryMethodId, storeContext.CurrentStore.Id) == ManageInventoryMethod.DontManageStock,
        //                Price = price,
        //                MinPrice = product.MinPrice,
        //                MaxPrice = product.MaxPrice,
        //                IsClubMember = isClubMember ? isClubMember : false,
        //                ClubMemberPrice = clubmemberprice,
        //                //ClubMemberMinPrice = GroupClubPrice != null && GroupClubPrice.ClubMemberMinPrice != (decimal)9999.99m ? GroupClubPrice.ClubMemberMinPrice : 0m, // TODO: NOTE! 11/24/21
        //                //ClubMemberMaxPrice = GroupClubPrice != null && GroupClubPrice.ClubMemberMaxPrice != (decimal)9999.99m ? GroupClubPrice.ClubMemberMaxPrice : 0m, // TODO: NOTE! 11/24/21
        //                ReviewOverviewModel = new CustomProductReviewOverviewModel
        //                {
        //                    ProductId = product.Id,
        //                    RatingScore = product.RatingScore.HasValue
        //                        ? (double)Math.Round(product.RatingScore.Value, 2)
        //                        : 0,
        //                    RatingCount = product.RatingCount.HasValue ? product.RatingCount.Value : 0
        //                },
        //                Score = product.Score,
        //                SearchRankExplanation = product.SearchRankExplanation?.ToSearchRankExplanationHtml(),
        //                ParsedQueryString = product.ParsedQueryString,
        //                IsGroup = product.IsGroup.HasValue ? product.IsGroup.Value : false,
        //                IsThirdPartyApiGroup = isThirdPartyApiGroup
        //            };

        //            var manufacturer = manufacturerEntities.FirstOrDefault(x => x.Id == product.BrandId);
        //            if (manufacturer != null)
        //            {
        //                model.Manufacturer.Id = manufacturer.Id;
        //                model.Manufacturer.Name = manufacturer.Name;
        //                model.Manufacturer.SeName = manufacturer.GetSeName();
        //            }

        //            if (!model.IsPriceHidden)
        //            {
        //                #region Prepare product price

        //                var priceModel = new ProductOverviewModel.ProductPriceModel
        //                {
        //                    ForceRedirectionAfterAddingToCart = forceRedirectionAfterAddingToCart,
        //                };

        //                //add to cart button
        //                priceModel.DisableBuyButton = product.DisableBuyButton;

        //                //add to wishlist button
        //                priceModel.DisableWishlistButton = true;

        //                //calculate for the maximum quantity (in case if we have tier prices)
        //                priceModel.OldPrice = null;
        //                if (product.CallForPrice)
        //                {
        //                    //call for price
        //                    priceModel.Price = localizationService.GetResource("Products.CallForPrice");
        //                }
        //                else
        //                {
        //                    priceModel.Price = priceFormatter.FormatPrice(price);
        //                }
                        
        //                priceModel.ClubMemberPrice =  priceFormatter.FormatPrice(clubmemberprice);

        //                priceModel.MinPrice = model.MinPrice.HasValue ? priceFormatter.FormatPrice(model.MinPrice.Value) : priceFormatter.FormatPrice(decimal.Zero);
        //                priceModel.MaxPrice = model.MaxPrice.HasValue ? priceFormatter.FormatPrice(model.MaxPrice.Value) : priceFormatter.FormatPrice(decimal.Zero);

        //                //priceModel.ClubMemberMinPrice = model.ClubMemberMinPrice.HasValue ? priceFormatter.FormatPrice(model.ClubMemberMinPrice.Value) : priceFormatter.FormatPrice(decimal.Zero); // TODO: NOTE! 11/24/21
        //                //priceModel.ClubMemberMaxPrice = model.ClubMemberMaxPrice.HasValue ? priceFormatter.FormatPrice(model.ClubMemberMaxPrice.Value) : priceFormatter.FormatPrice(decimal.Zero); // TODO: NOTE! 11/24/21

        //                model.ProductPrice = priceModel;

        //                #endregion
        //            }

        //            #region Prepare product picture

        //            if (!isThirdPartyApiGroup)
        //            {
        //                var defaultProductPictureCacheKey = string.Format(PRODUCT_OVERVIEW_PICTURE_MODEL_KEY, product.Id);
        //                model.DefaultPictureModel = cacheManager.Get(defaultProductPictureCacheKey, () =>
        //                {
        //                    var storeImagesUrl = webHelper.GetStoreImagesLocation();
        //                    var folderNumber = product.PictureId.HasValue ? product.PictureId.Value / 10000 : -1;
        //                    var thumbsDirectoryPath = string.Format("{0}content/images/thumbs/{1}", storeImagesUrl,
        //                        folderNumber == -1 ? string.Empty : folderNumber.ToString());

        //                    string pictureUrl;
        //                    if (product.PictureId.HasValue)
        //                    {
        //                        pictureUrl = string.Format("{0}/{1}_{2}.{3}", thumbsDirectoryPath,
        //                            product.PictureId.Value.ToString("00000000"), 250,
        //                            GetFileExtensionFromMimeType(product.MimeType));
        //                    }
        //                    else
        //                    {
        //                        pictureUrl = $"{storeImagesUrl}ImageLoader/{product.Id}";
        //                        //pictureUrl = string.Format("{0}/{1}", thumbsDirectoryPath, "default-image_233_175.gif");
        //                    }

        //                    var pictureModel = new PictureModel
        //                    {
        //                        ImageUrl = pictureUrl,
        //                        Title = string.Format(localizationService.GetResource("Media.Product.ImageLinkTitleFormat"),
        //                            model.Name),
        //                        AlternateText =
        //                            string.Format(localizationService.GetResource("Media.Product.ImageAlternateTextFormat"),
        //                                model.Name),
        //                        PictureId = product.PictureId ?? -1
        //                    };

        //                    return pictureModel;
        //                });

        //                model.IsImageLoader = model.DefaultPictureModel != null &&
        //                                      !string.IsNullOrEmpty(model.DefaultPictureModel.ImageUrl) &&
        //                                      model.DefaultPictureModel.ImageUrl.Contains("/ImageLoader/");
        //            }
        //            else
        //            {
        //                var defaultProductGroupPictureCacheKey = string.Format(PRODUCT_GROUP_OVERVIEW_PICTURE_MODEL_KEY, product.Id);
        //                model.DefaultPictureModel = cacheManager.Get(defaultProductGroupPictureCacheKey, () =>
        //                {
        //                    var pictureModel = new PictureModel
        //                    {
        //                        ImageUrl = productGroupService.GetDefaultPictureUrl(product.Id - 200000000, 233, 175),
        //                        Title = string.Format(localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), model.Name),
        //                        AlternateText = string.Format(localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), model.Name),
        //                        PictureId = product.PictureId ?? -1
        //                    };

        //                    return pictureModel;
        //                });
        //            }

        //            #endregion

        //            //prepare picture model
        //            var manufacturerPictureCacheKey =
        //                string.Format(MANUFACTURER_OVERVIEW_PICTURE_MODEL_KEY, manufacturer.Id);
        //            model.Manufacturer.Logo = cacheManager.Get(manufacturerPictureCacheKey, () =>
        //            {
        //                var picture = pictureEntities.FirstOrDefault(x => x.Id == manufacturer.PictureId);

        //                var pictureModel = new PictureModel
        //                {
        //                    ImageUrl = cacheManager.Get(
        //                        string.Format(MANUFACTURER_OVERVIEW_PICTURE_URL_KEY, manufacturer.Id),
        //                        () => pictureService.GetWidthHeightPictureUrl(picture, 128, 30)),
        //                    Title = string.Format(
        //                        localizationService.GetResource("Media.Manufacturer.ImageLinkTitleFormat"), model.Name),
        //                    AlternateText =
        //                        string.Format(
        //                            localizationService.GetResource("Media.Manufacturer.ImageAlternateTextFormat"),
        //                            model.Name)
        //                };

        //                return pictureModel;
        //            });
        //            //specification attributes
        //            /*var hasSpecificationValues = specifications.Any(spec =>
        //                (int) spec.GetValue(product) != 0 &&
        //                Attribute.GetCustomAttribute(spec, typeof(SolrSpecificationAttribute)) != null);

        //            if (hasSpecificationValues)
        //            {
        //                string cacheKey = string.Format(PRODUCT_OVERVIEW_SPECATTRIBUTE_MODEL_KEY, product.Id, storeId,
        //                    string.Join(",", specifications.Select(x => x.Name)));

        //                model.SpecificationAttributeModels = cacheManager.Get(cacheKey, () =>
        //                {
        //                    var spaModel = new List<ProductSpecificationModel>();

        //                    var specificationsModel = getSpecifications(workContext,
        //                        specificationAttributeService,
        //                        cacheManager,
        //                        product.Id);

        //                    Parallel.ForEach(specifications, (spec) =>
        //                    {
        //                        var specAttrValue = (int)spec.GetValue(product);
        //                        if (specAttrValue == 0)
        //                        {
        //                            return;
        //                        }

        //                        var typeAttributeInstance =
        //                            Attribute.GetCustomAttribute(spec, typeof(SolrSpecificationAttribute));
        //                        if (typeAttributeInstance == null)
        //                        {
        //                            return;
        //                        }

        //                        var specAttrName = typeAttributeInstance.GetType().GetProperties()
        //                            .SingleOrDefault(p => p.Name == "DisplayName")
        //                            ?.GetValue(typeAttributeInstance, null)?.ToString();
        //                        var specAttrId = (int?)typeAttributeInstance.GetType().GetProperties()
        //                            .SingleOrDefault(p => p.Name == "Id")?.GetValue(typeAttributeInstance, null);
        //                        //var attribute = specificationAttributeOptions.SingleOrDefault(m => m.Id == specAttrValue); TODO: investigate why attribute isn't used

        //                        if (!string.IsNullOrEmpty(specAttrName) && specAttrId.HasValue)
        //                        {
        //                            var specModel = specificationsModel.FirstOrDefault(sm =>
        //                                sm.SpecificationAttributeId == specAttrId.Value);
        //                            if (specModel != null)
        //                            {
        //                                spaModel.Add(specModel);
        //                            }
        //                        }
        //                    });

        //                    return spaModel;
        //                });
        //            }*/

        //            models.Add(model);
        //        }
        //    }

        //    //models = models.OrderByDescending(r => r.Score).ToList();
        //    return models;
        //}

        public static PictureModel PrepareProductPictureModel(this Controller controller,
            int productId,
            int pictureSize,
            bool showDefaultPicture,
            IWorkContext workContext,
            ICacheManager cacheManager,
            IPictureService pictureService,
            IStoreContext storeContext,
            IWebHelper webHelper)
        {
            var pictureCacheKey = $"Nop.product.picture-{productId}-{pictureSize}-{true}-{workContext.WorkingLanguage.Id}-{webHelper.IsCurrentConnectionSecured()}-{storeContext.CurrentStore.Id}";
            var model = cacheManager.Get(pictureCacheKey, 3, () =>
            {
                var picture = pictureService.GetPicturesByProductId(productId, 1).FirstOrDefault();
                return new PictureModel
                {
                    ImageUrl = pictureService.GetPictureUrl(picture, pictureSize, showDefaultPicture),
                };
            });

            return model;
        }

        public static List<ReturnItemModel> GetPureCancels(this Controller controller,
            CrmSalesOrder order,
            MediaSettings mediaSettings,
            IWorkContext workContext,
            IWebHelper webHelper,
            IStoreContext storeContext,
            IPictureService pictureService,
            ICacheManager cacheManager,
            IList<WarrantyProductAssociation> warrantyProductAssociations,
            out List<SalesCancel> pureCancels, out decimal pureCancelsCreditAmount)
        {
            pureCancels = order.SalesCancels.Where(i => i.PureCancel != null).ToList();
            pureCancels.Where(i => i.Items.Any(a => a.OrderLine.ProductId == null)).ToList().ForEach(i => i.Items.ToList().ForEach(a => a.OrderLine.ProductId = 0));

            var cancels = pureCancels.SelectMany(i => i.Items.Select(k => PrepareReturnItemModel(controller, k,
                i.CreatedOn,
                mediaSettings,
                workContext,
                webHelper,
                cacheManager,
                storeContext,
                pictureService,
                warrantyProductAssociations))).ToList();

            pureCancelsCreditAmount = pureCancels.Where(m => m.CancelCredits != null).SelectMany(m => m.CancelCredits).Where(cr => cr.Credit.Charges != null).SelectMany(cr => cr.Credit.Charges).Sum(ch => ch.Amount);

            return cancels;
        }

        public static List<ReturnItemModel> GetPureCancels(this Controller controller,
            CrmSalesOrder order,
            MediaSettings mediaSettings,
            IWorkContext workContext,
            IWebHelper webHelper,
            IStoreContext storeContext,
            IPictureService pictureService,
            ICacheManager cacheManager,
            IList<WarrantyProductAssociation> warrantyProductAssociations, out decimal pureCancelsCreditAmount)
        {
            var pureCancels = order.SalesCancels.Where(i => i.PureCancel != null).ToList();
            pureCancels.Where(i => i.Items.Any(a => a.OrderLine.ProductId == null)).ToList().ForEach(i => i.Items.ToList().ForEach(a => a.OrderLine.ProductId = 0));

            var cancels = pureCancels.SelectMany(i => i.Items.Select(k => PrepareReturnItemModel(controller, k, i.CreatedOn,
            mediaSettings, workContext, webHelper, cacheManager, storeContext, pictureService, warrantyProductAssociations))).ToList();

            pureCancelsCreditAmount = pureCancels.Where(m => m.CancelCredits != null).SelectMany(m => m.CancelCredits).Where(cr => cr.Credit.Charges != null).SelectMany(cr => cr.Credit.Charges).Sum(ch => ch.Amount);

            return cancels;
        }

        public static List<ReturnRequestModel> GetExistingReturnRequests(this Controller controller, CrmSalesOrder order,
            IReturnService returnService,
            MediaSettings mediaSettings,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IPictureService pictureService,
            IWorkContext workContext,
            ICacheManager cacheManager,
            IShipmentService shipmentService)
        {
            var existingReturnRequests = returnService.GetReturnRequests(order.Id);
            var pendingReturns = existingReturnRequests
                .Where(i => i.Import == null)
                .Select(i => new ReturnRequestModel
            {
                Id = i.Id,
                Number = null,
                CreatedOn = i.CreatedOn.ToLocalTime(),
                Events = new List<ReturnEventModel> { GetPendingEvent(i) }
            });

            var processingReturns = existingReturnRequests
                .Where(i => i.Import != null)
                .Select(i => new ReturnRequestModel
            {
                Id = i.Id,
                Number = i.Import.ReturnId.ToString(),
                CreatedOn = i.CreatedOn.ToLocalTime(),
                Events = GetReturnEvents(i, shipmentService)
            });

            return pendingReturns.Union(processingReturns).OrderByDescending(i => i.CreatedOn).ToList();
        }

        //private static decimal GetPrice(this SolrProduct product, int storeId, bool isClubMember)
        //{
        //    var price = 0M;
        //    switch (storeId)
        //    {
        //        case 1:
        //            //price = isClubMember && product.ClubMemberPrice1.HasValue 
        //            //    ? product.ClubMemberPrice1.Value 
        //            //    : product.Price1.HasValue ? product.Price1.Value : product.Price;
        //            price = product.Price1.HasValue ? product.Price1.Value : product.Price;
        //            break;
        //        case 2:
        //            //price = isClubMember && product.ClubMemberPrice2.HasValue
        //            //    ? product.ClubMemberPrice2.Value
        //            //    : product.Price2.HasValue ? product.Price2.Value : product.Price;
        //            price = product.Price2.HasValue ? product.Price2.Value : product.Price;
        //            break;
        //        case 3:
        //            //price = isClubMember && product.ClubMemberPrice3.HasValue
        //            //   ? product.ClubMemberPrice3.Value
        //            //   : product.Price3.HasValue ? product.Price3.Value : product.Price;
        //            price = product.Price3.HasValue ? product.Price3.Value : product.Price;
        //            break;
        //        case 4:
        //            //price = isClubMember && product.ClubMemberPrice4.HasValue
        //            //   ? product.ClubMemberPrice4.Value
        //            //   : product.Price4.HasValue ? product.Price4.Value : product.Price;
        //            price = product.Price4.HasValue ? product.Price4.Value : product.Price;
        //            break;
        //        default:
        //            price = product.Price;
        //            break;
        //    }

        //    return Math.Round(price, 2);
        //}

        //private static decimal GetClubMemberPrice(this SolrProduct product, int storeId, bool isClubMember)
        //{
        //    var clubmemberprice = 0M;
        //    switch (storeId)
        //    {
        //        case 1:
        //            clubmemberprice = isClubMember && product.ClubMemberPrice1.HasValue
        //                ? product.ClubMemberPrice1.Value
        //                : product.Price;
        //            break;
        //        case 2:
        //            clubmemberprice = isClubMember && product.ClubMemberPrice2.HasValue
        //                ? product.ClubMemberPrice2.Value
        //                : product.Price;
        //            break;
        //        case 3:
        //            clubmemberprice = isClubMember && product.ClubMemberPrice3.HasValue
        //               ? product.ClubMemberPrice3.Value
        //               : product.Price;
        //            break;
        //        case 4:
        //            clubmemberprice = isClubMember && product.ClubMemberPrice4.HasValue
        //               ? product.ClubMemberPrice4.Value
        //               : product.Price;
        //            break;
        //        default:
        //            clubmemberprice = product.Price;
        //            break;
        //    }

        //    return Math.Round(clubmemberprice, 2);
        //}

        //private static bool GetFreeShippingValue(this SolrProduct product, int storeId)
        //{
        //    bool isFreeShipping;
        //    switch (storeId)
        //    {
        //        case 1:
        //            isFreeShipping = product.IsFreeShipping1 ?? product.IsFreeShipping;
        //            break;
        //        case 2:
        //            isFreeShipping = product.IsFreeShipping2 ?? product.IsFreeShipping;
        //            break;
        //        case 3:
        //            isFreeShipping = product.IsFreeShipping3 ?? product.IsFreeShipping;
        //            break;
        //        case 4:
        //            isFreeShipping = product.IsFreeShipping4 ?? product.IsFreeShipping;
        //            break;
        //        default:
        //            isFreeShipping = product.IsFreeShipping;
        //            break;
        //    }

        //    return isFreeShipping;
        //}

        private static ReturnEventModel GetPendingEvent(ReturnRequest request)
        {
            return new ReturnEventModel
            {
                Items = request.Items.Select(i => new ReturnItemModel
                {
                    OrderItem = i.OrderLine.ToModel(),
                    Quantity = i.Quantity
                }).ToList(),
                Type = ReturnEventType.Pending
            };
        }

        private static List<ReturnEventModel> GetReturnEvents(ReturnRequest request, IShipmentService shipmentService)
        {
            var events = new List<ReturnEventModel>();
            var returnRequest = request?.Import?.Return;
            if (returnRequest == null)
            {
                return events;
            }

            var returnCreation = new ReturnEventModel
            {
                Date = request.CreatedOn,
                Type = ReturnEventType.NewRequest,
                Items = returnRequest.ReturnItems.Select(i => new ReturnItemModel
                {
                    OrderItem = i.OrderLine.ToModel(),
                    Quantity = i.Quantity
                }).ToList()
            };

            events.Add(returnCreation);

            var returnCancels = returnRequest.ReturnCancels.Select(rc => new ReturnEventModel
            {
                Id = rc.CancelId,
                Date = rc.Cancel.CreatedOn,
                Type = rc.Cancel.CancelCredits.Any() ? ReturnEventType.CancelCredit : ReturnEventType.Cancel,
                Items = rc.Cancel.Items.Where(i => i.Quantity > 0).GroupBy(i => i.OrderLineId).Select(i => new ReturnItemModel
                {
                    OrderItem = i.Select(oi => oi.OrderLine.ToModel()).Single(),
                    Quantity = i.Select(oi => oi.Quantity).Single()
                }).ToList(),
                Attributes = GetCancelAttributes(rc.Cancel)
            }).ToList();

            events.AddRange(returnCancels);

            var pureReturnRefusals = returnRequest.ReturnRefusals.Where(rf => rf.PureReturnRefusal != null).Select(rf => new ReturnEventModel
            {
                Id = rf.PureReturnRefusal.RefusalId,
                Date = rf.CreatedOn,
                Type = ReturnEventType.Refusal,
                Attributes = new List<ReturnEventAttributeModel>
                {
                    new ReturnEventAttributeModel
                    {
                        Name = "Reason",
                        Value = rf.PureReturnRefusal.ReturnReason.Name,
                        Type = ReturnEventAttributeType.Reason
                    }
                },
                Items = rf.Items.Where(i => i.Quantity > 0).GroupBy(i => i.OrderLineId).Select(i => new ReturnItemModel
                {
                    OrderItem = i.Select(ri => ri.OrderLine.ToModel()).Single(),
                    Quantity = i.Select(ri => ri.Quantity).Single()
                }).ToList()
            }).ToList();

            events.AddRange(pureReturnRefusals);

            var rmas = returnRequest.Rmas.Select(rma => new ReturnEventModel
            {
                Id = rma.Id,
                Number = rma.Number,
                Date = rma.CreatedOn,
                Type = ReturnEventType.Rma,
                Items = rma.RmaItems.Where(i => i.Quantity > 0).GroupBy(i => i.OrderLineId).Select(i => new ReturnItemModel
                {
                    OrderItem = i.Select(ri => ri.OrderLine.ToModel()).Single(),
                    Quantity = i.Select(ri => ri.Quantity).Single()
                }).ToList()
            }).ToList();

            events.AddRange(rmas);
            foreach (var rma in returnRequest.Rmas)
            {
                events.AddRange(GetRmaEvents(rma, shipmentService));
            }

            var totals = returnRequest.ReturnItems.GroupBy(ri => ri.OrderLineId).Select(ri => new { LineId = ri.Key, Entity = ri.Single().OrderLine, TotalQty = ri.Sum(i => i.Quantity) });
            var cancels = returnCancels.SelectMany(rc => rc.Items).GroupBy(oi => oi.OrderLineId).Select(oi => new { LineId = oi.Key, CancelsQty = oi.Sum(i => i.Quantity) });
            var refusals = pureReturnRefusals.SelectMany(rf => rf.Items).GroupBy(oi => oi.OrderLineId).Select(oi => new { LineId = oi.Key, RefusalsQty = oi.Sum(i => i.Quantity) });
            var rmass = rmas.SelectMany(rf => rf.Items).GroupBy(oi => oi.OrderLineId).Select(oi => new { LineId = oi.Key, RmaQty = oi.Sum(i => i.Quantity) });

            var processing = (from a in totals
                              join b in cancels on a.LineId equals b.LineId
                              join c in refusals on b.LineId equals c.LineId
                              join d in rmass on c.LineId equals d.LineId
                              select new { a.LineId, a.Entity, Qty = a.TotalQty - b.CancelsQty - c.RefusalsQty - d.RmaQty }).Where(i => i.Qty > 0).ToList();

            events = events.OrderBy(e => e.Date.Value).ToList();
            events.Insert(0, new ReturnEventModel
            {
                Items = processing.Select(p => new ReturnItemModel
                {
                    OrderItem = p.Entity.ToModel(),
                    Quantity = p.Qty
                }).ToList(),
                Type = ReturnEventType.Processing
            });


            return events;
        }

        private static List<ReturnEventAttributeModel> GetCancelAttributes(SalesCancel cancel)
        {
            var order = cancel.CrmOrder;
            var subtotal = cancel.Items.Sum(i => i.Quantity * i.OrderLine.UnitPrice);
            var orderSubtotal = order.Lines.GroupJoin(cancel.Items, a => a.Id, b => b.OrderLineId, (a, b) => a.Quantity * a.UnitPrice).Sum();
            var feesTotal = cancel.CancelFees.Where(cf => cf.FeeCharges != null).SelectMany(cf => cf.FeeCharges).Sum(ch => ch.Amount);
            var attributes = cancel.CancelCredits.Where(cr => cr.Credit.Charges != null).SelectMany(cr => cr.Credit.Charges).GroupBy(m => m.Type).Select(g => new ReturnEventAttributeModel
            {
                Name = g.Key.Name,
                Type = ReturnEventAttributeType.Credit,
                Value = g.Sum(ch => ch.Amount).ToString("N")
            }).ToList();

            var creditTotal = cancel.CancelCredits.Where(cr => cr.Credit.Charges != null).SelectMany(cr => cr.Credit.Charges).Sum(ch => ch.Amount);
            var itemsOverallAmount = subtotal + (order.GetOrderChargeAmount(SalesPaymentChargeType.Tax) + order.GetOrderChargeAmount(SalesPaymentChargeType.Shipping) - order.GetOrderChargeAmount(SalesPaymentChargeType.Discount)) * (subtotal / orderSubtotal);
            attributes.AddRange(cancel.CancelFees.Select(cf => new ReturnEventAttributeModel
            {
                Name = $"{cf.Name} fee",
                Type = ReturnEventAttributeType.Fee,
                Value = cf.FeeCharges.Sum(ch => ch.Amount).ToString("N")
            }).ToList());

            var returnedPreviously = itemsOverallAmount - creditTotal - feesTotal;
            if (returnedPreviously > decimal.Zero)
            {
                attributes.Add(new ReturnEventAttributeModel
                {
                    Name = "Returned previously",
                    Type = ReturnEventAttributeType.Other,
                    Value = returnedPreviously.ToString("N")
                });
            }

            attributes.Add(new ReturnEventAttributeModel
            {
                Name = "Total credited",
                Type = ReturnEventAttributeType.Total,
                Value = creditTotal.ToString("N")
            });

            return attributes;
        }

        private static List<ReturnEventModel> GetRmaEvents(Rma rma, IShipmentService shipmentService)
        {
            var events = new List<ReturnEventModel>();
            if (rma == null)
            {
                return events;
            }

            var rmaCancels = rma.RmaReturnCancels.Select(rc => new ReturnEventModel
            {
                Id = rc.CancelId,
                Date = rc.Cancel.CreatedOn,
                Type = rc.Cancel.CancelCredits.Any() ? ReturnEventType.CancelCredit : ReturnEventType.Cancel,
                Items = rc.Cancel.Items.Where(i => i.Quantity > 0).GroupBy(i => i.OrderLineId).Select(i => new ReturnItemModel
                {
                    OrderItem = i.Select(ri => ri.OrderLine.ToModel()).Single(),
                    Quantity = i.Select(ri => ri.Quantity).Single()
                }).ToList(),
                Attributes = GetCancelAttributes(rc.Cancel)
            }).ToList();

            events.AddRange(rmaCancels);

            var rmaRefusals = rma.RmaReturnRefusals.Select(rr => new ReturnEventModel
            {
                Id = rr.RefusalId,
                Date = rr.ReturnRefusal.CreatedOn,
                Type = ReturnEventType.Refusal,
                Attributes = new List<ReturnEventAttributeModel>
                {
                    new ReturnEventAttributeModel
                    {
                        Name = "Reason",
                        Value = rr.ReturnReason.Name,
                        Type = ReturnEventAttributeType.Other
                    }
                },
                Items = rr.ReturnRefusal.Items.Where(i => i.Quantity > 0).GroupBy(i => i.OrderLineId).Select(i => new ReturnItemModel
                {
                    OrderItem = i.Select(ri => ri.OrderLine.ToModel()).Single(),
                    Quantity = i.Select(ri => ri.Quantity).Single()
                }).ToList()
            }).ToList();

            events.AddRange(rmaRefusals);
            var rmaShipments = rma.RmaShipments.Select(s => new ReturnEventModel
            {
                Id = s.ShipmentId,
                Date = s.Shipment.CreatedOn,
                Type = ReturnEventType.Shipment,
                Attributes = new List<ReturnEventAttributeModel>
                {
                    new ReturnEventAttributeModel { Name = s.Shipment.ShippingService.Name, Value = s.Shipment.TrackingNumber, Type = ReturnEventAttributeType.Tracking },
                    new ReturnEventAttributeModel { Name = "Shipped", Value = GetShipmentDateAttribute(s.Shipment.ShippedOn), Type = ReturnEventAttributeType.Other  },
                    new ReturnEventAttributeModel { Name = "Estimated", Value = GetShipmentDateAttribute(s.Shipment.EstimatedDeliveryDate), Type = ReturnEventAttributeType.Other },
                    new ReturnEventAttributeModel { Name = "Delivered", Value = GetShipmentDateAttribute(s.Shipment.DeliveredOn), Type = ReturnEventAttributeType.Other },
                    new ReturnEventAttributeModel { Name = "Track", Value = shipmentService.GetCarrierUrl(s.ShipmentId), Type = ReturnEventAttributeType.Track }
                }.Where(a => !string.IsNullOrEmpty(a.Value)).ToList(),
                Items = s.Shipment.Items.Where(i => i.Quantity > 0).GroupBy(i => i.OrderLineId).Select(i => new ReturnItemModel
                {
                    OrderItem = i.Select(ri => ri.OrderLine.ToModel()).Single(),
                    Quantity = i.Select(ri => ri.Quantity).Single()
                }).ToList()
            });

            events.AddRange(rmaShipments);
            events = events.OrderBy(e => e.Date.Value).ToList();

            return events;
        }

        private static string GetShipmentDateAttribute(DateTime? date)
        {
            return date?.ToString("ddd, d MMM yyyy");
        }

        public static ReturnItemModel PrepareReturnItemModel(this Controller controller,
            SalesCancelItem item,
            DateTime updatedOn,
            MediaSettings mediaSettings,
            IWorkContext workContext,
            IWebHelper webHelper,
            ICacheManager cacheManager,
            IStoreContext storeContext,
            IPictureService pictureService,
            IList<WarrantyProductAssociation> warrantyProductAssociations)
        {
            if (item == null)
            {
                return null;
            }

            var returnItem = new ReturnItemModel
            {
                OrderItemId = item.OrderLine.ThubOrderItemId,
                OrderLineId = item.OrderLine.Id,
                Quantity = item.Quantity,
                OrderItem = item.OrderLine.ToModel(),
                UpdatedOn = updatedOn.ToLocalTime(),
                Picture = PrepareProductPictureModel(controller, item.OrderLine.ProductId.Value, mediaSettings.CartThumbPictureSize, true, workContext, cacheManager, pictureService, storeContext, webHelper),
                IsWarranty = item.OrderLine.IsWarranty
            };

            returnItem.AssociatedOrderLineId = returnItem.IsWarranty
                ? warrantyProductAssociations.SingleOrDefault(m => m.SalesOrderWarrantyLineId == returnItem.OrderLineId)?.SalesOrderLineId
                : warrantyProductAssociations.SingleOrDefault(m => m.SalesOrderLineId == returnItem.OrderLineId)?.SalesOrderWarrantyLineId;

            return returnItem;
        }

        public static ReturnItemModel PrepareReturnItemModel(this Controller controller,
            ReturnRequestItem item,
            MediaSettings mediaSettings,
            IWorkContext workContext,
            IWebHelper webHelper,
            ICacheManager cacheManager,
            IStoreContext storeContext,
            IPictureService pictureService)
        {
            if (item == null)
                return null;

            return new ReturnItemModel
            {
                OrderLineId = item.LineId,
                AssociatedOrderLineId = item.OrderLine.WarrantyLineId,
                IsWarranty = item.OrderLine.IsWarranty,
                OrderItemId = item.OrderItemId,
                Quantity = item.Quantity,
                OrderItem = item.OrderLine.ToModel(),
            };
        }

        public static Channel GetStoreChannel(this Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            Channel channel;
            try
            {
                channel = (Channel)store.Id;
            }
            catch
            {
                throw new ArgumentOutOfRangeException(nameof(store), "Unknown store");
            }

            return channel;
        }

        private static string ToSearchRankExplanationHtml(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            text = text.Replace("\r", string.Empty).Replace("\n", "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(" ", "&nbsp;");

            int start = 0;
            do
            {
                start = text.IndexOf("weight(", start, StringComparison.Ordinal);
                if (start == -1)
                {
                    break;
                }

                start += "weight(".Length;
                text = text.Insert(start, "<strong style='color:#000'>");

                start = text.IndexOf("&nbsp;in&nbsp;", start, StringComparison.Ordinal);
                if (start == -1)
                {
                    break;
                }

                text = text.Insert(start, "</strong>");
                start += " in ".Length;
            }
            while (start < text.Length);

            return text;
        }

        private static string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //also see System.Web.MimeMapping for more mime types

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
                case "jpeg":
                    lastPart = "jpeg";
                    break;
                case "gif":
                    lastPart = "gif";
                    break;
            }
            return lastPart;
        }
    }
}