using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Core;
using Asu.Core.Caching;
using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Directory;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Shipping;
using Asu.Core.Plugins;
using Asu.Services.Catalog;
using Asu.Services.Common;
using Asu.Services.Events;
using Asu.Services.Localization;
using Asu.Services.Logging;
using Asu.Services.Orders;

namespace Asu.Services.Shipping
{
    using Asu.Core.Domain.Logging;
    using Asu.Data.Mapping.Shipping;

    /// <summary>
    /// Shipping service
    /// </summary>
    public partial class ShippingService : IShippingService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : warehouse ID
        /// </remarks>
        private const string WAREHOUSES_BY_ID_KEY = "Nop.warehouse.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string WAREHOUSES_PATTERN_KEY = "Nop.warehouse.";

        #endregion

        #region Fields

        private readonly IRepository<ShippingMethod> _shippingMethodRepository;
        private readonly IRepository<FreeShippingProduct> freeShippingProductRepository;
        private readonly IRepository<ZipCode> _zipCodeRepository;
        private readonly IRepository<ZipPrefix> _zipPrefixRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly IRepository<DeliveryDate> _deliveryDateRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly ILogger _logger;
        private readonly IProductService _productService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IAddressService _addressService;
        private readonly ShippingSettings _shippingSettings;
        private readonly IPluginFinder _pluginFinder;
        private readonly IStoreContext _storeContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<OriginalShippingRate> originalShipingRateRepository;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shippingMethodRepository">Shipping method repository</param>
        /// <param name="deliveryDateRepository">Delivery date repository</param>
        /// <param name="warehouseRepository">Warehouse repository</param>
        /// <param name="logger">Logger</param>
        /// <param name="productService">Product service</param>
        /// <param name="productAttributeParser">Product attribute parser</param>
        /// <param name="checkoutAttributeParser">Checkout attribute parser</param>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="addressService">Address service</param>
        /// <param name="shippingSettings">Shipping settings</param>
        /// <param name="pluginFinder">Plugin finder</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="eventPublisher">Event published</param>
        /// <param name="shoppingCartSettings">Shopping cart settings</param>
        /// <param name="cacheManager">Cache manager</param>
        public ShippingService(IRepository<ShippingMethod> shippingMethodRepository,
            IRepository<DeliveryDate> deliveryDateRepository,
            IRepository<Warehouse> warehouseRepository,
            IRepository<ZipCode> zipCodeRepository,
            IRepository<ZipPrefix> zipPrefixRepository,
            IRepository<StateProvince> stateProvinceRepository,
            ILogger logger,
            IProductService productService,
            IProductAttributeParser productAttributeParser,
            ICheckoutAttributeParser checkoutAttributeParser,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IAddressService addressService,
            ShippingSettings shippingSettings,
            IPluginFinder pluginFinder,
            IStoreContext storeContext,
            IEventPublisher eventPublisher,
            ShoppingCartSettings shoppingCartSettings,
            ICacheManager cacheManager,
            IRepository<OriginalShippingRate> originalShipingRateRepository,
            IRepository<FreeShippingProduct> freeShippingProductRepository,
            IWorkContext workContext)
        {
            this._shippingMethodRepository = shippingMethodRepository;
            this._deliveryDateRepository = deliveryDateRepository;
            this._warehouseRepository = warehouseRepository;
            this._logger = logger;
            this._productService = productService;
            this._productAttributeParser = productAttributeParser;
            this._checkoutAttributeParser = checkoutAttributeParser;
            this._genericAttributeService = genericAttributeService;
            this._localizationService = localizationService;
            this._addressService = addressService;
            this._shippingSettings = shippingSettings;
            this._pluginFinder = pluginFinder;
            this._storeContext = storeContext;
            this._eventPublisher = eventPublisher;
            this._shoppingCartSettings = shoppingCartSettings;
            this._cacheManager = cacheManager;
            this.originalShipingRateRepository = originalShipingRateRepository;
            this._zipCodeRepository = zipCodeRepository;
            this._zipPrefixRepository = zipPrefixRepository;
            this._stateProvinceRepository = stateProvinceRepository;
            this.freeShippingProductRepository = freeShippingProductRepository;
            this._workContext = workContext;
        }

        #endregion
        
        #region Methods

        #region Shipping rate computation methods

