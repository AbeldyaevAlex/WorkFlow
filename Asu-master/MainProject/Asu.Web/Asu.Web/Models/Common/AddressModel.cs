using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Asu.Framework;
using Asu.Framework.Mvc;
using Asu.Web.Validators.Common;
using System.ComponentModel.DataAnnotations;
using Asu.Framework.UI;

namespace Asu.Web.Models.Common
{
    [Validator(typeof(AddressValidator))]
    public partial class AddressModel : BaseNopEntityModel
    {
        public AddressModel()
        {
            this.AvailableCountries = new List<CustomSelectListItem>();
            this.AvailableStates = new List<CustomSelectListItem>();
            this.CustomAddressAttributes = new List<AddressAttributeModel>();
        }

        [NopResourceDisplayName("Address.Fields.FirstName")]
        [AllowHtml]
        [RegularExpression(@"^[\S]*$", ErrorMessage = "White spaces are not allowed")]
        public string FirstName { get; set; }
        [NopResourceDisplayName("Address.Fields.LastName")]
        [AllowHtml]
        [RegularExpression(@"^[\S]*$", ErrorMessage = "White spaces are not allowed")]
        public string LastName { get; set; }
        [NopResourceDisplayName("Address.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }


        public bool CompanyEnabled { get; set; }
        public bool CompanyRequired { get; set; }
        [NopResourceDisplayName("Address.Fields.Company")]
        [AllowHtml]
        public string Company { get; set; }

        public bool CountryEnabled { get; set; }
        [NopResourceDisplayName("Address.Fields.Country")]
        public int? CountryId { get; set; }
        [NopResourceDisplayName("Address.Fields.Country")]
        [AllowHtml]
        public string CountryName { get; set; }

        public bool StateProvinceEnabled { get; set; }
        [NopResourceDisplayName("Address.Fields.StateProvince")]
        public int? StateProvinceId { get; set; }
        [NopResourceDisplayName("Address.Fields.StateProvince")]
        [AllowHtml]
        public string StateProvinceName { get; set; }

        public bool CityEnabled { get; set; }
        public bool CityRequired { get; set; }
        [NopResourceDisplayName("Address.Fields.City")]
        [AllowHtml]
        public string City { get; set; }

        public bool StreetAddressEnabled { get; set; }
        public bool StreetAddressRequired { get; set; }
        [NopResourceDisplayName("Address.Fields.Address1")]
        [AllowHtml]
        public string Address1 { get; set; }

        public bool StreetAddress2Enabled { get; set; }
        public bool StreetAddress2Required { get; set; }
        [NopResourceDisplayName("Address.Fields.Address2")]
        [AllowHtml]
        public string Address2 { get; set; }

        public bool ZipPostalCodeEnabled { get; set; }
        public bool ZipPostalCodeRequired { get; set; }
        [NopResourceDisplayName("Address.Fields.ZipPostalCode")]
        [AllowHtml]
        public string ZipPostalCode { get; set; }

        public bool PhoneEnabled { get; set; }
        public bool PhoneRequired { get; set; }
        [NopResourceDisplayName("Address.Fields.PhoneNumber")]
        [AllowHtml]
        public string PhoneNumber { get; set; }

        public bool FaxEnabled { get; set; }
        public bool FaxRequired { get; set; }
        [NopResourceDisplayName("Address.Fields.FaxNumber")]
        [AllowHtml]
        public string FaxNumber { get; set; }

        public IList<CustomSelectListItem> AvailableCountries { get; set; }
        public IList<CustomSelectListItem> AvailableStates { get; set; }


        public string FormattedCustomAddressAttributes { get; set; }
        public IList<AddressAttributeModel> CustomAddressAttributes { get; set; }
    }
}