using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Messages;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Customization
{
    public class BackorderUpdateEtaEmailNotificationTask : ITask
    {
        private readonly IRepository<Shipment> shipmentRepository;
        private readonly IRepository<CrmShipment> crmShipmentRepository;
        private readonly IRepository<CrmSalesOrder> crmSalesOrderRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<PurchaseOrder> purchaseOrderRepository;
        private readonly IRepository<BackorderNotification> backorderNotificationRepository;
        private readonly IRepository<Backorder> backorderRepository;
        private readonly ICustomService customService;
        private readonly IOrderProcessingService orderProcessingService;
        private readonly IWorkflowMessageService workflowMessageService;
        private readonly IOrderService orderService;
        private readonly ILogger logger;
        private static readonly object Locker = new object();
        private const string LOCKER_NAME = "OrderShippedEmailLocker";
        private readonly ShopperApprovedSettings shopperApprovedSettings;
        private readonly IStoreService storeService;
        private readonly IQueuedEmailSendGridService queuedEmailSendGridService;
        private static readonly Random randomizer = new Random();

        public BackorderUpdateEtaEmailNotificationTask(
            ICustomService customService,
            IOrderProcessingService orderProcessingService,
            IRepository<CrmSalesOrder> crmSalesOrderRepository,
            ILogger logger,
            ShopperApprovedSettings shopperApprovedSettings,
            IRepository<Order> orderRepository,
            IStoreService storeService,
            IQueuedEmailSendGridService queuedEmailSendGridService,
            IRepository<BackorderNotification> backorderNotificationRepository, 
            IRepository<PurchaseOrder> purchaseOrderRepository,
            IWorkflowMessageService workflowMessageService,
            IOrderService orderService,
            IRepository<Backorder> backorderRepository)
        {
            this.customService = customService;
            this.orderProcessingService = orderProcessingService;
            this.logger = logger;
            this.shopperApprovedSettings = shopperApprovedSettings;
            this.orderRepository = orderRepository;
            this.storeService = storeService;
            this.queuedEmailSendGridService = queuedEmailSendGridService;
            this.crmSalesOrderRepository = crmSalesOrderRepository;
            this.backorderNotificationRepository = backorderNotificationRepository;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.workflowMessageService = workflowMessageService;
            this.orderService = orderService;
            this.backorderRepository = backorderRepository;
        }

        public void Execute()
        {
            try
            {
                if (this.customService.IsLocked(LOCKER_NAME, 300))
                {
                    return;
                }
            }
            catch (Exception exc)
            {
                this.logger.Error(string.Format("Error when OrderEtaNotificationTask locker checking. {0}", exc.Message), exc);
                return;
            }

            this.customService.SetLocked(LOCKER_NAME);

            //this.SendOosWithEtaNotifications();

            var backorders = this.backorderRepository.TableNoTracking
                .OrderBy(bo => bo.OrderNumber)
                .ToList();

            foreach (var bo in backorders)
            {
                var sentBackorderNotifications = this.backorderNotificationRepository.Table
                    .Where(bon => bon.PurchaseOrderId == bo.PurchaseOrderId)
                    .ToList();

                if (sentBackorderNotifications.All(sbon => sbon.UpdatedOn != bo.StartedOn && sbon.Esd != bo.Esd))
                {
                    int orderId;
                    if (int.TryParse(bo.OrderNumber, out orderId) && orderId > 0)
                    {
                        var order = this.orderService.GetOrderById(orderId);
                        var isEsdSet = bo.Esd.HasValue;
                        var queuedEmailId = bo.Esd.HasValue 
                            ? this.workflowMessageService.SendUpdatedEtaCustomerNotification(order, bo.Esd.Value) 
                            : this.workflowMessageService.SendUpdatedEtaNoDateCustomerNotification(order);
                        
                        if (queuedEmailId > 0)
                        {
                            this.backorderNotificationRepository.Insert(new BackorderNotification()
                            {
                                PurchaseOrderId = bo.PurchaseOrderId,
                                Esd = bo.Esd.HasValue ? bo.Esd.Value : (DateTime?)null,
                                TypeId = (int)(bo.Esd.HasValue ? BackorderNotificationType.UpdatedETA : BackorderNotificationType.UpdatedETANoDate),
                                UpdatedOn = bo.StartedOn,
                                SentOn = DateTime.UtcNow
                            });
                        }
                    }
                }
            }

            this.customService.SetUnlocked(LOCKER_NAME);
        }

        private void SendOosWithEtaNotifications()
        {
            var orders = this.orderRepository.TableNoTracking
                .OrderByDescending(o => o.Id)
                .Take(10)
                .ToList();

            foreach (var order in orders)
            {
                var backorderCustomerNotificationQueuedEmailId = this.workflowMessageService.SendOosWithEtaCustomerNotification(order, order.CustomerLanguageId);
            }
        }
    }
}
