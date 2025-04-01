namespace Asu.Core.Domain.SalesQuotes
{
    using Asu.Core.Domain.Catalog;

    public class SalesQuoteLine : BaseEntity
    {
        public int QuoteId { get; set; }

        public int ProductId { get; set; }

        public int? OrderItemId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual SalesQuote Quote { get; set; }

        public virtual Product Product { get; set; }
    }
}
