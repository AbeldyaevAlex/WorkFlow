namespace Asu.Web.Models.SimpleCheckout
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Asu.Framework.Mvc;
    using Asu.Web.Models.Common;
    using Asu.Framework.UI;
    using Asu.Core;

    public class AddressModel : BaseNopEntityModel
    {
        public AddressModel()
        {
            this.AvailableCountries = new List<CustomSelectListItem>();
            this.AvailableStates = new List<CustomSelectListItem>();
            this.CustomAddressAttributes = new List<AddressAttributeModel>();
        }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        public bool CompanyEnabled { get; set; }

        public bool CompanyRequired { get; set; }

        public string Company { get; set; }

        public bool CountryEnabled { get; set; }

        public int? CountryId { get; set; }

        public string CountryName { get; set; }

        public bool StateProvinceEnabled { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string StateProvinceShortName { get; set; }

        public bool CityEnabled { get; set; }

        public bool CityRequired { get; set; }

        public string City { get; set; }

        public bool StreetAddressEnabled { get; set; }

        public bool StreetAddressRequired { get; set; }

        [RegularExpression(@"^((?!.*p-o-b-o-x|P.O. Box|PO box|PO |Postal.*).)*$", ErrorMessage = @"""PO box"" or ""Postal"" are not allowed")]
        [Display(Name = "Address line")]
        public string Address1 { get; set; }

        public bool StreetAddress2Enabled { get; set; }

        public bool StreetAddress2Required { get; set; }

        [RegularExpression(@"^((?!.*p-o-b-o-x|P.O. Box|PO box|PO |Postal.*).)*$", ErrorMessage = @"""PO box"" or ""Postal"" are not allowed")]
        [Display(Name = "Address line 2")]
        public string Address2 { get; set; }

        public bool ZipPostalCodeEnabled { get; set; }

        public bool ZipPostalCodeRequired { get; set; }

        [Display(Name = "Zip code")]
        public string ZipPostalCode { get; set; }

        public bool PhoneEnabled { get; set; }

        public bool PhoneRequired { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public bool FaxEnabled { get; set; }

        public bool FaxRequired { get; set; }

        [Display(Name = "Fax number")]
        public string FaxNumber { get; set; }

        public IList<CustomSelectListItem> AvailableCountries { get; set; }

        public IList<CustomSelectListItem> AvailableStates { get; set; }

        public string FormattedCustomAddressAttributes { get; set; }

        public IList<AddressAttributeModel> CustomAddressAttributes { get; set; }

        public bool EditButtonsHidden { get; set; }

        public bool Selected { get; set; }

        public int TypeCode { get; set; }
    }
}