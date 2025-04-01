namespace Asu.Web.Models.SalesQuote
{
    using FluentValidation.Attributes;

    using Asu.Web.Validators.SalesQuote;

    [Validator(typeof(SalesQuoteValidator))]
    public class SalesQuoteModel
    {
        public string Email { get; set; }

        public string CustomerName { get; set; }

        public string Note { get; set; }
    }
}