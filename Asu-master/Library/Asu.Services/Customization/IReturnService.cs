using System;

namespace Asu.Services.Customization
{
    using Asu.Core.Domain.Returns;
    using System.Collections.Generic;

    public interface IReturnService
    {
        CrmSalesOrder SearchCrmOrder(string orderNumber, string zip);

        CrmSalesOrder SearchCrmOrder(int crmOrderId);

        CrmSalesOrder GetCrmOrderByRma(int rmaId);

        ThubOrder GetByOrderReference(string orderReference, int channelId);

        int? GetCrmOrderIdByOrderReference(string orderReference, int channelId);

        List<ReturnRequest> GetReturnRequests(int crmOrderId);

        IList<Return> GetReturns(int crmOrderId);

        ReturnRequest GetReturnRequest(int requestId);

        List<ReturnReason> GetReturnReasons();

        void CreateCheckOrderCookie(CrmSalesOrder order);

        bool IsCustomerAuthorized(int crmOrderId, Guid? crmUserId = null, string hash = null);

        int CreateReturnRequest(ReturnRequest request);

        void RemoveReturnRequests(int crmOrderId);

        void CreateFreshdeskTicket(ReturnRequest request);

        Rma GetRma(int id);

        ThubOrderItem GetThubOrderItem(long orderItemId);

        IList<RmaShipment> GetRmaShipments(int rmaId);

        IList<CrmShipment> GetCrmRmaShipments(int rmaId);

        void SendMailToSupportInbox(string orderNumber, string fullname, string message, string marketplace, string email, string phone, long channelId);

        List<ReturnRequest> GetFreshdeskTicketsReturnRequests();
    }
}