        /// <summary>
        /// Load active shipping rate computation methods
        /// </summary>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Shipping rate computation methods</returns>
        public virtual IList<IShippingRateComputationMethod> LoadActiveShippingRateComputationMethods(int storeId = 0)
        {
            return LoadAllShippingRateComputationMethods(storeId)
                   .Where(provider => _shippingSettings.ActiveShippingRateComputationMethodSystemNames.Contains(provider.PluginDescriptor.SystemName, StringComparer.InvariantCultureIgnoreCase))
                   .ToList();
        }

        /// <summary>
        /// Load shipping rate computation method by system name
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Found Shipping rate computation method</returns>
        public virtual IShippingRateComputationMethod LoadShippingRateComputationMethodBySystemName(string systemName)
        {
            var descriptor = _pluginFinder.GetPluginDescriptorBySystemName<IShippingRateComputationMethod>(systemName);
            if (descriptor != null)
                return descriptor.Instance<IShippingRateComputationMethod>();

            return null;
        }

        /// <summary>
        /// Load all shipping rate computation methods
        /// </summary>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Shipping rate computation methods</returns>
        public virtual IList<IShippingRateComputationMethod> LoadAllShippingRateComputationMethods(int storeId = 0)
        {
            return _pluginFinder.GetPlugins<IShippingRateComputationMethod>(storeId: storeId).ToList();
        }

        #endregion

        #region Shipping methods


        /// <summary>
        /// Deletes a shipping method
        /// </summary>
        /// <param name="shippingMethod">The shipping method</param>
        public virtual void DeleteShippingMethod(ShippingMethod shippingMethod)
        {
            if (shippingMethod == null)
                throw new ArgumentNullException("shippingMethod");

            _shippingMethodRepository.Delete(shippingMethod);

            //event notification
            _eventPublisher.EntityDeleted(shippingMethod);
        }

        /// <summary>
        /// Gets a shipping method
        /// </summary>
        /// <param name="shippingMethodId">The shipping method identifier</param>
        /// <returns>Shipping method</returns>
        public virtual ShippingMethod GetShippingMethodById(int shippingMethodId)
        {
            if (shippingMethodId == 0)
                return null;

            return _shippingMethodRepository.GetById(shippingMethodId);
        }
        
        /// <summary>
        /// Gets all shipping methods
        /// </summary>
        /// <param name="filterByCountryId">The country indentifier to filter by</param>
        /// <returns>Shipping method collection</returns>
        public virtual IList<ShippingMethod> GetAllShippingMethods(int? filterByCountryId = null)
        {
            if (filterByCountryId.HasValue && filterByCountryId.Value > 0)
            {
                var query1 = from sm in _shippingMethodRepository.Table
                             where
                             sm.RestrictedCountries.Select(c => c.Id).Contains(filterByCountryId.Value)
                             select sm.Id;

                var query2 = from sm in _shippingMethodRepository.Table
                             where !query1.Contains(sm.Id)
                             orderby sm.DisplayOrder
                             select sm;

                var shippingMethods = query2.ToList();
                return shippingMethods;
            }
            else
            {
                var query = from sm in _shippingMethodRepository.Table
                            orderby sm.DisplayOrder
                            select sm;
                var shippingMethods = query.ToList();
                return shippingMethods;
            }
        }

        /// <summary>
        /// Inserts a shipping method
        /// </summary>
        /// <param name="shippingMethod">Shipping method</param>
        public virtual void InsertShippingMethod(ShippingMethod shippingMethod)
        {
            if (shippingMethod == null)
                throw new ArgumentNullException("shippingMethod");

            _shippingMethodRepository.Insert(shippingMethod);

            //event notification
            _eventPublisher.EntityInserted(shippingMethod);
        }

        /// <summary>
        /// Updates the shipping method
        /// </summary>
        /// <param name="shippingMethod">Shipping method</param>
        public virtual void UpdateShippingMethod(ShippingMethod shippingMethod)
        {
            if (shippingMethod == null)
                throw new ArgumentNullException("shippingMethod");

            _shippingMethodRepository.Update(shippingMethod);

            //event notification
            _eventPublisher.EntityUpdated(shippingMethod);
        }

        #endregion

        #region Delivery dates

        /// <summary>
        /// Deletes a delivery date
        /// </summary>
        /// <param name="deliveryDate">The delivery date</param>
        public virtual void DeleteDeliveryDate(DeliveryDate deliveryDate)
        {
            if (deliveryDate == null)
                throw new ArgumentNullException("deliveryDate");

            _deliveryDateRepository.Delete(deliveryDate);

            //event notification
            _eventPublisher.EntityDeleted(deliveryDate);
        }

