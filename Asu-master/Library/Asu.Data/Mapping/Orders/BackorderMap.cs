using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    public class BackorderMap : NopEntityTypeConfiguration<Backorder>
    {
        public BackorderMap()
        {
            this.ToTable("vw_Backorders");
            this.Ignore(m => m.Id);
            this.HasKey(m => m.PurchaseOrderId);
            this.Property(m => m.Esd);
        }
    }
}
