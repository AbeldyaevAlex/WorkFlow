using Asu.Framework.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asu.Web.Models.Review;

namespace Asu.Web.Validators.Review
{
    public class SubmitReviewValidator: BaseNopValidator<SubmitReviewModel>
    {
        public SubmitReviewValidator()
        {
            this.RuleFor(m => m.Text).NotEmpty().NotNull().WithMessage("Review text should be present");
            this.RuleFor(m => m.Title).NotEmpty().NotNull().WithMessage("Review title should be present");
        }
    }
}