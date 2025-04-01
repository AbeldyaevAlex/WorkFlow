namespace Asu.Data.Mapping.Orders
{
    using Asu.Core.Domain.Orders;

    public class SalesPaymentChargeMap : NopEntityTypeConfiguration<SalesPaymentCharge>
    {
        public SalesPaymentChargeMap()
        {
            this.ToTable("vw_crm_SalesPaymentCharges");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.PaymentId, m.TypeId });
        }
    }
}
