namespace Asu.Data.Mapping.Orders
{
    using Asu.Core.Domain.Orders;

    public class CrmAddressMap : NopEntityTypeConfiguration<CrmAddress>
    {
        public CrmAddressMap()
        {
            this.ToTable("vw_crm_Addresses");
            this.HasKey(m => m.Id);
            this.HasRequired(m => m.Country).WithMany().HasForeignKey(m => m.CountryId);
            this.HasRequired(m => m.State).WithMany().HasForeignKey(m => m.StateId);
        }
    }
}
