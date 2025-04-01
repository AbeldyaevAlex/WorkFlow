using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Asu.Core;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.GoogleTagManager;
using Asu.Core.Domain.Media;
using Asu.Core.Domain.Orders;
using Asu.Services.Catalog;
using Asu.Services.Customers;
using Asu.Services.Localization;
using Asu.Services.Logging;
using Asu.Services.Media;
using Asu.Services.Orders;
using Asu.Services.Security;
using Asu.Services.Seo;
using Asu.Services.Stores;
using Order = Asu.Core.Domain.GoogleTagManager.Order;

namespace Asu.Services.Customization
{
    using Asu.Core.Domain.Security;
    using Asu.Core.Domain.Stores;
    using Asu.Core.Infrastructure;
    using Asu.Services.Common;

    public class GoogleTagManagerService : IGoogleTagManagerService
    {
        private static MD5 md5;

        private readonly DataLayer dataLayer = new DataLayer();
        private readonly DataLayerPush dataLayerPush = new DataLayerPush();
        private readonly IWorkContext workContext;
        private readonly IWebHelper webHelper;
        private readonly IProductAttributeParser productAttributeParser;
        private readonly ICustomService customService;
        private readonly IPictureService pictureService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ILogger log;
        private readonly IAclService aclService;
        private readonly IStoreMappingService storeMappingService;
        private readonly MediaSettings mediaSettings;
        private readonly IStoreContext storeContext;
        private readonly IPriceCalculationService priceCalculationService;
        private readonly IEncryptionService encryptionService;
        private readonly IGenericAttributeService genericAttributeService;

        private static MD5 Md5 => md5 ?? (md5 = MD5.Create());

        public PageType PageType => this.dataLayer.PageType;
        public GroupingPageType GroupingPageType => this.dataLayer.ContentGroupingPageType;

        public GoogleTagManagerService(IWorkContext workContext, 
            IWebHelper webHelper, 
            IProductAttributeParser productAttributeParser,
            ICustomService customService,
            IPictureService pictureService,
            IOrderService orderService,
            IProductService productService,
            ICategoryService categoryService,
            ILogger log,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            MediaSettings mediaSettings,
            IStoreContext storeContext,
            IPriceCalculationService priceCalculationService,
            IEncryptionService encryptionService,
            IGenericAttributeService genericAttributeService)
        {
            this.workContext = workContext;
            this.webHelper = webHelper;
            this.productAttributeParser = productAttributeParser;
            this.customService = customService;
            this.pictureService = pictureService;
            this.orderService = orderService;
            this.productService = productService;
            this.categoryService = categoryService;
            this.log = log;
            this.aclService = aclService;
            this.storeMappingService = storeMappingService;
            this.mediaSettings = mediaSettings;
            this.storeContext = storeContext;
            this.genericAttributeService = genericAttributeService;

            var customer = this.workContext.CurrentCustomer;

            this.dataLayerPush = new DataLayerPush();
            this.dataLayer.IsAdmin = customer.IsAdmin();
            this.dataLayer.IsGuest = customer.IsGuest();
            this.dataLayer.CustomerGuid = customer.CustomerGuid;
            this.dataLayer.customerFirstName = customer.BillingAddress == null
                ? customer.GetAttribute(SystemCustomerAttributeNames.FirstName, this.genericAttributeService, this.storeContext.CurrentStore.Id)?.Value
                : customer.BillingAddress.FirstName;

            this.dataLayer.CustomerEmail = string.IsNullOrEmpty(customer.Email)
                ? customer.BillingAddress?.Email
                : customer.Email;

            this.dataLayer.PageType = PageType.Other;
            this.dataLayer.ContentGroupingPageType = GroupingPageType.OtherPages;
            this.priceCalculationService = priceCalculationService;
            this.encryptionService = encryptionService;
        }

        public DataLayer GetDataLayer()
        {
            return this.dataLayer;
        }

