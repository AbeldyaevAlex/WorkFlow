using Asu.Core.Domain.Returns;
using Asu.Core.Domain.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Orders
{
    public partial interface IManualOrderService
    {
        void SendManualOrderShipment(CrmSalesOrder crmOrder, CrmShipment crmShipment);
        void SendManualOrderDelivered(CrmSalesOrder crmOrder, CrmShipment crmShipment);
        void SendManualOrderCancelledCustomerNotification(CrmSalesOrder order);
    }
}
