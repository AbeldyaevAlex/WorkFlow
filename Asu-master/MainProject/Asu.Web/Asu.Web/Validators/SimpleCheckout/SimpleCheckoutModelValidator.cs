namespace Asu.Web.Validators.SimpleCheckout
{
    using Asu.Core.Domain.Common;
    using Asu.Services.Directory;
    using Asu.Services.Localization;
    using Asu.Framework.Validators;
    using Asu.Web.Models.SimpleCheckout;

    public class CommonCheckoutAddressModelValidator : BaseNopValidator<CommonCheckoutAddressModel>
    {
        private readonly ILocalizationService localizationService;
        private readonly IStateProvinceService stateProvinceService;
        private readonly AddressSettings addressSettings;

        public CommonCheckoutAddressModelValidator(ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            AddressSettings addressSettings)
        {
            this.localizationService = localizationService;
            this.stateProvinceService = stateProvinceService;
            this.addressSettings = addressSettings;

            this.RuleFor(m => m.ShippingAddress.NewAddress).SetValidator(new AddressValidator(localizationService, stateProvinceService, addressSettings));

            this.When(m => !m.IsBillingSameAsShipping, () =>
            {
                this.RuleFor(m => m.BillingAddress.NewAddress).SetValidator(new AddressValidator(localizationService, stateProvinceService, addressSettings));
            });
        }
    }
}