        public string GetDataLayerScript()
        {
            try
            {
                var stringBuilder = new StringBuilder();
                using (var stringWriter = new StringWriter(stringBuilder))
                {
                    using (var writer = new JsonTextWriter(stringWriter))
                    {
                        writer.QuoteChar = '\'';
                        var serializer = new JsonSerializer
                        {
                            Formatting = Formatting.None,
                            NullValueHandling = NullValueHandling.Ignore
                        };

                        serializer.Serialize(writer, this.dataLayer);
                    }
                }

                return stringBuilder.ToString();

            }
            catch (Exception ex)
            {
                this.log.Error("GoogleTagManagerService.GetDataLayerScript", ex);
                return string.Empty;
            }
        }

        public string GetDataLayerPushScript()
        {
            try
            {
                var stringBuilder = new StringBuilder();
                using (var stringWriter = new StringWriter(stringBuilder))
                {
                    using (var writer = new JsonTextWriter(stringWriter))
                    {
                        writer.QuoteChar = '\'';
                        var serializer = new JsonSerializer
                        {
                            Formatting = Formatting.None,
                            NullValueHandling = NullValueHandling.Ignore
                        };

                        serializer.Serialize(writer, dataLayerPush);
                    }
                }

                return stringBuilder.ToString();

            }
            catch (Exception ex)
            {
                this.log.Error("GoogleTagManagerService.GetDataLayerPushScript", ex);
                return string.Empty;
            }
        }

        public void SetPage(PageType page, GroupingPageType pageType)
        {
            this.dataLayer.PageType = page;
            this.dataLayer.ContentGroupingPageType = pageType;
        }

        public void SetShoppingCartData(PageType page, GroupingPageType pageType, int? removeProductId)
        {
            if (this.workContext.CurrentCustomer == null)
                return;

            if (this.workContext.CurrentCustomer.ShoppingCartItems == null || this.workContext.CurrentCustomer.ShoppingCartItems.Count == 0)
            {
                if (removeProductId.HasValue)
                {
                    this.dataLayer.ShoppingCart = new ShoppingCart { RemoveProductId = removeProductId };
                }

                return;
            }

            var cart = this.workContext.CurrentCustomer.ShoppingCartItems.ToList();
            this.SetShoppingCartData(cart, page, pageType, removeProductId);
        }

        public void SetShoppingCartData(IList<ShoppingCartItem> cart, PageType page, GroupingPageType pageType, int? removeProductId = null)
        {
            this.dataLayer.PageType = page;
            this.dataLayer.ContentGroupingPageType = pageType;

            if (cart == null)
            {
                return;
            }
            
            this.dataLayer.ShoppingCart = new ShoppingCart { Items = new List<Item>(), RemoveProductId = removeProductId };

            try
            {
                foreach (var item in cart)
                {
                    var price = this.priceCalculationService.GetFinalPrice(item.Product, this.workContext.CurrentCustomer);
                    var cartItem = new Item
                    {
                        ProductId = item.Product.Id,
                        Name = item.Product.GetLocalized(x => x.Name),
                        Quantity = item.Quantity,
                        ProductUrl = this.storeContext.CurrentStore.Url + item.Product.GetSeName(),
                        Price = price,
                        SubTotal = price * item.Quantity,
                        PictureUrl = this.GetProductPictureUrl(item.Product),
                        CategoryId = 0,
                        CategoryName = string.Empty
                    };
                    
                    if (item.Product.ProductManufacturers != null)
                    {
                        var productManufacturer = item.Product.ProductManufacturers.FirstOrDefault();
                        if (productManufacturer != null)
                        {
                            var manufacturer = productManufacturer.Manufacturer;
                            if (manufacturer != null)
                            {
                                cartItem.ManufacturerId = manufacturer.Id;
                                cartItem.ManufacturerName = manufacturer.Name;
                            }
                        }
                    }

                    if (item.Product.ProductCategories != null)
                    {
                        var productCategory = item.Product.ProductCategories.FirstOrDefault();
                        if (productCategory != null)
                        {
                            var category = productCategory.Category;
                            if (category != null)
                            {
                                cartItem.CategoryId = category.Id;
                                cartItem.CategoryName = category.Name;
                            }
                        }
                    }

                    this.dataLayer.ShoppingCart.Items.Add(cartItem);
                }
            }
            catch (Exception ex)
            {
                this.log.Error("GoogleTagManagerService.SetShoppingCartData", ex);
            }

            this.dataLayer.ShoppingCart.SubTotal = this.dataLayer.ShoppingCart.Items.Sum(i => i.SubTotal);
            this.dataLayer.ShoppingCart.CustomerEmail = this.workContext.CurrentCustomer.IsGuest() ? null : this.workContext.CurrentCustomer.Email;
        }

