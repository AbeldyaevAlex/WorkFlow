namespace Asu.Data.Mapping.SalesQuotes
{
    using Asu.Core.Domain.SalesQuotes;

    public class SalesQuoteMap : NopEntityTypeConfiguration<SalesQuote>
    {
        public SalesQuoteMap()
        {
            this.ToTable("WCS_SalesQuotes");
            this.HasKey(m => m.Id);
            this.HasMany(m => m.Lines).WithRequired(m => m.Quote).HasForeignKey(m => m.QuoteId);
        }
    }
}
