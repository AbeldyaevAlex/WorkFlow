using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Returns;
using Asu.Core.Domain.Shipping;
using Asu.Services.Logging;
using Asu.Services.Messages;
using Asu.Services.Orders;
using Asu.Services.Stores;
using Asu.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asu.Services.Customization
{
    public class ShipmentDeliveredNotificationTask : ITask
    {
        private readonly IRepository<Shipment> shipmentRepository;
        private readonly IRepository<CrmShipment> crmShipmentRepository;
        private readonly IRepository<CrmSalesOrder> crmSalesOrderRepository;
        private readonly IRepository<ManualOrderDeliveredWithoutEmailNotification> manualOrderDeliveredWithoutEmailNotificationRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly ICustomService customService;
        private readonly IStoreContext storeContext;
        private readonly IOrderProcessingService orderProcessingService;
        private readonly ILogger logger;
        private static readonly object Locker = new object();
        private const string LOCKER_NAME = "ShipmentDeliveredEmailLocker";
        private readonly IStoreService storeService;
        private readonly IQueuedEmailSendGridService queuedEmailSendGridService;
        private static readonly Random randomizer = new Random();
        private readonly string subjectTemplate = "Your shipment has arrived!";
        //private readonly ManualOrderShipment manualOrderShipment;
        //private readonly ShipmentLine shipmentLine;
        private readonly IManualOrderService manualOrderService;

        public ShipmentDeliveredNotificationTask(IRepository<Shipment> shipmentRepository,
           ICustomService customService,
           IStoreContext storeContext,
           IOrderProcessingService orderProcessingService,
           ILogger logger,
           IRepository<Order> orderRepository,
           IStoreService storeService,
           IQueuedEmailSendGridService queuedEmailSendGridService,
           IManualOrderService manualOrderService,
           IRepository<CrmShipment> crmShipmentRepository,
           IRepository<CrmSalesOrder> crmSalesOrderRepository,
           IRepository<ManualOrderDeliveredWithoutEmailNotification> manualOrderDeliveredWithoutEmailNotificationRepository
        )
        {
            this.shipmentRepository = shipmentRepository;
            this.customService = customService;
            this.storeContext = storeContext;
            this.orderProcessingService = orderProcessingService;
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.storeService = storeService;
            this.queuedEmailSendGridService = queuedEmailSendGridService;
            this.manualOrderService = manualOrderService;
            this.crmShipmentRepository = crmShipmentRepository;
            this.crmSalesOrderRepository = crmSalesOrderRepository;
            this.manualOrderDeliveredWithoutEmailNotificationRepository = manualOrderDeliveredWithoutEmailNotificationRepository;
        }

        public void Execute()
        {
            if (Environment.MachineName.ToLower() != "web01")
            {
                return;
            }
            try
            {
                if (this.customService.IsLocked(LOCKER_NAME, 60 * 60))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.logger.Error($"Error with Shipment Delivered Task queue locker checking. {ex.Message}", ex);
                return;
            }

            this.customService.SetLocked(LOCKER_NAME);

            var dateLimit = DateTime.UtcNow.AddDays(-14);
            var featureDate = DateTime.ParseExact("08/01/2021", "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var shipments = this.shipmentRepository.Table
                .Where(s => (s.IsEmailSent.HasValue && s.IsEmailSent.Value) && !s.DeliveryDateUtc.HasValue && s.Order.CreatedOnUtc > featureDate && s.ShippedDateUtc.HasValue && s.ShippedDateUtc.Value >= dateLimit) 
                .OrderBy(s => s.Id)
                .ToList();

            var stores = this.storeService.GetAllStores();
            foreach (var shipment in shipments)
            {
                try
                {
                    var crmShipment = this.crmShipmentRepository.TableNoTracking.FirstOrDefault(s => s.TrackingNumber == shipment.TrackingNumber);
                    if (crmShipment == null || !crmShipment.DeliveredOn.HasValue || crmShipment.DeliveredOn.Value.Date != DateTime.UtcNow.Date)
                    {
                        continue;
                    }

                    var toEmail = shipment.Order.ShippingAddress.Email;
                    var store = stores.Single(m => m.Id == shipment.Order.StoreId);
                    var alreadySentEmails = this.queuedEmailSendGridService.SearchEmails(null, toEmail, null, null, false, 7, true, 0, 100);
                    var subject = string.Format(this.subjectTemplate);
                    if (alreadySentEmails.Where(m => m.Subject == subject).Any(m => m.Data.IndexOf($"{shipment.TrackingNumber}", StringComparison.Ordinal) > -1))
                    {
                        continue;
                    }

                    this.orderProcessingService.Deliver(shipment, true);
                }
                catch (Exception ex)
                {
                    this.logger.Error($"Error with Shipment Delivered Task email sending. {ex.Message}", ex);
                }
                finally
                {

                }
            }

            //Manual Orders
            var manualOrderCrmShipments = (from a in this.crmShipmentRepository.TableNoTracking
                                          join b in this.manualOrderDeliveredWithoutEmailNotificationRepository.TableNoTracking on a.Id equals b.ShipmentId
                                           where (this.storeContext.CurrentStore.Id == (int)NopStore.Autoplicity && b.ChannelId == (int)Channel.ManualOrdersAp)
                                                                || (this.storeContext.CurrentStore.Id == (int)NopStore.Thmotorsports && b.ChannelId == (int)Channel.ManualOrdersThm)
                                           select a).ToList();

            foreach (var shipment in manualOrderCrmShipments)
            {
                try
                {
                    var crmSalesOrder = (from a in this.crmSalesOrderRepository.TableNoTracking
                                                   join b in this.manualOrderDeliveredWithoutEmailNotificationRepository.TableNoTracking on a.Id equals b.SalesOrderId
                                                   where b.ShipmentId == shipment.Id
                                                   select a).FirstOrDefault();

                    this.manualOrderService.SendManualOrderDelivered(crmSalesOrder, shipment);
                }
                catch (Exception ex)
                {
                    this.logger.Error($"Error with Shipment Delivered Task email sending. ManualOrders. {ex.Message}", ex);
                }
            }

            this.customService.SetUnlocked(LOCKER_NAME);
        }
    }
}
