using System;
using System.Linq;
using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Messages;
using Asu.Core.Domain.Orders;
using Asu.Services.Logging;
using Asu.Services.Messages;
using Asu.Services.Tasks;

namespace Asu.Services.Customization
{
    using Asu.Core.Domain.Catalog;
    using Asu.Core.Domain.Customization;
    using Asu.Services.Catalog;
    using System.Threading;
    public class BackInStockNotificationTask : ITask
    {
        private readonly IRepository<BackInStockSubscription> backInStockSubscriptionRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IStoreContext storeContext; 
        private readonly IBackInStockSubscriptionService backInStockSubscriptionService;
        private readonly ICustomService customService;
        private readonly ILogger logger;
        private const string LOCKER_NAME = "BackInStockEmailLocker";
        private static readonly Random Randomizer = new Random();

        public BackInStockNotificationTask(IRepository<BackInStockSubscription> backInStockSubscriptionRepository,
            IRepository<Product> productRepository,
            IStoreContext storeContext,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            ICustomService customService,
            ILogger logger)
        {
            this.backInStockSubscriptionRepository = backInStockSubscriptionRepository;
            this.productRepository = productRepository;
            this.storeContext = storeContext;
            this.backInStockSubscriptionService = backInStockSubscriptionService;
            this.customService = customService;
            this.logger = logger;
        }

        public void Execute()
        {
            if (Environment.MachineName.ToLower() != "web01" && this.storeContext.CurrentStore.Id == (int)NopStore.Autoplicity)
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
                this.logger.Error($"Error with BackInStockNotification queue locker checking. {exc.Message}", exc);
                return;
            }

            this.customService.SetLocked(LOCKER_NAME);

            var store = this.storeContext.CurrentStore;
            
            try
            {
                // select cancelled orders without sent email notification
                var query = from a in this.backInStockSubscriptionRepository.Table
                            join b in this.productRepository.Table on a.ProductId equals b.Id
                            where (b.StockQuantity > 0 || (b.ProductExtra != null && b.ProductExtra.IsShippingFromManufacturer)) && b.Published == true && b.Deleted == false
                            select a;

                var productsSubscription = query.OrderByDescending(i => i.Id).Take(100).ToList();
                foreach (var productSubscription in productsSubscription)
                {
                    int queuedEmailId = this.backInStockSubscriptionService.SendBackInStockNotification(productSubscription);
                    if (queuedEmailId > 0)
                    {
                        this.backInStockSubscriptionService.DeleteSubscription(productSubscription);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("BackInStockEmailTask. {0}", ex.Message), ex);
            }

            this.customService.SetUnlocked(LOCKER_NAME);
        }
    }
}
