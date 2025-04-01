namespace Asu.Web.Validators.SalesQuote
{
    using FluentValidation;

    using Asu.Framework.Validators;
    using Asu.Web.Models.SalesQuote;

    public class SalesQuoteValidator : BaseNopValidator<SalesQuoteModel>
    {
        public SalesQuoteValidator()
        {
            this.RuleFor(m => m.Email).NotNull().WithMessage("Email is required");
            this.RuleFor(m => m.Email).EmailAddress().WithMessage("Incorrect Email format");
            this.RuleFor(m => m.Email).Length(1, 256).WithMessage("Email length must be between 1 and 256 characters");

            this.RuleFor(m => m.CustomerName).NotNull().WithMessage("Customer name is required");
            this.RuleFor(m => m.CustomerName).Length(1, 500).WithMessage("Customer name length must be between 1 and 500 characters");

            this.RuleFor(m => m.Note).Length(1, 4000).WithMessage("Notes must be between 1 and 4000 characters");
        }
    }
}