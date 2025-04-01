namespace Asu.Data.Mapping.Orders
{
    using Asu.Core.Domain.Orders;

    public class CrmChannelMap : NopEntityTypeConfiguration<CrmChannel>
    {
        public CrmChannelMap()
        {
            this.ToTable("vw_crm_Channels");
            this.HasKey(m => m.Id);
        }
    }
}
