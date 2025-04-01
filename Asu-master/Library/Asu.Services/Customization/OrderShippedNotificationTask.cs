using System;
using System.Globalization;

namespace Asu.Services.Customization
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web.Services.Protocols;

    using Core.Data;
    using Core.Domain.Shipping;
    using Logging;

    using Asu.Core;
    using Asu.Core.Domain.Catalog;
    using Asu.Core.Domain.Customization;
    using Asu.Core.Domain.Messages;
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.Returns;
    using Asu.Services.Configuration;
    using Asu.Services.Messages;
    using Asu.Services.Stores;
    using Orders;
    using Tasks;

    public class OrderShippedNotificationTask : ITask
    {
        private readonly IRepository<Shipment> shipmentRepository;
        private readonly IRepository<CrmShipment> crmShipmentRepository;
        private readonly IRepository<CrmSalesOrder> crmSalesOrderRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<ManualOrderShipmentsWithoutEmailNotification> manualOrderShipmentsWithoutEmailNotificationRepository;
        private readonly ICustomService customService;
        private readonly IStoreContext storeContext;
        private readonly IOrderProcessingService orderProcessingService;
        private readonly ILogger logger;
        private static readonly object Locker = new object();
        private const string LOCKER_NAME = "OrderShippedEmailLocker";
        private readonly ShopperApprovedSettings shopperApprovedSettings;
        private readonly IStoreService storeService;
        private readonly IQueuedEmailSendGridService queuedEmailSendGridService;
        private static readonly Random randomizer = new Random();
        private readonly string subjectTemplate = "Your order #{0} from {1} has been shipped.";
        private readonly int batchSize = 200;
        //private readonly ManualOrderShipment manualOrderShipment;
        //private readonly ShipmentLine shipmentLine;
        private readonly IManualOrderService manualOrderService;

        public OrderShippedNotificationTask(IRepository<Shipment> shipmentRepository,
            ICustomService customService,
            IStoreContext storeContext,
            IOrderProcessingService orderProcessingService,
            IRepository<CrmShipment> crmShipmentRepository,
            IRepository<CrmSalesOrder> crmSalesOrderRepository,
            IRepository<ManualOrderShipmentsWithoutEmailNotification> manualOrderShipmentsWithoutEmailNotificationRepository,
            ILogger logger,
            ShopperApprovedSettings shopperApprovedSettings,
            IRepository<Order> orderRepository,
            IStoreService storeService,
            IQueuedEmailSendGridService queuedEmailSendGridService,
            IManualOrderService manualOrderService)
        {
            this.shipmentRepository = shipmentRepository;
            this.customService = customService;
            this.storeContext = storeContext;
            this.orderProcessingService = orderProcessingService;
            this.logger = logger;
            this.shopperApprovedSettings = shopperApprovedSettings;
            this.orderRepository = orderRepository;
            this.storeService = storeService;
            this.queuedEmailSendGridService = queuedEmailSendGridService;
            this.crmShipmentRepository = crmShipmentRepository;
            this.manualOrderService = manualOrderService;
            this.manualOrderShipmentsWithoutEmailNotificationRepository = manualOrderShipmentsWithoutEmailNotificationRepository;
            this.crmSalesOrderRepository = crmSalesOrderRepository;
        }

        public void Execute()
        {
            Thread.Sleep(randomizer.Next(10000, 60000));
            try
            {
                if (this.customService.IsLocked(LOCKER_NAME, 60 * 60))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.logger.Error($"Error with OrderShipped queue locker checking. {ex.Message}", ex);
                return;
            }

            try
            {
                this.customService.SetLocked(LOCKER_NAME);

                var dateLimit = DateTime.UtcNow.AddHours(-72);
                var featureDate = DateTime.ParseExact("10/01/2021", "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var shipments = this.shipmentRepository.Table
                    .Where(s => (!s.IsEmailSent.HasValue || !s.IsEmailSent.Value) && s.Order.CreatedOnUtc > featureDate && s.CreatedOnUtc >= dateLimit)
                    .OrderBy(s => s.Order.CreatedOnUtc)
                    .Take(batchSize)
                    .ToList();

                var stores = this.storeService.GetAllStores();

                foreach (var shipment in shipments)
                {
                    try
                    {
                        var toEmail = shipment.Order.ShippingAddress.Email;
                        var store = stores.Single(m => m.Id == shipment.Order.StoreId);
                        var alreadySentEmails = this.queuedEmailSendGridService.SearchEmails(null, toEmail, null, null, false, 7, true, 0, batchSize);
                        var subject = string.Format(this.subjectTemplate, shipment.OrderId, store.Name);
                        if (alreadySentEmails.Where(m => m.Subject == subject).Any(m => m.Data.IndexOf($"\"ShipmentTrackingNumber\":{shipment.TrackingNumber}", StringComparison.Ordinal) > -1))
                        {
                            continue;
                        }

                        // API "Cancel" call to shopperapproved.com
                        if ((DateTime.UtcNow - shipment.Order.CreatedOnUtc).TotalHours > 48)
                        {
                            this.ShopperApprovedApiCall(shipment.Order.Id);
                        }

                        this.orderProcessingService.Ship(shipment, true);
                    }
                    catch (Exception ex)
                    {
                        this.logger.Error($"OrderShippedNotificationTask. {ex.Message}", ex);
                    }
                }

                //Manual Orders
                var manualOrderCrmShipments = (from a in this.crmShipmentRepository.TableNoTracking
                                   join b in this.manualOrderShipmentsWithoutEmailNotificationRepository.TableNoTracking on a.Id equals b.ShipmentId
                                   where (this.storeContext.CurrentStore.Id == (int)NopStore.Autoplicity && b.ChannelId == (int)Channel.ManualOrdersAp)
                                            || (this.storeContext.CurrentStore.Id == (int)NopStore.Thmotorsports && b.ChannelId == (int)Channel.ManualOrdersThm)
                                   select a).ToList();

                foreach (var shipment in manualOrderCrmShipments)
                {
                    try
                    {
                        CrmSalesOrder crmSalesOrder = (from a in this.crmSalesOrderRepository.TableNoTracking
                                                       join b in this.manualOrderShipmentsWithoutEmailNotificationRepository.TableNoTracking on a.Id equals b.SalesOrderId
                                                       where b.ShipmentId == shipment.Id
                                                       select a).FirstOrDefault();

                        this.manualOrderService.SendManualOrderShipment(crmSalesOrder, shipment);
                    }
                    catch (Exception ex)
                    {
                        this.logger.Error($"OrderShippedNotificationTask. ManualOrders. {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Error($"OrderShippedNotificationTask failed. {ex.Message}", ex);
            }
            finally
            {
                this.customService.SetUnlocked(LOCKER_NAME);
            }
        }

        private void ShopperApprovedApiCall(int orderId)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create($"{this.shopperApprovedSettings.EndPoint}?siteid={this.shopperApprovedSettings.SiteId}&token={this.shopperApprovedSettings.Token}&cancel=1&orderid={orderId}");
                request.Method = WebRequestMethods.Http.Post;
                request.Timeout = 10000;
                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    var responseString = new StreamReader(responseStream).ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                this.logger.Error($"OrderShippedNotificationTask. ShopperApprovedApiCall. {ex.Message}", ex);
            }
        }
    }
}
