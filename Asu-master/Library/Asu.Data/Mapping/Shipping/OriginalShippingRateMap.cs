namespace Asu.Data.Mapping.Shipping
{
    using Asu.Core.Domain.Shipping;

    public class OriginalShippingRateMap : NopEntityTypeConfiguration<OriginalShippingRate>
    {
        public OriginalShippingRateMap()
        {
            this.ToTable("WCS_OriginalShippingRates");
            this.HasKey(m => m.Id);
        }
    }
}
