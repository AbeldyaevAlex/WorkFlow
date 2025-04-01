namespace Asu.Services.Warranty
{
    using System;
    using System.Linq;
    using System.Threading;
    using Asu.Core;
    using Asu.Core.Domain.Customization;
    using Asu.Core.Domain.Orders;
    using Asu.Services.Customization;
    using Asu.Services.Logging;
    using Asu.Services.Orders;
    using Asu.Services.Tasks;

    public class UpdateWarrantyAssociationTask : ITask
    {
        private static readonly object locker = new object();
        private const string LockerName = "UpdateWarrantyAssociation";
        private static readonly Random Randomizer = new Random();
        private readonly ILogger logger;
        private readonly IWarrantyService warrantyService;
        private readonly ICustomService customService;
        private readonly IOrderService orderService;
        private readonly IReturnService returnService;
        private readonly IOrderProcessingService orderProcessingService;

        public UpdateWarrantyAssociationTask(ICustomService customService, IOrderService orderService, IReturnService returnService, IWarrantyService warrantyService, ILogger logger, IOrderProcessingService orderProcessingService)
        {
            this.warrantyService = warrantyService;
            this.logger = logger;
            this.customService = customService;
            this.orderService = orderService;
            this.returnService = returnService;
            this.orderProcessingService = orderProcessingService;
        }

        public void Execute()
        {
            Thread.Sleep(Randomizer.Next(3000, 10000));
            lock (locker)
            {
                try
                {
                    if (!this.customService.SetLockedIfUnlocked(LockerName, 60 * 60))
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.logger.Error($"Error with WCS_Locker busy checking. {ex.Message}", ex);
                }

                try
                {
                    this.ProcessInsurance();
                }
                catch (Exception ex)
                {
                    this.logger.Error($"Error while updating insurance. {ex.Message}", ex);
                }

                this.customService.SetUnlocked(LockerName);
            }
        }

        private void ProcessInsurance()
        {
            var insuranceMappings = this.warrantyService
                .GetAllAssociations()
                .Where(m => !m.UpdatedOn.HasValue && ConstantStorage.SHIPPING_INSURANCE_PRODUCT_IDS.Contains(m.WarrantyProductId) && !ConstantStorage.RETURN_EXTENSION_PRODUCT_IDS.Contains(m.WarrantyProductId))
                .ToList();

            foreach (var im in insuranceMappings)
            {
                var nopOrder = this.orderService.GetOrderById(im.OrderId);
                if (nopOrder != null && nopOrder.Deleted)
                {
                    continue;
                }

                var channelId = (int)this.orderService.GetChannel((NopStore)nopOrder.StoreId);
                var orderId = this.returnService.GetCrmOrderIdByOrderReference(im.OrderId.ToString(), channelId);
                if (!orderId.HasValue)
                {
                    this.logger.Error($"UpdateWarrantyAssociationTask(). Order id {im.OrderId} has been not found.");
                    continue;
                }

                var order = this.orderService.GetCrmOrder(orderId.Value);
                if (order == null)
                {
                    this.logger.Error($"UpdateWarrantyAssociationTask(). Cannot get CRM order {orderId.Value}.");
                    continue;
                }

                var orderLineId = order.Lines?.SingleOrDefault(m => m.ProductId == im.ProductId)?.Id;
                if (!orderLineId.HasValue)
                {
                    this.logger.Error($"UpdateWarrantyAssociationTask(). Cannot get order item id for product {im.ProductId}.");
                    continue;
                }

                var warrantyOrderLineId = order.Lines.SingleOrDefault(m => m.ProductId == im.WarrantyProductId)?.Id;
                if (!warrantyOrderLineId.HasValue)
                {
                    this.logger.Error($"UpdateWarrantyAssociationTask(). Cannot get order item id for warranty {im.WarrantyProductId}.");
                    continue;
                }

                im.SalesOrderLineId = orderLineId.Value;
                im.SalesOrderWarrantyLineId = warrantyOrderLineId.Value;
                im.UpdatedOn = DateTime.UtcNow;
                this.warrantyService.Update(im);
            }
        }

        //private void ProcessWarranties()
        //{
        //    var associations = this.warrantyService.GetAllAssociations().Where(m => !m.UpdatedOn.HasValue);
        //    foreach (var association in associations)
        //    {
        //        var orderId = this.returnService.GetCrmOrderIdByOrderReference(association.OrderId.ToString(), (int)Channel.Autoplicity);
        //        var nopOrderId = association.OrderId;
        //        var nopOrder = this.orderService.GetOrderById(nopOrderId);
        //        if (nopOrder != null && nopOrder.Deleted)
        //        {
        //            continue;
        //        }

        //        var orderId = this.returnService.GetCrmOrderIdByOrderReference(nopOrderId.ToString(), (int)Channel.Autoplicity);
        //        if (!orderId.HasValue)
        //        {
        //            this.logger.Error($"UpdateWarrantyAssociationTask(). Order id {association.OrderId} has been not found.");
        //            continue;
        //        }

        //        var order = this.orderService.GetCrmOrder(orderId.Value);
        //        if (order == null)
        //        {
        //            this.logger.Error($"UpdateWarrantyAssociationTask(). Cannot get order {association.OrderId}.");
        //            continue;
        //        }

        //        var orderItemId = order.ThubOrder?.OrderItems?.SingleOrDefault(m => m.ProductId == association.ProductId)?.OrderItemId;
        //        if (!orderItemId.HasValue)
        //        {
        //            this.logger.Error($"UpdateWarrantyAssociationTask(). Cannot get order item id for product {association.ProductId}.");
        //            continue;
        //        }

        //        association.OrderItemId = orderItemId.Value;
        //        orderItemId = order.ThubOrder.OrderItems.SingleOrDefault(m => m.ProductId == association.WarrantyProductId)?.OrderItemId;
        //        if (!orderItemId.HasValue)
        //        {
        //            this.logger.Error($"UpdateWarrantyAssociationTask(). Cannot get order item id for warranty {association.WarrantyProductId}.");
        //            continue;
        //        }

        //        association.WarrantyOrderItemId = orderItemId.Value;
        //        association.UpdatedOn = DateTime.UtcNow;
        //        this.warrantyService.Update(association);
        //    }
        //}
    }
}