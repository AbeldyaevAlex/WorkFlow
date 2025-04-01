namespace Asu.Services.Orders
{
    using Asu.Core.Data;
    using Asu.Core.Domain.Customization;
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.Payments;
    using Asu.Services.Common;
    using Asu.Services.Customization;
    using Asu.Services.Logging;
    using Asu.Services.Messages;
    using Asu.Services.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateOrderDelayedTask : ITask
    {
        private readonly ILogger logger;
        private readonly ICustomService customService;
        private readonly IWorkflowMessageService workflowMessageService;
        private readonly IRepository<OrderProductVariantStock> orderProductVariantStockRepository;
        private readonly IOrderService orderService;
        private static readonly Random randomizer = new Random();
        private readonly IQueuedEmailService queuedEmailService;
        private readonly string subjectTemplate = "Update On Your Order | {0} {1}";

        private static readonly object locker = new object();
        private const string LOCKER_NAME = "UpdateOrderDelayedLocker";

        public UpdateOrderDelayedTask(ILogger logger, 
            ICustomService customService, 
            IWorkflowMessageService workflowMessageService, 
            IOrderService orderService, 
            IRepository<OrderProductVariantStock> orderProductVariantStockRepository,
            IQueuedEmailService queuedEmailService)
        {
            this.logger = logger;
            this.customService = customService;
            this.workflowMessageService = workflowMessageService;
            this.orderService = orderService;
            this.orderProductVariantStockRepository = orderProductVariantStockRepository;
            this.queuedEmailService = queuedEmailService;
        }

        public void Execute()
        {
            Thread.Sleep(randomizer.Next(10000, 60000));
            lock (locker)
            {
                try
                {
                    if (this.customService.IsLocked(LOCKER_NAME, 60 * 60))
                    {
                        return;
                    }
                }
                catch (Exception exc)
                {
                    this.logger.Error(string.Format("UpdateOrderDelayedTask says: cannot check lock status. {0}", exc.Message), exc);
                }

                this.customService.SetLocked(LOCKER_NAME);


                var ordersQuery = from o in this.orderService.GetOrdersByTimeRange(DateTime.UtcNow.AddDays(-14))
                                  where o.PaymentStatusId != (int)PaymentStatus.Pending && o.OrderStatusId != (int)OrderStatus.Cancelled && (DateTime.UtcNow - o.CreatedOnUtc).Days > 1
                                  select o;

                var orderVariantProductQuery = from a in this.orderProductVariantStockRepository.TableNoTracking
                                               where a.OrderId.HasValue && a.ProductId.HasValue && a.Cost.HasValue
                                                    && (a.Cost < a.InStockLowestCost * 0.98m && a.Cost < a.InStockLowestCost - 5 || !a.InStockLowestCost.HasValue)
                                               select a;

                var query = (from a in ordersQuery
                            join b in orderVariantProductQuery on a.Id equals b.OrderId
                            select a).ToList();

                var orderShipmentEtaList = this.customService.GetOrderShipmentEta();
                foreach (var order in query)
                {
                    var toEmail = order.ShippingAddress.Email;
                    var alreadySentEmails = this.queuedEmailService.SearchEmails(null, toEmail, null, null, false, 7, true, 0, 50);
                    if (alreadySentEmails.Any(m => m.Subject == string.Format(this.subjectTemplate, Enum.GetName(typeof(NopStore), order.StoreId), order.Id))) 
                    {
                        continue;
                    }

                    if (order.StoreId == (int)NopStore.Boatplicity)
                    {
                        this.workflowMessageService.SendOldUpdateOrderDelayedOutstockLowestCostCustomerNotification(order);
                    }
                    else
                    {
                        var eta = orderShipmentEtaList.SingleOrDefault(m => m.OrderId == order.Id)?.ShipmentEta ?? DateTime.UtcNow.AddDays(7);
                        if (eta >= DateTime.UtcNow)
                        {
                            this.workflowMessageService.SendUpdateOrderDelayedOutstockLowestCostCustomerNotification(order, (eta - DateTime.UtcNow).Days);
                        }
                    }
                } 
                
                customService.SetUnlocked(LOCKER_NAME);
            }
        }
    }
}
