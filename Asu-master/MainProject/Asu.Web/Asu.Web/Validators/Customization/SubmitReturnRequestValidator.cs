namespace Asu.Web.Validators.Customization
{
    using System;
    using System.Linq;

    using FluentValidation;
    using FluentValidation.Results;

    using Asu.Web.Models.Returns;

    using Asu.Services.Customization;

    public class SubmitReturnRequestValidator : AbstractValidator<SubmitReturnRequestModel>
    {
        public SubmitReturnRequestValidator(IReturnService returnService)
        {

        }
    }
}