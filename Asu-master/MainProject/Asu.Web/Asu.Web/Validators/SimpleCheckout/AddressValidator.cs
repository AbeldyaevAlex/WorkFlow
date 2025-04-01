using FluentValidation;
using FluentValidation.Results;
using Asu.Core;
using Asu.Core.Domain.Common;
using Asu.Services.Directory;
using Asu.Services.Localization;
using Asu.Framework.Validators;
using Asu.Web.Models.SimpleCheckout;

namespace Asu.Web.Validators.SimpleCheckout
{
    public class AddressValidator : BaseNopValidator<AddressModel>
    {
        private static readonly int addressLineMaxLength = 35;
        private static readonly int COMPANY_MAX_LENGTH = 100;

        public AddressValidator(ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            AddressSettings addressSettings)
        {
            this.RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Address.Fields.FirstName.Required"));
            this.RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Address.Fields.LastName.Required"));
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Address.Fields.Email.Required"));
            this.RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(localizationService.GetResource("Common.WrongEmail"));
            if (addressSettings.CountryEnabled)
            {
                this.RuleFor(x => x.CountryId)
                    .NotNull()
                    .WithMessage(localizationService.GetResource("Address.Fields.Country.Required"));
                this.RuleFor(x => x.CountryId)
                    .NotEqual(0)
                    .WithMessage(localizationService.GetResource("Address.Fields.Country.Required"));
            }
            if (addressSettings.CountryEnabled && addressSettings.StateProvinceEnabled)
            {
                this.When(m => stateProvinceService.GetStateProvincesByCountryId(m.CountryId ?? 0).Count > 0 && string.IsNullOrEmpty(m.StateProvinceShortName), () =>
                {
                    this.RuleFor(m => m.StateProvinceId).NotNull().GreaterThan(0).WithMessage(localizationService.GetResource("Address.Fields.StateProvince.Required")) ;  
                });
            }

            if (addressSettings.ZipPostalCodeRequired && addressSettings.ZipPostalCodeEnabled)
            {
                this.RuleFor(x => x.ZipPostalCode)
                  .Matches(ConstantStorage.USZipCodeValidationRegex)
                  .When(x => x.CountryId == ConstantStorage.US)
                  .WithMessage(localizationService.GetResource("Account.Fields.ZipPostalCode.ConditionalRegex"));
            }

            if (addressSettings.CompanyEnabled)
            {
                if (addressSettings.CompanyRequired)
                {
                    this.RuleFor(x => x.Company).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Company.Required"));
                    this.RuleFor(x => x.Company).Length(1, COMPANY_MAX_LENGTH).WithMessage($"Company name should be up to {COMPANY_MAX_LENGTH} characters");
                }
                else
                {
                    this.RuleFor(x => x.Company).Length(0, COMPANY_MAX_LENGTH).WithMessage($"Company name should be up to {COMPANY_MAX_LENGTH} characters");
                }
            }

            if (addressSettings.StreetAddressRequired && addressSettings.StreetAddressEnabled)
            {
                this.RuleFor(x => x.Address1)
                    //.Matches(ConstantStorage.AddressValidationRegex)
                    .NotEmpty()
                    .WithMessage(localizationService
                    .GetResource("Account.Fields.StreetAddress.Required"));
                this.RuleFor(x => x.Address1)
                    //.Matches(ConstantStorage.AddressValidationRegex)
                    .Length(1, addressLineMaxLength)
                    .WithMessage(localizationService
                    .GetResource("Account.Fields.StreetAddress.Length"));
            }

            if (addressSettings.StreetAddress2Required && addressSettings.StreetAddress2Enabled)
            {
                this.RuleFor(x => x.Address2).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.StreetAddress2.Required"));
            }

            if (addressSettings.StreetAddress2Enabled)
            {
                this.RuleFor(x => x.Address2).Length(0, addressLineMaxLength).WithMessage(localizationService.GetResource("Account.Fields.StreetAddress2.Length"));
            }

            if (addressSettings.ZipPostalCodeRequired && addressSettings.ZipPostalCodeEnabled)
            {
                this.RuleFor(x => x.ZipPostalCode).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.ZipPostalCode.Required"));
            }
            if (addressSettings.CityRequired && addressSettings.CityEnabled)
            {
                this.RuleFor(x => x.City).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.City.Required"));
            }
            if (addressSettings.PhoneRequired && addressSettings.PhoneEnabled)
            {
                this.RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Phone.Required"));
            }
            if (addressSettings.FaxRequired && addressSettings.FaxEnabled)
            {
                this.RuleFor(x => x.FaxNumber).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Fax.Required"));
            }
        }
    }
}