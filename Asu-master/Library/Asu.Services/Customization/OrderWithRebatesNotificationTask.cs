using System;
using Asu.Core.Domain.Customization;
using Asu.Services.Logging;
using Asu.Services.Tasks;

namespace Asu.Services.Customization
{
    using System.Threading;

    public class OrderWithRebatesNotificationTask : ITask
    {
        private readonly ICustomService customService;
        private readonly ILogger logger;
        private static readonly object Locker = new object();
        private const string LockerName = "OrderWithRebatesNotificationTask";
        private static readonly Random randomizer = new Random();

        public OrderWithRebatesNotificationTask(ICustomService customService, ILogger logger)
        {
            this.customService = customService;
            this.logger = logger;
        }

        public void Execute()
        {
            Thread.Sleep(randomizer.Next(3000, 10000));
            lock (Locker)
            {
                try
                {
                    if (this.customService.IsLocked(LockerName, 1200))
                    {
                        return;
                    }
                }
                catch (Exception exc)
                {
                    this.logger.Error(string.Format("Error when OrderWithRebatesNotificationTask locker checking. {0}", exc.Message), exc);
                    return;
                }

                this.customService.SetLocked(LockerName);

                var ordersWithRebatesList = this.customService.GetOrdersWithRebates();
                foreach (var order in ordersWithRebatesList)
                {
                    this.customService.NotifyOrderWithRebatesCustomer(order);
                    this.customService.InsertOrderWithRebatesNotification(new OrderWithRebatesNotification{OrderId = order.OrderId, CreatedOnUtc = DateTime.UtcNow});
                }

                this.customService.SetUnlocked(LockerName);
            }
        }
    }
}
