using FluentValidation;
using Asu.Framework.Validators;
using Asu.Web.Models.Customization;

namespace Asu.Web.Validators.Customization
{
    public class CheckOrderModelValidator : BaseNopValidator<CheckOrderModel>
    {
        public CheckOrderModelValidator()
        {
            this.RuleFor(x => x.OrderNumber).NotEmpty().WithMessage("Order number is required");
            this.RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip code is required");
        }
    }
}