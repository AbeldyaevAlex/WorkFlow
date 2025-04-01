using FluentValidation;
using Asu.Services.Localization;
using Asu.Framework.Validators;
using Asu.Web.Models.PrivateMessages;

namespace Asu.Web.Validators.PrivateMessages
{
    public class SendPrivateMessageValidator : BaseNopValidator<SendPrivateMessageModel>
    {
        public SendPrivateMessageValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage(localizationService.GetResource("PrivateMessages.SubjectCannotBeEmpty"));
            RuleFor(x => x.Message).NotEmpty().WithMessage(localizationService.GetResource("PrivateMessages.MessageCannotBeEmpty"));
        }
    }
}