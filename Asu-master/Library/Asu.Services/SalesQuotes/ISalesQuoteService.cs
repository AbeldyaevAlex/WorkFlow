namespace Asu.Services.SalesQuotes
{
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.SalesQuotes;

    public interface ISalesQuoteService
    {
        SalesQuote GetQuoteById(int id);

        void UpdateQuote(SalesQuote quote);

        void InsertQuote(SalesQuote quote);

        bool VerifyQuote(SalesQuote quote, out string errorMessage);

        void CreateQuoteCookie(SalesQuote quote);

        void UpdatePaidQuote(Order order);

        int GetQuoteIdFromCookie();

        void InsertRestoreHistory(SalesQuoteRestoreHistory history);
    }
}
