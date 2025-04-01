namespace Asu.Core.Domain.Orders
{
    using Asu.Core.Domain.Returns;
    using System.Linq;

    public static class OrdersExtensions
    {
        public static decimal GetOrderChargeAmount(this CrmSalesOrder order, SalesPaymentChargeType chargeType)
        {
            if (chargeType == SalesPaymentChargeType.Total)
            {
                var charges = order.Payments.SelectMany(m => m.Charges);
                var discount = charges.SingleOrDefault(m => m.Type == SalesPaymentChargeType.Discount)?.Amount ?? decimal.Zero;

                return order.Payments.SelectMany(m => m.Charges.Where(c => c.Type != SalesPaymentChargeType.Discount)).Sum(m => m.Amount) - discount;
            }

            return order.Payments.SelectMany(m => m.Charges).SingleOrDefault(m => m.Type == chargeType)?.Amount ?? decimal.Zero;
        }
    }
}
