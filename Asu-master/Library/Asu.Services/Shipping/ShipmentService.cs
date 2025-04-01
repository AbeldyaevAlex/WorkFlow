using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Shipping;
using Asu.Services.Events;
using Carrier = Asu.Core.Domain.Shipping.ShippingService;

namespace Asu.Services.Shipping
{
    using System.Transactions;

    using Asu.Core.Domain.Returns;
    using Asu.Services.Catalog;

    /// <summary>
    /// Shipment service
    /// </summary>
    public partial class ShipmentService : IShipmentService
    {
        #region Fields

        private readonly IRepository<Shipment> _shipmentRepository;
        private readonly IRepository<ShipmentItem> _siRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<CrmShipment> crmShipmentRepository;
        private readonly IRepository<Core.Domain.Returns.ShippingService> shippingServiceRepository;
        private readonly IRepository<RmaShipment> rmaShipmentRepository;
        private readonly IRepository<RmaShipmentItem> rmaShipmentItemRepository;
        private readonly IStoreContext storeContext;
        private readonly IProductService productService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shipmentRepository">Shipment repository</param>
        /// <param name="siRepository">Shipment item repository</param>
        /// <param name="orderItemRepository">Order item repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ShipmentService(IRepository<Shipment> shipmentRepository,
            IRepository<ShipmentItem> siRepository,
            IRepository<OrderItem> orderItemRepository,
            IEventPublisher eventPublisher,
            IRepository<CrmShipment> crmShipmentRepository,
            IRepository<Core.Domain.Returns.ShippingService> shippingServiceRepository,
            IRepository<RmaShipment> rmaShipmentRepository,
            IRepository<RmaShipmentItem> rmaShipmentItemRepository,
            IStoreContext storeContext,
            IProductService productService)
        {
            this._shipmentRepository = shipmentRepository;
            this._siRepository = siRepository;
            this._orderItemRepository = orderItemRepository;
            this._eventPublisher = eventPublisher;
            this.crmShipmentRepository = crmShipmentRepository;
            this.shippingServiceRepository = shippingServiceRepository;
            this.rmaShipmentRepository = rmaShipmentRepository;
            this.rmaShipmentItemRepository = rmaShipmentItemRepository;
            this.storeContext = storeContext;
            this.productService = productService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a shipment
        /// </summary>
        /// <param name="shipment">Shipment</param>
        public virtual void DeleteShipment(Shipment shipment)
        {
            if (shipment == null)
                throw new ArgumentNullException("shipment");

            _shipmentRepository.Delete(shipment);

            //event notification
            _eventPublisher.EntityDeleted(shipment);
        }

        public string GetCarrierName(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var carrier = this.shippingServiceRepository.GetById(id);
            if (carrier == null)
            {
                return null;
            }

            return carrier.Name;
        }

        public string GetCarrierUrl(int shipmentId)
        {
            var shipment = this.GetCrmShipment(shipmentId);
            if (shipment == null)
            {
                return null;
            }

            var url = string.Empty;
            switch (shipment.ShippingServiceId)
            {
                case (int)Carrier.FedEx:
                    url = $"https://www.fedex.com/apps/fedextrack/?action=track&tracknumbers={shipment.TrackingNumber}";
                    break;
                case (int)Carrier.UpsCanadaGround:
                case (int)Carrier.UpsFreight:
                case (int)Carrier.Ups:
                    url = $"http://wwwapps.ups.com/WebTracking/track?trackNums={shipment.TrackingNumber}&track.x=Track";
                    break;
                case (int)Carrier.Usps:
                    url = $"https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1={shipment.TrackingNumber}";
                    break;
            }

            return url;
        }

        public string GetCarrierUrl(int id, string tracking)
        {
            if (id <= 0 || string.IsNullOrEmpty(tracking))
            {
                throw new ArgumentNullException();
            }

            var url = null as string;
            switch (id)
            {
                case (int)Carrier.FedEx:
                    url = $"https://www.fedex.com/apps/fedextrack/?action=track&tracknumbers={tracking}";
                    break;
                case (int)Carrier.UpsCanadaGround:
                case (int)Carrier.UpsFreight:
                case (int)Carrier.Ups:
                    url = $"http://wwwapps.ups.com/WebTracking/track?trackNums={tracking}&track.x=Track";
                    break;
                case (int)Carrier.Usps:
                    url = $"https://tools.usps.com/go/TrackConfirmAction_input?qtc_tLabels1={tracking}";
                    break;
            }

            return url;
        }

        public RmaShipment GetShipment(string tracking, int rmaId)
        {
            var query = from i in this.rmaShipmentRepository.Table
                        where i.RmaId == rmaId && i.TrackingNumber == tracking
                        select i;

            return query.FirstOrDefault();
        }

        public IList<Core.Domain.Returns.ShippingService> GetAllCarriers()
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = IsolationLevel.Serializable
            //}))
            {

                var query = from c in this.shippingServiceRepository.Table
                            select c;

                var entities = query.ToList();
                //scope.Complete();
                return entities;
            }
        }

