using FluentValidation;
using Asu.Services.Localization;
using Asu.Framework.Validators;
using Asu.Web.Models.Common;

namespace Asu.Web.Validators.Common
{
    public class ContactUsValidator : BaseNopValidator<ContactUsModel>
    {
        public ContactUsValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("ContactUs.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));
            RuleFor(x => x.FullName).NotEmpty().WithMessage(localizationService.GetResource("ContactUs.FullName.Required"));
            RuleFor(x => x.Enquiry).NotEmpty().WithMessage(localizationService.GetResource("ContactUs.Enquiry.Required"));
        }}
}