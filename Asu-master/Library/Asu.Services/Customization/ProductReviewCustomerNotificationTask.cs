using System;
using Asu.Core.Domain.Customization;
using Asu.Services.Logging;
using Asu.Services.Tasks;

namespace Asu.Services.Customization
{
    using System.Threading;

    public class ProductReviewCustomerNotificationTask : ITask
    {
        private readonly ICustomService customService;
        private readonly ILogger logger;
        private static readonly object Locker = new object();
        private const string LockerName = "ProductReviewCustomerNotificationTask";
        private static readonly Random Randomizer = new Random();

        public ProductReviewCustomerNotificationTask(ICustomService customService, ILogger logger)
        {
            this.customService = customService;
            this.logger = logger;
        }

        public void Execute()
        {
            Thread.Sleep(Randomizer.Next(3000, 10000));
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
                    this.logger.Error(string.Format("Error when ProductReviewCustomerNotificationTask locker checking. {0}", exc.Message), exc);
                    return;
                }

                this.customService.SetLocked(LockerName);

                var orderProductsToReviewList = this.customService.GetOrderProductsToReview(10);
                foreach (var orderProductToReview in orderProductsToReviewList)
                {
                    this.customService.NotifyProductReviewCustomer(orderProductToReview);
                    this.customService.InsertProductReviewCustomerNotification(new ProductReviewCustomerNotification { OrderId = orderProductToReview.OrderId, CreatedOnUtc = DateTime.UtcNow });
                }

                this.customService.SetUnlocked(LockerName);
            }
        }
    }
}