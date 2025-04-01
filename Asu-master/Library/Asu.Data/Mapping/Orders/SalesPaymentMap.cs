using Asu.Core.Domain.Orders;

namespace Asu.Data.Mapping.Orders
{
    class SalesPaymentMap : NopEntityTypeConfiguration<SalesPayment>
    {
        public SalesPaymentMap()
        {
            this.ToTable("vw_crm_SalesPayments");
            this.HasKey(m => m.Id);
            this.HasRequired(m => m.Order).WithMany(m => m.Payments).HasForeignKey(m => m.OrderId);
            this.HasMany(m => m.Charges).WithRequired(m => m.Payment);
        }
    }
}
