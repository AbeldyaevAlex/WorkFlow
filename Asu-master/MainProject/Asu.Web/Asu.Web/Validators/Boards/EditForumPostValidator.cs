using FluentValidation;
using Asu.Services.Localization;
using Asu.Framework.Validators;
using Asu.Web.Models.Boards;

namespace Asu.Web.Validators.Boards
{
    public class EditForumPostValidator : BaseNopValidator<EditForumPostModel>
    {
        public EditForumPostValidator(ILocalizationService localizationService)
        {            
            RuleFor(x => x.Text).NotEmpty().WithMessage(localizationService.GetResource("Forum.TextCannotBeEmpty"));
        }
    }
}