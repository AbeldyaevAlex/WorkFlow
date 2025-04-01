using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using RestSharp;
using Asu.Core.Domain.Klaviyo;
using Asu.Core.Domain.Catalog;
using Asu.Services.Customization;
using Asu.Core;
using Asu.Core.Domain.Stores;
using Asu.Services.Seo;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Customers;
using Asu.Services.Common;
using Asu.Core.Domain.Customization;
using Asu.Services.Media;
using Asu.Services.Logging;

namespace Asu.Services.Messages
{
    public partial class KlaviyoService : IKlaviyoService
    {
        private string[] UnfriendlyIps = { "192.168.200.10", "157.55.39.143", "157.55.39.163", "157.55.39.80", "207.46.13.90", "40.77.167.3", "40.77.167.60", "40.77.167.65", "40.77.167.85", "40.77.167.87", "40.77.167.88", "40.77.167.90" };
        private string _baseAddressUri = "https://a.klaviyo.com/api/";
        private string _token = "aESz9R";
        private string _securityApiKey = "pk_9a949ff1db4d05ad55cfc358d95ec0effe";
        private NopStore[] activeStores = { NopStore.Autoplicity };

        private readonly IStoreContext storeContext;
        private readonly IPictureService pictureService;
        private readonly IWebHelper webHelper;
        private readonly ILogger logger;

        public KlaviyoService(
            IStoreContext storeContext,
            IPictureService pictureService,
            IWebHelper webHelper,
            ILogger logger
            )
        {
            this.storeContext = storeContext;
            this.pictureService = pictureService;
            this.webHelper = webHelper;
            this.logger = logger;
        }

        public virtual bool TrackAddToCartActivity(Customer customer, Product product, int quantity)
        {
            try
            {
                if (!activeStores.Contains((NopStore)storeContext.CurrentStore.Id))
                    return false;

                if (customer.LastIpAddress != null && UnfriendlyIps.Any(i => i == customer.LastIpAddress))
                    return false;

                var addToCartProperties = new AddToCartProperties()
                {
                    AddedItemProductName = product.Name,
                    AddedItemProductID = product.Id,
                    AddedItemSKU = product.Sku,
                    AddedItemImageURL = this.GetProductPictureUrl(product),
                    AddedItemURL = this.storeContext.CurrentStore.Url + product.GetSeName(),
                    AddedItemPrice = product.Price,
                    AddedItemQuantity = quantity,
                    AddedItemCategoryName = product.ProductCategories.FirstOrDefault()?.Category.Name,
                };

                var trackPayload = new TrackPayload<AddToCartProperties>(){
                    Token = _token,
                    Event = "Added to Cart",
                    CustomerProperties = GetCustomerProperties(customer),
                    Properties = addToCartProperties
                };
            
                return Track(trackPayload);
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo TrackAddToCartActivity error", ex);
                return false;
            }
        }

        public virtual bool TrackViewedProductActivity(Customer customer, Product product)
        {
            try 
            { 
                if (!activeStores.Contains((NopStore)storeContext.CurrentStore.Id))
                    return false;

                if (customer.LastIpAddress != null && UnfriendlyIps.Any(i => i == customer.LastIpAddress))
                    return false;

                var viewedProductProperties = new ViewedProductProperties()
                {
                    ProductName = product.Name,
                    ProductId = product.Id,
                    Sku = product.Sku,
                    ImageURL = this.GetProductPictureUrl(product),
                    URL = this.storeContext.CurrentStore.Url + product.GetSeName(),
                    Price = product.Price
                };

                var trackPayload = new TrackPayload<ViewedProductProperties>()
                {
                    Token = _token,
                    Event = "Viewed Product",
                    CustomerProperties = GetCustomerProperties(customer),
                    Properties = viewedProductProperties
                };

                return Track(trackPayload);
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo TrackViewedProductActivity error", ex);
                return false;
            }
        }

