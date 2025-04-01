using Asu.Core;
using Asu.Core.Domain.Customization;
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
    public class OrderReviewNotificationTask : ITask
    {
        private const string LOCKER_NAME = "OrderReviewEmailLocker";
        //private readonly string subjectTemplate = "Your order #{0} from {1} has been shipped.";
        //private readonly string subjectTemplateManualOrder = "Your order {0} from {1} has been shipped.";
        private readonly int batchSize = 100;
        private static readonly object Locker = new object();
        private static readonly Random randomizer = new Random();

        private readonly ICustomService customService;
        private readonly ILogger logger;
        private readonly IStoreService storeService;
        private readonly IQueuedEmailSendGridService queuedSendGridEmailService;
        private readonly IOrderService orderService;
        private readonly IStoreContext storeContext;

        public OrderReviewNotificationTask(ICustomService customService,
            ILogger logger,
            IStoreService storeService,
            IQueuedEmailSendGridService queuedSendGridEmailService,
            IOrderService orderService,
            IStoreContext storeContext)
        {
            this.customService = customService;
            this.logger = logger;
            this.storeService = storeService;
            this.queuedSendGridEmailService = queuedSendGridEmailService;
            this.orderService = orderService;
            this.storeContext = storeContext;
        }

        public void Execute()
        {
            if (Environment.MachineName.ToLower() != "web01" && this.storeContext.CurrentStore.Id != (int)NopStore.Autoplicity)
            {
                return;
            }

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
                this.logger.Error($"Error with OrderReview queue locker checking. {ex.Message}", ex);
                return;
            }

            try
            {
                this.customService.SetLocked(LOCKER_NAME);

                var startDate = DateTime.UtcNow.Date.AddDays(-30);
                var endDate = startDate.AddHours(23).AddMinutes(59).AddSeconds(59);

                var orders = this.orderService.GetOrdersByTimeRange(startDate, endDate)
                    .Where(o => o.OrderStatus != Core.Domain.Orders.OrderStatus.Cancelled)
                    .ToList();
                
                foreach (var order in orders)
                {
                    var alreadySentEmails = this.queuedSendGridEmailService.SearchEmails(null, order.ShippingAddress.Email, null, null, false, 7, true, 0, batchSize);
                    var subject = "Tell us what you think";
                    if (alreadySentEmails.Any(m => m.Subject == subject) 
                        && alreadySentEmails.Any(m => m.Data.IndexOf($"\"OrderNumber\":{order.Id}", StringComparison.Ordinal) > -1))
                    {
                        continue;
                    }

                    this.customService.InsertOrderReviewNotification(order);
                }
            }
            catch (Exception ex)
            {
                this.logger.Error($"OrderReviewNotificationTask failed. {ex.Message}", ex);
            }
            finally
            {
                this.customService.SetUnlocked(LOCKER_NAME);
                this.logger.Information($"OrderReviewNotificationTask finished on {Environment.MachineName}");
            }
        }
    }
}
