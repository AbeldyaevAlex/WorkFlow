using Asu.Core.Data;
using Asu.Core.Domain.Orders;
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
using System.Threading;
using System.Threading.Tasks;

namespace Asu.Services.Customization
{
    public class EbayOrderDeliveryNotificationTask : ITask
    {
        private readonly ICustomService customService;
        private readonly ILogger logger;
        private static readonly object Locker = new object();
        private const string LOCKER_NAME = "EbayOrderDeliveredTask";
        private readonly IQueuedEmailSendGridService queuedEmailSendGridService;
        private static readonly Random randomizer = new Random();
        private readonly string subjectTemplate = "Has your order #{0} been delivered?";
        private readonly int batchSize = 100;
        private readonly IOrderService orderService;
        private readonly ISendGridMessageTemplateService sendGridMessageTemplateService;
        private readonly string templateName = "EbayOrderDelivered.CustomerNotification";

        public EbayOrderDeliveryNotificationTask(ICustomService customService,
            ILogger logger,
            IStoreService storeService,
            IQueuedEmailSendGridService queuedEmailSendGridService,
            IOrderService orderService,
            ISendGridMessageTemplateService sendGridMessageTemplateService)
        {
            this.customService = customService;
            this.logger = logger;
            this.queuedEmailSendGridService = queuedEmailSendGridService;
            this.orderService = orderService;
            this.sendGridMessageTemplateService = sendGridMessageTemplateService;
        }

        public void Execute()
        {
            Thread.Sleep(randomizer.Next(10000, 60000));
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
                this.logger.Error($"Error with EbayOrderDeliveryNotification queue locker checking. {ex.Message}", ex);
                return;
            }

            try
            {
                this.customService.SetLocked(LOCKER_NAME);
                var startDate = DateTime.UtcNow.AddDays(-30);
                var messageTemplate = this.sendGridMessageTemplateService.GetMessageTemplateByName(templateName, 0);
                var orders = this.orderService.GetSalesOrdersByTimeRange(startDate, channel: Channel.Ebay);
                foreach (var order in orders)
                {
                    var orderedCount = order.Lines.Sum(i => i.Quantity);
                    var deliveredCount = order.PurchaseOrders
                        .SelectMany(po => po.Shipments.Where(s => s.DeliveredOn.HasValue))
                        .SelectMany(s => s.Items)
                        .Sum(i => i.Quantity);

                    if (orderedCount != deliveredCount)
                    {
                        continue;
                    }

                    var subject = string.Format(this.subjectTemplate, order.Number);
                    var alreadySentEmails = this.queuedEmailSendGridService.SearchEmails(messageTemplate.Email, order.ShippingAddress.Email, null, null, false, 7, true, 0, batchSize);
                    if (alreadySentEmails.Any(m => m.Subject == subject))
                    {
                        continue;
                    }

                    this.customService.InsertEbayOrderDeliveryNotification(order);
                }
            }
            catch (Exception ex)
            {
                this.logger.Error($"EbayOrderDeliveryNotificationTask failed. {ex.Message}", ex);
            }
            finally
            {
                this.customService.SetUnlocked(LOCKER_NAME);
            }
        }
    }
}
