namespace Asu.Data.Mapping.SalesQuotes
{
    using Asu.Core.Domain.SalesQuotes;

    public class SalesQuoteLineMap : NopEntityTypeConfiguration<SalesQuoteLine>
    {
        public SalesQuoteLineMap()
        {
            this.ToTable("WCS_SalesQuoteLines");
            this.HasKey(m => m.Id);
            this.HasRequired(m => m.Quote).WithMany(m => m.Lines).HasForeignKey(m => m.QuoteId);
            this.HasRequired(m => m.Product).WithMany().HasForeignKey(m => m.ProductId);
        }
    }
}