        /// <summary>
        /// Gets a delivery date
        /// </summary>
        /// <param name="deliveryDateId">The delivery date identifier</param>
        /// <returns>Delivery date</returns>
        public virtual DeliveryDate GetDeliveryDateById(int deliveryDateId)
        {
            if (deliveryDateId == 0)
                return null;

            return _deliveryDateRepository.GetById(deliveryDateId);
        }

        /// <summary>
        /// Gets all delivery dates
        /// </summary>
        /// <returns>Delivery dates</returns>
        public virtual IList<DeliveryDate> GetAllDeliveryDates()
        {
            var query = from dd in _deliveryDateRepository.Table
                        orderby dd.DisplayOrder
                        select dd;
            var deliveryDates = query.ToList();
            return deliveryDates;
        }

        /// <summary>
        /// Inserts a delivery date
        /// </summary>
        /// <param name="deliveryDate">Delivery date</param>
        public virtual void InsertDeliveryDate(DeliveryDate deliveryDate)
        {
            if (deliveryDate == null)
                throw new ArgumentNullException("deliveryDate");

            _deliveryDateRepository.Insert(deliveryDate);

            //event notification
            _eventPublisher.EntityInserted(deliveryDate);
        }

        /// <summary>
        /// Inserts original shipping rate (not rounded)
        /// </summary>
        /// <param name="rate">Original rate</param>
        public virtual void InsertOriginalShippingRate(OriginalShippingRate rate)
        {
            try
            {
                this.originalShipingRateRepository.Insert(rate);

                //event notification
                this._eventPublisher.EntityInserted(rate);
            }
            catch (Exception ex)
            {
                this._logger.InsertLog(LogLevel.Error, "Original shipping rate saving", ex.Message);
            }
            
        }

        /// <summary>
        /// Updates the delivery date
        /// </summary>
        /// <param name="deliveryDate">Delivery date</param>
        public virtual void UpdateDeliveryDate(DeliveryDate deliveryDate)
        {
            if (deliveryDate == null)
                throw new ArgumentNullException("deliveryDate");

            _deliveryDateRepository.Update(deliveryDate);

            //event notification
            _eventPublisher.EntityUpdated(deliveryDate);
        }

        #endregion

        #region Warehouses

        /// <summary>
        /// Deletes a warehouse
        /// </summary>
        /// <param name="warehouse">The warehouse</param>
        public virtual void DeleteWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException("warehouse");

            _warehouseRepository.Delete(warehouse);

            //clear cache
            _cacheManager.RemoveByPattern(WAREHOUSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(warehouse);
        }

        /// <summary>
        /// Gets a warehouse
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier</param>
        /// <returns>Warehouse</returns>
        public virtual Warehouse GetWarehouseById(int warehouseId)
        {
            if (warehouseId == 0)
                return null;

            string key = string.Format(WAREHOUSES_BY_ID_KEY, warehouseId);
            return _cacheManager.Get(key, () => _warehouseRepository.GetById(warehouseId));
        }

        /// <summary>
        /// Gets all warehouses
        /// </summary>
        /// <returns>Warehouses</returns>
        public virtual IList<Warehouse> GetAllWarehouses()
        {
            var query = from wh in _warehouseRepository.Table
                        orderby wh.Name
                        select wh;
            var warehouses = query.ToList();
            return warehouses;
        }

        /// <summary>
        /// Inserts a warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        public virtual void InsertWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException("warehouse");

            _warehouseRepository.Insert(warehouse);

            //clear cache
            _cacheManager.RemoveByPattern(WAREHOUSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(warehouse);
        }

        /// <summary>
        /// Updates the warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        public virtual void UpdateWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException("warehouse");

            _warehouseRepository.Update(warehouse);

            //clear cache
            _cacheManager.RemoveByPattern(WAREHOUSES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(warehouse);
        }

        #endregion

        #region Workflow