        public virtual bool TrackStartedCheckoutActivity(Customer customer, IList<ShoppingCartItem> shoppingCartItemList)
        {
            try
            {
                if (!activeStores.Contains((NopStore)storeContext.CurrentStore.Id))
                    return false;

                if (customer.LastIpAddress != null && UnfriendlyIps.Any(i => i == customer.LastIpAddress))
                    return false;

                var productProperties = new List<ProductProperties>();
                var categories = new List<string>();
                var itemNames = new List<string>();

                foreach (var shoppingCartItem in shoppingCartItemList)
                {
                    if (IsInsuranceProduct(shoppingCartItem.ProductId))
                        continue;

                    if (shoppingCartItem.Product.ProductCategories.Count > 0)
                    {
                        categories.Add(shoppingCartItem.Product.ProductCategories.FirstOrDefault().Category.Name);
                    }
                    itemNames.Add(shoppingCartItem.Product.Name);

                    productProperties.Add(new ProductProperties
                    {
                        ProductId = shoppingCartItem.ProductId,
                        Sku = shoppingCartItem.Product.Sku,
                        ProductName = shoppingCartItem.Product.Name,
                        Quantity = shoppingCartItem.Quantity,
                        ItemPrice = shoppingCartItem.Product.Price,
                        RowTotal = shoppingCartItem.Product.Price * shoppingCartItem.Quantity,
                        ProductURL = this.storeContext.CurrentStore.Url + shoppingCartItem.Product.GetSeName(),
                        ImageURL = this.GetProductPictureUrl(shoppingCartItem.Product),
                        ProductCategoryName = shoppingCartItem.Product.ProductCategories.FirstOrDefault()?.Category.Name,
                        ProductCategoryId = shoppingCartItem.Product.ProductCategories.FirstOrDefault()?.CategoryId
                    });
                }

                var viewedProductProperties = new StartedCheckoutProperties()
                {
                    Categories = categories,
                    ItemNames = itemNames,
                    Items = productProperties
                };

                var trackPayload = new TrackPayload<StartedCheckoutProperties>()
                {
                    Token = _token,
                    Event = "Started Checkout",
                    CustomerProperties = GetCustomerProperties(customer),
                    Properties = viewedProductProperties
                };

                return Track(trackPayload);
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo TrackStartedCheckoutActivity error", ex);
                return false;
            }
        }

        public virtual bool TrackPlacedOrderActivity(Order order)
        {
            try
            {
                if (!activeStores.Contains((NopStore)order.StoreId))
                    return false;

                var productProperties = new List<ProductProperties>();
                var categories = new List<string>();
                var itemNames = new List<string>();

                foreach (var item in order.OrderItems)
                {
                    if (IsInsuranceProduct(item.ProductId))
                        continue;

                    if (item.Product.ProductCategories.Count > 0)
                    {
                        categories.Add(item.Product.ProductCategories.FirstOrDefault().Category.Name);
                    }
                    itemNames.Add(item.Product.Name);

                    productProperties.Add(new ProductProperties
                    {
                        ProductId = item.ProductId,
                        Sku = item.Product.Sku,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        ItemPrice = item.Product.Price,
                        RowTotal = item.Product.Price * item.Quantity,
                        ProductURL = this.storeContext.CurrentStore.Url + item.Product.GetSeName(),
                        ImageURL = this.GetProductPictureUrl(item.Product),
                        ProductCategoryName = item.Product.ProductCategories.FirstOrDefault()?.Category.Name,
                        ProductCategoryId = item.Product.ProductCategories.FirstOrDefault()?.CategoryId
                    });
                }

                var placedOrderProperties = new PlacedOrderProperties()
                {
                    Categories = categories,
                    ItemNames = itemNames,
                    Items = productProperties
                };

                var trackPayload = new TrackPayload<PlacedOrderProperties>()
                {
                    Token = _token,
                    Event = "Placed Order",
                    CustomerProperties = GetCustomerProperties(order.Customer),
                    Properties = placedOrderProperties
                };

                return Track(trackPayload);
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo TrackPlacedOrderActivity error", ex);
                return false;
            }
        }