        public IList<RmaShipment> GetShipments(int rmaId)
        {
            var query = from s in this.rmaShipmentRepository.Table
                        where s.RmaId == rmaId
                        select s;

            return query.ToList();
        }

        /// <summary>
        /// Search shipments
        /// </summary>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="warehouseId">Warehouse identifier, only shipments with products from a specified warehouse will be loaded; 0 to load all orders</param>
        /// <param name="trackingNumber">Search by tracking number</param>
        /// <param name="shippingCountryId">Shipping country identifier; 0 to load all records</param>
        /// <param name="shippingStateId">Shipping state identifier; 0 to load all records</param>
        /// <param name="shippingCity">Shipping city; null to load all records</param>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customer collection</returns>
        public virtual IPagedList<Shipment> GetAllShipments(int vendorId = 0, int warehouseId = 0,
            int shippingCountryId = 0,
            int shippingStateId = 0,
            string shippingCity = null,
            string trackingNumber = null,
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _shipmentRepository.Table;
            if (!String.IsNullOrEmpty(trackingNumber))
                query = query.Where(s => s.TrackingNumber.Contains(trackingNumber));
            if (shippingCountryId > 0)
                query = query.Where(s => s.Order.ShippingAddress.CountryId == shippingCountryId);
            if (shippingStateId > 0)
                query = query.Where(s => s.Order.ShippingAddress.StateProvinceId == shippingStateId);
            if (!String.IsNullOrWhiteSpace(shippingCity))
                query = query.Where(s => s.Order.ShippingAddress.City.Contains(shippingCity));
            if (createdFromUtc.HasValue)
                query = query.Where(s => createdFromUtc.Value <= s.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(s => createdToUtc.Value >= s.CreatedOnUtc);
            query = query.Where(s => s.Order != null && !s.Order.Deleted);
            if (vendorId > 0)
            {
                var queryVendorOrderItems = from orderItem in _orderItemRepository.Table
                                             where orderItem.Product.VendorId == vendorId
                                             select orderItem.Id;

                query = from s in query
                        where queryVendorOrderItems.Intersect(s.ShipmentItems.Select(si => si.OrderItemId)).Any()
                        select s;
            }
            if (warehouseId > 0)
            {
                query = from s in query
                        where s.ShipmentItems.Any(si => si.WarehouseId == warehouseId)
                        select s;
            }
            query = query.OrderByDescending(s => s.CreatedOnUtc);

            var shipments = new PagedList<Shipment>(query, pageIndex, pageSize);
            return shipments;
        }

        /// <summary>
        /// Get shipment by identifiers
        /// </summary>
        /// <param name="shipmentIds">Shipment identifiers</param>
        /// <returns>Shipments</returns>
        public virtual IList<Shipment> GetShipmentsByIds(int[] shipmentIds)
        {
            if (shipmentIds == null || shipmentIds.Length == 0)
                return new List<Shipment>();

            var query = from o in _shipmentRepository.Table
                        where shipmentIds.Contains(o.Id)
                        select o;
            var shipments = query.ToList();
            //sort by passed identifiers
            var sortedOrders = new List<Shipment>();
            foreach (int id in shipmentIds)
            {
                var shipment = shipments.Find(x => x.Id == id);
                if (shipment != null)
                    sortedOrders.Add(shipment);
            }
            return sortedOrders;
        }

        /// <summary>
        /// Gets a shipment
        /// </summary>
        /// <param name="shipmentId">Shipment identifier</param>
        /// <returns>Shipment</returns>
        public virtual Shipment GetShipmentById(int shipmentId)
        {
            if (shipmentId == 0)
                return null;

            return _shipmentRepository.GetById(shipmentId);
        }

        public CrmShipment GetCrmShipment(int id)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = IsolationLevel.Serializable
            //}))
            {

                var query = from a in this.crmShipmentRepository.Table
                            where a.Id == id
                            select a;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        /// <summary>
        /// Inserts a shipment
        /// </summary>
        /// <param name="shipment">Shipment</param>
        public virtual void InsertShipment(Shipment shipment)
        {
            if (shipment == null)
                throw new ArgumentNullException("shipment");

            _shipmentRepository.Insert(shipment);
        }

        /// <summary>
        /// Inserts an RMA shipment
        /// </summary>
        /// <param name="shipment">Shipment</param>
        public virtual void InsertShipment(RmaShipment shipment)
        {
            if (shipment == null)
            {
                throw new ArgumentNullException(nameof(shipment));
            }

            this.rmaShipmentRepository.Insert(shipment);

            //event notification
            _eventPublisher.EntityInserted(shipment);
        }

        /// <summary>
        /// Updates the shipment
        /// </summary>
        /// <param name="shipment">Shipment</param>
        public virtual void UpdateShipment(Shipment shipment)
        {
            if (shipment == null)
                throw new ArgumentNullException("shipment");

            _shipmentRepository.Update(shipment);

            //event notification
            _eventPublisher.EntityUpdated(shipment);
        }


        
        /// <summary>
        /// Deletes a shipment item
        /// </summary>
        /// <param name="shipmentItem">Shipment item</param>
        public virtual void DeleteShipmentItem(ShipmentItem shipmentItem)
        {
            if (shipmentItem == null)
                throw new ArgumentNullException("shipmentItem");

            _siRepository.Delete(shipmentItem);

            //event notification
            _eventPublisher.EntityDeleted(shipmentItem);
        }

        /// <summary>
        /// Gets a shipment item
        /// </summary>
        /// <param name="shipmentItemId">Shipment item identifier</param>
        /// <returns>Shipment item</returns>
        public virtual ShipmentItem GetShipmentItemById(int shipmentItemId)
        {
            if (shipmentItemId == 0)
                return null;

            return _siRepository.GetById(shipmentItemId);
        }
        
        /// <summary>
        /// Inserts a shipment item
        /// </summary>
        /// <param name="shipmentItem">Shipment item</param>
        public virtual void InsertShipmentItem(ShipmentItem shipmentItem)
        {
            if (shipmentItem == null)
                throw new ArgumentNullException("shipmentItem");

            _siRepository.Insert(shipmentItem);

            //event notification
            _eventPublisher.EntityInserted(shipmentItem);
        }

        /// <summary>
        /// Inserts an RMA shipment item
        /// </summary>
        /// <param name="shipmentItem">Shipment item</param>
        public virtual void InsertShipmentItem(RmaShipmentItem shipmentItem)
        {
            if (shipmentItem == null)
            {
                throw new ArgumentNullException(nameof(shipmentItem));
            }
                

            this.rmaShipmentItemRepository.Insert(shipmentItem);
        }

        /// <summary>
        /// Updates the shipment item
        /// </summary>
        /// <param name="shipmentItem">Shipment item</param>
        public virtual void UpdateShipmentItem(ShipmentItem shipmentItem)
        {
            if (shipmentItem == null)
                throw new ArgumentNullException("shipmentItem");

            _siRepository.Update(shipmentItem);

            //event notification
            _eventPublisher.EntityUpdated(shipmentItem);
        }




        /// <summary>
        /// Get quantity in shipments. For example, get planned quantity to be shipped
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="warehouseId">Warehouse identifier</param>
        /// <param name="ignoreShipped">Ignore already shipped shipments</param>
        /// <param name="ignoreDelivered">Ignore already delivered shipments</param>
        /// <returns>Quantity</returns>
        public virtual int GetQuantityInShipments(Product product, int warehouseId,
            bool ignoreShipped, bool ignoreDelivered)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //only products with "use multiple warehouses" are handled this way
            this.productService.GetInventoryManageMethod(product, this.storeContext.CurrentStore.Id);
            if (product.ManageInventoryMethod != ManageInventoryMethod.ManageStock)
                return 0;
            if (!product.UseMultipleWarehouses)
                return 0;

            const int cancelledOrderStatusId = (int)OrderStatus.Cancelled;


            var query = _siRepository.Table;
            query = query.Where(si => !si.Shipment.Order.Deleted);
            query = query.Where(si => si.Shipment.Order.OrderStatusId != cancelledOrderStatusId);
            if (warehouseId > 0)
                query = query.Where(si => si.WarehouseId == warehouseId);
            if (ignoreShipped)
                query = query.Where(si => !si.Shipment.ShippedDateUtc.HasValue);
            if (ignoreDelivered)
                query = query.Where(si => !si.Shipment.DeliveryDateUtc.HasValue);

            var queryProductOrderItems = from orderItem in _orderItemRepository.Table
                                         where orderItem.ProductId == product.Id
                                         select orderItem.Id;
            query = from si in query
                    where queryProductOrderItems.Any(orderItemId => orderItemId == si.OrderItemId)
                    select si;

            //some null validation
            var result = Convert.ToInt32(query.Sum(si => (int?)si.Quantity));
            //UNDONE process associated products (AttributesXml)
            return result;
        }


        #endregion
    }
}
