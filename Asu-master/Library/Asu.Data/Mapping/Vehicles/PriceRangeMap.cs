namespace Asu.Data.Mapping.Vehicles
{
    using Asu.Core.Domain.Vehicles;

    public class PriceRangeMap : NopEntityTypeConfiguration<PriceRange>
    {
        public PriceRangeMap()
        {
            this.ToTable("WCS_PriceRange");
            this.HasKey(t => t.Id);
            this.Property(p => p.MinPrice).IsRequired();
            this.Property(p => p.MaxPrice).IsRequired();
        }
    }
}
