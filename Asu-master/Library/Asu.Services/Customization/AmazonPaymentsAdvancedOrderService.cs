//using AmazonPaymentsAdvanced;
using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Payments;
using Asu.Core.Domain.Shipping;
using Asu.Services.Catalog;
using Asu.Services.Logging;
using Asu.Services.Messages;
using Asu.Services.Orders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Asu.Services.Customization
{
    using Asu.Core.Domain.Discounts;
    using Asu.Services.Discounts;

    //public sealed class AmazonPaymentsAdvancedOrderService : IAmazonPaymentsAdvancedOrderService
    //{
    //    #region Ctor

    //    public AmazonPaymentsAdvancedOrderService(IRepository<AmazonPaymentAdvanced> amazonPaymentAdvancedRepository,
    //        IRepository<AmazonPaymentAdvancedTask> amazonPaymentAdvancedTaskRepository,
    //        IOrderService orderService,
    //        IWorkflowMessageService workflowMessageService,
    //        ICustomService customService,
    //        IProductService productService,
    //        ILogger logger,
    //        IStoreContext storeContext,
            
    //        IWorkContext workContext,
    //        IDiscountService discountService,
    //        AmazonPaymentSettings amazonPaymentSettings)
    //    {
    //        this.logger = logger;
    //        this.orderService = orderService;
    //        this.customService = customService;
    //        this.amazonPaymentAdvancedRepository = amazonPaymentAdvancedRepository;
    //        this.amazonPaymentAdvancedTaskRepository = amazonPaymentAdvancedTaskRepository;
    //        this.storeContext = storeContext;
    //        this.workContext = workContext;
    //        this.workflowMessageService = workflowMessageService;
    //        this.productService = productService;
    //        this.discountService = discountService;
    //        this.amazonPaymentSettings = amazonPaymentSettings;
    //    }

    //    #endregion

    //    #region Fields

    //    private readonly IRepository<AmazonPaymentAdvanced> amazonPaymentAdvancedRepository;
    //    private readonly IRepository<AmazonPaymentAdvancedTask> amazonPaymentAdvancedTaskRepository;

    //    private readonly IOrderService orderService;
    //    private readonly IWorkflowMessageService workflowMessageService;
    //    private readonly ICustomService customService;
    //    private readonly IProductService productService;

    //    private readonly ILogger logger;
    //    private readonly IStoreContext storeContext;
    //    private readonly IWorkContext workContext;
    //    private readonly IDiscountService discountService;
    //    private readonly AmazonPaymentSettings amazonPaymentSettings;

    //    private const string LOCKER_NAME = "AmazonPaymentLocker";

    //    #endregion

    //    #region Public Methods

    //    public List<AmazonOrderDetails> GetIncompleteOrdersFromDatabase(int storeId)
    //    {
    //        var amazonOrderRecords = amazonPaymentAdvancedRepository.Table
    //            .Where(apa => (apa.AmazonAuthorizationId != null
    //                && apa.Order.StoreId == storeId
    //                && apa.AuthorizeStatus.ToLower() != "DECLINED".ToLower() 
    //                && (apa.AuthorizeStatus.ToLower() == "PENDING".ToLower() || apa.CaptureStatus == null) 
    //                && apa.SecondOrder == null))
    //            .Distinct().Take(10).ToList();

    //        return amazonOrderRecords.Select(amazonOrder => new AmazonOrderDetails
    //        {
    //            OrderId = amazonOrder.Order.Id, 
    //            OrderReferenceId = amazonOrder.OrderReferenceId, 
    //            OrderAmount = amazonOrder.OrderAmount, 
    //            AmazonAuthorizationId = amazonOrder.AmazonAuthorizationId
    //        }).ToList();
    //    }

    //    public bool GetAuthorizeDetails(string orderReferenceId, string amazonAuthorizationId, out string status)
    //    {
    //        status = string.Empty;
    //        try
    //        {
    //            string errorMessage;
    //            var service = new AmazonPaymentsAdvancedService(
    //                this.amazonPaymentSettings.ApplicationName,
    //                this.amazonPaymentSettings.ApplicationVersion,
    //                this.amazonPaymentSettings.Region,
    //                this.amazonPaymentSettings.MerchantId,
    //                this.amazonPaymentSettings.AccessKey,
    //                this.amazonPaymentSettings.SecretAccessKey,
    //                this.amazonPaymentSettings.Environment,
    //                this.amazonPaymentSettings.ClientId,
    //                this.amazonPaymentSettings.WidgetUrl,
    //                this.amazonPaymentSettings.CertCn,
    //                this.amazonPaymentSettings.ServiceUrl,
    //                orderReferenceId,
    //                amazonAuthorizationId);
    //            var details = service.GetAuthorizeDetails(out errorMessage);
    //            if (details == null)
    //            {
    //                this.LogError(orderReferenceId, "GetAuthorizeDetails() - details == null : " + errorMessage);
    //                return false;
    //            }

    //            status = details.GetAuthorizationDetailsResult.AuthorizationDetails.AuthorizationStatus.State.ToString();
    //            UpdateAuthorizeStatus(orderReferenceId, status, errorMessage);
    //            if (!string.IsNullOrWhiteSpace(errorMessage))
    //            {
    //                this.LogError(orderReferenceId, "GetAuthorizeDetails(): " + errorMessage);
    //                return false;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("CBA_new_AmazonPaymentsAdvancedService GetAuthorizeDetails() - " + ex.Message, ex);
    //            return false;
    //        }

    //        return true;
    //    }

    //    public bool Capture(string orderReferenceId, string amazonAuthorizationId, decimal orderAmount, out string status)
    //    {
    //        string errorMessage;
    //        status = string.Empty;
    //        var service = new AmazonPaymentsAdvancedService(
    //            this.amazonPaymentSettings.ApplicationName,
    //            this.amazonPaymentSettings.ApplicationVersion,
    //            this.amazonPaymentSettings.Region,
    //            this.amazonPaymentSettings.MerchantId,
    //            this.amazonPaymentSettings.AccessKey,
    //            this.amazonPaymentSettings.SecretAccessKey,
    //            this.amazonPaymentSettings.Environment,
    //            this.amazonPaymentSettings.ClientId,
    //            this.amazonPaymentSettings.WidgetUrl,
    //            this.amazonPaymentSettings.CertCn,
    //            this.amazonPaymentSettings.ServiceUrl,
    //            orderReferenceId,
    //            amazonAuthorizationId);
    //        var captureResponse = service.CaptureAction(orderAmount.ToString(CultureInfo.InvariantCulture), out errorMessage);
    //        if (captureResponse != null)
    //        {
    //            if (!captureResponse.IsSetCaptureResult() || !captureResponse.CaptureResult.IsSetCaptureDetails()
    //                || !captureResponse.CaptureResult.CaptureDetails.IsSetCaptureStatus())
    //            {
    //                UpdateCaptureStatus(orderReferenceId, string.Empty, string.Empty, errorMessage);
    //                return false;
    //            }

    //            status = captureResponse.CaptureResult.CaptureDetails.CaptureStatus.State.ToString();
    //            var captureId = captureResponse.CaptureResult.CaptureDetails.AmazonCaptureId;
    //            UpdateCaptureStatus(orderReferenceId, status, captureId, errorMessage);

    //            var captureDetails = service.GetCaptureDetails(captureResponse, out errorMessage);
    //            if (captureDetails != null)
    //            {
    //                if (!captureDetails.IsSetGetCaptureDetailsResult() || !captureDetails.GetCaptureDetailsResult.IsSetCaptureDetails())
    //                {
    //                    UpdateCaptureStatus(orderReferenceId, string.Empty, string.Empty, errorMessage);
    //                    return false;
    //                }

    //                UpdateCaptureStatus(orderReferenceId, captureResponse.CaptureResult.CaptureDetails.CaptureStatus.State.ToString(), captureId, errorMessage);
    //            }
    //            else
    //            {
    //                this.LogError(orderReferenceId, "Capture() - GetCaptureDetails: " + errorMessage);
    //            }
    //        }
    //        else
    //        {
    //            this.LogError(orderReferenceId, "Capture() - CaptureAction: " + errorMessage);
    //        }

    //        return true;
    //    }

    //    public void GetCaptureDetails(OffAmazonPaymentsService.Model.CaptureResponse captureReponse, string orderReferenceId, string amazonAuthorizationId)
    //    {
    //        try
    //        {
    //            string errorMessage;
    //            var service = new AmazonPaymentsAdvancedService(
    //                this.amazonPaymentSettings.ApplicationName,
    //                this.amazonPaymentSettings.ApplicationVersion,
    //                this.amazonPaymentSettings.Region,
    //                this.amazonPaymentSettings.MerchantId,
    //                this.amazonPaymentSettings.AccessKey,
    //                this.amazonPaymentSettings.SecretAccessKey,
    //                this.amazonPaymentSettings.Environment,
    //                this.amazonPaymentSettings.ClientId,
    //                this.amazonPaymentSettings.WidgetUrl,
    //                this.amazonPaymentSettings.CertCn,
    //                this.amazonPaymentSettings.ServiceUrl,
    //                orderReferenceId,
    //                amazonAuthorizationId);
    //            var details = service.GetCaptureDetails(captureReponse, out errorMessage);
    //            UpdateAuthorizeStatus(orderReferenceId, details.GetCaptureDetailsResult.CaptureDetails.CaptureStatus.State.ToString(), errorMessage);
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("CBA_new_AmazonPaymentsAdvancedService GetCaptureDetails() - " + ex.Message, ex);
    //        }
    //    }

    //    public bool IsBusy()
    //    {
    //        try
    //        {
    //            return customService.IsLocked(LOCKER_NAME, 15 * 60);
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("CBA_new_AmazonPaymentsAdvancedService IsBusy() - " + ex.Message, ex);
    //            return true;
    //        }
    //    }

    //    public void SetBusyStatus(bool isBusy)
    //    {
    //        try
    //        {
    //            if (isBusy)
    //            {
    //                this.customService.SetLockedIfUnlocked(LOCKER_NAME, 900);
    //            }
    //            else
    //            {
    //                this.customService.SetUnlocked(LOCKER_NAME);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            this.logger.Error("CBA_new_AmazonPaymentsAdvancedService SetBusyStatus() - " + ex.Message, ex);
    //        }
    //    }

    //    public void UpdateOrderStatusMessage(AmazonOrderDetails orderDetails, string status)
    //    {
    //        try
    //        {
    //            var order = orderService.GetOrderById(orderDetails.OrderId);
    //            order.AuthorizationTransactionResult = status;
    //            orderService.UpdateOrder(order);
    //        }
    //        catch (Exception)
    //        {
    //        }
    //    }

    //    public void DeclineOrder(AmazonOrderDetails orderDetails)
    //    {
    //        try
    //        {
    //            var order = orderService.GetOrderById(orderDetails.OrderId);
    //            order.OrderStatus = OrderStatus.Cancelled;
    //            order.PaymentStatus = PaymentStatus.Voided;
    //            order.AuthorizationTransactionCode = orderDetails.AmazonAuthorizationId;
    //            order.OrderStatus = OrderStatus.Cancelled;
    //            order.PaidDateUtc = DateTime.UtcNow;
    //            order.Deleted = true;
    //            orderService.UpdateOrder(order);

    //            //MessageService.SendOrderCancelledCustomerNotification()
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("AmazonPaymentsAdvancedTask.SendDeclinedEmail()", ex);
    //        }
    //    }

    //    public bool IsOrderAlreadyCompleted(string orderReferenceId)
    //    {
    //        try
    //        {
    //            return amazonPaymentAdvancedRepository.Table.Any(apa => (apa.OrderReferenceId == orderReferenceId && apa.SecondOrder != null));
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("AmazonPaymentsAdvancedTask.CompleteAutoplicityOrder()", ex);
    //        }

    //        return false;
    //    }

    //    public Order CompleteAutoplicityOrder(int autoplicityOrderId, string amazonAuthorizationId)
    //    {
    //        try
    //        {
    //            using (var scope = new System.Transactions.TransactionScope())
    //            {
    //                var order = orderService.GetOrderById(autoplicityOrderId);
    //                order.AuthorizationTransactionCode = amazonAuthorizationId;
    //                order.AuthorizationTransactionResult = "COMPLETED";
    //                order.OrderStatus = OrderStatus.Cancelled;
    //                order.PaidDateUtc = DateTime.UtcNow;
    //                order.Deleted = true;
    //                orderService.UpdateOrder(order);

    //                var customerOrders = orderService.SearchOrders(storeId: storeContext.CurrentStore.Id,
    //                customerId: order.Customer.Id);
    //                if (customerOrders != null && customerOrders.Count > 0)
    //                {
    //                    if (customerOrders.Any(o => o.AuthorizationTransactionCode == order.AuthorizationTransactionCode && o.Deleted == false))
    //                    {
    //                        return null;
    //                    }
    //                }

    //                var newOrder = InsertNewOrder(order);

    //                #region OrderExtra

    //                if (newOrder != null)
    //                {
    //                    try
    //                    {
    //                        var orderExtra = this.customService.GetOrderExtra(order.Id);
    //                        if (orderExtra != null)
    //                        {
    //                            var newOrderExtra = new OrderExtra
    //                            {
    //                                OrderId = newOrder.Id,
    //                                SwapOrderNumber = orderExtra.SwapOrderNumber,
    //                                VehicleId = orderExtra.VehicleId,
    //                                BaseVehicleId = orderExtra.BaseVehicleId,
    //                                //KountScore = orderExtra.KountScore,
    //                                //KountResponse = orderExtra.KountResponse,
    //                            };

    //                            this.customService.InsertOrderExtra(newOrderExtra);
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                }

    //                #endregion

    //                scope.Complete();

    //                return newOrder;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("AmazonPaymentsAdvancedTask.CompleteAutoplicityOrder()", ex);
    //        }

    //        return null;
    //    }

    //    public Order InsertNewOrder(Order oldOrder)
    //    {
    //        var newOrder = new Order
    //        {
    //            OrderGuid = oldOrder.OrderGuid,
    //            CustomerId = oldOrder.CustomerId,
    //            CustomerLanguageId = oldOrder.CustomerLanguageId,
    //            CustomerTaxDisplayTypeId = oldOrder.CustomerTaxDisplayTypeId,
    //            OrderSubtotalInclTax = oldOrder.OrderSubtotalInclTax,
    //            OrderSubtotalExclTax = oldOrder.OrderSubtotalExclTax,
    //            OrderSubTotalDiscountInclTax = oldOrder.OrderSubTotalDiscountInclTax,
    //            OrderSubTotalDiscountExclTax = oldOrder.OrderSubTotalDiscountExclTax,
    //            OrderShippingInclTax = oldOrder.OrderShippingInclTax,
    //            OrderShippingExclTax = oldOrder.OrderShippingExclTax,
    //            PaymentMethodAdditionalFeeExclTax = oldOrder.PaymentMethodAdditionalFeeExclTax,
    //            TaxRates = oldOrder.TaxRates,
    //            OrderTax = oldOrder.OrderTax,
    //            OrderTotal = oldOrder.OrderTotal,
    //            RefundedAmount = oldOrder.RefundedAmount,
    //            OrderDiscount = oldOrder.OrderDiscount,
    //            CheckoutAttributeDescription = oldOrder.CheckoutAttributeDescription,
    //            CheckoutAttributesXml = oldOrder.CheckoutAttributesXml,
    //            CustomerCurrencyCode = oldOrder.CustomerCurrencyCode,
    //            AffiliateId = oldOrder.AffiliateId,
    //            OrderStatusId = (int)OrderStatus.Processing,
    //            AllowStoringCreditCardNumber = false,
    //            CardType = "Amazon",
    //            CardName = string.Empty,
    //            CardNumber = string.Empty,
    //            MaskedCreditCardNumber = string.Empty,
    //            CardCvv2 = string.Empty,
    //            CardExpirationMonth = string.Empty,
    //            CardExpirationYear = string.Empty,
    //            AuthorizationTransactionId = oldOrder.AuthorizationTransactionId,
    //            AuthorizationTransactionCode = oldOrder.AuthorizationTransactionCode,
    //            AuthorizationTransactionResult = "Success",
    //            CaptureTransactionId = oldOrder.CaptureTransactionId,
    //            CaptureTransactionResult = oldOrder.CaptureTransactionResult,
    //            SubscriptionTransactionId = oldOrder.SubscriptionTransactionId,
    //            PurchaseOrderNumber = oldOrder.PurchaseOrderNumber,
    //            PaymentStatusId = (int)PaymentStatus.Paid,
    //            PaidDateUtc = oldOrder.PaidDateUtc,
    //            ShippingStatusId = (int)ShippingStatus.NotYetShipped,
    //            ShippingMethod = oldOrder.ShippingMethod,
    //            VatNumber = oldOrder.VatNumber,
    //            Deleted = false,
    //            CreatedOnUtc = DateTime.UtcNow,
    //            BillingAddressId = oldOrder.BillingAddressId,
    //            ShippingAddressId = oldOrder.ShippingAddressId,
    //            CurrencyRate = oldOrder.CurrencyRate,
    //            CustomerIp = oldOrder.CustomerIp,
    //            CustomValuesXml = oldOrder.CustomValuesXml,
    //            PaymentMethodAdditionalFeeInclTax = oldOrder.PaymentMethodAdditionalFeeInclTax,
    //            PaymentMethodSystemName = oldOrder.PaymentMethodSystemName,
    //            PickUpInStore = oldOrder.PickUpInStore,
    //            StoreId = oldOrder.StoreId,
    //            ShippingRateComputationMethodSystemName = oldOrder.ShippingRateComputationMethodSystemName,
    //            RewardPointsWereAdded = oldOrder.RewardPointsWereAdded,
    //            RedeemedRewardPointsEntry = oldOrder.RedeemedRewardPointsEntry,
    //            BillingAddress = oldOrder.BillingAddress,
    //            ShippingAddress = oldOrder.ShippingAddress,
    //            Customer = oldOrder.Customer
    //        };

    //        #region Cancel test orders (which uses 99% off coupon)

    //        if (oldOrder.DiscountUsageHistory.Any(i => i.DiscountId == 4))
    //        {
    //            newOrder.OrderStatus = OrderStatus.Cancelled;
    //        }

    //        #endregion

    //        orderService.InsertOrder(newOrder);

    //        foreach (var oi in oldOrder.OrderItems)
    //        {
    //            //save order item
    //            var orderItem = new OrderItem
    //            {
    //                OrderItemGuid = Guid.NewGuid(),
    //                Order = newOrder,
    //                ProductId = oi.ProductId,
    //                UnitPriceInclTax = oi.UnitPriceInclTax,
    //                UnitPriceExclTax = oi.UnitPriceExclTax,
    //                PriceInclTax = oi.PriceInclTax,
    //                PriceExclTax = oi.PriceExclTax,
    //                OriginalProductCost = oi.OriginalProductCost,
    //                AttributeDescription = oi.AttributeDescription,
    //                AttributesXml = oi.AttributesXml,
    //                Quantity = oi.Quantity,
    //                DiscountAmountInclTax = oi.DiscountAmountInclTax,
    //                DiscountAmountExclTax = oi.DiscountAmountExclTax,
    //                DownloadCount = oi.DownloadCount,
    //                IsDownloadActivated = oi.IsDownloadActivated,
    //                LicenseDownloadId = oi.LicenseDownloadId,
    //                ItemWeight = oi.ItemWeight,
    //            };

    //            newOrder.OrderItems.Add(orderItem);
    //            orderService.UpdateOrder(newOrder);

    //            //productService.AdjustInventory(opv.ProductVariantId, true, pv.Quantity, string.Empty);
    //        }



    //        foreach (var orderItem in newOrder.OrderItems)
    //        {
    //            orderItem.Product = productService.GetProductById(orderItem.ProductId);
    //        }

    //        this.customService.SaveProductCashRebates(newOrder);

    //        // save attached discounts 
    //        try
    //        {
    //            if (oldOrder.DiscountUsageHistory.Any())
    //            {
    //                foreach (var history in oldOrder.DiscountUsageHistory)
    //                {
    //                    var discountUsageHistory = new DiscountUsageHistory
    //                    {
    //                        Discount = history.Discount,
    //                        Order = newOrder,
    //                        CreatedOnUtc = DateTime.UtcNow
    //                    };

    //                    this.discountService.InsertDiscountUsageHistory(discountUsageHistory);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            this.logger.Error($"CBA_new_AmazonPaymentsAdvancedService InserNewOrder(): Saving attached discounts (orderId={newOrder.Id}) - {ex.Message}", ex);
    //        }
            

    //        try
    //        {
    //            workflowMessageService.SendOrderPlacedCustomerNotification(newOrder, newOrder.CustomerLanguageId);
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error(string.Format("CBA_new_AmazonPaymentsAdvancedService InserNewOrder() (orderId={0}) - {1}", newOrder.Id, ex.Message), ex);
    //        }

    //        return newOrder;
    //    }

    //    public void AddNewOrderId(Order newOrder, string orderReferenceId)
    //    {
    //        try
    //        {
    //            using (var scope = new System.Transactions.TransactionScope())
    //            {
    //                var ordersToUpdate = amazonPaymentAdvancedRepository.Table.Where(apa => apa.OrderReferenceId == orderReferenceId).ToList();
    //                foreach (var amazonOrder in ordersToUpdate)
    //                {
    //                    amazonOrder.SecondOrder = newOrder;
    //                    amazonOrder.UpdatedOn = DateTime.UtcNow;
    //                    amazonPaymentAdvancedRepository.Update(amazonOrder);
    //                }

    //                scope.Complete();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error(string.Format("CBA_new_AmazonPaymentsAdvancedService AddNewOrderId() (orderId={0}) - ", newOrder.Id, ex.Message), ex);
    //        }
    //    }

    //    #endregion

    //    #region Private fields

    //    private void UpdateAuthorizeStatus(string orderReferenceId, string status, string error)
    //    {
    //        try
    //        {
    //            var ordersToUpdate = amazonPaymentAdvancedRepository.Table.Where(apa => apa.OrderReferenceId == orderReferenceId).ToList();
    //            foreach (var amazonOrder in ordersToUpdate)
    //            {
    //                amazonOrder.AuthorizeStatus = status;
    //                amazonOrder.LastError = error;
    //                amazonOrder.UpdatedOn = DateTime.UtcNow;
    //                amazonPaymentAdvancedRepository.Update(amazonOrder);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("CBA_new_AmazonPaymentsAdvancedService UpdateAuthorizeStatus() - " + ex.Message, ex);
    //        }
    //    }

    //    private void UpdateCaptureStatus(string orderReferenceId, string status, string captureId, string error)
    //    {

    //        try
    //        {
    //            var ordersToUpdate = amazonPaymentAdvancedRepository.Table.Where(apa => apa.OrderReferenceId == orderReferenceId).ToList();
    //            foreach (var amazonOrder in ordersToUpdate)
    //            {
    //                if(!string.IsNullOrEmpty(status))
    //                    amazonOrder.CaptureStatus = status;
    //                amazonOrder.AmazonCaptureId = captureId;
    //                amazonOrder.LastError = error;
    //                amazonOrder.UpdatedOn = DateTime.UtcNow;
    //                amazonPaymentAdvancedRepository.Update(amazonOrder);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("CBA_new_AmazonPaymentsAdvancedService CaptureStatus() - " + ex.Message, ex);
    //        }
    //    }

    //    private void LogError(string orderReferenceId, string error)
    //    {
    //        try
    //        {
    //            var ordersToUpdate = amazonPaymentAdvancedRepository.Table.Where(apa => apa.OrderReferenceId == orderReferenceId).ToList();
    //            foreach (var amazonOrder in ordersToUpdate)
    //            {
    //                amazonOrder.LastError = error;
    //                amazonOrder.UpdatedOn = DateTime.UtcNow;
    //                amazonPaymentAdvancedRepository.Update(amazonOrder);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error("CBA_new_AmazonPaymentsAdvancedService LogError() - " + ex.Message, ex);
    //        }
    //    }

    //    #endregion
    //}
}
