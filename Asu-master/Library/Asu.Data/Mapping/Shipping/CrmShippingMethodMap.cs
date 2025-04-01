namespace Asu.Data.Mapping.Shipping
{
    using Asu.Core.Domain.Shipping;

    public class CrmShippingMethodMap : NopEntityTypeConfiguration<CrmShippingMethod>
    {
        public CrmShippingMethodMap()
        {
            this.ToTable("vw_crm_ShippingMethods");
            this.HasKey(m => m.Id);
        }
    }
}