        /// <summary>
        /// Gets shopping cart item weight (of one item)
        /// </summary>
        /// <param name="shoppingCartItem">Shopping cart item</param>
        /// <returns>Shopping cart item weight</returns>
        public virtual decimal GetShoppingCartItemWeight(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
                throw new ArgumentNullException("shoppingCartItem");

            if (shoppingCartItem.Product == null)
                return decimal.Zero;

            //attribute weight
            decimal attributesTotalWeight = decimal.Zero;
            if (!String.IsNullOrEmpty(shoppingCartItem.AttributesXml))
            {
                var pvaValues = _productAttributeParser.ParseProductVariantAttributeValues(shoppingCartItem.AttributesXml);
                foreach (var pvaValue in pvaValues)
                {
                    switch (pvaValue.AttributeValueType)
                    {
                        case AttributeValueType.Simple:
                        {
                            //simple attribute
                            attributesTotalWeight += pvaValue.WeightAdjustment;
                        }
                            break;
                        case AttributeValueType.AssociatedToProduct:
                        {
                            //bundled product
                            var associatedProduct = _productService.GetProductById(pvaValue.AssociatedProductId);
                            if (associatedProduct != null && associatedProduct.IsShipEnabled)
                            {
                                attributesTotalWeight += associatedProduct.Weight*pvaValue.Quantity;
                            }
                        }
                            break;
                    }
                }
            }

            var weight = shoppingCartItem.Product.Weight + attributesTotalWeight;
            return weight;
        }

        /// <summary>
        /// Gets shopping cart weight
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="includeCheckoutAttributes">A value indicating whether we should calculate weights of selected checkotu attributes</param>
        /// <returns>Shopping cart weight</returns>
        public virtual decimal GetTotalWeight(IList<ShoppingCartItem> cart, bool includeCheckoutAttributes = true)
        {
            Customer customer = cart.GetCustomer();

            decimal totalWeight = decimal.Zero;
            //shopping cart items
            foreach (var shoppingCartItem in cart)
                totalWeight += GetShoppingCartItemWeight(shoppingCartItem) * shoppingCartItem.Quantity;

            //checkout attributes
            if (customer != null && includeCheckoutAttributes)
            {
                var checkoutAttributesXml = customer.GetAttribute<string>(SystemCustomerAttributeNames.CheckoutAttributes, _genericAttributeService, _storeContext.CurrentStore.Id);
                if (!String.IsNullOrEmpty(checkoutAttributesXml))
                {
                    var caValues = _checkoutAttributeParser.ParseCheckoutAttributeValues(checkoutAttributesXml);
                    foreach (var caValue in caValues)
                        totalWeight += caValue.WeightAdjustment;
                }
            }
            return totalWeight;
        }

