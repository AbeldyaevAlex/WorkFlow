namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class CrmOrderMap : NopEntityTypeConfiguration<CrmSalesOrder>
    {
        public CrmOrderMap()
        {
            this.ToTable("vw_crm_SalesOrders");
            this.HasKey(i => i.Id);
            //this.HasOptional(i => i.ThubOrder).WithMany().HasForeignKey(i => i.ThubOrderId);
            this.HasMany(m => m.Lines).WithRequired(m => m.Order).HasForeignKey(m => m.OrderId);
            this.HasOptional(m => m.BillingAddress).WithMany().HasForeignKey(m => m.BillingAddressId);
            this.HasOptional(m => m.ShippingAddress).WithMany().HasForeignKey(m => m.ShippingAddressId);
            this.HasRequired(m => m.CrmChannel).WithMany().HasForeignKey(m => m.ChannelId);

            //this.HasRequired(i => i.Status).WithMany();
        }
    }
}