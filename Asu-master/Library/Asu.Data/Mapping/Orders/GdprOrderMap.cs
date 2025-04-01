namespace Asu.Data.Mapping.Orders
{
    using Asu.Core.Domain.Orders;

    public class GdprOrderMap : NopEntityTypeConfiguration<GdprOrder>
    {
        public GdprOrderMap()
        {
            this.ToTable("WCS_GDPR_Orders");
            this.Ignore(m => m.Id);
            this.HasKey(m => m.OrderId);
        }
    }
}