        public virtual bool TrackCanceledOrder(Order order)
        {
            try
            {
                if (!activeStores.Contains((NopStore)order.StoreId))
                    return false;

                var productProperties = new List<ProductProperties>();
                var categories = new List<string>();
                var itemNames = new List<string>();

                foreach (var item in order.OrderItems)
                {
                    if (IsInsuranceProduct(item.ProductId))
                        continue;

                    if (item.Product.ProductCategories.Count > 0)
                    {
                        categories.Add(item.Product.ProductCategories.FirstOrDefault().Category.Name);
                    }
                    itemNames.Add(item.Product.Name);

                    productProperties.Add(new ProductProperties
                    {
                        ProductId = item.ProductId,
                        Sku = item.Product.Sku,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        ItemPrice = item.Product.Price,
                        RowTotal = item.Product.Price * item.Quantity,
                        ProductURL = this.storeContext.CurrentStore.Url + item.Product.GetSeName(),
                        ImageURL = this.GetProductPictureUrl(item.Product),
                        ProductCategoryName = item.Product.ProductCategories.FirstOrDefault()?.Category.Name,
                        ProductCategoryId = item.Product.ProductCategories.FirstOrDefault()?.CategoryId
                    });
                }

                var placedOrderProperties = new PlacedOrderProperties()
                {
                    Categories = categories,
                    ItemNames = itemNames,
                    Items = productProperties
                };

                var trackPayload = new TrackPayload<PlacedOrderProperties>()
                {
                    Token = _token,
                    Event = "Canceled Order",
                    CustomerProperties = GetCustomerProperties(order.Customer),
                    Properties = placedOrderProperties
                };

                return Track(trackPayload);
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo TrackCanceledOrder error", ex);
                return false;
            }
        }

        public virtual bool TrackOrderShipped(Order order)
        {
            try
            {
                if (!activeStores.Contains((NopStore)order.StoreId))
                    return false;

                var productProperties = new List<ProductProperties>();
                var categories = new List<string>();
                var itemNames = new List<string>();

                foreach (var item in order.OrderItems)
                {
                    if (IsInsuranceProduct(item.ProductId))
                        continue;

                    if (item.Product.ProductCategories.Count > 0)
                    {
                        categories.Add(item.Product.ProductCategories.FirstOrDefault().Category.Name);
                    }
                    itemNames.Add(item.Product.Name);

                    productProperties.Add(new ProductProperties
                    {
                        ProductId = item.ProductId,
                        Sku = item.Product.Sku,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        ItemPrice = item.Product.Price,
                        RowTotal = item.Product.Price * item.Quantity,
                        ProductURL = this.storeContext.CurrentStore.Url + item.Product.GetSeName(),
                        ImageURL = this.GetProductPictureUrl(item.Product),
                        ProductCategoryName = item.Product.ProductCategories.FirstOrDefault()?.Category.Name,
                        ProductCategoryId = item.Product.ProductCategories.FirstOrDefault()?.CategoryId
                    });
                }

                var placedOrderProperties = new PlacedOrderProperties()
                {
                    Categories = categories,
                    ItemNames = itemNames,
                    Items = productProperties
                };

                var trackPayload = new TrackPayload<PlacedOrderProperties>()
                {
                    Token = _token,
                    Event = "Order Shipped",
                    CustomerProperties = GetCustomerProperties(order.Customer),
                    Properties = placedOrderProperties
                };

                return Track(trackPayload);
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo TrackOrderShipped error", ex);
                return false;
            }
        }

        public virtual bool TrackOrderDelivered(Order order)
        {
            try
            {
                if (!activeStores.Contains((NopStore)order.StoreId))
                    return false;

                var productProperties = new List<ProductProperties>();
                var categories = new List<string>();
                var itemNames = new List<string>();

                foreach (var item in order.OrderItems)
                {
                    if (IsInsuranceProduct(item.ProductId))
                        continue;

                    if (item.Product.ProductCategories.Count > 0)
                    {
                        categories.Add(item.Product.ProductCategories.FirstOrDefault().Category.Name);
                    }
                    itemNames.Add(item.Product.Name);

                    productProperties.Add(new ProductProperties
                    {
                        ProductId = item.ProductId,
                        Sku = item.Product.Sku,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        ItemPrice = item.Product.Price,
                        RowTotal = item.Product.Price * item.Quantity,
                        ProductURL = this.storeContext.CurrentStore.Url + item.Product.GetSeName(),
                        ImageURL = this.GetProductPictureUrl(item.Product),
                        ProductCategoryName = item.Product.ProductCategories.FirstOrDefault()?.Category.Name,
                        ProductCategoryId = item.Product.ProductCategories.FirstOrDefault()?.CategoryId
                    });
                }

                var placedOrderProperties = new PlacedOrderProperties()
                {
                    Categories = categories,
                    ItemNames = itemNames,
                    Items = productProperties
                };

                var trackPayload = new TrackPayload<PlacedOrderProperties>()
                {
                    Token = _token,
                    Event = "Order Delivered",
                    CustomerProperties = GetCustomerProperties(order.Customer),
                    Properties = placedOrderProperties
                };

                return Track(trackPayload);
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo TrackOrderDelivered error", ex);
                return false;
            }
        }

