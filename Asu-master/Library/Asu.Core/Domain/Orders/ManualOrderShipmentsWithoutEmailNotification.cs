namespace Asu.Core.Domain.Orders
{
    public partial class ManualOrderShipmentsWithoutEmailNotification : BaseEntity
    {
        public int SalesOrderId { get; set; }
        public string OrderNumber { get; set; }
        public int ShipmentId { get; set; }
        public int ChannelId { get; set; }
    }

}