using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class OrderShipmentEtaMap : NopEntityTypeConfiguration<OrderShipmentEta>
    {
        public OrderShipmentEtaMap()
        {
            this.ToTable("vw_OrderEtaForCustomerNotification_Test");
            this.HasKey(or => or.OrderId);

            this.Ignore(or => or.Id);

            this.Property(or => or.OrderId).IsRequired();
            this.Property(or => or.CustomerFullName).IsRequired();
            this.Property(or => or.Email).IsRequired();
            this.Property(or => or.ShipmentEta).IsRequired();
        }
    }
}