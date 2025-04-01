using Asu.Services.Logging;
using Asu.Services.Orders;
using Asu.Services.Tasks;
using System;

namespace Asu.Services.Customization
{
    using System.Linq;
    using System.Threading;

    using Asu.Core;
    using Asu.Core.Data;
    using Asu.Core.Domain.Orders;

    public class AmazonPaymentsAdvancedTask : ITask
    {
        private readonly IAmazonPaymentsAdvancedOrderService amazonPaymentsAdvancedOrderService;
        private readonly IOrderService orderService;
        private readonly ILogger logger;
        private static readonly object locker = new object();
        private readonly IStoreContext storeContext;
        private static readonly Random randomizer = new Random();

        public AmazonPaymentsAdvancedTask (IAmazonPaymentsAdvancedOrderService amazonPaymentsAdvancedOrderService,
            ILogger logger,
			IStoreContext storeContext,
            IOrderService orderService)
        {
            this.orderService = orderService;
            this.logger = logger;
            this.amazonPaymentsAdvancedOrderService = amazonPaymentsAdvancedOrderService;
            this.storeContext = storeContext;
        }

        public void Execute()
        {
            Thread.Sleep(randomizer.Next(3000, 10000));
            lock (locker)
            {
                try
                {
                    if (this.amazonPaymentsAdvancedOrderService.IsBusy())
                    {
                        return;
                    }

                    this.amazonPaymentsAdvancedOrderService.SetBusyStatus(true);
                }
                catch (Exception)
                {
                }

                try
                {
                    
                    var orderDetailsArray = amazonPaymentsAdvancedOrderService.GetIncompleteOrdersFromDatabase(this.storeContext.CurrentStore.Id);
                    foreach (var orderDetails in orderDetailsArray)
                    {
                        string status;
                        if (amazonPaymentsAdvancedOrderService.GetAuthorizeDetails(orderDetails.OrderReferenceId, orderDetails.AmazonAuthorizationId, out status))
                        {
                            if (status.ToUpper() == "DECLINED")
                            {
                                amazonPaymentsAdvancedOrderService.DeclineOrder(orderDetails);
                                amazonPaymentsAdvancedOrderService.UpdateOrderStatusMessage(orderDetails, "DECLINED");
                                return;
                            }

                            if (amazonPaymentsAdvancedOrderService.Capture(orderDetails.OrderReferenceId, orderDetails.AmazonAuthorizationId, orderDetails.OrderAmount, out status))
                            {
                                if (status.ToUpper() == "COMPLETED")
                                {
                                    if (!amazonPaymentsAdvancedOrderService.IsOrderAlreadyCompleted(orderDetails.AmazonAuthorizationId))
                                    {
                                        // try to find already created amazon order to prevent duplicate orders creation
                                        var orders = this.orderService.GetOrdersByAuthorizationTransactionIdAndPaymentMethod(orderDetails.AmazonAuthorizationId, "Payments.Amazon");
                                        if (orders.Length > 1 && orders.Any(i => i.Deleted) && orders.Any(i => !i.Deleted))
                                        {
                                            var exstingOrder = orders.First(i => !i.Deleted);
                                            
                                            this.amazonPaymentsAdvancedOrderService.AddNewOrderId(exstingOrder, orderDetails.OrderReferenceId);
                                            this.logger.Warning(string.Format("AmazonPaymentsAdvancedTask.Execute() - attempt to add duplicate has been prevented. Existing order id is {0}", exstingOrder.Id));
                                            continue;
                                        }

                                        var newOrder = amazonPaymentsAdvancedOrderService.CompleteAutoplicityOrder(orderDetails.OrderId, orderDetails.AmazonAuthorizationId);
                                        if (newOrder == null)
                                        {
                                            logger.Warning("AmazonPaymentsAdvancedTask.CompleteAutoplicityOrder() - New Order does not placed!");
                                        }
                                        else
                                        {
                                            amazonPaymentsAdvancedOrderService.AddNewOrderId(newOrder, orderDetails.OrderReferenceId);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                logger.Warning("AmazonPaymentsAdvancedTask.Execute() - Status of Capture action is not completed: " + status);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("AmazonPaymentsAdvancedTask.Execute()", ex);
                }

                this.amazonPaymentsAdvancedOrderService.SetBusyStatus(false);
            }

            //IsInExecuting = false;
        }
    }
}
