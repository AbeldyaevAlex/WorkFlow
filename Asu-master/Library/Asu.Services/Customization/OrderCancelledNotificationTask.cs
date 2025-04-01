using System;
using System.Linq;
using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Messages;
using Asu.Core.Domain.Orders;
using Asu.Services.Logging;
using Asu.Services.Messages;
using Asu.Services.Orders;
using Asu.Services.Tasks;

namespace Asu.Services.Customization
{
    using Asu.Core.Domain.Customization;
    using Asu.Core.Domain.Returns;
    using System.Threading;

    public class OrderCancelledNotificationTask : ITask
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<CrmSalesOrder> crmSalesOrderRepository;
        private readonly IRepository<CancelledOrderWithoutEmailNotification> cancelledOrderWithoutEmailNotificationRepository;
        private readonly IRepository<CancelledManualOrderWithoutEmailNotification> cancelledManualOrderWithoutEmailNotificationRepository;
        private readonly IOrderService orderService;
        private readonly IMessageTemplateService messageTemplateService;
        private readonly ICustomService customService;
        private readonly IStoreContext storeContext;
        private readonly IWorkflowMessageService workflowMessageService;
        private readonly IManualOrderService manualOrderService;
        private readonly ILogger logger;
        private const string LOCKER_NAME = "OrderCancelledEmailLocker";
        private static readonly Random Randomizer = new Random();
        private readonly IKlaviyoService klaviyoService;

        public OrderCancelledNotificationTask(IRepository<Order> orderRepository, 
            IRepository<CrmSalesOrder> crmSalesOrderRepository,
            IRepository<CancelledOrderWithoutEmailNotification> cancelledOrderWithoutEmailNotificationRepository,
            IRepository<CancelledManualOrderWithoutEmailNotification> cancelledManualOrderWithoutEmailNotificationRepository,
            IOrderService orderService,
            IMessageTemplateService messageTemplateService,
            ICustomService customService,
            IStoreContext storeContext,
            IWorkflowMessageService workflowMessageService,
            IManualOrderService manualOrderService,
            ILogger logger,
            IKlaviyoService klaviyoService)
        {
            this.orderRepository = orderRepository;
            this.crmSalesOrderRepository = crmSalesOrderRepository;
            this.cancelledOrderWithoutEmailNotificationRepository = cancelledOrderWithoutEmailNotificationRepository;
            this.cancelledManualOrderWithoutEmailNotificationRepository = cancelledManualOrderWithoutEmailNotificationRepository;
            this.orderService = orderService;
            this.messageTemplateService = messageTemplateService;
            this.customService = customService;
            this.storeContext = storeContext;
            this.workflowMessageService = workflowMessageService;
            this.manualOrderService = manualOrderService;
            this.logger = logger;
            this.klaviyoService = klaviyoService;
        }

        public void Execute()
        {
            if (Environment.MachineName.ToLower() != "web01")
            {
                return;
            }
            Thread.Sleep(Randomizer.Next(3000, 10000));
            try
            {
                if (this.customService.IsLocked(LOCKER_NAME, 60 * 60))
                {
                    return;
                }
            }
            catch (Exception exc)
            {
                this.logger.Error($"Error with OrderCancelled queue locker checking. {exc.Message}", exc);
                return;
            }

            this.customService.SetLocked(LOCKER_NAME);

            try
            {
                var query = from a in this.orderRepository.Table
                            join b in this.cancelledOrderWithoutEmailNotificationRepository.Table on a.Id equals b.OrderId
                            where b.StoreId == this.storeContext.CurrentStore.Id
                            select a;

                var orders = query.OrderByDescending(i => i.Id).Take(100).ToList();
                foreach (var order in orders)
                {
                    int queuedEmailId = this.workflowMessageService.SendOrderCancelledCustomerNotification(order, order.CustomerLanguageId);
                    if (queuedEmailId > 0)
                    {
                        order.OrderNotes.Add(new OrderNote
                        {
                            Note = $"\"Order cancelled\" email (to customer) has been queued. Queued email identifier: {queuedEmailId}.",
                            DisplayToCustomer = false,
                            CreatedOnUtc = DateTime.UtcNow
                        });

                        this.klaviyoService.TrackCanceledOrder(order);
                        this.orderService.UpdateOrder(order);
                    }
                }

                //manual orders
                var startDate = DateTime.UtcNow.AddDays(-30);
                var manualOrdersWithoutEmailNotification = (from a in crmSalesOrderRepository.TableNoTracking
                                                            join b in this.cancelledManualOrderWithoutEmailNotificationRepository.TableNoTracking on a.Id equals b.SalesOrderId
                                                            where (this.storeContext.CurrentStore.Id == (int)NopStore.Autoplicity && b.ChannelId == (int)Channel.ManualOrdersAp) 
                                                                    || (this.storeContext.CurrentStore.Id == (int)NopStore.Thmotorsports && b.ChannelId == (int)Channel.ManualOrdersThm)
                                                            select a).ToList();

                foreach (var order in manualOrdersWithoutEmailNotification)
                {
                    this.manualOrderService.SendManualOrderCancelledCustomerNotification(order);
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("OrderCancelledNotificationTask. {0}", ex.Message), ex);
            }

            this.customService.SetUnlocked(LOCKER_NAME);
        }
    }
}
