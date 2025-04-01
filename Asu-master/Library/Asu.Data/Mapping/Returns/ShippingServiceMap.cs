namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ShippingServiceMap : NopEntityTypeConfiguration<ShippingService>
    {
        public ShippingServiceMap()
        {
            this.ToTable("vw_crm_ShippingServices");
            this.HasKey(m => m.Id);
        }
    }
}
