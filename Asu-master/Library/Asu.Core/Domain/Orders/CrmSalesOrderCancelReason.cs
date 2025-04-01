namespace Asu.Core.Domain.Orders
{
    public partial class CrmSalesOrderCancelReason : BaseEntity
    {
        public int SalesOrderId { get; set; }
        public int? NopOrderId { get; set; }
        public string CancelReasonName { get; set; }
        public string CancelReasonNameForEmailNotification { get; set; }
    }
}
