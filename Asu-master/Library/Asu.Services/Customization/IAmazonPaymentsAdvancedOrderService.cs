using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Orders;
using OffAmazonPaymentsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Customization
{
    public interface IAmazonPaymentsAdvancedOrderService
    {
        List<AmazonOrderDetails> GetIncompleteOrdersFromDatabase(int storeId);
        bool GetAuthorizeDetails(string orderReferenceId, string amazonAuthorizationId, out string status);
        bool Capture(string orderReferenceId, string amazonAuthorizationId, decimal orderAmount, out string status);
        void GetCaptureDetails(CaptureResponse captureReponse, string orderReferenceId, string amazonAuthorizationId);
        bool IsBusy();
        void SetBusyStatus(bool isBusy);
        void UpdateOrderStatusMessage(AmazonOrderDetails orderDetails, string status);
        void DeclineOrder(AmazonOrderDetails orderDetails);
        bool IsOrderAlreadyCompleted(string orderReferenceId);
        Order CompleteAutoplicityOrder(int autoplicityOrderId, string amazonAuthorizationId);
        Order InsertNewOrder(Order oldOrder);
        void AddNewOrderId(Order newOrder, string orderReferenceId);
    }
}
