using FluentValidation;
using Asu.Core.Domain.Customers;
using Asu.Services.Localization;
using Asu.Framework.Validators;
using Asu.Web.Models.Customer;

namespace Asu.Web.Validators.Customer
{
    public class PasswordRecoveryConfirmValidator : BaseNopValidator<PasswordRecoveryConfirmModel>
    {
        public PasswordRecoveryConfirmValidator(ILocalizationService localizationService, CustomerSettings customerSettings)
        {
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(localizationService.GetResource("Account.PasswordRecovery.NewPassword.Required"));
            RuleFor(x => x.NewPassword).Length(customerSettings.PasswordMinLength, 999).WithMessage(string.Format(localizationService.GetResource("Account.PasswordRecovery.NewPassword.LengthValidation"), customerSettings.PasswordMinLength));
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage(localizationService.GetResource("Account.PasswordRecovery.ConfirmNewPassword.Required"));
            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage(localizationService.GetResource("Account.PasswordRecovery.NewPassword.EnteredPasswordsDoNotMatch"));
        }}
}