namespace Asu.Services.Orders
{
    using Asu.Core;
    using Asu.Core.Domain.Returns;
    using Asu.Core.Domain.Shipping;
    using Asu.Services.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ManualOrderService :IManualOrderService
    {
        private readonly IWorkflowMessageService workflowMessageService;
        private readonly IWorkContext workCotext;

        public ManualOrderService(IWorkflowMessageService _workflowMessageService, IWorkContext workCotext)
        {
            this.workflowMessageService = _workflowMessageService;
            this.workCotext = workCotext;
        }

        public virtual void SendManualOrderShipment(CrmSalesOrder crmOrder, CrmShipment crmShipment)
        {
            int queuedEmailId = this.workflowMessageService.SendManualOrderShipment(crmOrder, crmShipment, this.workCotext.WorkingLanguage.Id);
        }

        public virtual void SendManualOrderDelivered(CrmSalesOrder crmOrder, CrmShipment crmShipment)
        {
            int queuedEmailId = this.workflowMessageService.SendManualOrderDelivered(crmOrder, crmShipment, this.workCotext.WorkingLanguage.Id);
        }

        public virtual void SendManualOrderCancelledCustomerNotification(CrmSalesOrder order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("OrderCancelled");
            }

            this.workflowMessageService.SendManualOrderCancelledCustomerNotification(order, this.workCotext.WorkingLanguage.Id);
        }
    }
}
