namespace Asu.Services.SalesQuotes
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Security;
    using Asu.Core;
    using Asu.Core.Data;
    using Asu.Core.Domain.Customers;
    using Asu.Core.Domain.Logging;
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.SalesQuotes;
    using Asu.Services.Common;
    using Asu.Services.Customization;
    using Asu.Services.Logging;

    public class SalesQuoteService : ISalesQuoteService
    {
        private readonly IRepository<SalesQuote> salesQuoteRepository;
        private readonly IRepository<SalesQuoteRestoreHistory> salesQuoteRestoreHistory;
        private readonly ICustomHelper customHelper;
        private readonly ILogger logger;
        private readonly IGenericAttributeService genericAttributeService;


        public SalesQuoteService(IRepository<SalesQuote> salesQuoteRepository,
            IRepository<SalesQuoteRestoreHistory> salesQuoteRestoreHistory,
            ICustomHelper customHelper,
            ILogger logger,
            IGenericAttributeService genericAttributeService)
        {
            this.salesQuoteRepository = salesQuoteRepository;
            this.customHelper = customHelper;
            this.logger = logger;
            this.genericAttributeService = genericAttributeService;
            this.salesQuoteRestoreHistory = salesQuoteRestoreHistory;
        }

        public SalesQuote GetQuoteById(int id)
        {
            return id == 0 ? null : this.salesQuoteRepository.GetById(id);
        }

        public void UpdateQuote(SalesQuote quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            this.salesQuoteRepository.Update(quote);
        }

        public void InsertQuote(SalesQuote quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            this.salesQuoteRepository.Insert(quote);
        }

        public bool VerifyQuote(SalesQuote quote, out string errorMessage)
        {
            errorMessage = null;
            if (quote.CreatedOn < DateTime.UtcNow.AddDays(-14))
            {
                errorMessage = "The quote has expired.";
                return false;
            }

            if (quote.OrderId.HasValue)
            {
                errorMessage = "The quote is already paid.";
                return false;
            }

            return true;
        }

        public void CreateQuoteCookie(SalesQuote quote)
        {
            this.customHelper.AddToCookie("quote", Protect(quote.Id.ToString(CultureInfo.InvariantCulture)), DateTime.UtcNow.AddHours(24));
        }

        public void DeleteQuoteCookie()
        {
            this.customHelper.DeleteCookieValue("quote");
        }

        public int GetQuoteIdFromCookie()
        {
            var quoteCookieValue = this.customHelper.GetCookieValue("quote");
            var quoteIdString = Unprotect(quoteCookieValue);
            if (string.IsNullOrEmpty(quoteIdString))
            {
                return 0;
            }

            int quoteId;
            return !int.TryParse(quoteIdString, out quoteId) ? 0 : quoteId;
        }

        public void UpdatePaidQuote(Order order)
        {
            try
            {
                var quoteId = this.GetQuoteIdFromCookie();
                if (quoteId < 0)
                {
                    return;
                }

                var quote = this.GetQuoteById(quoteId);
                if (quote == null)
                {
                    return;
                }

                quote.OrderId = order.Id;
                foreach (var line in quote.Lines)
                {
                    var orderItem = order.OrderItems.SingleOrDefault(m => m.ProductId == line.ProductId);
                    if (orderItem != null)
                    {
                        line.OrderItemId = orderItem.Id;
                    }

                    var adminPriceAttribute = order.Customer.GetAttribute($"AdminProductPrice-{line.ProductId}", this.genericAttributeService, order.StoreId);
                    if (!string.IsNullOrEmpty(adminPriceAttribute.Value))
                    {
                        this.genericAttributeService.DeleteAttribute(adminPriceAttribute);
                    }
                }

                this.UpdateQuote(quote);
                this.customHelper.DeleteCookieValue("quote");
            }
            catch (Exception ex)
            {
                this.logger.InsertLog(LogLevel.Error, ex.Message, ex.StackTrace);
            }

        }

        public void InsertRestoreHistory(SalesQuoteRestoreHistory history)
        {
            if (history == null)
            {
                throw new ArgumentNullException(nameof(history));
            }

            this.salesQuoteRestoreHistory.Insert(history);
        }

        private static string Protect(string text, params string[] purpose)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var stream = Encoding.UTF8.GetBytes(text);
            var encodedValue = MachineKey.Protect(stream, purpose);
            return HttpServerUtility.UrlTokenEncode(encodedValue);
        }

        private static string Unprotect(string text, params string[] purpose)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var stream = HttpServerUtility.UrlTokenDecode(text);
            if (stream == null)
            {
                return null;
            }

            try
            {
                var decodedValue = MachineKey.Unprotect(stream, purpose);
                if (decodedValue != null)
                {
                    return Encoding.UTF8.GetString(decodedValue);
                }
            }
            catch
            {
                return null;
            }

            return null;
        }
    }
}