        private CustomerProperties GetCustomerProperties(Customer customer)
        {
            try
            {
                CustomerProperties customerProperties;

                if (customer.BillingAddress == null)
                {
                    customerProperties = new CustomerProperties()
                    {
                        Id = customer.CustomerGuid.ToString(),
                        Email = customer.Email,
                    };
                }
                else
                {
                    customerProperties = new CustomerProperties()
                    {
                        Id = customer.CustomerGuid.ToString(),
                        Email = customer.BillingAddress.Email,
                        FirstName = customer.BillingAddress.FirstName,
                        LastName = customer.BillingAddress.LastName,
                        PhoneNumber = customer.BillingAddress.PhoneNumber,
                        City = customer.BillingAddress.City,
                        Region = customer.BillingAddress.StateProvince?.Name,
                        Country = customer.BillingAddress.Country?.Name,
                        ZipCode = customer.BillingAddress.ZipPostalCode
                    };
                }

                return customerProperties;
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo CustomerProperties error", ex);
                return null;
            }
        }

        private bool Track<T>(TrackPayload<T> trackPayload)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_baseAddressUri + "track");
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(trackPayload);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                try
                {
                    var result = streamReader.ReadToEnd();
                    if (result.ToString() == "1") return true;
                    else return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public void NewsLetterSubscription(string email, bool remove = false)
        {
            if (!activeStores.Contains((NopStore)storeContext.CurrentStore.Id))
                return;

            try
            {
                object payload;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_baseAddressUri + "v2/list/RztZU9/subscribe?api_key=" + _securityApiKey);
                httpWebRequest.Accept = "application/json";
                httpWebRequest.ContentType = "application/json";
                if (!remove)
                {
                    httpWebRequest.Method = "POST";
                    payload = new SubscriptionPayload()
                    {
                        Profiles = new SubscriptionProfile
                        {
                            Email = email
                        }
                    };
                }
                else
                {
                    httpWebRequest.Method = "DELETE";
                    payload = new
                    {
                        emails = new string[] { email }
                    };
                }

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(payload);
                    streamWriter.Write(json);
                }

                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    this.logger.Warning("Klaviyo AddNewsLetterSubscription warning", ex);
                }
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo AddNewsLetterSubscription error", ex);
            }
        }

        public bool IdentifyUpdate(Customer customer)
        {
            try
            {
                if (!activeStores.Contains((NopStore)storeContext.CurrentStore.Id))
                    return false;

                if (customer.LastIpAddress != null && UnfriendlyIps.Any(i => i == customer.LastIpAddress))
                    return false;

                CustomerProperties identifyPayload = GetCustomerProperties(customer);

                if (identifyPayload == null)
                    return false;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_baseAddressUri + "identify");
                httpWebRequest.Accept = "application/json";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(identifyPayload);
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    try
                    {
                        var result = streamReader.ReadToEnd();
                        if (result.ToString() == "1") return true;
                        else return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                this.logger.Error("Klaviyo IdentifyUpdate error", ex);
                return false;
            }
        }

        private string GetProductPictureUrl(Core.Domain.Catalog.Product product)
        {
            var productPicture = this.pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
            string imageUrl;
            if (productPicture == null)
            {
                imageUrl = !string.IsNullOrEmpty(this.pictureService.GetProductAdditionalImageName(product.Id))
                    ? $"{this.webHelper.GetStoreImagesLocation()}ImageLoader/{product.Id}"
                    : $"{this.webHelper.GetStoreImagesLocation()}content/images/{this.storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif";
            }
            else
            {
                //imageUrl = this.pictureService.GetPictureUrl(productPicture);

                //moved from service to skip picture binary checking
                var pictureId = productPicture.Id;
                var lastPart = GetFileExtensionFromMimeType(productPicture.MimeType);
                var folder = pictureId / 10000;
                var fileName = $"{pictureId:00000000}_0.{lastPart}";
                imageUrl = $"{ this.storeContext.CurrentStore.SecureUrl}content/images/{folder}/{fileName}";
            }

            return imageUrl;
        }

        private string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

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

        /// <summary>
        /// Retrurns bool value if the product in list of insurance products
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private bool IsInsuranceProduct(int productId)
        {
            if (ConstantStorage.SHIPPING_INSURANCE_PRODUCT_IDS.Any(i => i == productId) && ConstantStorage.RETURN_EXTENSION_PRODUCT_IDS.Any(i => i == productId))
            {
                return true;
            }
            return false;
        }
    }
}
