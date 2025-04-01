namespace Asu.Web.Validators.Customization
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using FluentValidation;

    using Asu.Web.Models.Returns;

    public class ReturnRequestItemValidator : AbstractValidator<ReturnRequestItemModel>
    {
        private static readonly string[] ValidExtensions = { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
        public ReturnRequestItemValidator()
        {
            //this.RuleFor(i => i.SelectedReturnReasonId).SetValidator(new ReturnReasonSelectedPropertyValidator());
            this.When(i => i.SelectedQuantity > 0, () =>
            {
                this.RuleFor(i => i.SelectedReturnReasonId).GreaterThan(0).WithMessage("Please select a return reason for return");
                this.RuleFor(i => i.Comment).NotEmpty().WithMessage("An item to return must have a comment");
                this.RuleFor(i => i.Image).Must(i =>
                {
                    if (i == null)
                    {
                        return true;
                    }

                    if (i.ContentLength > 5242880 || ValidExtensions.All(e => e != Path.GetExtension(i.FileName)))
                    {
                        return false;
                    }

                    return true;
                }).WithMessage(string.Format("Allowed picture extensions are {0}. Maximum size of a picture is 5MB.", string.Join(", ", ValidExtensions)));
            });
        }

        public bool HaveFormat(HttpPostedFileBase image)
        {
            if (image.ContentLength > 5242880 || ValidExtensions.All(i => i != Path.GetExtension(image.FileName)))
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetReturnReasonValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = "Description required",
                ValidationType = "validateDescription"
            };
        }
    }
}