        public void SetOrderData(PageType page, GroupingPageType pageType, Core.Domain.Orders.Order order)
        {
            if (this.workContext.CurrentCustomer == null)
            {
                return;
            }

            this.dataLayer.PageType = page;
            this.dataLayer.ContentGroupingPageType = pageType;

            if (order == null)
            {
                return;
            }

            string creditCardNumber = null;
            if (!string.IsNullOrEmpty(order.MaskedCreditCardNumber))
            {
                creditCardNumber = this.encryptionService.DecryptText(order.MaskedCreditCardNumber);
                creditCardNumber = creditCardNumber.Length > 6 ? creditCardNumber.Substring(0, 6) : null;
            }
            
            this.dataLayer.Order = new Order
            {
                OrderId = order.Id,
                SubTotal = order.OrderSubtotalExclTax,
                Tax = order.OrderTax,
                Shipping = order.OrderShippingExclTax,
                Total = order.OrderTotal,
                CustomerId = order.CustomerId,
                CustomerEmail = string.IsNullOrEmpty(order.Customer.Email) ? order.BillingAddress.Email : order.Customer.Email,
                CustomerFullName = string.IsNullOrEmpty(order.Customer.GetFullName()) ? string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName).Trim() : order.Customer.GetFullName(),
                CustomerFirstName = order.BillingAddress.FirstName,
                CustomerLastName = order.BillingAddress.LastName,
                CustomerPhone = order.BillingAddress.PhoneNumber?.Replace("(", string.Empty)?.Replace(")", string.Empty)?.Replace("-", string.Empty),
                City = order.BillingAddress.City,
                Address1 = order.BillingAddress.Address1,
                Zip = order.BillingAddress.ZipPostalCode,
                Country = order.ShippingAddress.Country.Name,
                State = order.ShippingAddress.StateProvince == null ? string.Empty : order.ShippingAddress.StateProvince.Abbreviation,
                Items = new List<Item>(),
                Items2 = new List<GA4Item>(),
                CcBin = this.GetCcBin(order.CardNumber, 6)
            };

            /*this.dataLayer.OrderId = order.Id;
            this.dataLayer.OrderTotal = order.OrderTotal;
            this.dataLayer.OrderTax = order.OrderTax;
            this.dataLayer.OrderShipping = order.OrderShippingExclTax;
            this.dataLayer.TransactionProducts = new List<TransactionProduct>();*/
            if(order.DiscountUsageHistory != null && order.DiscountUsageHistory.Count > 0)
            {
                var discount = order.DiscountUsageHistory.FirstOrDefault();
                if (discount != null && discount.Discount!= null)
                {
                    this.dataLayer.Order.CouponCode = discount.Discount.CouponCode;
                }
            }

            try
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var price = this.priceCalculationService.GetFinalPrice(orderItem.Product, this.workContext.CurrentCustomer);
                    var item = new Item
                    {
                        ProductId = orderItem.Product.Id,
                        Name = orderItem.Product.GetLocalized(x => x.Name),
                        Mpn = orderItem.Product.ManufacturerPartNumber,
                        Quantity = orderItem.Quantity,
                        ProductUrl = this.storeContext.CurrentStore.Url + orderItem.Product.GetSeName(),
                        Price = price,
                        SubTotal = price * orderItem.Quantity,
                        PictureUrl = this.GetProductPictureUrl(orderItem.Product),
                        CategoryId = 0,
                        CategoryName = string.Empty
                    };

