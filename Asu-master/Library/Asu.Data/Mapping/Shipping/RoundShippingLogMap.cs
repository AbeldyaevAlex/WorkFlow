namespace Asu.Data.Mapping.Shipping
{
    using Asu.Core.Domain.Shipping;

    public class RoundShippingLogMap : NopEntityTypeConfiguration<RoundShippingLog>
    {
        public RoundShippingLogMap()
        {
            this.ToTable("WCS_RoundShippingLogs");
            this.HasKey(m => m.Id);
        }
    }
}
