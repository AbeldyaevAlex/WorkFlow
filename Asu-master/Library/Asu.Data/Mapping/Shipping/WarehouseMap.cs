using Asu.Core.Domain.Shipping;

namespace Asu.Data.Mapping.Shipping
{
    public class WarehouseMap : NopEntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            this.ToTable("Warehouse");
            this.HasKey(wh => wh.Id);
            this.Property(wh => wh.Name).IsRequired().HasMaxLength(400);
        }
    }
}