                    var ga4Item = new GA4Item
                    {
                        Id = item.ProductId.ToString(),
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                    };

                    /*var transactionProduct = new TransactionProduct
                    {
                        ProductId = orderItem.Product.Id,
                        Name = orderItem.Product.GetLocalized(x => x.Name),
                        Price = price,
                        Quantity = orderItem.Quantity
                    };*/

                    var product = orderItem.Product;
                    if (product != null)
                    {
                        if (product.ProductCategories != null)
                        {
                            var productCategory = product.ProductCategories.FirstOrDefault();
                            if (productCategory != null)
                            {
                                var category = productCategory.Category;
                                if (category != null)
                                {
                                    item.CategoryId = category.Id;
                                    ga4Item.Category3 = item.CategoryName = category.Name;

                                    var subCategory = this.categoryService.GetCategoryById(category.ParentCategoryId);
                                    if (subCategory != null)
                                    {
                                        ga4Item.Category2 = subCategory.Name;
                                        var rootCategory = this.categoryService.GetCategoryById(subCategory.ParentCategoryId);
                                        if (rootCategory != null)
                                        {
                                            ga4Item.Category = rootCategory.Name;
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (product.ProductManufacturers != null)
                        {
                            var productManufacturer = product.ProductManufacturers.FirstOrDefault();
                            if (productManufacturer != null)
                            {
                                var manufacturer = productManufacturer.Manufacturer;
                                if (manufacturer != null)
                                {
                                    item.ManufacturerId = manufacturer.Id;
                                    ga4Item.Brand = item.ManufacturerName = manufacturer.Name;
                                }
                            }
                        }
                    }

                    this.dataLayer.Order.Items.Add(item);
                    this.dataLayer.Order.Items2.Add(ga4Item);
                    //this.dataLayer.TransactionProducts.Add(transactionProduct);
                }
            }
            catch (Exception ex)
            {
                this.log.Error("GoogleTagManagerService.SetOrderData", ex);
            }
        }

        public void SetOrderData(PageType page, GroupingPageType pageType, int orderId)
        {
            this.SetOrderData(page, pageType, this.orderService.GetOrderById(orderId));
        }

        public void SetProductData(Core.Domain.Catalog.Product product, bool usesImageLoader = false, PageType page = PageType.Product, GroupingPageType pageType = GroupingPageType.ProductPages)
        {
            this.dataLayer.PageType = page;
            this.dataLayer.ContentGroupingPageType = pageType;

            if (product == null)
            {
                return;
            }

            try
            {
                var price = this.priceCalculationService.GetFinalPrice(product, this.workContext.CurrentCustomer);
                this.dataLayer.Product = new Product
                {
                    ProductId = product.Id,
                    Name = product.GetLocalized(x => x.Name),
                    Mpn = product.ManufacturerPartNumber,
                    ProductUrl = this.storeContext.CurrentStore.Url + product.GetSeName(),
                    PictureUrl = this.GetProductPictureUrl(product),
                    Price = price,
                    StockQty = product.StockQuantity,
                    UsesImageLoader = usesImageLoader,
                };

                if (product.ProductExtra != null)
                {
                    this.dataLayer.Product.ShipsFromManufacturer = product.ProductExtra.IsShippingFromManufacturer;
                    this.dataLayer.Product.PriceBelowUsQty = product.ProductExtra.PriceBelowUsQty;
                    this.dataLayer.Product.ShippingType = product.ProductExtra.ShippingType;
                }

                if (product.ProductCategories != null)
                {
                    var productCategory = product.ProductCategories.FirstOrDefault();
                    if (productCategory != null)
                    {
                        var category = productCategory.Category;
                        if (category != null)
                        {
                            this.dataLayer.Product.CategoryId = category.Id;
                            this.dataLayer.Product.CategoryName = category.Name;

                            try
                            {
                                var categoryBreadCrumb = category.GetCategoryBreadCrumb(this.categoryService, this.aclService, this.storeMappingService);
                                var count = categoryBreadCrumb.Count;
                                this.dataLayer.Product.CategoryBreadCrumb = 
                                    count == 1 
                                    ? category.Name 
                                    : string.Format(@"{0} > {1}", categoryBreadCrumb[count - 2].Name, categoryBreadCrumb[count - 1].Name);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }

                if (product.ProductManufacturers != null)
                {
                    var productManufacturer = product.ProductManufacturers.FirstOrDefault();
                    if (productManufacturer != null)
                    {
                        var manufacturer = productManufacturer.Manufacturer;
                        if (manufacturer != null)
                        {
                            this.dataLayer.Product.ManufacturerId = manufacturer.Id;
                            this.dataLayer.Product.ManufacturerName = manufacturer.Name;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.log.Error("GoogleTagManagerService.SetProductData", ex);
            }
        }

        public void SetProductData(int productId, bool usesImageLoader = false, PageType page = PageType.Product, GroupingPageType pageType = GroupingPageType.ProductPages)
        {
            this.SetProductData(this.productService.GetProductById(productId), usesImageLoader, page, pageType);
        }

        public void SetCategoryData(Core.Domain.Catalog.Category category, PageType page = PageType.Category, GroupingPageType pageType = GroupingPageType.SearchPages)
        {
            this.dataLayer.PageType = page;
            this.dataLayer.ContentGroupingPageType = pageType; 

            if (category == null)
            {
                return;
            }

            this.dataLayer.Category = new Category
            {
                CategoryId = category.Id,
                Name = category.Name,
                BreadCrumb = category.GetFormattedBreadCrumb(category.GetCategoryBreadCrumb(categoryService, aclService, storeMappingService), "/")
            };
        }

        public void SetManufacturerData(Core.Domain.Catalog.Manufacturer manufacturer, PageType page = PageType.Manufacturer, GroupingPageType pageType = GroupingPageType.SearchPages)
        {
            this.dataLayer.PageType = page;
            this.dataLayer.ContentGroupingPageType = pageType;

            if (manufacturer == null)
            {
                return;
            }

            this.dataLayer.Manufacturer = new Manufacturer
            {
                ManufacturerId = manufacturer.Id,
                Name = manufacturer.Name
            };
        }

        public void SetEcommerceImpressions(List<Impression> impressions)
        {
            if (impressions == null)
            {
                return;
            }

            if (this.dataLayerPush.Ecommerce == null)
            {
                this.dataLayerPush.Ecommerce = new Ecommerce
                {
                    CurrencyCode = "USD"
                };
            }

            this.dataLayerPush.Ecommerce.Impressions = impressions;
        }

        public void SetProductIds(int[] productIds)
        {
        }

        private string GetProductPictureUrl(Core.Domain.Catalog.Product product)
        {
            var imageUrl = !string.IsNullOrEmpty(this.customService.GetProductAdditionalImageName(product.Id))
                    ? $"{this.webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                    : $"{this.webHelper.GetStoreImagesLocation()}content/images/{this.storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";

            return imageUrl;
        }

        private static string Md5Encode(string sourceString)
        {
            var inputBytes = Encoding.UTF8.GetBytes(sourceString);
            var hash = Md5.ComputeHash(inputBytes);
            var sb = new StringBuilder(32);
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static string HashEmail(string sourceEmail)
        {
            return Md5Encode(sourceEmail.ToLowerInvariant().Trim());
        }

        private string GetCcBin(string encryptedCreditCardNumber, int length)
        {
            if (string.IsNullOrEmpty(encryptedCreditCardNumber))
            {
                return null;
            }

            var creditCardNumber = this.encryptionService.DecryptText(encryptedCreditCardNumber);
            return !string.IsNullOrEmpty(creditCardNumber) && creditCardNumber.Length >= length ? creditCardNumber.Substring(0, length) : null;
        }
    }
}