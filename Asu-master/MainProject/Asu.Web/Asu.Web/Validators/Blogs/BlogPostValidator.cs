using FluentValidation;
using Asu.Services.Localization;
using Asu.Framework.Validators;
using Asu.Web.Models.Blogs;

namespace Asu.Web.Validators.Blogs
{
    public class BlogPostValidator : BaseNopValidator<BlogPostModel>
    {
        public BlogPostValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.AddNewComment.CommentText).NotEmpty().WithMessage(localizationService.GetResource("Blog.Comments.CommentText.Required")).When(x => x.AddNewComment != null);
        }}
}