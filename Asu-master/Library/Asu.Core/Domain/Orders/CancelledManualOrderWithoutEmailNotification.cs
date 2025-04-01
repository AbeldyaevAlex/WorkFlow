namespace Asu.Core.Domain.Orders
{
    public partial class CancelledManualOrderWithoutEmailNotification : BaseEntity
    {
        public int SalesOrderId { get; set; }
        public string SalesOrderNumber { get; set; }
        public int ChannelId { get; set; }
    }
}