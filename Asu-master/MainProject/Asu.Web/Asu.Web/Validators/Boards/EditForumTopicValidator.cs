using FluentValidation;
using Asu.Services.Localization;
using Asu.Framework.Validators;
using Asu.Web.Models.Boards;

namespace Asu.Web.Validators.Boards
{
    public class EditForumTopicValidator : BaseNopValidator<EditForumTopicModel>
    {
        public EditForumTopicValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage(localizationService.GetResource("Forum.TopicSubjectCannotBeEmpty"));
            RuleFor(x => x.Text).NotEmpty().WithMessage(localizationService.GetResource("Forum.TextCannotBeEmpty"));
        }
    }
}