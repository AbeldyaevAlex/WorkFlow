namespace Asu.Data.Mapping.SalesQuotes
{
    using Asu.Core.Domain.SalesQuotes;

    public class SalesQuoteRestoreHistoryMap : NopEntityTypeConfiguration<SalesQuoteRestoreHistory>
    {
        public SalesQuoteRestoreHistoryMap()
        {
            this.ToTable("WCS_SalesQuoteRestoreHistory");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.QuoteId, m.CustomerId, m.RestoredOn });
        }
    }
}
