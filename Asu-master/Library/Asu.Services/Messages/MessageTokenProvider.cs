using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using Asu.Core;
using Asu.Core.Domain.Blogs;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Directory;
using Asu.Core.Domain.Forums;
using Asu.Core.Domain.Media;
using Asu.Core.Domain.Messages;
using Asu.Core.Domain.News;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Shipping;
using Asu.Core.Domain.Stores;
using Asu.Core.Domain.Tax;
using Asu.Core.Html;
using Asu.Core.Infrastructure;
using Asu.Services.Catalog;
using Asu.Services.Common;
using Asu.Services.Customers;
using Asu.Services.Customization;
using Asu.Services.Directory;
using Asu.Services.Events;
using Asu.Services.Forums;
using Asu.Services.Helpers;
using Asu.Services.Localization;
using Asu.Services.Media;
using Asu.Services.Orders;
using Asu.Services.Payments;
using Asu.Services.Seo;
using Asu.Services.Stores;

namespace Asu.Services.Messages
{
    using Asu.Core.Caching;
    using Asu.Core.Domain.Returns;
    using Asu.Core.Domain.SalesQuotes;
    using Asu.Services.Security;
    using Asu.Services.Topics;
    using MvcWeb = System.Web.Mvc;
    using System.Web.Routing;
    using Asu.Core.Domain.Common;
    using System.Security.Cryptography;
    using Asu.Services.Configuration;

    public partial class MessageTokenProvider : IMessageTokenProvider
    {
        #region Fields

        private const string TOPIC_TOP_MENU_MODEL_KEY = "Nop.pres.topic.topmenu-{0}-{1}";
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPriceFormatter _priceFormatter;
        private readonly ICurrencyService _currencyService;
        private readonly IWorkContext _workContext;
        private readonly IDownloadService _downloadService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IProductRecommendationService _productRecommendationService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;

        private readonly MessageTemplatesSettings _templatesSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly TaxSettings _taxSettings;

        private readonly IEventPublisher _eventPublisher;

        private readonly IPictureService _pictureService;   //WC
        private readonly ICacheManager cacheManager;
        private readonly ITopicService topicService;
        private readonly IEncryptionService encryptionService;
        private readonly INewsLetterSubscriptionService newsLetterSubscriptionService;
        private readonly IUrlRecordService urlRecordService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public MessageTokenProvider(ILanguageService languageService,
            ILocalizationService localizationService,
            IDateTimeHelper dateTimeHelper,
            IPriceFormatter priceFormatter,
            ICurrencyService currencyService,
            IWorkContext workContext,
            IDownloadService downloadService,
            IOrderService orderService,
            IPaymentService paymentService,
            IProductRecommendationService productRecommendationService,
            IStoreService storeService,
            IStoreContext storeContext,
            IProductAttributeParser productAttributeParser,
            IAddressAttributeFormatter addressAttributeFormatter,
            MessageTemplatesSettings templatesSettings,
            CatalogSettings catalogSettings,
            TaxSettings taxSettings,
            IEventPublisher eventPublisher,
            IPictureService pictureService,
            ICacheManager cacheManager,
            ITopicService topicService,
            IEncryptionService encryptionService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IUrlRecordService urlRecordService,
            IWebHelper webHelper)
        {
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._dateTimeHelper = dateTimeHelper;
            this._priceFormatter = priceFormatter;
            this._currencyService = currencyService;
            this._workContext = workContext;
            this._downloadService = downloadService;
            this._orderService = orderService;
            this._paymentService = paymentService;
            this._productRecommendationService = productRecommendationService;
            this._productAttributeParser = productAttributeParser;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._storeService = storeService;
            this._storeContext = storeContext;

            this._templatesSettings = templatesSettings;
            this._catalogSettings = catalogSettings;
            this._taxSettings = taxSettings;
            this._eventPublisher = eventPublisher;

            this._pictureService = pictureService;  //WC
            this.cacheManager = cacheManager;
            this.topicService = topicService;
            this.encryptionService = encryptionService;
            this.newsLetterSubscriptionService = newsLetterSubscriptionService;
            this.urlRecordService = urlRecordService;
            this._webHelper = webHelper;
        }

        #endregion

        /// <summary>
        /// Convert a collection to a HTML table
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="vendorId">Vendor identifier (used to limit products by vendor</param>
        /// <returns>HTML table of products</returns>
        protected virtual string ProductListToHtmlTable(Order order, int languageId, int vendorId)
        {
            var result = "";

            var language = _languageService.GetLanguageById(languageId);

            var sb = new StringBuilder();
            sb.AppendLine("<table border=\"0\" style=\"width:100%;\">");

            #region Products
            sb.AppendLine(string.Format("<tr style=\"background-color:{0};text-align:center;\">", _templatesSettings.Color1));
            sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Name", languageId)));
            sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Price", languageId)));
            sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Quantity", languageId)));
            sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Total", languageId)));
            sb.AppendLine("</tr>");

            var table = order.OrderItems.ToList();
            for (int i = 0; i <= table.Count - 1; i++)
            {
                var orderItem = table[i];
                var product = orderItem.Product;
                if (product == null)
                    continue;

                if (vendorId > 0 && product.VendorId != vendorId)
                    continue;

                sb.AppendLine(string.Format("<tr style=\"background-color: {0};text-align: center;\">", _templatesSettings.Color2));
                //product name
                string productName = product.GetLocalized(x => x.Name, languageId);

                sb.AppendLine("<td style=\"padding: 0.6em 0.4em;text-align: left;\">" + HttpUtility.HtmlEncode(productName));
                //add download link
                if (_downloadService.IsDownloadAllowed(orderItem))
                {
                    //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
                    string downloadUrl = string.Format("{0}download/getdownload/{1}", GetStoreUrl(order.StoreId), orderItem.OrderItemGuid);
                    string downloadLink = string.Format("<a class=\"link\" href=\"{0}\">{1}</a>", downloadUrl, _localizationService.GetResource("Messages.Order.Product(s).Download", languageId));
                    sb.AppendLine("&nbsp;&nbsp;(");
                    sb.AppendLine(downloadLink);
                    sb.AppendLine(")");
                }
                //attributes
                if (!String.IsNullOrEmpty(orderItem.AttributeDescription))
                {
                    sb.AppendLine("<br />");
                    sb.AppendLine(orderItem.AttributeDescription);
                }
                //sku
                if (_catalogSettings.ShowProductSku)
                {
                    var sku = product.FormatSku(orderItem.AttributesXml, _productAttributeParser);
                    if (!String.IsNullOrEmpty(sku))
                    {
                        sb.AppendLine("<br />");
                        sb.AppendLine(string.Format(_localizationService.GetResource("Messages.Order.Product(s).SKU", languageId), HttpUtility.HtmlEncode(sku)));
                    }
                }
                sb.AppendLine("</td>");

                string unitPriceStr = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    //including tax
                    var unitPriceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceInclTax, order.CurrencyRate);
                    unitPriceStr = _priceFormatter.FormatPrice(unitPriceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                }
                else
                {
                    //excluding tax
                    var unitPriceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceExclTax, order.CurrencyRate);
                    unitPriceStr = _priceFormatter.FormatPrice(unitPriceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                }
                sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: right;\">{0}</td>", unitPriceStr));

                sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: center;\">{0}</td>", orderItem.Quantity));

                string priceStr = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    //including tax
                    var priceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceInclTax, order.CurrencyRate);
                    priceStr = _priceFormatter.FormatPrice(priceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                }
                else
                {
                    //excluding tax
                    var priceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceExclTax, order.CurrencyRate);
                    priceStr = _priceFormatter.FormatPrice(priceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                }
                sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: right;\">{0}</td>", priceStr));

                sb.AppendLine("</tr>");
            }
            #endregion

            if (vendorId == 0)
            {
                //we render checkout attributes and totals only for store owners (hide for vendors)

                #region Checkout Attributes

                if (!String.IsNullOrEmpty(order.CheckoutAttributeDescription))
                {
                    sb.AppendLine("<tr><td style=\"text-align:right;\" colspan=\"1\">&nbsp;</td><td colspan=\"3\" style=\"text-align:right\">");
                    sb.AppendLine(order.CheckoutAttributeDescription);
                    sb.AppendLine("</td></tr>");
                }

                #endregion

                #region Totals

                //subtotal
                string cusSubTotal = string.Empty;
                bool displaySubTotalDiscount = false;
                string cusSubTotalDiscount = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax && !_taxSettings.ForceTaxExclusionFromOrderSubtotal)
                {
                    //including tax

                    //subtotal
                    var orderSubtotalInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalInclTax, order.CurrencyRate);
                    cusSubTotal = _priceFormatter.FormatPrice(orderSubtotalInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                    //discount (applied to order subtotal)
                    var orderSubTotalDiscountInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountInclTax, order.CurrencyRate);
                    if (orderSubTotalDiscountInclTaxInCustomerCurrency > decimal.Zero)
                    {
                        cusSubTotalDiscount = _priceFormatter.FormatPrice(-orderSubTotalDiscountInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                        displaySubTotalDiscount = true;
                    }
                }
                else
                {
                    //exсluding tax

                    //subtotal
                    var orderSubtotalExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalExclTax, order.CurrencyRate);
                    cusSubTotal = _priceFormatter.FormatPrice(orderSubtotalExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                    //discount (applied to order subtotal)
                    var orderSubTotalDiscountExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountExclTax, order.CurrencyRate);
                    if (orderSubTotalDiscountExclTaxInCustomerCurrency > decimal.Zero)
                    {
                        cusSubTotalDiscount = _priceFormatter.FormatPrice(-orderSubTotalDiscountExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                        displaySubTotalDiscount = true;
                    }
                }

                //shipping, payment method fee
                string cusShipTotal = string.Empty;
                string cusPaymentMethodAdditionalFee = string.Empty;
                var taxRates = new SortedDictionary<decimal, decimal>();
                string cusTaxTotal = string.Empty;
                string cusDiscount = string.Empty;
                string cusTotal = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    //including tax

                    //shipping
                    var orderShippingInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderShippingInclTax, order.CurrencyRate);
                    cusShipTotal = _priceFormatter.FormatShippingPrice(orderShippingInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                    //payment method additional fee
                    var paymentMethodAdditionalFeeInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeInclTax, order.CurrencyRate);
                    cusPaymentMethodAdditionalFee = _priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                }
                else
                {
                    //excluding tax

                    //shipping
                    var orderShippingExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderShippingExclTax, order.CurrencyRate);
                    cusShipTotal = _priceFormatter.FormatShippingPrice(orderShippingExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                    //payment method additional fee
                    var paymentMethodAdditionalFeeExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeExclTax, order.CurrencyRate);
                    cusPaymentMethodAdditionalFee = _priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                }

                //shipping
                bool displayShipping = order.ShippingStatus != ShippingStatus.ShippingNotRequired;

                //payment method fee
                bool displayPaymentMethodFee = order.PaymentMethodAdditionalFeeExclTax > decimal.Zero;

                //tax
                bool displayTax = true;
                bool displayTaxRates = true;
                if (_taxSettings.HideTaxInOrderSummary && order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    displayTax = false;
                    displayTaxRates = false;
                }
                else
                {
                    if (order.OrderTax == 0 && _taxSettings.HideZeroTax)
                    {
                        displayTax = false;
                        displayTaxRates = false;
                    }
                    else
                    {
                        taxRates = new SortedDictionary<decimal, decimal>();
                        foreach (var tr in order.TaxRatesDictionary)
                            taxRates.Add(tr.Key, _currencyService.ConvertCurrency(tr.Value, order.CurrencyRate));

                        displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Count > 0;
                        displayTax = !displayTaxRates;

                        var orderTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTax, order.CurrencyRate);
                        string taxStr = _priceFormatter.FormatPrice(orderTaxInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);
                        cusTaxTotal = taxStr;
                    }
                }

                //discount
                bool displayDiscount = false;
                if (order.OrderDiscount > decimal.Zero)
                {
                    var orderDiscountInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderDiscount, order.CurrencyRate);
                    cusDiscount = _priceFormatter.FormatPrice(-orderDiscountInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);
                    displayDiscount = true;
                }

                //total
                var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
                cusTotal = _priceFormatter.FormatPrice(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);




                //subtotal
                sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.SubTotal", languageId), cusSubTotal));

                //discount (applied to order subtotal)
                if (displaySubTotalDiscount)
                {
                    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.SubTotalDiscount", languageId), cusSubTotalDiscount));
                }


                //shipping
                if (displayShipping)
                {
                    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.Shipping", languageId), cusShipTotal));
                }

                //payment method fee
                if (displayPaymentMethodFee)
                {
                    string paymentMethodFeeTitle = _localizationService.GetResource("Messages.Order.PaymentMethodAdditionalFee", languageId);
                    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, paymentMethodFeeTitle, cusPaymentMethodAdditionalFee));
                }

                //tax
                if (displayTax)
                {
                    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.Tax", languageId), cusTaxTotal));
                }
                if (displayTaxRates)
                {
                    foreach (var item in taxRates)
                    {
                        string taxRate = String.Format(_localizationService.GetResource("Messages.Order.TaxRateLine"), _priceFormatter.FormatTaxRate(item.Key));
                        string taxValue = _priceFormatter.FormatPrice(item.Value, true, order.CustomerCurrencyCode, false, language);
                        sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, taxRate, taxValue));
                    }
                }

                //discount
                if (displayDiscount)
                {
                    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.TotalDiscount", languageId), cusDiscount));
                }

                //gift cards
                var gcuhC = order.GiftCardUsageHistory;
                foreach (var gcuh in gcuhC)
                {
                    string giftCardText = String.Format(_localizationService.GetResource("Messages.Order.GiftCardInfo", languageId), HttpUtility.HtmlEncode(gcuh.GiftCard.GiftCardCouponCode));
                    string giftCardAmount = _priceFormatter.FormatPrice(-(_currencyService.ConvertCurrency(gcuh.UsedValue, order.CurrencyRate)), true, order.CustomerCurrencyCode, false, language);
                    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, giftCardText, giftCardAmount));
                }

                //reward points
                if (order.RedeemedRewardPointsEntry != null)
                {
                    string rpTitle = string.Format(_localizationService.GetResource("Messages.Order.RewardPoints", languageId), -order.RedeemedRewardPointsEntry.Points);
                    string rpAmount = _priceFormatter.FormatPrice(-(_currencyService.ConvertCurrency(order.RedeemedRewardPointsEntry.UsedAmount, order.CurrencyRate)), true, order.CustomerCurrencyCode, false, language);
                    sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, rpTitle, rpAmount));
                }

                //total
                sb.AppendLine(string.Format("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>", _templatesSettings.Color3, _localizationService.GetResource("Messages.Order.OrderTotal", languageId), cusTotal));
                #endregion

            }

            sb.AppendLine("</table>");
            result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Convert a collection to a JSON data
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="vendorId">Vendor identifier (used to limit products by vendor</param>
        /// <returns>HTML table of products</returns>
        protected virtual void AddProductsData(Order order, DynamicTemplateData data, int languageId, int vendorId)
        {
            var result = new List<Asu.Services.Messages.SendGridProduct>();
            var language = this._languageService.GetLanguageById(languageId);

            #region Products

            foreach (var orderItem in order.OrderItems)
            {
                var product = orderItem.Product;
                if (vendorId > 0 && product.VendorId != vendorId)
                {
                    continue;
                }
                var store = this._storeService.GetStoreById(order.StoreId);

                var productPicture = this._pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
                string imageUrl;
                if (productPicture == null)
                {
                    imageUrl = !string.IsNullOrEmpty(this._pictureService.GetProductAdditionalImageName(product.Id))
                        ? $"{this._webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                        : $"{this._webHelper.GetStoreImagesLocation()}content/images/{store.GetDefaultPictureNameWithoutExtension()}.gif";
                }
                else
                {
                    imageUrl = this._pictureService.GetPictureUrl(productPicture, 250, true);
                }
                
                var sendGridProduct = new SendGridProduct
                {
                    ImageUrl = imageUrl,
                    Link = $"{store.Url}{product.GetSeName()}",
                    Name = product.GetLocalized(x => x.Name, languageId)
                };

                //add download link
                if (this._downloadService.IsDownloadAllowed(orderItem))
                {
                    //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
                    string downloadUrl = string.Format("{0}download/getdownload/{1}", GetStoreUrl(order.StoreId), orderItem.OrderItemGuid);
                    sendGridProduct.Link = downloadUrl;
                }

                //attributes
                if (!string.IsNullOrEmpty(orderItem.AttributeDescription))
                {
                    sendGridProduct.AttributeDescription = orderItem.AttributeDescription;
                }

                //sku
                if (this._catalogSettings.ShowProductSku)
                {
                    var sku = product.FormatSku(orderItem.AttributesXml, _productAttributeParser);
                    if (!string.IsNullOrEmpty(sku))
                    {
                        sendGridProduct.Sku = sku;
                    }
                }

                string unitPriceStr = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    //including tax
                    var unitPriceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceInclTax, order.CurrencyRate);
                    unitPriceStr = this._priceFormatter.FormatPrice(unitPriceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                }
                else
                {
                    //excluding tax
                    var unitPriceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.UnitPriceExclTax, order.CurrencyRate);
                    unitPriceStr = this._priceFormatter.FormatPrice(unitPriceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                }


                sendGridProduct.UnitPrice = unitPriceStr;
                sendGridProduct.Quantity = orderItem.Quantity;

                string priceStr = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    //including tax
                    var priceInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceInclTax, order.CurrencyRate);
                    priceStr = this._priceFormatter.FormatPrice(priceInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                }
                else
                {
                    //excluding tax
                    var priceExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(orderItem.PriceExclTax, order.CurrencyRate);
                    priceStr = this._priceFormatter.FormatPrice(priceExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                }

                sendGridProduct.Price = priceStr;
                data.Products.Add(sendGridProduct);
            }

            #endregion

            if (vendorId == 0)
            {
                //we render checkout attributes and totals only for store owners (hide for vendors)

                #region Checkout Attributes

                if (!string.IsNullOrEmpty(order.CheckoutAttributeDescription))
                {
                    data.CheckoutAttributeDescription = order.CheckoutAttributeDescription;
                }

                #endregion

                #region Totals

                //subtotal
                string cusSubTotal = string.Empty;
                bool displaySubTotalDiscount = false;
                string cusSubTotalDiscount = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax && !_taxSettings.ForceTaxExclusionFromOrderSubtotal)
                {
                    //subtotal
                    var orderSubtotalInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalInclTax, order.CurrencyRate);
                    cusSubTotal = this._priceFormatter.FormatPrice(orderSubtotalInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                    //discount (applied to order subtotal)
                    var orderSubTotalDiscountInclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountInclTax, order.CurrencyRate);
                    if (orderSubTotalDiscountInclTaxInCustomerCurrency > decimal.Zero)
                    {
                        cusSubTotalDiscount = this._priceFormatter.FormatPrice(-orderSubTotalDiscountInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                        displaySubTotalDiscount = true;
                    }
                }
                else
                {
                    //subtotal
                    var orderSubtotalExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubtotalExclTax, order.CurrencyRate);
                    cusSubTotal = this._priceFormatter.FormatPrice(orderSubtotalExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                    //discount (applied to order subtotal)
                    var orderSubTotalDiscountExclTaxInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderSubTotalDiscountExclTax, order.CurrencyRate);
                    if (orderSubTotalDiscountExclTaxInCustomerCurrency > decimal.Zero)
                    {
                        cusSubTotalDiscount = this._priceFormatter.FormatPrice(-orderSubTotalDiscountExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                        displaySubTotalDiscount = true;
                    }
                }

                //shipping, payment method fee
                string cusShipTotal = string.Empty;
                string cusPaymentMethodAdditionalFee = string.Empty;
                var taxRates = new SortedDictionary<decimal, decimal>();
                string cusTaxTotal = string.Empty;
                string cusDiscount = string.Empty;
                string cusTotal = string.Empty;
                if (order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    //shipping
                    var orderShippingInclTaxInCustomerCurrency = this._currencyService.ConvertCurrency(order.OrderShippingInclTax, order.CurrencyRate);
                    cusShipTotal = this._priceFormatter.FormatShippingPrice(orderShippingInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                    //payment method additional fee
                    var paymentMethodAdditionalFeeInclTaxInCustomerCurrency = this._currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeInclTax, order.CurrencyRate);
                    cusPaymentMethodAdditionalFee = this._priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeInclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, true);
                }
                else
                {
                    //shipping
                    var orderShippingExclTaxInCustomerCurrency = this._currencyService.ConvertCurrency(order.OrderShippingExclTax, order.CurrencyRate);
                    cusShipTotal = _priceFormatter.FormatShippingPrice(orderShippingExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                    //payment method additional fee
                    var paymentMethodAdditionalFeeExclTaxInCustomerCurrency = this._currencyService.ConvertCurrency(order.PaymentMethodAdditionalFeeExclTax, order.CurrencyRate);
                    cusPaymentMethodAdditionalFee = this._priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeExclTaxInCustomerCurrency, true, order.CustomerCurrencyCode, language, false);
                }

                //shipping
                bool displayShipping = order.ShippingStatus != ShippingStatus.ShippingNotRequired;

                //payment method fee
                bool displayPaymentMethodFee = order.PaymentMethodAdditionalFeeExclTax > decimal.Zero;

                //tax
                bool displayTax = true;
                bool displayTaxRates = true;
                if (this._taxSettings.HideTaxInOrderSummary && order.CustomerTaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    displayTax = false;
                    displayTaxRates = false;
                }
                else
                {
                    if (order.OrderTax == 0 && this._taxSettings.HideZeroTax)
                    {
                        displayTax = false;
                        displayTaxRates = false;
                    }
                    else
                    {
                        taxRates = new SortedDictionary<decimal, decimal>();
                        foreach (var tr in order.TaxRatesDictionary)
                            taxRates.Add(tr.Key, this._currencyService.ConvertCurrency(tr.Value, order.CurrencyRate));

                        displayTaxRates = this._taxSettings.DisplayTaxRates && taxRates.Count > 0;
                        displayTax = !displayTaxRates;

                        var orderTaxInCustomerCurrency = this._currencyService.ConvertCurrency(order.OrderTax, order.CurrencyRate);
                        string taxStr = this._priceFormatter.FormatPrice(orderTaxInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);
                        cusTaxTotal = taxStr;
                    }
                }

                //discount
                bool displayDiscount = false;
                if (order.OrderDiscount > decimal.Zero)
                {
                    var orderDiscountInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderDiscount, order.CurrencyRate);
                    cusDiscount = _priceFormatter.FormatPrice(-orderDiscountInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);
                    displayDiscount = true;
                }

                //total
                var orderTotalInCustomerCurrency = _currencyService.ConvertCurrency(order.OrderTotal, order.CurrencyRate);
                cusTotal = _priceFormatter.FormatPrice(orderTotalInCustomerCurrency, true, order.CustomerCurrencyCode, false, language);

                //subtotal
                data.SubTotal = cusSubTotal;

                //discount (applied to order subtotal)
                if (displaySubTotalDiscount)
                {
                    data.SubTotalDiscount = cusSubTotalDiscount;
                }

                //shipping
                if (displayShipping)
                {
                    data.ShipTotal = cusShipTotal;
                }

                //payment method fee
                if (displayPaymentMethodFee)
                {
                    data.PaymentMethodAdditionalFee = cusPaymentMethodAdditionalFee;
                }

                //tax
                if (displayTax)
                {
                    data.TaxTotal = cusTaxTotal;
                }

                if (displayTaxRates)
                {
                    foreach (var item in taxRates)
                    {
                        string taxValue = this._priceFormatter.FormatPrice(item.Value, true, order.CustomerCurrencyCode, false, language);
                        data.TaxValue = taxValue;
                    }
                }

                //discount
                if (displayDiscount)
                {
                    data.Discount = cusDiscount;
                }

                //total
                data.OrderTotal = cusTotal;

                #endregion Totals
            }
        }

        protected virtual string ShippmentLineToHtmlTable(ManualOrderShipment shipment, int languageId)
        {
            var sb = new StringBuilder($"<table border=\"0\" style=\"width:100%;\">{'\n'}");
            sb.AppendLine(string.Format("<tr style=\"background-color:{0};text-align:center;\">", this._templatesSettings.Color1));
            sb.AppendLine(string.Format("<th>{0}</th>", this._localizationService.GetResource("Messages.Order.Product(s).Name", languageId)));
            sb.AppendLine(string.Format("<th>{0}</th>", this._localizationService.GetResource("Messages.Order.Product(s).Price", languageId)));
            sb.AppendLine(string.Format("<th>{0}</th>", this._localizationService.GetResource("Messages.Order.Product(s).Quantity", languageId)));
            sb.AppendLine(string.Format("<th>{0}</th>", this._localizationService.GetResource("Messages.Order.Product(s).Total", languageId)));
            sb.AppendLine("</tr>");

            foreach (var line in shipment.ShipmentLines)
            {
                sb.AppendLine(string.Format("<tr style=\"background-color: {0};text-align: center;\">", this._templatesSettings.Color2));

                //product name
                sb.AppendLine("<td style=\"padding: 0.6em 0.4em;text-align: left;\">" + HttpUtility.HtmlEncode(line.Name));

                if (!string.IsNullOrEmpty(line.ManufacturerPartNumber))
                {
                    sb.AppendLine("<br />");
                    sb.AppendLine(String.Format("MPN: {0}", HttpUtility.HtmlEncode(line.ManufacturerPartNumber)));
                }

                sb.AppendLine("</td>");

                sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: right;\">{0}</td>", this._priceFormatter.FormatPrice(line.Price, true, false)));

                sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: center;\">{0}</td>", line.Quantity));

                sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: right;\">{0}</td>", this._priceFormatter.FormatPrice(line.Subtotal, true, false)));

                sb.AppendLine("</tr>");
            }

            //subtotal
            sb.Append("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" ");
            sb.AppendLine(string.Format("style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>",
                this._templatesSettings.Color3, this._localizationService.GetResource("Messages.Order.SubTotal", languageId),
                this._priceFormatter.FormatPrice(shipment.Subtotal, true, false)));

            Action<decimal?> appendSubTotalValue = (x) =>
            {
                if (x.HasValue)
                {
                    sb.Append("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" ");
                    sb.AppendLine(string.Format("style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>",
                        this._templatesSettings.Color3,
                        this._localizationService.GetResource("Messages.Order.Shipping", languageId),
                        this._priceFormatter.FormatPrice(x.Value, true, false)));
                }
            };

            //totals
            appendSubTotalValue(shipment.Shipping);
            appendSubTotalValue(shipment.Tax);
            appendSubTotalValue(shipment.Discount);
            sb.Append("<tr style=\"text-align:right;\"><td>&nbsp;</td><td colspan=\"2\" ");
            sb.AppendLine(string.Format("style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{1}</strong></td> <td style=\"background-color: {0};padding:0.6em 0.4 em;\"><strong>{2}</strong></td></tr>",
                this._templatesSettings.Color3,
                this._localizationService.GetResource("Messages.Order.OrderTotal", languageId),
                this._priceFormatter.FormatPrice(shipment.Total, true, false)));

            sb.AppendLine("</table>");

            return sb.ToString();
        }

        /// <summary>
        /// Convert a collection to a HTML table
        /// </summary>
        /// <param name="shipment">Shipment</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>HTML table of products</returns>
        protected virtual string ProductListToHtmlTable(Shipment shipment, int languageId)
        {
            var result = "";

            var sb = new StringBuilder();
            sb.AppendLine("<table border=\"0\" style=\"width:100%;\">");

            #region Products
            sb.AppendLine(string.Format("<tr style=\"background-color:{0};text-align:center;\">", _templatesSettings.Color1));
            sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Name", languageId)));
            sb.AppendLine(string.Format("<th>{0}</th>", _localizationService.GetResource("Messages.Order.Product(s).Quantity", languageId)));
            sb.AppendLine("</tr>");

            var table = shipment.ShipmentItems.ToList();
            for (int i = 0; i <= table.Count - 1; i++)
            {
                var si = table[i];
                var orderItem = _orderService.GetOrderItemById(si.OrderItemId);
                if (orderItem == null)
                    continue;

                var product = orderItem.Product;
                if (product == null)
                    continue;

                sb.AppendLine(string.Format("<tr style=\"background-color: {0};text-align: center;\">", _templatesSettings.Color2));
                //product name
                string productName = product.GetLocalized(x => x.Name, languageId);

                sb.AppendLine("<td style=\"padding: 0.6em 0.4em;text-align: left;\">" + HttpUtility.HtmlEncode(productName));
                //attributes
                if (!String.IsNullOrEmpty(orderItem.AttributeDescription))
                {
                    sb.AppendLine("<br />");
                    sb.AppendLine(orderItem.AttributeDescription);
                }
                //sku
                if (_catalogSettings.ShowProductSku)
                {
                    var sku = product.FormatSku(orderItem.AttributesXml, _productAttributeParser);
                    if (!String.IsNullOrEmpty(sku))
                    {
                        sb.AppendLine("<br />");
                        sb.AppendLine(string.Format(_localizationService.GetResource("Messages.Order.Product(s).SKU", languageId), HttpUtility.HtmlEncode(sku)));
                    }
                }
                sb.AppendLine("</td>");

                sb.AppendLine(string.Format("<td style=\"padding: 0.6em 0.4em;text-align: center;\">{0}</td>", si.Quantity));

                sb.AppendLine("</tr>");
            }
            #endregion

            sb.AppendLine("</table>");
            result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Convert a collection to a HTML table
        /// </summary>
        /// <param name="shipment">Shipment</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>HTML table of products</returns>
        protected virtual List<Asu.Services.Messages.SendGridProduct> GetShipmentProducts(Shipment shipment, int languageId)
        {
            var products = new List<Asu.Services.Messages.SendGridProduct>();
            var table = shipment.ShipmentItems.ToList();
            foreach (var row in table)
            {
                var orderItem = this._orderService.GetOrderItemById(row.OrderItemId);
                if (orderItem == null)
                {
                    continue;
                }

                var product = orderItem.Product;
                var productPicture = _pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();

                string imageUrl;
                var store = this._storeService.GetStoreById(shipment.Order.StoreId);
                var p = new Asu.Services.Messages.SendGridProduct
                {
                    Name = product.GetLocalized(x => x.Name, languageId),
                    Link = store.Url + product.GetSeName(),
                    ImageUrl = productPicture == null ? !string.IsNullOrEmpty(this._pictureService.GetProductAdditionalImageName(product.Id))
                            ? $"{_webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                            : $"{_webHelper.GetStoreImagesLocation()}content/images/{this._storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif"
                        : this._pictureService.GetPictureUrl(productPicture, 250, true),
                    Quantity = row.Quantity
                };

                if (!String.IsNullOrEmpty(orderItem.AttributeDescription))
                {
                    p.AttributeDescription = orderItem.AttributeDescription;
                }

                if (this._catalogSettings.ShowProductSku)
                {
                    var sku = product.FormatSku(orderItem.AttributesXml, this._productAttributeParser);
                    if (!string.IsNullOrEmpty(sku))
                    {
                        p.Sku = sku;
                    }
                }

                products.Add(p);
            }

            return products;
        }

        /// <summary>
        /// Get store URL
        /// </summary>
        /// <param name="storeId">Store identifier; Pass 0 to load URL of the current store</param>
        /// <param name="useSsl">Use SSL</param>
        /// <returns></returns>
        protected virtual string GetStoreUrl(int storeId = 0, bool useSsl = true)
        {
            var store = _storeService.GetStoreById(storeId) ?? _storeContext.CurrentStore;

            if (store == null)
                throw new Exception("No store could be loaded");

            return useSsl ? store.SecureUrl : store.Url;
        }


        #region Methods

        public virtual void AddStoreTokens(IList<Token> tokens, Store store, EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            tokens.Add(new Token("Store.Name", store.GetLocalized(x => x.Name)));
            tokens.Add(new Token("Store.URL", store.SecureUrl, true));
            tokens.Add(new Token("Store.Email", emailAccount.Email));
            tokens.Add(new Token("Store.CompanyName", store.CompanyName));
            tokens.Add(new Token("Store.CompanyAddress", store.CompanyAddress));
            tokens.Add(new Token("Store.CompanyPhoneNumber", store.CompanyPhoneNumber));
            tokens.Add(new Token("Store.CompanyVat", store.CompanyVat));

            //topics
            this.AddTopicTokens(tokens, store.Id);

            //event notification
            _eventPublisher.EntityTokensAdded(store, tokens);
        }

        public virtual void AddManualOrderTokens(IList<Token> tokens, Store store, ManualOrderShipment shipment, int languageId)
        {
            tokens.Add(new Token("Shipment.ShipmentNumber", shipment.Id.ToString()));
            tokens.Add(new Token("Shipment.TrackingNumber", shipment.TrackingNumber));

            tokens.Add(new Token("Order.OrderNumber", shipment.OrderNumber));
            tokens.Add(new Token("Order.CustomerFullName", shipment.CustomerFullName));
            tokens.Add(new Token("Order.BillingPhoneNumber", shipment.BillingPhoneNumber));
            tokens.Add(new Token("Order.BillingEmail", shipment.BillingEmail));
            tokens.Add(new Token("Order.BillingAddress1", shipment.BillingLine1));
            tokens.Add(new Token("Order.BillingAddress2", !string.IsNullOrEmpty(shipment.BillingLine2) ? shipment.BillingLine2 : string.Empty));
            tokens.Add(new Token("Order.BillingCity", shipment.BillingCity));
            tokens.Add(new Token("Order.BillingStateProvince", shipment.BillingStateProvince));
            tokens.Add(new Token("Order.BillingZipPostalCode", shipment.BillingZipPostalCode));
            tokens.Add(new Token("Order.BillingCountry", shipment.BillingCountry));

            tokens.Add(new Token("Order.ShippingMethod", shipment.ShippingMethod));
            tokens.Add(new Token("Order.ShippingAddress1", !string.IsNullOrEmpty(shipment.ShippingLine1) ? shipment.ShippingLine1 : string.Empty));
            tokens.Add(new Token("Order.ShippingAddress2", !string.IsNullOrEmpty(shipment.ShippingLine2) ? shipment.ShippingLine2 : string.Empty));
            tokens.Add(new Token("Order.ShippingCity", !string.IsNullOrEmpty(shipment.ShippingCity) ? shipment.ShippingCity : string.Empty));
            tokens.Add(new Token("Order.ShippingStateProvince", !string.IsNullOrEmpty(shipment.ShippingStateProvince) ? shipment.ShippingStateProvince : string.Empty));
            tokens.Add(new Token("Order.ShippingZipPostalCode", !string.IsNullOrEmpty(shipment.ShippingZipPostalCode) ? shipment.ShippingZipPostalCode : string.Empty));
            tokens.Add(new Token("Order.ShippingCountry", !string.IsNullOrEmpty(shipment.ShippingCountry) ? shipment.ShippingCountry : string.Empty));

            tokens.Add(new Token("Order.Product(s)", ShippmentLineToHtmlTable(shipment, languageId), true));

            tokens.Add(new Token("Order.Total", this._priceFormatter.FormatPrice(shipment.Total, true, false)));
        }

        public virtual void AddReturnTokens(IList<Token> tokens, string orderNumber, string fullname, string message, string marketplace, string email, string phone)
        {
            tokens.Add(new Token("Store.Name", marketplace));
            tokens.Add(new Token("Order.OrderNumber", orderNumber));
            tokens.Add(new Token("Order.CustomerFullName", fullname));
            tokens.Add(new Token("Order.BillingEmail", email));
            tokens.Add(new Token("Order.BillingPhoneNumber", phone));
            tokens.Add(new Token("ReturnRequest.CustomerComment", message));
        }

        public virtual void AddOrderTokens(IList<Token> tokens, Order order, int languageId, int vendorId = 0)
        {
            tokens.Add(new Token("Order.OrderNumber", order.Id.ToString()));

            tokens.Add(new Token("Order.CustomerFullName", string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName)));
            tokens.Add(new Token("Order.CustomerEmail", order.BillingAddress.Email));


            tokens.Add(new Token("Order.BillingFirstName", order.BillingAddress.FirstName));
            tokens.Add(new Token("Order.BillingLastName", order.BillingAddress.LastName));
            tokens.Add(new Token("Order.BillingPhoneNumber", order.BillingAddress.PhoneNumber));
            tokens.Add(new Token("Order.BillingEmail", order.BillingAddress.Email));
            tokens.Add(new Token("Order.BillingFaxNumber", order.BillingAddress.FaxNumber));
            tokens.Add(new Token("Order.BillingCompany", order.BillingAddress.Company));
            tokens.Add(new Token("Order.BillingAddress1", order.BillingAddress.Address1));
            tokens.Add(new Token("Order.BillingAddress2", order.BillingAddress.Address2));
            tokens.Add(new Token("Order.BillingCity", order.BillingAddress.City));
            tokens.Add(new Token("Order.BillingStateProvince", order.BillingAddress.StateProvince != null ? order.BillingAddress.StateProvince.GetLocalized(x => x.Name) : ""));
            tokens.Add(new Token("Order.BillingZipPostalCode", order.BillingAddress.ZipPostalCode));
            tokens.Add(new Token("Order.BillingCountry", order.BillingAddress.Country != null ? order.BillingAddress.Country.GetLocalized(x => x.Name) : ""));
            tokens.Add(new Token("Order.BillingCustomAttributes", _addressAttributeFormatter.FormatAttributes(order.BillingAddress.CustomAttributes), true));

            tokens.Add(new Token("Order.ShippingMethod", order.ShippingMethod));
            tokens.Add(new Token("Order.ShippingFirstName", order.ShippingAddress != null ? order.ShippingAddress.FirstName : ""));
            tokens.Add(new Token("Order.ShippingLastName", order.ShippingAddress != null ? order.ShippingAddress.LastName : ""));
            tokens.Add(new Token("Order.ShippingPhoneNumber", order.ShippingAddress != null ? order.ShippingAddress.PhoneNumber : ""));
            tokens.Add(new Token("Order.ShippingEmail", order.ShippingAddress != null ? order.ShippingAddress.Email : ""));
            tokens.Add(new Token("Order.ShippingFaxNumber", order.ShippingAddress != null ? order.ShippingAddress.FaxNumber : ""));
            tokens.Add(new Token("Order.ShippingCompany", order.ShippingAddress != null ? order.ShippingAddress.Company : ""));
            tokens.Add(new Token("Order.ShippingAddress1", order.ShippingAddress != null ? order.ShippingAddress.Address1 : ""));
            tokens.Add(new Token("Order.ShippingAddress2", order.ShippingAddress != null ? order.ShippingAddress.Address2 : ""));
            tokens.Add(new Token("Order.ShippingCity", order.ShippingAddress != null ? order.ShippingAddress.City : ""));
            tokens.Add(new Token("Order.ShippingStateProvince", order.ShippingAddress != null && order.ShippingAddress.StateProvince != null ? order.ShippingAddress.StateProvince.GetLocalized(x => x.Name) : ""));
            tokens.Add(new Token("Order.ShippingZipPostalCode", order.ShippingAddress != null ? order.ShippingAddress.ZipPostalCode : ""));
            tokens.Add(new Token("Order.ShippingCountry", order.ShippingAddress != null && order.ShippingAddress.Country != null ? order.ShippingAddress.Country.GetLocalized(x => x.Name) : ""));
            tokens.Add(new Token("Order.ShippingCustomAttributes", _addressAttributeFormatter.FormatAttributes(order.ShippingAddress != null ? order.ShippingAddress.CustomAttributes : ""), true));


            var paymentMethod = _paymentService.LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
            var paymentMethodName = paymentMethod != null ? paymentMethod.GetLocalizedFriendlyName(_localizationService, _workContext.WorkingLanguage.Id) : order.PaymentMethodSystemName;
            tokens.Add(new Token("Order.PaymentMethod", paymentMethodName));
            tokens.Add(new Token("Order.VatNumber", order.VatNumber));

            tokens.Add(new Token("Order.Product(s)", ProductListToHtmlTable(order, languageId, vendorId), true));

            var language = _languageService.GetLanguageById(languageId);
            if (language != null && !String.IsNullOrEmpty(language.LanguageCulture))
            {
                DateTime createdOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, TimeZoneInfo.Utc, _dateTimeHelper.GetCustomerTimeZone(order.Customer));
                tokens.Add(new Token("Order.CreatedOn", createdOn.ToString("ddd, dd MMM yyy", new CultureInfo(language.LanguageCulture))));
            }
            else
            {
                tokens.Add(new Token("Order.CreatedOn", order.CreatedOnUtc.ToString("D")));
            }

            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            tokens.Add(new Token("Order.OrderURLForCustomer", string.Format("{0}orderdetails/{1}", GetStoreUrl(order.StoreId), order.Id), true));
            tokens.Add(new Token("Order.Total", this._priceFormatter.FormatPrice(order.OrderTotal, true, false)));

            //event notification
            _eventPublisher.EntityTokensAdded(order, tokens);
        }

        public virtual void AddShipmentTokens(IList<Token> tokens, Shipment shipment, int languageId)
        {
            tokens.Add(new Token("Shipment.ShipmentNumber", shipment.Id.ToString()));
            tokens.Add(new Token("Shipment.TrackingNumber", shipment.TrackingNumber));
            tokens.Add(new Token("Shipment.Product(s)", ProductListToHtmlTable(shipment, languageId), true));
            tokens.Add(new Token("Shipment.URLForCustomer", string.Format("{0}orderdetails/shipment/{1}", GetStoreUrl(shipment.Order.StoreId), shipment.Id), true));

            //event notification
            _eventPublisher.EntityTokensAdded(shipment, tokens);
        }

        public virtual void AddOrderNoteTokens(IList<Token> tokens, OrderNote orderNote)
        {
            tokens.Add(new Token("Order.NewNoteText", orderNote.FormatOrderNoteText(), true));

            //UNDONE: should we display a link to download an attached file (if exists)?

            //event notification
            _eventPublisher.EntityTokensAdded(orderNote, tokens);
        }

        public virtual void AddRecurringPaymentTokens(IList<Token> tokens, RecurringPayment recurringPayment)
        {
            tokens.Add(new Token("RecurringPayment.ID", recurringPayment.Id.ToString()));

            //event notification
            _eventPublisher.EntityTokensAdded(recurringPayment, tokens);
        }

        public virtual void AddGiftCardTokens(IList<Token> tokens, GiftCard giftCard)
        {
            tokens.Add(new Token("GiftCard.SenderName", giftCard.SenderName));
            tokens.Add(new Token("GiftCard.SenderEmail", giftCard.SenderEmail));
            tokens.Add(new Token("GiftCard.RecipientName", giftCard.RecipientName));
            tokens.Add(new Token("GiftCard.RecipientEmail", giftCard.RecipientEmail));
            tokens.Add(new Token("GiftCard.Amount", _priceFormatter.FormatPrice(giftCard.Amount, true, false)));
            tokens.Add(new Token("GiftCard.CouponCode", giftCard.GiftCardCouponCode));

            var giftCardMesage = !String.IsNullOrWhiteSpace(giftCard.Message) ?
                HtmlHelper.FormatText(giftCard.Message, false, true, false, false, false, false) : string.Empty;

            tokens.Add(new Token("GiftCard.Message", giftCardMesage, true));

            //event notification
            _eventPublisher.EntityTokensAdded(giftCard, tokens);
        }

        public virtual void AddCustomerTokens(IList<Token> tokens, Customer customer)
        {
            tokens.Add(new Token("Customer.Email", customer.Email));
            tokens.Add(new Token("Customer.Username", customer.Username));
            tokens.Add(new Token("Customer.FullName", customer.GetFullName()));
            tokens.Add(new Token("Customer.FirstName", customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName)));
            tokens.Add(new Token("Customer.LastName", customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName)));
            tokens.Add(new Token("Customer.VatNumber", customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber)));
            tokens.Add(new Token("Customer.VatNumberStatus", ((VatNumberStatus)customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId)).ToString()));



            //note: we do not use SEO friendly URLS because we can get errors caused by having .(dot) in the URL (from the email address)
            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            string passwordRecoveryUrl = string.Format("{0}passwordrecovery/confirm?token={1}&email={2}", GetStoreUrl(), customer.GetAttribute<string>(SystemCustomerAttributeNames.PasswordRecoveryToken), HttpUtility.UrlEncode(customer.Email));
            string accountActivationUrl = string.Format("{0}customer/activation?token={1}&email={2}", GetStoreUrl(), customer.GetAttribute<string>(SystemCustomerAttributeNames.AccountActivationToken), HttpUtility.UrlEncode(customer.Email));
            var wishlistUrl = string.Format("{0}wishlist/{1}", GetStoreUrl(), customer.CustomerGuid);
            tokens.Add(new Token("Customer.PasswordRecoveryURL", passwordRecoveryUrl, true));
            tokens.Add(new Token("Customer.AccountActivationURL", accountActivationUrl, true));
            tokens.Add(new Token("Wishlist.URLForCustomer", wishlistUrl, true));

            //event notification
            _eventPublisher.EntityTokensAdded(customer, tokens);
        }

        public virtual void AddNewsLetterSubscriptionTokens(IList<Token> tokens, NewsLetterSubscription subscription)
        {
            tokens.Add(new Token("NewsLetterSubscription.Email", subscription.Email));


            const string urlFormat = "{0}newsletter/subscriptionactivation/{1}/{2}";

            var activationUrl = String.Format(urlFormat, GetStoreUrl(), subscription.NewsLetterSubscriptionGuid, "true");
            tokens.Add(new Token("NewsLetterSubscription.ActivationUrl", activationUrl, true));

            var deActivationUrl = String.Format(urlFormat, GetStoreUrl(), subscription.NewsLetterSubscriptionGuid, "false");
            tokens.Add(new Token("NewsLetterSubscription.DeactivationUrl", deActivationUrl, true));

            //event notification
            _eventPublisher.EntityTokensAdded(subscription, tokens);
        }

        public virtual void AddProductReviewTokens(IList<Token> tokens, ProductReview productReview)
        {
            tokens.Add(new Token("ProductReview.ProductName", productReview.Product.Name));

            //event notification
            _eventPublisher.EntityTokensAdded(productReview, tokens);
        }

        public virtual void AddBlogCommentTokens(IList<Token> tokens, BlogComment blogComment)
        {
            tokens.Add(new Token("BlogComment.BlogPostTitle", blogComment.BlogPost.Title));

            //event notification
            _eventPublisher.EntityTokensAdded(blogComment, tokens);
        }

        public virtual void AddNewsCommentTokens(IList<Token> tokens, NewsComment newsComment)
        {
            tokens.Add(new Token("NewsComment.NewsTitle", newsComment.NewsItem.Title));

            //event notification
            _eventPublisher.EntityTokensAdded(newsComment, tokens);
        }

        public virtual void AddProductTokens(IList<Token> tokens, Product product, int languageId)
        {
            tokens.Add(new Token("Product.ID", product.Id.ToString()));
            tokens.Add(new Token("Product.Name", product.GetLocalized(x => x.Name, languageId)));
            tokens.Add(new Token("Product.ShortDescription", product.GetLocalized(x => x.ShortDescription, languageId), true));
            tokens.Add(new Token("Product.StockQuantity", product.GetTotalStockQuantity().ToString()));

            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            var productUrl = string.Format("{0}{1}", GetStoreUrl(), product.GetSeName());
            tokens.Add(new Token("Product.ProductURLForCustomer", productUrl, true));

            //event notification
            _eventPublisher.EntityTokensAdded(product, tokens);
        }

        public virtual void AddAttributeCombinationTokens(IList<Token> tokens, ProductVariantAttributeCombination combination, int languageId)
        {
            //attributes
            //we cannot inject IProductAttributeFormatter into constructor because it'll cause circular references.
            //that's why we resolve it here this way
            var productAttributeFormatter = EngineContext.Current.Resolve<IProductAttributeFormatter>();
            string attributes = productAttributeFormatter.FormatAttributes(combination.Product,
                combination.AttributesXml,
                _workContext.CurrentCustomer,
                renderPrices: false);

            tokens.Add(new Token("AttributeCombination.Formatted", attributes, true));
            tokens.Add(new Token("AttributeCombination.StockQuantity", combination.StockQuantity.ToString()));

            //event notification
            _eventPublisher.EntityTokensAdded(combination, tokens);
        }

        public virtual void AddForumTopicTokens(IList<Token> tokens, ForumTopic forumTopic,
            int? friendlyForumTopicPageIndex = null, int? appendedPostIdentifierAnchor = null)
        {
            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            string topicUrl;
            if (friendlyForumTopicPageIndex.HasValue && friendlyForumTopicPageIndex.Value > 1)
                topicUrl = string.Format("{0}boards/topic/{1}/{2}/page/{3}", GetStoreUrl(), forumTopic.Id, forumTopic.GetSeName(), friendlyForumTopicPageIndex.Value);
            else
                topicUrl = string.Format("{0}boards/topic/{1}/{2}", GetStoreUrl(), forumTopic.Id, forumTopic.GetSeName());
            if (appendedPostIdentifierAnchor.HasValue && appendedPostIdentifierAnchor.Value > 0)
                topicUrl = string.Format("{0}#{1}", topicUrl, appendedPostIdentifierAnchor.Value);
            tokens.Add(new Token("Forums.TopicURL", topicUrl, true));
            tokens.Add(new Token("Forums.TopicName", forumTopic.Subject));

            //event notification
            _eventPublisher.EntityTokensAdded(forumTopic, tokens);
        }

        public virtual void AddForumPostTokens(IList<Token> tokens, ForumPost forumPost)
        {
            tokens.Add(new Token("Forums.PostAuthor", forumPost.Customer.FormatUserName()));
            tokens.Add(new Token("Forums.PostBody", forumPost.FormatPostText(), true));

            //event notification
            _eventPublisher.EntityTokensAdded(forumPost, tokens);
        }

        public virtual void AddForumTokens(IList<Token> tokens, Forum forum)
        {
            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            var forumUrl = string.Format("{0}boards/forum/{1}/{2}", GetStoreUrl(), forum.Id, forum.GetSeName());
            tokens.Add(new Token("Forums.ForumURL", forumUrl, true));
            tokens.Add(new Token("Forums.ForumName", forum.Name));

            //event notification
            _eventPublisher.EntityTokensAdded(forum, tokens);
        }

        public virtual void AddPrivateMessageTokens(IList<Token> tokens, PrivateMessage privateMessage)
        {
            tokens.Add(new Token("PrivateMessage.Subject", privateMessage.Subject));
            tokens.Add(new Token("PrivateMessage.Text", privateMessage.FormatPrivateMessageText(), true));

            //event notification
            _eventPublisher.EntityTokensAdded(privateMessage, tokens);
        }

        public virtual void AddBackInStockTokens(IList<Token> tokens, BackInStockSubscription subscription)
        {
            tokens.Add(new Token("BackInStockSubscription.ProductName", subscription.Product.Name));
            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            var productUrl = string.Format("{0}{1}", GetStoreUrl(subscription.StoreId), subscription.Product.GetSeName());
            tokens.Add(new Token("BackInStockSubscription.ProductUrl", productUrl, true));

            //event notification
            _eventPublisher.EntityTokensAdded(subscription, tokens);
        }

        /// <summary>
        /// Gets list of allowed (supported) message tokens for campaigns
        /// </summary>
        /// <returns>List of allowed (supported) message tokens for campaigns</returns>
        public virtual string[] GetListOfCampaignAllowedTokens()
        {
            var allowedTokens = new List<string>
            {
                "%Store.Name%",
                "%Store.URL%",
                "%Store.Email%",
                "%Store.CompanyName%",
                "%Store.CompanyAddress%",
                "%Store.CompanyPhoneNumber%",
                "%Store.CompanyVat%",
                "%NewsLetterSubscription.Email%",
                "%NewsLetterSubscription.ActivationUrl%",
                "%NewsLetterSubscription.DeactivationUrl%"
            };
            return allowedTokens.ToArray();
        }

        public virtual string[] GetListOfAllowedTokens()
        {
            var allowedTokens = new List<string>
            {
                "%Store.Name%",
                "%Store.URL%",
                "%Store.Email%",
                "%Store.CompanyName%",
                "%Store.CompanyAddress%",
                "%Store.CompanyPhoneNumber%",
                "%Store.CompanyVat%",
                "%Order.OrderNumber%",
                "%Order.CustomerFullName%",
                "%Order.CustomerEmail%",
                "%Order.BillingFirstName%",
                "%Order.BillingLastName%",
                "%Order.BillingPhoneNumber%",
                "%Order.BillingEmail%",
                "%Order.BillingFaxNumber%",
                "%Order.BillingCompany%",
                "%Order.BillingAddress1%",
                "%Order.BillingAddress2%",
                "%Order.BillingCity%",
                "%Order.BillingStateProvince%",
                "%Order.BillingZipPostalCode%",
                "%Order.BillingCountry%",
                "%Order.BillingCustomAttributes%",
                "%Order.ShippingMethod%",
                "%Order.ShippingFirstName%",
                "%Order.ShippingLastName%",
                "%Order.ShippingPhoneNumber%",
                "%Order.ShippingEmail%",
                "%Order.ShippingFaxNumber%",
                "%Order.ShippingCompany%",
                "%Order.ShippingAddress1%",
                "%Order.ShippingAddress2%",
                "%Order.ShippingCity%",
                "%Order.ShippingStateProvince%",
                "%Order.ShippingZipPostalCode%",
                "%Order.ShippingCountry%",
                "%Order.ShippingCustomAttributes%",
                "%Order.PaymentMethod%",
                "%Order.VatNumber%",
                "%Order.Product(s)%",
                "%Order.CreatedOn%",
                "%Order.OrderURLForCustomer%",
                "%Order.NewNoteText%",
                "%Order.Total%",
                "%Order.Item(s)%",
                "%RecurringPayment.ID%",
                "%Shipment.ShipmentNumber%",
                "%Shipment.TrackingNumber%",
                "%Shipment.Product(s)%",
                "%Shipment.URLForCustomer%",
                "%Shipment.ShipInDays%",
                "%ReturnRequest.ID%",
                "%ReturnRequest.Product.Quantity%",
                "%ReturnRequest.Product.Name%",
                "%ReturnRequest.Reason%",
                "%ReturnRequest.RequestedAction%",
                "%ReturnRequest.CustomerComment%",
                "%ReturnRequest.StaffNotes%",
                "%ReturnRequest.Status%",
                "%GiftCard.SenderName%",
                "%GiftCard.SenderEmail%",
                "%GiftCard.RecipientName%",
                "%GiftCard.RecipientEmail%",
                "%GiftCard.Amount%",
                "%GiftCard.CouponCode%",
                "%GiftCard.Message%",
                "%Customer.Email%",
                "%Customer.Username%",
                "%Customer.FullName%",
                "%Customer.FirstName%",
                "%Customer.LastName%",
                "%Customer.VatNumber%",
                "%Customer.VatNumberStatus%",
                "%Customer.PasswordRecoveryURL%",
                "%Customer.AccountActivationURL%",
                "%Wishlist.URLForCustomer%",
                "%NewsLetterSubscription.Email%",
                "%NewsLetterSubscription.ActivationUrl%",
                "%NewsLetterSubscription.DeactivationUrl%",
                "%ProductReview.ProductName%",
                "%BlogComment.BlogPostTitle%",
                "%NewsComment.NewsTitle%",
                "%Product.ID%",
                "%Product.Name%",
                "%Product.ShortDescription%",
                "%Product.ProductURLForCustomer%",
                "%Product.StockQuantity%",
                "%Forums.TopicURL%",
                "%Forums.TopicName%",
                "%Forums.PostAuthor%",
                "%Forums.PostBody%",
                "%Forums.ForumURL%",
                "%Forums.ForumName%",
                "%AttributeCombination.Formatted%",
                "%AttributeCombination.StockQuantity%",
                "%PrivateMessage.Subject%",
                "%PrivateMessage.Text%",
                "%BackInStockSubscription.ProductName%",
                "%BackInStockSubscription.ProductUrl%",
                "%Rebate.Amount%",
                "%Rebate.CouponCode%",
                "%ProductReview.ReviewUrl%",
                "%ProductReview.ProductPictureUrl%",
                "%ProductReview.ManufacturerPictureUrl%",
                "%Shipment.ETA%",
                "%Topic.AboutUs%",
                "%Topic.PrivacyNotice%",
                "%Topic.ContactUs%",
                "%Topic.MyAccount%",
                "%Topic.SalesTax%",
                "%SalesQuote.Id%",
                "%SalesQuote.CustomerName%",
                "%SalesQuote.Email%",
                "%SalesQuote.Product(s)%",
                "%SalesQuote.RestoreLink%",
                "%Common.CurrentYear%",
                "%Links.CancelOrderItems%",
                "%Links.NewsLetterSubcription%"
            };

            return allowedTokens.ToArray();
        }

        public void AddOrderWithRebatesTokens(IList<Token> tokens, OrderWithRebates orderWithRebates)
        {
            tokens.Add(new Token("Rebate.Amount", _priceFormatter.FormatPrice(orderWithRebates.RebateAmount, true, false)));
            tokens.Add(new Token("Rebate.CouponCode", orderWithRebates.CouponCode));
            tokens.Add(new Token("Customer.FullName", orderWithRebates.CustomerFullName));

            //event notification
            _eventPublisher.EntityTokensAdded(orderWithRebates, tokens);
        }

        public void AddOrderProductToReviewTokens(IList<Token> tokens, OrderProductToReview orderProductToReview)
        {
            tokens.Add(new Token("Customer.FullName", orderProductToReview.CustomerFullName));
            tokens.Add(new Token("ProductReview.ProductName", orderProductToReview.ProductName));
            tokens.Add(new Token("ProductReview.ReviewUrl", string.Format("{0}productreviews/{1}?utm_source=reviewrequest&utm_medium=email&utm_campaign=reviewrequest", GetStoreUrl(), orderProductToReview.ProductId)));
            tokens.Add(new Token("ProductReview.ProductPictureUrl", string.Format("{0}imageloader/{1}", GetStoreUrl(), orderProductToReview.ProductId)));

            var manufacturerPicture = _pictureService.GetPictureById(orderProductToReview.ManufacturerPictureId);
            tokens.Add(new Token("ProductReview.ManufacturerPictureUrl", _pictureService.GetWidthHeightPictureUrl(manufacturerPicture, 128, 30)));

            //event notification
            _eventPublisher.EntityTokensAdded(orderProductToReview, tokens);
        }

        public void AddSalesQuoteTokens(IList<Token> tokens, SalesQuote quote)
        {
            var subTotal = this._priceFormatter.FormatPrice(quote.Lines.Sum(m => m.UnitPrice * m.Quantity));
            tokens.Add(new Token("SalesQuote.Id", quote.Id.ToString(CultureInfo.InvariantCulture)));
            tokens.Add(new Token("SalesQuote.Email", quote.Email));
            tokens.Add(new Token("SalesQuote.CustomerFullName", quote.CustomerName));
            tokens.Add(new Token("SalesQuote.Product(s)", this.ProductListToHtmlTable(quote), true));
            tokens.Add(new Token("SalesQuote.SubTotal", subTotal));
#if DEBUG
            tokens.Add(new Token("SalesQuote.RestoreLink", $"http://thmotorsports.localhost:18888/quote/restore/{quote.Id}/{quote.ComputeHash()}"));
#else
            tokens.Add(new Token("SalesQuote.RestoreLink", $"{GetStoreUrl()}quote/restore/{quote.Id}/{quote.ComputeHash()}"));
#endif
        }

        public void AddOrderShipmentEtaTokens(IList<Token> tokens, OrderShipmentEta orderShipmentEta)
        {
            tokens.Add(new Token("Customer.FullName", orderShipmentEta.CustomerFullName));
            tokens.Add(new Token("Order.OrderNumber", orderShipmentEta.OrderId.ToString()));
            tokens.Add(new Token("Shipment.ETA", orderShipmentEta.ShipmentEta.ToShortDateString()));

            //event notification
            _eventPublisher.EntityTokensAdded(orderShipmentEta, tokens);
        }

        public void AddTopicTokens(IList<Token> tokens, int storeId)
        {
            var topicCacheKey = string.Format(TOPIC_TOP_MENU_MODEL_KEY,
                this._workContext.WorkingLanguage.Id, storeId);

            var topics = this.cacheManager.Get(topicCacheKey, () =>
                this.topicService.GetAllTopics(storeId)
            );

            tokens.Add(new Token("Topic.AboutUs", topics.Single(m => m.SystemName.Contains("AboutUs")).GetSeName()));
            tokens.Add(new Token("Topic.PrivacyNotice", topics.Single(m => m.SystemName.Contains("PrivacyInfo")).GetSeName()));
            tokens.Add(new Token("Topic.SalesTax", topics.Single(m => m.SystemName.Contains("Sales Tax")).GetSeName()));
            tokens.Add(new Token("Topic.ContactUs", topics.Single(m => m.SystemName.Contains("ContactUs")).GetSeName()));
        }



        protected virtual string ProductListToHtmlTable(SalesQuote quote, int languageId = 0)
        {
            var result = string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("<table style=\"border-collapse: collapse; width: 750px; margin: 0; padding: 0; border: 0; \">");
            sb.AppendLine("<tr style=\"margin: 0; padding: 0; \">");
            sb.AppendLine("<td style=\"font-family: arial, sans-serif; font-size: 11pt; text-align: left; width: 80px; margin: 0; padding: 10px; border: 1px solid black;\" align=\"left\">Item Name</td>");
            sb.AppendLine("<td style=\"font-family: arial, sans-serif; font-size: 11pt; text-align: center; width: 80px; margin: 0; padding: 10px; border: 1px solid black;\" align=\"center\">Quantity</td>");
            sb.AppendLine("<td style=\"font-family: arial, sans-serif; font-size: 11pt; text-align: center; width: 80px; margin: 0; padding: 10px; border: 1px solid black;\" align=\"center\">Total Price</td>");
            sb.AppendLine("</tr>");

            var table = quote.Lines.ToList();
            for (var i = 0; i <= table.Count - 1; i++)
            {
                var line = table[i];
                var product = line.Product;
                if (product == null)
                {
                    continue;
                }

                sb.AppendLine("<tr style=\"margin: 0; padding: 0;\">");
                sb.AppendLine($"<td style=\"font-family: arial, sans-serif; font-size: 11pt; text-align: left; margin: 0; padding: 10px; border: 1px solid black;\" align=\"left\">{HttpUtility.HtmlEncode(product.GetLocalized(m => m.Name, languageId))}</td>");
                sb.AppendLine($"<td style=\"font-family: arial, sans-serif; font-size: 11pt; text-align: center; margin: 0; padding: 10px; border: 1px solid black;\" align=\"center\">{line.Quantity}</td>");
                sb.AppendLine($"<td style=\"font-family: arial, sans-serif; font-size: 11pt; text-align: center; margin: 0; padding: 10px; border: 1px solid black;\" align=\"center\">{this._priceFormatter.FormatPrice(line.UnitPrice)}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            result = sb.ToString();
            return result;
        }

        protected virtual string OrderItemsToListTable(OrderItem[] orderItems, Store store, int languageId = 0)
        {
            var result = string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"details\" style=\"border-top: 1px solid rgb(231,229,230);border-bottom: 1px solid rgb(231,229,230);\">");

            for (var i = 0; i < orderItems.Length; i++)
            {
                var item = orderItems[i];
                var product = item.Product;
                if (product == null)
                {
                    continue;
                }

                sb.AppendLine("<div style=\"display: flex;padding: 20px;flex-direction: row;\">");

                var defaultProductPicture = this._pictureService.GetPicturesByProductId(item.ProductId, 1).FirstOrDefault();
                var thumbUrl = this._pictureService.GetPictureUrl(defaultProductPicture, 250, true);

                sb.AppendLine($"<img src=\"{thumbUrl}\" style=\"width:110px;height:110px;\" >");
                sb.AppendLine($"<a href=\"{store.SecureUrl}{this.urlRecordService.GetActiveSlug(item.ProductId, "Product", languageId)}\" style=\"display: block; margin: 0 auto; padding: 25px 0 0 0; flex: 0 1 30%; text-decoration:none; color: rgb(161,155,155);\">{HttpUtility.HtmlEncode(product.GetLocalized(m => m.Name, languageId))}</a>");
                sb.AppendLine($"<div style=\"margin: 0 auto; padding: 25px 0 0 0;\">{this._priceFormatter.FormatPrice(product.Price)}</div>");
                sb.AppendLine($"<div style=\"margin: 0 auto; padding: 25px 0 0 0;\">{item.Quantity}x</div>");
                sb.AppendLine($"<div style=\"margin: 0 auto; padding: 25px 0 0 0;\">{this._priceFormatter.FormatPrice(product.Price * item.Quantity)}</div>");

                sb.AppendLine("</div>");
            }

            sb.AppendLine("</div>");
            result = sb.ToString();
            return result;
        }

        public void AddShipmentDelayedTokens(IList<Token> tokens, OrderItem[] orderItems, CrmSalesOrder order, int shipInDays, Store store, string email)
        {
            tokens.Add(new Token("Shipment.ShipInDays", shipInDays.ToString()));
            tokens.Add(new Token("Order.Item(s)", this.OrderItemsToListTable(orderItems, store), true));
            tokens.Add(new Token("Common.CurrentYear", DateTime.UtcNow.Year.ToString()));
            tokens.Add(new Token("Links.CancelOrderItems", $"{store.SecureUrl}return/request/{order.Id}"));

            var subscription = this.newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(email, store.Id);
            tokens.Add(new Token("Links.NewsLetterSubcription", subscription == null ? "Subscribe" : "Unsubscribe"));
        }

        #region JsonDataProcessing

        public virtual void AddOrderData(Order order, DynamicTemplateData data, int languageId, int vendorId = 0)
        {
            data.OrderNumber = order.Id.ToString();
            data.CustomerFullName = order.BillingAddress.GetCustomerFullName();
            data.OrderCustomerEmail = order.BillingAddress.Email;
            data.OrderBillingFirstName = order.BillingAddress.FirstName;

            data.OrderBillingFirstName = order.BillingAddress.FirstName;
            data.OrderBillingLastName = order.BillingAddress.LastName;
            data.OrderBillingPhoneNumber = order.BillingAddress.PhoneNumber;
            data.OrderBillingEmail = order.BillingAddress.Email;
            data.OrderBillingFaxNumber = order.BillingAddress.FaxNumber;
            data.OrderBillingCompany = order.BillingAddress.Company;
            data.OrderBillingAddress1 = order.BillingAddress.Address1;
            data.OrderBillingAddress2 = order.BillingAddress.Address2;
            data.OrderBillingCity = order.BillingAddress.City;
            data.OrderBillingStateProvince = order.BillingAddress.StateProvince != null ? order.BillingAddress.StateProvince.GetLocalized(x => x.Name) : " ";
            data.OrderBillingZipPostalCode = order.BillingAddress.ZipPostalCode;
            data.OrderBillingCountry = order.BillingAddress.Country != null ? order.BillingAddress.Country.GetLocalized(x => x.Name) : " ";
            data.OrderBillingCustomAttributes = _addressAttributeFormatter.FormatAttributes(order.BillingAddress.CustomAttributes);

            data.OrderShippingMethod = order.ShippingMethod;
            data.OrderShippingFirstName = order.ShippingAddress != null ? order.ShippingAddress.FirstName : " ";
            data.OrderShippingLastName = order.ShippingAddress != null ? order.ShippingAddress.LastName : " ";
            data.OrderShippingPhoneNumber = order.ShippingAddress != null ? order.ShippingAddress.PhoneNumber : " ";
            data.OrderShippingEmail = order.ShippingAddress != null ? order.ShippingAddress.Email : " ";
            data.OrderShippingFaxNumber = order.ShippingAddress != null ? order.ShippingAddress.FaxNumber : " ";
            data.OrderShippingCompany = order.ShippingAddress != null ? order.ShippingAddress.Company : " ";
            data.OrderShippingAddress1 = order.ShippingAddress != null ? order.ShippingAddress.Address1 : " ";
            data.OrderShippingAddress2 = order.ShippingAddress != null ? order.ShippingAddress.Address2 : " ";
            data.OrderShippingCity = order.ShippingAddress != null ? order.ShippingAddress.City : " ";
            data.OrderShippingStateProvince = order.ShippingAddress != null && order.ShippingAddress.StateProvince != null ? order.ShippingAddress.StateProvince.GetLocalized(x => x.Name) : " ";
            data.OrderShippingZipPostalCode = order.ShippingAddress != null ? order.ShippingAddress.ZipPostalCode : " ";
            data.OrderShippingCountry = order.ShippingAddress != null && order.ShippingAddress.Country != null ? order.ShippingAddress.Country.GetLocalized(x => x.Name) : " ";
            data.OrderShippingCustomAttributes = _addressAttributeFormatter.FormatAttributes(order.ShippingAddress != null ? order.ShippingAddress.CustomAttributes : " ");
            
            var paymentMethod = this._paymentService.LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
            var paymentMethodName = paymentMethod != null 
                ? paymentMethod.GetLocalizedFriendlyName(_localizationService, _workContext.WorkingLanguage.Id) 
                : order.PaymentMethodSystemName;

            data.OrderPaymentMethod = paymentMethodName;
            data.OrderVatNumber = order.VatNumber;

            if (data.Products.Count == 0)
            {
                this.AddProductsData(order, data, languageId, vendorId);
            }

            var language = this._languageService.GetLanguageById(languageId);
            if (language != null && !string.IsNullOrEmpty(language.LanguageCulture))
            {
                DateTime createdOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, TimeZoneInfo.Utc, _dateTimeHelper.GetCustomerTimeZone(order.Customer));
                data.OrderCreatedOn = createdOn.ToString("MM/dd/yyyy", new CultureInfo(language.LanguageCulture));
            }
            else
            {
                data.OrderCreatedOn = order.CreatedOnUtc.ToString("D");
            }

            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)

            //data += "'OrderURLForCustomer': '" + string.Format("{0}orderdetails/{1}", GetStoreUrl(order.StoreId), order.Id) + "',";
           
            var hash = Sha256Hash($"{order.Id}{order.CreatedOnUtc.Ticks}");
            string orderUrl = string.Format($"{GetStoreUrl(order.StoreId)}orderdetails/{order.Id}/{hash}");

            // UPDATED URL WITH CHECK ORDER WITH PASS ORDER ID AND SHIPPING ZIP CODE
            data.OrderURLForCustomer = orderUrl;
            //data += $"'OrderURLForCustomer': '{string.Format(" GetStoreUrl(order.StoreId), order.Id)}',";
            data.OrderTotal = this._priceFormatter.FormatPrice(order.OrderTotal, true, false);

            //event notification
            // TODO: Add Event Publisher
            //_eventPublisher.EntityTokensAdded(order, tokens);
        }

        public virtual void AddManualOrderData(CrmSalesOrder order, DynamicTemplateData data, int languageId)
        {
            data.OrderNumber = order.Number.ToString();
            data.CustomerFullName = order.BillingAddress.FirstName + ' ' + order.BillingAddress.LastName;
            data.OrderCustomerEmail = order.BillingAddress.Email;
            data.OrderBillingFirstName = order.BillingAddress.FirstName;

            data.OrderBillingFirstName = order.BillingAddress.FirstName;
            data.OrderBillingLastName = order.BillingAddress.LastName;
            data.OrderBillingPhoneNumber = order.BillingAddress.Phone;
            data.OrderBillingEmail = order.BillingAddress.Email;
            data.OrderBillingCompany = order.BillingAddress.Company;
            data.OrderBillingAddress1 = order.BillingAddress.Line1;
            data.OrderBillingAddress2 = order.BillingAddress.Line2;
            data.OrderBillingCity = order.BillingAddress.City;
            data.OrderBillingStateProvince = order.BillingAddress.State != null ? order.BillingAddress.State.GetLocalized(x => x.Name) : " ";
            data.OrderBillingZipPostalCode = order.BillingAddress.Zip;
            data.OrderBillingCountry = order.BillingAddress.Country != null ? order.BillingAddress.Country.GetLocalized(x => x.Name) : " ";

            data.OrderShippingFirstName = order.ShippingAddress != null ? order.ShippingAddress.FirstName : " ";
            data.OrderShippingLastName = order.ShippingAddress != null ? order.ShippingAddress.LastName : " ";
            data.OrderShippingPhoneNumber = order.ShippingAddress != null ? order.ShippingAddress.Phone : " ";
            data.OrderShippingEmail = order.ShippingAddress != null ? order.ShippingAddress.Email : " ";
            data.OrderShippingCompany = order.ShippingAddress != null ? order.ShippingAddress.Company : " ";
            data.OrderShippingAddress1 = order.ShippingAddress != null ? order.ShippingAddress.Line1 : " ";
            data.OrderShippingAddress2 = order.ShippingAddress != null ? order.ShippingAddress.Line2 : " ";
            data.OrderShippingCity = order.ShippingAddress != null ? order.ShippingAddress.City : " ";
            data.OrderShippingStateProvince = order.ShippingAddress != null && order.ShippingAddress.State != null ? order.ShippingAddress.State.GetLocalized(x => x.Name) : " ";
            data.OrderShippingZipPostalCode = order.ShippingAddress != null ? order.ShippingAddress.Zip : " ";
            data.OrderShippingCountry = order.ShippingAddress != null && order.ShippingAddress.Country != null ? order.ShippingAddress.Country.GetLocalized(x => x.Name) : " ";
            
            this.AddSalesOrderLineData(order, data, languageId);

            var language = this._languageService.GetLanguageById(languageId);
            if (language != null && !string.IsNullOrEmpty(language.LanguageCulture))
            {
                data.OrderCreatedOn = order.CreatedOn.ToString("MM/dd/yyyy", new CultureInfo(language.LanguageCulture));
            }
            else
            {
                data.OrderCreatedOn = order.CreatedOn.ToString("D");
            }

            int orderId = 0;
            var result = int.TryParse(order.Number, out orderId);
            var nopOrder = this._orderService.GetOrderById(orderId);
            var hash = Sha256Hash($"{order.Id}-{order.Number}-{order.CreatedOn.ToUniversalTime().Ticks}");
            var store = this._storeService.GetStoreById((int)NopStore.Autoplicity);
            string orderUrl = string.Format($"{GetStoreUrl(store.Id)}orderinfo/{order.Id}/{HttpUtility.UrlEncode(hash)}");

            data.OrderURLForCustomer = orderUrl;
        }

        public virtual void AddSalesOrderData(CrmSalesOrder order, DynamicTemplateData data, int languageId)
        {
            data.OrderNumber = order.Number;
            //data.CustomerFullName = order.BillingAddress.GetCustomerFullName();
            //data.OrderCustomerEmail = order.BillingAddress.Email;
            //data.OrderBillingFirstName = order.BillingAddress.FirstName;

            //data.OrderBillingFirstName = order.BillingAddress.FirstName;
            //data.OrderBillingLastName = order.BillingAddress.LastName;
            //data.OrderBillingPhoneNumber = order.BillingAddress.PhoneNumber;
            //data.OrderBillingEmail = order.BillingAddress.Email;
            //data.OrderBillingFaxNumber = order.BillingAddress.FaxNumber;
            //data.OrderBillingCompany = order.BillingAddress.Company;
            //data.OrderBillingAddress1 = order.BillingAddress.Address1;
            //data.OrderBillingAddress2 = order.BillingAddress.Address2;
            //data.OrderBillingCity = order.BillingAddress.City;
            //data.OrderBillingStateProvince = order.BillingAddress.StateProvince != null ? order.BillingAddress.StateProvince.GetLocalized(x => x.Name) : " ";
            //data.OrderBillingZipPostalCode = order.BillingAddress.ZipPostalCode;
            //data.OrderBillingCountry = order.BillingAddress.Country != null ? order.BillingAddress.Country.GetLocalized(x => x.Name) : " ";
            //data.OrderBillingCustomAttributes = _addressAttributeFormatter.FormatAttributes(order.BillingAddress.CustomAttributes);

            //data.OrderShippingMethod = order.ShippingMethod;
            data.OrderShippingFirstName = order.ShippingAddress != null ? order.ShippingAddress.FirstName : " ";
            //data.OrderShippingLastName = order.ShippingAddress != null ? order.ShippingAddress.LastName : " ";
            //data.OrderShippingPhoneNumber = order.ShippingAddress != null ? order.ShippingAddress.PhoneNumber : " ";
            //data.OrderShippingEmail = order.ShippingAddress != null ? order.ShippingAddress.Email : " ";
            //data.OrderShippingFaxNumber = order.ShippingAddress != null ? order.ShippingAddress.FaxNumber : " ";
            //data.OrderShippingCompany = order.ShippingAddress != null ? order.ShippingAddress.Company : " ";
            //data.OrderShippingAddress1 = order.ShippingAddress != null ? order.ShippingAddress.Address1 : " ";
            //data.OrderShippingAddress2 = order.ShippingAddress != null ? order.ShippingAddress.Address2 : " ";
            //data.OrderShippingCity = order.ShippingAddress != null ? order.ShippingAddress.City : " ";
            //data.OrderShippingStateProvince = order.ShippingAddress != null && order.ShippingAddress.StateProvince != null ? order.ShippingAddress.StateProvince.GetLocalized(x => x.Name) : " ";
            //data.OrderShippingZipPostalCode = order.ShippingAddress != null ? order.ShippingAddress.ZipPostalCode : " ";
            //data.OrderShippingCountry = order.ShippingAddress != null && order.ShippingAddress.Country != null ? order.ShippingAddress.Country.GetLocalized(x => x.Name) : " ";
            //data.OrderShippingCustomAttributes = _addressAttributeFormatter.FormatAttributes(order.ShippingAddress != null ? order.ShippingAddress.CustomAttributes : " ");

            //var paymentMethod = this._paymentService.LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
            //var paymentMethodName = paymentMethod != null
            //    ? paymentMethod.GetLocalizedFriendlyName(_localizationService, _workContext.WorkingLanguage.Id)
            //    : order.PaymentMethodSystemName;


            //data.OrderPaymentMethod = paymentMethodName;
            //data.OrderVatNumber = order.VatNumber;

            this.AddSalesOrderLineData(order, data, languageId);

            //var language = this._languageService.GetLanguageById(languageId);
            //if (language != null && !string.IsNullOrEmpty(language.LanguageCulture))
            //{
            //    DateTime createdOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, TimeZoneInfo.Utc, _dateTimeHelper.GetCustomerTimeZone(order.Customer));
            //    data.OrderCreatedOn = createdOn.ToString("MM/dd/yyyy", new CultureInfo(language.LanguageCulture));
            //}
            //else
            //{
            //    data.OrderCreatedOn = order.CreatedOnUtc.ToString("D");
            //}

            ////TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)

            //data += "'OrderURLForCustomer': '" + string.Format("{0}orderdetails/{1}", GetStoreUrl(order.StoreId), order.Id) + "',";
            int orderId = 0;
            var result = int.TryParse(order.Number, out orderId);
            var nopOrder = this._orderService.GetOrderById(orderId);
            var hash = Sha256Hash($"{order.Id}-{order.Number}-{order.CreatedOn.ToUniversalTime().Ticks}");
            var store = this._storeService.GetStoreById((int)NopStore.Autoplicity);
            string orderUrl = string.Format($"{GetStoreUrl(store.Id)}orderinfo/{order.Id}/{HttpUtility.UrlEncode(hash)}");

            data.OrderURLForCustomer = orderUrl;
        }
        
        public virtual void AddOrderCancelData(Order order, DynamicTemplateData data, int languageId, int vendorId = 0)
        {
            var defaultCancelReason = "an inventory issue. As a result you will not be charged, but you may see a temporary charge that is being reversed";
            var orderCancellationReason = this._orderService.GetCancellationCustomerReason(order);
            data.OrderCancellationReason = orderCancellationReason != null ? orderCancellationReason.CancelReasonNameForEmailNotification : defaultCancelReason;
        }

        public virtual void AddOrderCancelData(CrmSalesOrder order, DynamicTemplateData data, int languageId, int vendorId = 0)
        {
            var defaultCancelReason = "an inventory issue. As a result you will not be charged, but you may see a temporary charge that is being reversed";
            var orderCancellationReason = this._orderService.GetCancellationCustomerReason(order);
            data.OrderCancellationReason = orderCancellationReason != null ? orderCancellationReason.CancelReasonNameForEmailNotification : defaultCancelReason;
        }

        private static string Sha256Hash(string value)
        {
            return string.Concat(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)).Select(i => i.ToString("X2")));
        }

        protected virtual void AddSalesOrderLineData(CrmSalesOrder order, DynamicTemplateData data, int languageId)
        {
            var result = new List<Asu.Services.Messages.SendGridProduct>();
            var language = this._languageService.GetLanguageById(languageId);

            #region Products

            foreach (var orderLine in order.Lines)
            {
                var product = orderLine.Product;
                string imageUrl;
                var productName = string.Empty;
                var productLink = string.Empty;
                var store = this._storeService.GetStoreById((int)NopStore.Autoplicity);

                if (product != null)
                {
                    var productPicture = this._pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();

                    if (productPicture == null)
                    {
                        imageUrl = !string.IsNullOrEmpty(this._pictureService.GetProductAdditionalImageName(product.Id))
                            ? $"{this._webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                            : $"{this._webHelper.GetStoreImagesLocation()}content/images/{this._storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
                    }
                    else
                    {
                        imageUrl = this._pictureService.GetPictureUrl(productPicture, 250, true);
                    }

                    productName = product.GetLocalized(x => x.Name, languageId);
                    productLink = $"{store.Url}{product.GetSeName()}";
                }
                else
                {
                    imageUrl = $"{this._webHelper.GetStoreImagesLocation()}content/images/{this._storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
                    productName = orderLine.Description;
                }

                var currency = this._currencyService.GetAllCurrencies().FirstOrDefault();
                var priceInCustomerCurrency = _currencyService.ConvertCurrency(orderLine.UnitPrice * orderLine.Quantity, currency.Rate);
                var priceStr = this._priceFormatter.FormatPrice(priceInCustomerCurrency, true, currency.CurrencyCode, language, true);
                
                var sendGridProduct = new SendGridProduct
                {
                    ImageUrl = imageUrl,
                    Link = productLink,
                    Name = productName,
                    Quantity = orderLine.Quantity,
                    Price = priceStr
                };

                data.Products.Add(sendGridProduct);
            }

            #endregion

            #region Payments

            decimal shipping = 0;
            decimal tax = 0;
            decimal subTotal = 0;
            decimal discount = 0;
            foreach (var p in order.Payments)
            {
                var paymentShipping = p.Charges.Where(x => x.Type == SalesPaymentChargeType.Shipping).FirstOrDefault();
                shipping += paymentShipping != null ? paymentShipping.Amount : 0;

                var paymentTax = p.Charges.Where(x => x.Type == SalesPaymentChargeType.Tax).FirstOrDefault();
                tax += paymentTax != null ? paymentTax.Amount : 0;

                var paymentSubtotal = p.Charges.Where(x => x.Type == SalesPaymentChargeType.Subtotal).FirstOrDefault();
                subTotal += paymentSubtotal != null ? paymentSubtotal.Amount : 0;

                var paymentDiscount = p.Charges.Where(x => x.Type == SalesPaymentChargeType.Discount).FirstOrDefault();
                discount += paymentDiscount != null ? paymentDiscount.Amount : 0;
            }

            data.SubTotal = subTotal.ToString();
            data.ShipTotal = shipping.ToString();
            data.TaxTotal = tax.ToString();
            data.Discount = discount.ToString();
            data.OrderTotal = (subTotal + shipping + tax - discount).ToString();
            #endregion Payments
        }

        public virtual void AddShipmentData(Shipment shipment, DynamicTemplateData data, int languageId)
        {
            data.ShipmentShipmentNumber = shipment.Id.ToString();
            data.ShipmentTrackingNumber = shipment.TrackingNumber;
            data.ShipmentURLForCustomer = $"{GetStoreUrl(shipment.Order.StoreId)}orderdetails/shipment/{shipment.Id}";
            data.ProductReviewSourceName = GetProductReviewSource(shipment.Order.StoreId);
            data.ProductReviewLink = this.GetProductReviewLink(data.ProductReviewSourceName, shipment.Order.StoreId);
            data.Products.AddRange(this.GetShipmentProducts(shipment, languageId));
        }

        private string GetProductReviewLink(string sourceName, int storeId)
        {
            string result = string.Empty;
            var store = this._storeService.GetStoreById(storeId);
            switch (sourceName)
            {
                case "Google":
                    var settingService = EngineContext.Current.Resolve<ISettingService>();
                    var googleMapsSettings = settingService.LoadSetting<GoogleMapsSettings>(storeId);
                    result = $"https://search.google.com/local/writereview?placeid={googleMapsSettings.PlaceId}";
                    break;
                case "Reseller Ratings":
                    result = $"https://www.resellerratings.com/store/{store.CompanyName.ToLowerInvariant()}";
                    break;
                case "Trustpilot":
                    result = $"https://trustpilot.com/evaluate/{store.Name.ToLowerInvariant()}";
                    break;
            }

            return result;
        }

        private string GetProductReviewSource(int storeId)
        {
            var sources = new[] { "Google", "Reseller Ratings", "Trustpilot" };
            var randomizer = new Random();

            return storeId != (int)NopStore.Autoplicity && storeId != (int)NopStore.Thmotorsports ? sources.First() : sources[randomizer.Next(0, 1)];
        }

        public virtual void AddManualOrderShipmentData(CrmShipment crmShipment, int crmSalesOrderId, int storeId, DynamicTemplateData data, int languageId)
        {
            data.ShipmentShipmentNumber = crmShipment.Id.ToString();
            data.ShipmentTrackingNumber = crmShipment.TrackingNumber;
            data.ShipmentURLForCustomer = $"{GetStoreUrl(storeId)}shipment/{crmSalesOrderId}/{crmShipment.Id}";
        }

        public virtual void AddStoreData(Store store, DynamicTemplateData data, string fromEmail)
        {
            data.StoreName = store.GetLocalized(x => x.Name);
            data.StoreURL = store.SecureUrl;
            data.StoreEmail = fromEmail;
            data.StoreCompanyName = store.CompanyName;
            data.StoreCompanyAddress = store.CompanyAddress;
            data.StoreCompanyPhoneNumber = store.CompanyPhoneNumber;
            data.StoreCompanyVat = store.CompanyVat;

            //topics
            this.AddTopicData(store.Id, data);
        }

        public void AddTopicData(int storeId, DynamicTemplateData data)
        {
            var topicCacheKey = string.Format(TOPIC_TOP_MENU_MODEL_KEY,
                this._workContext.WorkingLanguage.Id, storeId);

            var topics = this.cacheManager.Get(topicCacheKey, () =>
                this.topicService.GetAllTopics(storeId)
            );

            data.TopicAboutUs = topics.Single(m => m.SystemName.Contains("AboutUs")).GetSeName();
            data.TopicPrivacyNotice = topics.Single(m => m.SystemName.Contains("PrivacyInfo")).GetSeName();
            data.TopicSalesTax = topics.Single(m => m.SystemName.Contains("Sales Tax")).GetSeName();
            data.TopicContactUs = topics.Single(m => m.SystemName.Contains("ContactUs")).GetSeName();
        }

        public virtual void AddCustomerData(Customer customer, DynamicTemplateData data)
        {
            data.CustomerEmail = customer.Email;
            data.CustomerUsername = customer.Username;
            data.CustomerFullName = customer.GetFullName();
            data.CustomerFirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
            data.CustomerLastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
            data.CustomerVatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);
            data.CustomerVatNumberStatus = (VatNumberStatus)customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId);

            //note: we do not use SEO friendly URLS because we can get errors caused by having .(dot) in the URL (from the email address)
            //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
            string passwordRecoveryUrl = string.Format("{0}passwordrecovery/confirm?token={1}&email={2}", GetStoreUrl(), customer.GetAttribute<string>(SystemCustomerAttributeNames.PasswordRecoveryToken), HttpUtility.UrlEncode(customer.Email));
            string accountActivationUrl = string.Format("{0}customer/activation?token={1}&email={2}", GetStoreUrl(), customer.GetAttribute<string>(SystemCustomerAttributeNames.AccountActivationToken), HttpUtility.UrlEncode(customer.Email));

            data.CustomerPasswordRecoveryURL = passwordRecoveryUrl;
            data.CustomerAccountActivationURL = accountActivationUrl;
            data.WishlistURLForCustomer = $"{GetStoreUrl()}wishlist/{customer.CustomerGuid}";
        }

        public void AddProductRecommendationsData(Order order, DynamicTemplateData data, int[] incomingProductIds, int productsCount, int languageId)
        {
            var recommendedProducts = new List<RecommendedProduct>();
            if (order == null)
            {
                return;
            }

            List<int> newProductIdsList = new List<int>();
            if (incomingProductIds != null && incomingProductIds.Length > 0)
            {
                foreach (var item in incomingProductIds)
                {
                    var additionalProductIds = this._productRecommendationService.GetProductsRecommendationIds(_storeContext.CurrentStore.Id, item);

                    additionalProductIds = additionalProductIds.Except(newProductIdsList).ToArray();
                    newProductIdsList.AddRange(additionalProductIds.Except(incomingProductIds));
                }
            }

            var productService = EngineContext.Current.Resolve<IProductService>();
            var productIds = newProductIdsList.ToArray();
            if (!productIds.Any() || productIds.Length < productsCount)
            {
                var bestSellers = productService.GetBestSellerProducts(productsCount);
                productIds = productIds.Union(bestSellers.Select(p => p.Id)).Take(productsCount).ToArray();
            }

            var products = productService.GetProductsByIds(productIds, true).Take(productsCount);
            foreach (var product in products)
            {
                if (product == null)
                {
                    continue;
                }
                    
                var recommendedProduct = new RecommendedProduct
                {
                    ProductName = product.GetLocalized(x => x.Name, languageId),
                    Price = this._priceFormatter.FormatPrice(product.Price, true, order.CustomerCurrencyCode, this._languageService.GetLanguageById(languageId), false)
                };

                Picture productPicture = this._pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
                string imageUrl;
                if (productPicture == null)
                {
                    imageUrl = !string.IsNullOrEmpty(_pictureService.GetProductAdditionalImageName(product.Id))
                        ? $"{this._webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                        : $"{this._webHelper.GetStoreImagesLocation()}content/images/{this._storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
                }
                else
                {
                    imageUrl = _pictureService.GetPictureUrl(productPicture);
                }

                recommendedProduct.ProductLink = $"{this.GetStoreUrl(order.StoreId)}{product.GetSeName()}";
                recommendedProduct.ImgLink = imageUrl;
                data.RecommendedProducts.Add(recommendedProduct);
            }
        }

        public void AddProductBackInStockData(BackInStockSubscription backInStockSubscription, DynamicTemplateData data, int languageId)
        {
            var product = backInStockSubscription.Product;

            var productPicture = this._pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
            string imageUrl;
            if (productPicture == null)
            {
                imageUrl = !string.IsNullOrEmpty(this._pictureService.GetProductAdditionalImageName(product.Id))
                    ? $"{this._webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                    : $"{this._webHelper.GetStoreImagesLocation()}content/images/{this._storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
            }
            else
            {
                imageUrl = this._pictureService.GetPictureUrl(productPicture, 250, true);
            }

            var store = this._storeService.GetStoreById(backInStockSubscription.StoreId);
            var sendGridProduct = new SendGridProduct
            {
                ImageUrl = imageUrl,
                Link = $"{store.Url}{product.GetSeName()}",
                Name = product.GetLocalized(x => x.Name, languageId)
            };

            //sku
            if (!string.IsNullOrEmpty(product.Sku))
            {
                sendGridProduct.Sku = product.Sku;
            }

            data.Products.Add(sendGridProduct);
        }

        public void AddProductBackInStockRecommendationsData(BackInStockSubscription backInStockSubscription, DynamicTemplateData data, int productsCount, int languageId)
        {
            var recommendedProducts = new List<RecommendedProduct>();
            if (backInStockSubscription == null)
            {
                return;
            }

            var incomingProductIds = backInStockSubscription.ProductId;

            var additionalProductIds = this._productRecommendationService.GetProductsRecommendationIds(_storeContext.CurrentStore.Id, incomingProductIds);
            var newProductIdsList = additionalProductIds.Where(x => x != incomingProductIds).ToList();

            var productService = EngineContext.Current.Resolve<IProductService>();
            var productIds = newProductIdsList.ToArray();
            if (!productIds.Any() || productIds.Length < productsCount)
            {
                var bestSellers = productService.GetBestSellerProducts(productsCount);
                productIds = productIds.Union(bestSellers.Select(p => p.Id)).Take(productsCount).ToArray();
            }

            var products = productService.GetProductsByIds(productIds, true).Take(productsCount);
            foreach (var product in products)
            {
                if (product == null)
                {
                    continue;
                }

                var recommendedProduct = new RecommendedProduct
                {
                    ProductName = product.GetLocalized(x => x.Name, languageId),
                    Price = this._priceFormatter.FormatPrice(product.Price, true, "USD", this._languageService.GetLanguageById(languageId), false)
                };

                Picture productPicture = this._pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
                string imageUrl;
                if (productPicture == null)
                {
                    imageUrl = !string.IsNullOrEmpty(_pictureService.GetProductAdditionalImageName(product.Id))
                        ? $"{this._webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                        : $"{this._webHelper.GetStoreImagesLocation()}content/images/{this._storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
                }
                else
                {
                    imageUrl = _pictureService.GetPictureUrl(productPicture);
                }

                recommendedProduct.ProductLink = $"{this.GetStoreUrl(backInStockSubscription.StoreId)}{product.GetSeName()}";
                recommendedProduct.ImgLink = imageUrl;
                data.RecommendedProducts.Add(recommendedProduct);
            }
        }

        public void AddProductManualOrderRecommendationsData(CrmSalesOrder crmSalesOrder, NopStore nopStore, DynamicTemplateData data, int productsCount, int languageId)
        {
            var recommendedProducts = new List<RecommendedProduct>();
            if (crmSalesOrder == null)
            {
                return;
            }

            if (crmSalesOrder.Lines.Where(x => x.Product != null).Count() == 0)
            {
                return;
            }

            var incomingProductId = crmSalesOrder.Lines.Where(x => x.Product != null).Select(x => x.Product).FirstOrDefault().Id;

            var additionalProductIds = this._productRecommendationService.GetProductsRecommendationIds((int)nopStore, incomingProductId);
            var newProductIdsList = additionalProductIds.Where(x => x != incomingProductId).ToList();

            var productService = EngineContext.Current.Resolve<IProductService>();
            var productIds = newProductIdsList.ToArray();
            if (!productIds.Any() || productIds.Length < productsCount)
            {
                var bestSellers = productService.GetBestSellerProducts(productsCount);
                productIds = productIds.Union(bestSellers.Select(p => p.Id)).Take(productsCount).ToArray();
            }

            var products = productService.GetProductsByIds(productIds, true).Take(productsCount);
            foreach (var product in products)
            {
                if (product == null)
                {
                    continue;
                }

                var recommendedProduct = new RecommendedProduct
                {
                    ProductName = product.GetLocalized(x => x.Name, languageId),
                    Price = this._priceFormatter.FormatPrice(product.Price, true, "USD", this._languageService.GetLanguageById(languageId), false)
                };

                Picture productPicture = this._pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
                string imageUrl;
                if (productPicture == null)
                {
                    imageUrl = !string.IsNullOrEmpty(_pictureService.GetProductAdditionalImageName(product.Id))
                        ? $"{this._webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                        : $"{this._webHelper.GetStoreImagesLocation()}content/images/{this._storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
                }
                else
                {
                    imageUrl = _pictureService.GetPictureUrl(productPicture);
                }

                var store = _storeService.GetStoreById((int)nopStore);
                var productUrl = store.Url + product.GetSeName();

                recommendedProduct.ProductLink = productUrl;
                recommendedProduct.ImgLink = imageUrl;
                data.RecommendedProducts.Add(recommendedProduct);
            }
        }

        public void AddBackorderEtaData(Order order, DynamicTemplateData data, int languageId = 0)
        {
            var channel = this._orderService.GetChannel((NopStore)order.StoreId);

            var backorderItems = order.OrderItems.Where(i => i.Product.ProductExtra.IsShippingFromManufacturer).ToList();
            if (backorderItems.Any())
            {
                var defaultESDInDays = 14;
                var productService = EngineContext.Current.Resolve<IProductService>();
                var shipLeadTimes = backorderItems.Select(boi => productService.GetOutstockOrderAvgShipLeadTime(boi.ProductId, (int)channel)).Where(t => t > 0).ToList();
                var avgLeadTime = shipLeadTimes.Any() && shipLeadTimes.Min() > 0 ? shipLeadTimes.Min() : defaultESDInDays;
                avgLeadTime = avgLeadTime < 1 ? 1 : avgLeadTime - 1;

                data.OrderETA = DateTime.UtcNow.AddDays(avgLeadTime).ToString("dddd, MMMM dd");
            }
        }

        #endregion

        #endregion Methods
    }
}