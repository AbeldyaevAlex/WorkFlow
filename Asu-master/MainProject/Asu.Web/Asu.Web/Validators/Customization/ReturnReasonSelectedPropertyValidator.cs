namespace Asu.Web.Validators.Customization
{
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using FluentValidation.Internal;
    using FluentValidation.Mvc;
    using FluentValidation.Validators;


    public class ReturnReasonSelectedPropertyValidator : FluentValidationPropertyValidator // PropertyValidator, IClientValidatable
    {
//private readonly Func<ModelMetadata, ControllerContext, IEnumerable<ModelClientValidationRule>> getClientValidationRulesFunc;

        public ReturnReasonSelectedPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (!this.ShouldGenerateClientSideRules())
            {
                yield break;
            }

            var validator = this.Validator as GreaterThanValidator;

            var errorMessage = new MessageFormatter()
                .AppendPropertyName(this.Rule.GetDisplayName()).ToString();

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = "reason-selected"
            };

            rule.ValidationParameters["other"] = CompareAttribute.FormatPropertyForClientValidation(validator.MemberToCompare.Name);
            yield return rule;
        }

        /*public ReturnReasonSelectedPropertyValidator(Func<ModelMetadata, ControllerContext, IEnumerable<ModelClientValidationRule>> getClientValidationRulesFunc)
            : base((string)null)
        {
            this.getClientValidationRulesFunc = getClientValidationRulesFunc;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return this.getClientValidationRulesFunc(metadata, context);
        }*/

        /*protected override bool IsValid(PropertyValidatorContext context)
        {
            // Suppress any server side validation
            return true;
        }*/
    }
}