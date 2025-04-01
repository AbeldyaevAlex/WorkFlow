using System;
using Asu.Core.Domain.Customization;

namespace Asu.Services.Customization
{
    using System.Linq;
    using System.Threading;

    using Logging;

    using Asu.Services.Messages;
    using Asu.Services.Stores;

    using Tasks;

    public class OrderEtaNotificationTask : ITask
    {
        private readonly ICustomService customService;
        private readonly ILogger logger;
        private static readonly object Locker = new object();
        private const string LockerName = "OrderEtaNotificationTask";
        private static readonly Random Randomizer = new Random();
        private readonly IQueuedEmailService queuedEmailService;
        private readonly string subjectTemplate = "Update on order {0}";
        private readonly int batchSize = 50;

        public OrderEtaNotificationTask(ICustomService customService, ILogger logger, IQueuedEmailService queuedEmailService)
        {
            this.customService = customService;
            this.logger = logger;
            this.queuedEmailService = queuedEmailService;
        }

        public void Execute()
        {
            Thread.Sleep(Randomizer.Next(3000, 10000));
            lock (Locker)
            {
                try
                {
                    if (this.customService.IsLocked(LockerName, 300))
                    {
                        return;
                    }
                }
                catch (Exception exc)
                {
                    this.logger.Error(string.Format("Error when OrderEtaNotificationTask locker checking. {0}", exc.Message), exc);
                    return;
                }

                this.customService.SetLocked(LockerName);

                var orderShipmentEtaList = this.customService.GetOrderShipmentEta();
                foreach (var order in orderShipmentEtaList)
                {
                    var toEmail = order.Email;
                    var alreadySentEmails = this.queuedEmailService.SearchEmails(null, toEmail, null, null, false, 7, true, 0, this.batchSize);
                    if (alreadySentEmails.Any(m => m.Subject == string.Format(this.subjectTemplate, order.Id)))
                    {
                        continue;
                    }

                    this.customService.NotifyOrderShipmentEtaCustomer(order);
                    this.customService.InsertOrderShipmentEtaNotification(new OrderEtaNotification { OrderId = order.OrderId, CreatedOnUtc = DateTime.UtcNow });
                }

                this.customService.SetUnlocked(LockerName);
            }
        }
    }
}