        /// <summary>
        /// Get dimensions
        /// </summary>
        /// <param name="cart">Shipping cart items</param>
        /// <param name="width">Width</param>
        /// <param name="length">Length</param>
        /// <param name="height">Height</param>
        public virtual void GetDimensions(IList<ShoppingCartItem> cart,
            out decimal width, out decimal length, out decimal height)
        {
            if (_shippingSettings.UseCubeRootMethod)
            {
                //cube root of volume
                decimal totalVolume = 0;
                decimal maxProductWidth = 0;
                decimal maxProductLength = 0;
                decimal maxProductHeight = 0;
                foreach (var shoppingCartItem in cart)
                {
                    var product = shoppingCartItem.Product;
                    if (product != null)
                    {
                        var productWidth = product.Width;
                        var productLength = product.Length;
                        var productHeight = product.Height;
                        //attributes
                        if (!String.IsNullOrEmpty(shoppingCartItem.AttributesXml))
                        {
                            //bundled products (associated attributes)
                            var pvaValues = _productAttributeParser.ParseProductVariantAttributeValues(shoppingCartItem.AttributesXml)
                                .Where(x => x.AttributeValueType == AttributeValueType.AssociatedToProduct)
                                .ToList();
                            foreach (var pvaValue in pvaValues)
                            {
                                var associatedProduct = _productService.GetProductById(pvaValue.AssociatedProductId);
                                if (associatedProduct != null && associatedProduct.IsShipEnabled)
                                {
                                    productWidth += associatedProduct.Width * pvaValue.Quantity;
                                    productLength += associatedProduct.Length * pvaValue.Quantity;
                                    productHeight += associatedProduct.Height * pvaValue.Quantity;
                                }
                            }
                        }

                        totalVolume += shoppingCartItem.Quantity * productHeight * productWidth * productLength;

                        if (productWidth > maxProductWidth)
                            maxProductWidth = productWidth;
                        if (productLength > maxProductLength)
                            maxProductLength = productLength;
                        if (productHeight > maxProductHeight)
                            maxProductHeight = productHeight;
                    }
                }
                decimal dimension = Convert.ToDecimal(Math.Pow(Convert.ToDouble(totalVolume), (double)(1.0 / 3.0)));
                length = width = height = dimension;

                //sometimes we have products with sizes like 1x1x20
                //that's why let's ensure that a maximum dimension is always preserved
                //otherwise, shipping rate computation methods can return low rates
                if (width < maxProductWidth)
                    width = maxProductWidth;
                if (length < maxProductLength)
                    length = maxProductLength;
                if (height < maxProductHeight)
                    height = maxProductHeight;
            }
            else
            {
                //summarize all values (very inaccurate with multiple items)
                width = length = height = decimal.Zero;
                foreach (var shoppingCartItem in cart)
                {
                    var product = shoppingCartItem.Product;
                    if (product != null)
                    {
                        width += product.Width * shoppingCartItem.Quantity;
                        length += product.Length * shoppingCartItem.Quantity;
                        height += product.Height * shoppingCartItem.Quantity;
                        //attributes
                        if (!String.IsNullOrEmpty(shoppingCartItem.AttributesXml))
                        {
                            //bundled products (associated attributes)
                            var pvaValues = _productAttributeParser.ParseProductVariantAttributeValues(shoppingCartItem.AttributesXml)
                                .Where(x => x.AttributeValueType == AttributeValueType.AssociatedToProduct)
                                .ToList();
                            foreach (var pvaValue in pvaValues)
                            {
                                var associatedProduct = _productService.GetProductById(pvaValue.AssociatedProductId);
                                if (associatedProduct != null && associatedProduct.IsShipEnabled)
                                {
                                    width += associatedProduct.Width * pvaValue.Quantity;
                                    length += associatedProduct.Length * pvaValue.Quantity;
                                    height += associatedProduct.Height * pvaValue.Quantity;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets total width
        /// </summary>
        /// <param name="cart">Shipping cart items</param>
        /// <returns>Total width</returns>
        public virtual decimal GetTotalWidth(IList<ShoppingCartItem> cart)
        {
            decimal length, width, height;
            GetDimensions(cart, out width, out length, out height);
            return width;
        }

        /// <summary>
        /// Gets total length
        /// </summary>
        /// <param name="cart">Shipping cart items</param>
        /// <returns>Total length</returns>
        public virtual decimal GetTotalLength(IList<ShoppingCartItem> cart)
        {
            decimal length, width, height;
            GetDimensions(cart, out width, out length, out height);
            return length;
        }

        /// <summary>
        /// Gets total height
        /// </summary>
        /// <param name="cart">Shipping cart items</param>
        /// <returns>Total height</returns>
        public virtual decimal GetTotalHeight(IList<ShoppingCartItem> cart)
        {
            decimal length, width, height;
            GetDimensions(cart, out width, out length, out height);
            return height;
        }

        /// <summary>
        /// Get the nearest warehouse for the specified address
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="warehouses">List of warehouses, if null all warehouses are used.</param>
        /// <returns></returns>
        public virtual Warehouse GetNearestWarehouse(Address address, IList<Warehouse> warehouses = null)
        {
            warehouses = warehouses ?? GetAllWarehouses();

            //no address specified. return any
            if (address == null)
                return warehouses.FirstOrDefault();

            //of course, we should use some better logic to find nearest warehouse
            //but we don't have a built-in geographic database which supports "distance" functionality
            //that's why we simply look for exact matches

            //find by country
            var matchedByCountry = new List<Warehouse>();
            foreach (var warehouse in warehouses)
            {
                var warehouseAddress = _addressService.GetAddressById(warehouse.AddressId);
                if (warehouseAddress != null)
                    if (warehouseAddress.CountryId == address.CountryId)
                        matchedByCountry.Add(warehouse);
            }
            //no country matches. return any
            if (matchedByCountry.Count == 0)
                return warehouses.FirstOrDefault();


            //find by state
            var matchedByState = new List<Warehouse>();
            foreach (var warehouse in matchedByCountry)
            {
                var warehouseAddress = _addressService.GetAddressById(warehouse.AddressId);
                if (warehouseAddress != null)
                    if (warehouseAddress.StateProvinceId == address.StateProvinceId)
                        matchedByState.Add(warehouse);
            }
            if (matchedByState.Any())
                return matchedByState.FirstOrDefault();

            //no state matches. return any
            return matchedByCountry.FirstOrDefault();
        }

        /// <summary>
        /// Create shipment packages (requests) from shopping cart
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="shippingAddress">Shipping address</param>
        /// <returns>Shipment packages (requests)</returns>
        public virtual IList<GetShippingOptionRequest> CreateShippingOptionRequests(IList<ShoppingCartItem> cart, 
            Address shippingAddress)
        {
            //if we always ship from the default shipping origin, then there's only one request
            //if we ship from warehouses ("ShippingSettings.UseWarehouseLocation" enabled),
            //then there could be several requests


            //key - warehouse identifier (0 - default shipping origin)
            //value - request
            var requests = new Dictionary<int, GetShippingOptionRequest>();

            //a list of requests with products which should be shipped separately
            var separateRequests = new List<GetShippingOptionRequest>();

            foreach (var sci in cart)
            {
                if (!sci.IsShipEnabled)
                    continue;

                //warehouses
                Warehouse warehouse = null;
                if (_shippingSettings.UseWarehouseLocation)
                {
                    if (this._productService.GetInventoryManageMethod(sci.Product, this._storeContext.CurrentStore.Id) == ManageInventoryMethod.ManageStock &&
                        sci.Product.UseMultipleWarehouses)
                    {
                        var allWarehouses = new List<Warehouse>();
                        //multiple warehouses supported
                        foreach (var pwi in sci.Product.ProductWarehouseInventory)
                        {
                            //TODO validate stock quantity when backorder is not allowed?
                            var tmpWarehouse = GetWarehouseById(pwi.WarehouseId);
                            if (tmpWarehouse != null)
                                allWarehouses.Add(tmpWarehouse);
                        }
                        warehouse = GetNearestWarehouse(shippingAddress, allWarehouses);
                    }
                    else
                    {
                        //multiple warehouses are not supported
                        warehouse = GetWarehouseById(sci.Product.WarehouseId);
                    }
                }
                int warehouseId = warehouse != null ? warehouse.Id : 0;

                if (requests.ContainsKey(warehouseId) && !sci.Product.ShipSeparately)
                {
                    //add item to existing request
                    requests[warehouseId].Items.Add(sci);
                }
                else
                {
                    //create a new request
                    var request = new GetShippingOptionRequest();
                    //add item
                    request.Items.Add(sci);
                    //customer
                    request.Customer = cart.GetCustomer();
                    //ship to
                    request.ShippingAddress = shippingAddress;
                    //ship from
                    Address originAddress = null;
                    if (warehouse != null)
                    {
                        //warehouse address
                        originAddress = _addressService.GetAddressById(warehouse.AddressId);
                    }
                    if (originAddress == null)
                    {
                        //no warehouse address. in this case use the default shipping origin
                        originAddress = _addressService.GetAddressById(_shippingSettings.ShippingOriginAddressId);
                    }
                    if (originAddress != null)
                    {
                        request.CountryFrom = originAddress.Country;
                        request.StateProvinceFrom = originAddress.StateProvince;
                        request.ZipPostalCodeFrom = originAddress.ZipPostalCode;
                        request.CityFrom = originAddress.City;
                        request.AddressFrom = originAddress.Address1;
                    }

                    if (sci.Product.ShipSeparately)
                    {
                        //ship separately
                        separateRequests.Add(request);
                    }
                    else
                    {
                        //usual request
                        requests.Add(warehouseId, request);
                    }
                }
            }

            var result = requests.Values.ToList();
            result.AddRange(separateRequests);
            return result;
        }

        /// <summary>
        ///  Gets available shipping options
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="shippingAddress">Shipping address</param>
        /// <param name="allowedShippingRateComputationMethodSystemName">Filter by shipping rate computation method identifier; null to load shipping options of all shipping rate computation methods</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Shipping options</returns>
        public virtual GetShippingOptionResponse GetShippingOptions(IList<ShoppingCartItem> cart,
            Address shippingAddress, string allowedShippingRateComputationMethodSystemName = "", 
            int storeId = 0)
        {
            if (cart == null)
                throw new ArgumentNullException("cart");

            var result = new GetShippingOptionResponse();
            
            //create a package
            var shippingOptionRequests = CreateShippingOptionRequests(cart, shippingAddress);
            var shippingRateComputationMethods = LoadActiveShippingRateComputationMethods(storeId);
            //filter by system name
            if (!String.IsNullOrWhiteSpace(allowedShippingRateComputationMethodSystemName))
            {
                shippingRateComputationMethods = shippingRateComputationMethods
                    .Where(srcm => allowedShippingRateComputationMethodSystemName.Equals(srcm.PluginDescriptor.SystemName, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            }
            if (shippingRateComputationMethods.Count == 0)
                throw new NopException("Shipping rate computation method could not be loaded");



            //request shipping options from each shipping rate computation methods
            foreach (var srcm in shippingRateComputationMethods)
            {
                //request shipping options (separately for each package-request)
                IList<ShippingOption> srcmShippingOptions = null;
                foreach (var shippingOptionRequest in shippingOptionRequests)
                {
                    var getShippingOptionResponse = srcm.GetShippingOptions(shippingOptionRequest);

                    if (getShippingOptionResponse.Success)
                    {
                        //success
                        if (srcmShippingOptions == null)
                        {
                            //first shipping option request
                            srcmShippingOptions = getShippingOptionResponse.ShippingOptions;
                        }
                        else
                        {
                            //get shipping options which already exist for prior requested packages for this scrm (i.e. common options)
                            srcmShippingOptions = srcmShippingOptions
                                .Where(existingso => getShippingOptionResponse.ShippingOptions.Any(newso => newso.Name == existingso.Name))
                                .ToList();

                            //and sum the rates
                            foreach (var existingso in srcmShippingOptions)
                            {
                                existingso.Rate += getShippingOptionResponse
                                    .ShippingOptions
                                    .First(newso => newso.Name == existingso.Name)
                                    .Rate;
                            }
                        }
                    }
                    else
                    {
                        //errors
                        foreach (string error in getShippingOptionResponse.Errors)
                        {
                            result.AddError(error);
                            _logger.Warning(string.Format("Shipping ({0}). {1}", srcm.PluginDescriptor.FriendlyName, error));
                        }
                        //clear the shipping options in this case
                        srcmShippingOptions = new List<ShippingOption>();
                        break;
                    }
                }

                // add this scrm's options to the result
                if (srcmShippingOptions != null)
                {
                    foreach (var so in srcmShippingOptions)
                    {
                        so.ShippingRateComputationMethodSystemName = srcm.PluginDescriptor.SystemName;
                        if (_shoppingCartSettings.RoundPricesDuringCalculation)
                            so.Rate = Math.Round(so.Rate, 2);
                        result.ShippingOptions.Add(so);
                    }
                }
            }

            if (_shippingSettings.ReturnValidOptionsIfThereAreAny)
            {
                //return valid options if there are any (no matter of the errors returned by other shipping rate compuation methods).
                if (result.ShippingOptions.Count > 0 && result.Errors.Count > 0)
                    result.Errors.Clear();
            }
            
            //no shipping options loaded
            if (result.ShippingOptions.Count == 0 && result.Errors.Count == 0)
                result.Errors.Add(_localizationService.GetResource("Checkout.ShippingOptionCouldNotBeLoaded"));
            
            this.RoundShippingRates(result);

            this.AddAdditionalShippingOptions(cart, result);

            return result;
        }

        public string GetStateProvinceByZipCode(string zipCode)
        {
            string zipPrefix = string.Empty;
            string state = string.Empty;

            try
            {
                if (zipCode.Length > 0)
                    zipPrefix = zipCode.Substring(0, 3);

                var query = from zp in _zipPrefixRepository.Table
                            where zp.Prefix == zipPrefix
                            select zp.State;

                state = query.FirstOrDefault();

                if (string.IsNullOrEmpty(state))
                {
                    zipPrefix = zipCode.Substring(0, 5);

                    query = from zc in _zipCodeRepository.Table
                                where zc.ZIPCode == zipPrefix
                                select zc.State;

                    state = query.FirstOrDefault();
                }
            }
            catch
            {
                /*ZipService.USZip zipe = new ZipService.USZip();
                XmlNode docTest = zipe.GetInfoByZIP(zipCode);
                state = docTest.SelectSingleNode("STATE").InnerText;*/
                // What is it? What the zip service?
            }

            if(!string.IsNullOrEmpty(state))
                state = state.ToUpper();

            return state;
        }

        public int GetStateProvinceIdByZipCode(string zipCode, int countryId)
        {
            if (string.IsNullOrEmpty(zipCode) || countryId == 0)
            {
                return 0;
            }

            string zipPrefix = string.Empty;

            try
            {
                if (zipCode.Length > 0)
                    zipPrefix = zipCode.Substring(0, 3);

                var query = from st in _stateProvinceRepository.Table
                            join zp in _zipPrefixRepository.Table on st.Abbreviation equals zp.State
                            where zp.Prefix == zipPrefix && st.CountryId == countryId
                            select st.Id;

                return query.FirstOrDefault();
            }
            catch
            {
            }

            return 0;
        }

        public string GetCityByZipCode(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
            {
                return null;
            }

            try
            {
                if (zipCode.Length > 5)
                {
                    zipCode = zipCode.Substring(0, 5);
                }

                var query = from zc in _zipCodeRepository.Table
                            where zc.ZIPCode == zipCode
                            select zc.City;

                return query.FirstOrDefault();
            }
            catch(Exception ex)
            {
                this._logger.Error("ShippingService.GetCityByZipCode()", ex);
            }

            return null;
        }

        public bool IsFreeShipping(Product product, int storeId)
        {
            var query = from m in this.freeShippingProductRepository.TableNoTracking
                    where m.StoreId == storeId && m.ProductId == product.Id
                    select m;

            return query.SingleOrDefault() != null || product.IsFreeShipping;
        }

        private void RoundShippingRates(GetShippingOptionResponse response)
        {
            var increment = 2.5m;
            foreach (var option in response.ShippingOptions)
            {
                if (option.Name == ConstantStorage.TWO_DAY_SHIPPING_METHOD_NAME)
                {
                    continue;
                }

                if (option.Rate >= 0.01m && option.Rate <= 7.50m)
                {
                    option.OriginalRate = option.Rate;
                    option.Rate = 7.50m;
                }

                if (option.Rate > 7.50m && option.Rate < 10m)
                {
                    option.OriginalRate = option.Rate;
                    var ratio = (int)(option.Rate / increment) + 1;
                    option.Rate = ratio * increment - 0.01m;
                }

                //if (option.Rate > 7.50m && option.Rate < 60m)
                //{
                //    option.OriginalRate = option.Rate;
                //    var ratio = (int)(option.Rate / increment) + 1;
                //    option.Rate = ratio * increment - 0.01m;
                //}
            }
        }

        public void AddAdditionalShippingOptions(IList<ShoppingCartItem> items, GetShippingOptionResponse response)
        {
            if (items.Where(i => i.Product.ProductExtra != null).All(i => !i.Product.ProductExtra.IsFreight)
                && items.Where(i => i.Product.ProductExtra != null).All(i => !i.Product.ProductExtra.IsShippingFromManufacturer))
            {
                bool isClubMember = this._workContext.CurrentCustomer.IsClubMember();

                decimal rate = 0.0m, cartTotal = items.Sum(x => x.Product.Price * x.Quantity);
                if (cartTotal < 100m)
                {
                    if (response.ShippingOptions.Any(so => so.Rate == decimal.Zero))
                    {
                        rate = 4.99m;
                    }
                    else if (response.ShippingOptions.All(so => so.Rate > decimal.Zero))
                    {
                        var option = response.ShippingOptions.FirstOrDefault(x => x.Name.Contains("Ground"));
                        if (option != null)
                        {
                            rate = option.Rate + 5m;
                        }
                    }
                }
                else if (cartTotal >= 100m)
                {
                    if (response.ShippingOptions.Any(so => so.Rate == decimal.Zero))
                    {
                        rate = 9.99m;
                    }
                    else
                    {
                        var option = response.ShippingOptions.FirstOrDefault(x => x.Name.Contains("Ground"));
                        if (option != null)
                        {
                            rate = option.Rate + 5m;
                        }
                    }
                }

                var indexToInsert = _workContext.CurrentCustomer.Id == 2 && response.ShippingOptions.Any() ? 1 : 0;
                if (rate > decimal.Zero && !isClubMember)
                {
                    response.ShippingOptions.Insert(indexToInsert, new ShippingOption
                    {
                        Name = ConstantStorage.TWO_DAY_SHIPPING_METHOD_NAME,
                        Rate = rate,
                        ShippingRateComputationMethodSystemName = "Shipping.Vendor"
                    });
                }
                else if (rate > decimal.Zero)
                {
                    response.ShippingOptions.Insert(indexToInsert, new ShippingOption
                    {
                        Name = ConstantStorage.TWO_DAY_CLUB_SHIPPING_METHOD_NAME,
                        Rate = rate,
                        ShippingRateComputationMethodSystemName = "Shipping.Vendor"
                    });
                }
            }

            response.ShippingOptions = response.ShippingOptions.OrderBy(so => so.Rate).ToList();
        }

        #endregion

        public string GetStateAbbreviationByZipCode(string zip)
        {
            if (string.IsNullOrEmpty(zip))
            {
                throw new ArgumentNullException(nameof(zip));
            }

            var query = from a in this._zipCodeRepository.TableNoTracking
                        where a.ZIPCode == zip
                        select a.State;

            return query.SingleOrDefault();
        }

        #endregion
    }
}
