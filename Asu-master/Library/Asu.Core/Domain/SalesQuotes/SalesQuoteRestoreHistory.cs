namespace Asu.Core.Domain.SalesQuotes
{
    using System;

    public class SalesQuoteRestoreHistory : BaseEntity
    {
        public new long Id { get; set; }

        public int CustomerId { get; set; }

        public int QuoteId { get; set; }

        public DateTime RestoredOn { get; set; }
    }
}
