using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Directory;
using Asu.Core.Domain.Seo;
using Asu.Core.Domain.Vehicles;
using Asu.Core.Infrastructure;
using Asu.Services.Common;
using Asu.Services.Directory;
using Asu.Services.Localization;
using Asu.Services.Seo;
using Asu.Services.Vehicles;
using Asu.Web.Models.Catalog;
using Asu.Web.Models.Common;
using Asu.Web.Models.Customization;
using Asu.Web.Models.Vehicles;

namespace Asu.Web.Extensions
{
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.Returns;
    using Asu.Framework.UI;
    using Asu.Web.Models.Returns;

    public static class MappingExtensions
    {
        private static readonly int defaultCountryId = 1;
        //category
        public static CategoryModel ToModel(this Category entity)
        {
            if (entity == null)
                return null;

            var model = new CategoryModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle),
                SeName = entity.GetSeName(),
            };
            return model;
        }

        //manufacturer
        public static ManufacturerModel ToModel(this Manufacturer entity)
        {
            if (entity == null)
                return null;

            var model = new ManufacturerModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle),
                SeName = entity.GetSeName(),
            };
            return model;
        }


        //address
        /// <summary>
        /// Prepare address model
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="address">Address</param>
        /// <param name="excludeProperties">A value indicating whether to exclude properties</param>
        /// <param name="addressSettings">Address settings</param>
        /// <param name="localizationService">Localization service (used to prepare a select list)</param>
        /// <param name="stateProvinceService">State service (used to prepare a select list). null to don't prepare the list.</param>
        /// <param name="addressAttributeService">Address attribute service. null to don't prepare the list.</param>
        /// <param name="addressAttributeParser">Address attribute parser. null to don't prepare the list.</param>
        /// <param name="addressAttributeFormatter">Address attribute formatter. null to don't prepare the formatted custom attributes.</param>
        /// <param name="loadCountries">A function to load countries  (used to prepare a select list). null to don't prepare the list.</param>
        /// <param name="prePopulateWithCustomerFields">A value indicating whether to pre-populate an address with customer fields entered during registration. It's used only when "address" parameter is set to "null"</param>
        /// <param name="customer">Customer record which will be used to pre-populate address. Used only when "prePopulateWithCustomerFields" is "true".</param>
        public static void PrepareModel(this AddressModel model,
            Address address, bool excludeProperties, 
            AddressSettings addressSettings,
            ILocalizationService localizationService = null,
            IStateProvinceService stateProvinceService = null,
            IAddressAttributeService addressAttributeService = null,
            IAddressAttributeParser addressAttributeParser = null,
            IAddressAttributeFormatter addressAttributeFormatter = null,
            Func<IList<Country>> loadCountries = null,
            bool prePopulateWithCustomerFields = false,
            Customer customer = null)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (addressSettings == null)
                throw new ArgumentNullException("addressSettings");

            if (!excludeProperties && address != null)
            {
                model.Id = address.Id;
                model.FirstName = address.FirstName;
                model.LastName = address.LastName;
                model.Email = address.Email;
                model.Company = address.Company;
                model.CountryId = address.CountryId;
                model.CountryName = address.Country != null 
                    ? address.Country.GetLocalized(x => x.Name) 
                    : null;
                model.StateProvinceId = address.StateProvinceId;
                model.StateProvinceName = address.StateProvince != null 
                    ? address.StateProvince.GetLocalized(x => x.Name)
                    : null;
                model.City = address.City;
                model.Address1 = address.Address1;
                model.Address2 = address.Address2;
                model.ZipPostalCode = address.ZipPostalCode;
                model.PhoneNumber = address.PhoneNumber;
                model.FaxNumber = address.FaxNumber;
            }

            if (address == null && prePopulateWithCustomerFields)
            {
                if (customer == null)
                    throw new Exception("Customer cannot be null when prepopulating an address");
                model.Email = customer.Email;
                model.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                model.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                model.Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company);
                model.Address1 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress);
                model.Address2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2);
                model.ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode);
                model.City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City);
                //ignore country and state for prepopulation. it can cause some issues when posting pack with errors, etc
                //model.CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                //model.StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
                model.PhoneNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                model.FaxNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax);
            }

            //countries and states
            if (addressSettings.CountryEnabled && loadCountries != null)
            {
                if (localizationService == null)
                    throw new ArgumentNullException("localizationService");

                model.AvailableCountries.Add(new CustomSelectListItem { Text = localizationService.GetResource("Address.SelectCountry"), Value = "0" });
                foreach (var c in loadCountries())
                {
                    model.AvailableCountries.Add(new CustomSelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId,
                        HtmlAttributes = new { data_code = c.TwoLetterIsoCode }
                    });
                }

                if (!model.AvailableCountries.Any(m => m.Selected))
                {
                    model.AvailableCountries.Single(m => m.Value == defaultCountryId.ToString()).Selected = true;
                    model.CountryId = defaultCountryId;
                }

                if (addressSettings.StateProvinceEnabled)
                {
                    //states
                    if (stateProvinceService == null)
                        throw new ArgumentNullException("stateProvinceService");

                    var states = stateProvinceService
                        .GetStateProvincesByCountryId(model.CountryId.HasValue ? model.CountryId.Value : 0)
                        .ToList();
                    if (states.Count > 0)
                    {
                        model.AvailableStates.Add(new CustomSelectListItem { Text = localizationService.GetResource("Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new CustomSelectListItem
                            {
                                Text = s.GetLocalized(x => x.Name),
                                Value = s.Id.ToString(), 
                                Selected = s.Id == model.StateProvinceId,
                                HtmlAttributes = new { data_code = s.Abbreviation } 
                            });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);
                        model.AvailableStates.Add(new CustomSelectListItem
                        {
                            Text = localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }
                }
            }

            //form fields
            model.CompanyEnabled = addressSettings.CompanyEnabled;
            model.CompanyRequired = addressSettings.CompanyRequired;
            model.StreetAddressEnabled = addressSettings.StreetAddressEnabled;
            model.StreetAddressRequired = addressSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = addressSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = addressSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = addressSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = addressSettings.ZipPostalCodeRequired;
            model.CityEnabled = addressSettings.CityEnabled;
            model.CityRequired = addressSettings.CityRequired;
            model.CountryEnabled = addressSettings.CountryEnabled;
            model.StateProvinceEnabled = addressSettings.StateProvinceEnabled;
            model.PhoneEnabled = addressSettings.PhoneEnabled;
            model.PhoneRequired = addressSettings.PhoneRequired;
            model.FaxEnabled = addressSettings.FaxEnabled;
            model.FaxRequired = addressSettings.FaxRequired;

            //customer attribute services
            if (addressAttributeService != null && addressAttributeParser != null)
            {
                PrepareCustomAddressAttributes(model, address, addressAttributeService, addressAttributeParser);
            }
            if (addressAttributeFormatter != null && address != null)
            {
                model.FormattedCustomAddressAttributes = addressAttributeFormatter.FormatAttributes(address.CustomAttributes);
            }
        }

        public static void PrepareModel(this Models.SimpleCheckout.AddressModel model,
            Address address, bool excludeProperties,
            AddressSettings addressSettings,
            ILocalizationService localizationService = null,
            IStateProvinceService stateProvinceService = null,
            IAddressAttributeService addressAttributeService = null,
            IAddressAttributeParser addressAttributeParser = null,
            IAddressAttributeFormatter addressAttributeFormatter = null,
            Func<IList<Country>> loadCountries = null,
            bool prePopulateWithCustomerFields = false,
            Customer customer = null)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (addressSettings == null)
                throw new ArgumentNullException("addressSettings");

            if (!excludeProperties && address != null)
            {
                model.Id = address.Id;
                model.FirstName = address.FirstName;
                model.LastName = address.LastName;
                model.Email = address.Email;
                model.Company = address.Company;
                model.CountryId = address.CountryId;
                model.CountryName = address.Country != null
                    ? address.Country.GetLocalized(x => x.Name)
                    : null;
                model.StateProvinceId = address.StateProvinceId;
                model.StateProvinceName = address.StateProvince != null
                    ? address.StateProvince.GetLocalized(x => x.Name)
                    : null;
                model.City = address.City;
                model.Address1 = address.Address1;
                model.Address2 = address.Address2;
                model.ZipPostalCode = address.ZipPostalCode;
                model.PhoneNumber = address.PhoneNumber;
                model.FaxNumber = address.FaxNumber;
                model.StateProvinceShortName = address.StateProvince?.Abbreviation;
            }

            if (address == null && prePopulateWithCustomerFields)
            {
                if (customer == null)
                    throw new Exception("Customer cannot be null when prepopulating an address");
                model.Email = customer.Email;
                model.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                model.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                model.Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company);
                model.Address1 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress);
                model.Address2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2);
                model.ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode);
                model.City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City);
                //ignore country and state for prepopulation. it can cause some issues when posting pack with errors, etc
                //model.CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                //model.StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
                model.PhoneNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                model.FaxNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax);
            }

            //countries and states
            if (addressSettings.CountryEnabled && loadCountries != null)
            {
                if (localizationService == null)
                    throw new ArgumentNullException("localizationService");

                foreach (var c in loadCountries())
                {
                    model.AvailableCountries.Add(new CustomSelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId,
                        HtmlAttributes = new { data_code = c.TwoLetterIsoCode }
                    });
                }

                if (!model.AvailableCountries.Any(m => m.Selected))
                {
                    model.AvailableCountries.Single(m => m.Value == defaultCountryId.ToString()).Selected = true;
                    model.CountryId = defaultCountryId;
                }

                if (addressSettings.StateProvinceEnabled)
                {
                    //states
                    if (stateProvinceService == null)
                        throw new ArgumentNullException("stateProvinceService");

                    var states = stateProvinceService
                        .GetStateProvincesByCountryId(model.CountryId.HasValue ? model.CountryId.Value : 0)
                        .ToList();
                    if (states.Count > 0)
                    {
                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new CustomSelectListItem
                            {
                                Text = s.GetLocalized(x => x.Name),
                                Value = s.Id.ToString(),
                                Selected = (s.Id == model.StateProvinceId),
                                HtmlAttributes = new { data_code = s.Abbreviation }
                            });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);
                        model.AvailableStates.Add(new CustomSelectListItem
                        {
                            Text = localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }
                }
            }

            //form fields
            model.CompanyEnabled = addressSettings.CompanyEnabled;
            model.CompanyRequired = addressSettings.CompanyRequired;
            model.StreetAddressEnabled = addressSettings.StreetAddressEnabled;
            model.StreetAddressRequired = addressSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = addressSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = addressSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = addressSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = addressSettings.ZipPostalCodeRequired;
            model.CityEnabled = addressSettings.CityEnabled;
            model.CityRequired = addressSettings.CityRequired;
            model.CountryEnabled = addressSettings.CountryEnabled;
            model.StateProvinceEnabled = addressSettings.StateProvinceEnabled;
            model.PhoneEnabled = addressSettings.PhoneEnabled;
            model.PhoneRequired = addressSettings.PhoneRequired;
            model.FaxEnabled = addressSettings.FaxEnabled;
            model.FaxRequired = addressSettings.FaxRequired;

            //customer attribute services
            if (addressAttributeService != null && addressAttributeParser != null)
            {
                PrepareCustomAddressAttributes(model, address, addressAttributeService, addressAttributeParser);
            }
            if (addressAttributeFormatter != null && address != null)
            {
                model.FormattedCustomAddressAttributes = addressAttributeFormatter.FormatAttributes(address.CustomAttributes);
            }
        }

        private static void PrepareCustomAddressAttributes(this Models.SimpleCheckout.AddressModel model,
            Address address,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeParser addressAttributeParser)
        {
            if (addressAttributeService == null)
                throw new ArgumentNullException("addressAttributeService");

            if (addressAttributeParser == null)
                throw new ArgumentNullException("addressAttributeParser");

            var attributes = addressAttributeService.GetAllAddressAttributes();
            foreach (var attribute in attributes)
            {
                var aaModel = new AddressAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var aaValues = addressAttributeService.GetAddressAttributeValues(attribute.Id);
                    foreach (var aaValue in aaValues)
                    {
                        var aaValueModel = new AddressAttributeValueModel
                        {
                            Id = aaValue.Id,
                            Name = aaValue.GetLocalized(x => x.Name),
                            IsPreSelected = aaValue.IsPreSelected
                        };
                        aaModel.Values.Add(aaValueModel);
                    }
                }

                //set already selected attributes
                var selectedAddressAttributes = address != null ? address.CustomAttributes : null;
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                //clear default selection
                                foreach (var item in aaModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedAaValues = addressAttributeParser.ParseAddressAttributeValues(selectedAddressAttributes);
                                foreach (var aaValue in selectedAaValues)
                                    foreach (var item in aaModel.Values)
                                        if (aaValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                var enteredText = addressAttributeParser.ParseValues(selectedAddressAttributes, attribute.Id);
                                if (enteredText.Count > 0)
                                    aaModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                model.CustomAddressAttributes.Add(aaModel);
            }
        }

        private static void PrepareCustomAddressAttributes(this AddressModel model, 
            Address address,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeParser addressAttributeParser)
        {
            if (addressAttributeService == null)
                throw new ArgumentNullException("addressAttributeService");

            if (addressAttributeParser == null)
                throw new ArgumentNullException("addressAttributeParser");

            var attributes = addressAttributeService.GetAllAddressAttributes();
            foreach (var attribute in attributes)
            {
                var aaModel = new AddressAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var aaValues = addressAttributeService.GetAddressAttributeValues(attribute.Id);
                    foreach (var aaValue in aaValues)
                    {
                        var aaValueModel = new AddressAttributeValueModel
                        {
                            Id = aaValue.Id,
                            Name = aaValue.GetLocalized(x => x.Name),
                            IsPreSelected = aaValue.IsPreSelected
                        };
                        aaModel.Values.Add(aaValueModel);
                    }
                }

                //set already selected attributes
                var selectedAddressAttributes = address != null ? address.CustomAttributes : null;
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                //clear default selection
                                foreach (var item in aaModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedAaValues = addressAttributeParser.ParseAddressAttributeValues(selectedAddressAttributes);
                                foreach (var aaValue in selectedAaValues)
                                    foreach (var item in aaModel.Values)
                                        if (aaValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!String.IsNullOrEmpty(selectedAddressAttributes))
                            {
                                var enteredText = addressAttributeParser.ParseValues(selectedAddressAttributes, attribute.Id);
                                if (enteredText.Count > 0)
                                    aaModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                model.CustomAddressAttributes.Add(aaModel);
            }
        }
        public static Address ToEntity(this AddressModel model, bool trimFields = true)
        {
            if (model == null)
                return null;

            var entity = new Address();
            return ToEntity(model, entity, trimFields);
        }

        public static Address ToEntity(this AddressModel model, Address destination, bool trimFields = true)
        {
            if (model == null)
                return destination;

            if (trimFields)
            {
                if (model.FirstName != null)
                    model.FirstName = model.FirstName.Trim();
                if (model.LastName != null)
                    model.LastName = model.LastName.Trim();
                if (model.Email != null)
                    model.Email = model.Email.Trim();
                if (model.Company != null)
                    model.Company = model.Company.Trim();
                if (model.City != null)
                    model.City = model.City.Trim();
                if (model.Address1 != null)
                    model.Address1 = model.Address1.Trim();
                if (model.Address2 != null)
                    model.Address2 = model.Address2.Trim();
                if (model.ZipPostalCode != null)
                    model.ZipPostalCode = model.ZipPostalCode.Trim();
                if (model.PhoneNumber != null)
                    model.PhoneNumber = model.PhoneNumber.Trim();
                if (model.FaxNumber != null)
                    model.FaxNumber = model.FaxNumber.Trim();
            }
            destination.Id = model.Id;
            destination.FirstName = model.FirstName;
            destination.LastName = model.LastName;
            destination.Email = model.Email;
            destination.Company = model.Company;
            destination.CountryId = model.CountryId;
            destination.StateProvinceId = model.StateProvinceId;
            destination.City = model.City;
            destination.Address1 = model.Address1;
            destination.Address2 = model.Address2;
            destination.ZipPostalCode = model.ZipPostalCode;
            destination.PhoneNumber = model.PhoneNumber;
            destination.FaxNumber = model.FaxNumber;

            return destination;
        }

        public static Address ToEntity(this Models.SimpleCheckout.AddressModel model, bool trimFields = true)
        {
            if (model == null)
                return null;

            var entity = new Address();
            return ToEntity(model, entity, trimFields);
        }

        public static Address ToEntity(this Models.SimpleCheckout.AddressModel model, Address destination, bool trimFields = true)
        {
            if (model == null)
                return destination;

            if (trimFields)
            {
                if (model.FirstName != null)
                    model.FirstName = model.FirstName.Trim();
                if (model.LastName != null)
                    model.LastName = model.LastName.Trim();
                if (model.Email != null)
                    model.Email = model.Email.Trim();
                if (model.Company != null)
                    model.Company = model.Company.Trim();
                if (model.City != null)
                    model.City = model.City.Trim();
                if (model.Address1 != null)
                    model.Address1 = model.Address1.Trim();
                if (model.Address2 != null)
                    model.Address2 = model.Address2.Trim();
                if (model.ZipPostalCode != null)
                    model.ZipPostalCode = model.ZipPostalCode.Trim();
                if (model.PhoneNumber != null)
                    model.PhoneNumber = model.PhoneNumber.Trim();
                if (model.FaxNumber != null)
                    model.FaxNumber = model.FaxNumber.Trim();
            }
            destination.Id = model.Id;
            destination.FirstName = model.FirstName;
            destination.LastName = model.LastName;
            destination.Email = model.Email;
            destination.Company = model.Company;
            destination.CountryId = model.CountryId;
            destination.StateProvinceId = model.StateProvinceId;
            destination.City = model.City;
            destination.Address1 = model.Address1;
            destination.Address2 = model.Address2;
            destination.ZipPostalCode = model.ZipPostalCode;
            destination.PhoneNumber = model.PhoneNumber;
            destination.FaxNumber = model.FaxNumber;

            return destination;
        }

        #region WC

        //category
        public static CustomCategoryModel ToCustomModel(this Category entity, Manufacturer manufacturer = null)
        {
            if (entity == null)
                return null;

            var model = new CustomCategoryModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = string.Concat("Discount ", (manufacturer == null
                    ? entity.GetLocalized(x => x.MetaTitle)
                    : $"{entity.GetLocalized(x => x.MetaTitle)} {manufacturer.GetLocalized(x => x.MetaTitle)}")),
                SeName = entity.GetSeName(),
            };
            return model;
        }

        //manufacturer
        public static CustomManufacturerModel ToCustomModel(this Manufacturer entity, Category category)
        {
            if (entity == null)
                return null;

            var model = new CustomManufacturerModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(x => x.Name),
                Description = entity.GetLocalized(x => x.Description),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription),
                MetaTitle = string.Concat("Discount ", (category == null
                    ? entity.GetLocalized(x => x.MetaTitle)
                    : $"{entity.GetLocalized(x => x.MetaTitle)} {category.GetLocalized(x => x.MetaTitle)}")),
                SeName = entity.GetSeName(),
            };

            return model;
        }

        //public static IList<VehicleSeoModel> ToModel(this IList<KeyValuePair<Make, Model>> entities, string entityName, string entityTitle = null)
        //{
        //    if (entities == null)
        //    {
        //        return null;
        //    }

        //    return entities.Select(e => new VehicleSeoModel
        //    {
        //        EntityName = entityName,
        //        EntityTitle = string.IsNullOrEmpty(entityTitle) ? entityName : entityTitle,
        //        MakeId = e.Key.Id,
        //        MakeName = e.Key.Name,
        //        ModelId = e.Value.Id,
        //        ModelName = e.Value.Name
        //    }).ToList();
        //}

        public static IList<VehicleSeoModel> ToModel(this IList<MakeModelEntity> entities, string entityName, string entityTitle = null, int stub = 1)
        {
            if (entities == null)
            {
                return null;
            }

            return entities.Select(e => new VehicleSeoModel
            {
                EntityName = entityName,
                EntityTitle = string.IsNullOrEmpty(entityTitle) ? entityName : entityTitle,
                MakeId = e.MakeId,
                MakeName = e.MakeName,
                ModelId = e.ModelId,
                ModelName = e.MakeName
            }).ToList();
        }

        //public static IList<VehicleSeoModel> ToModel(this IList<Tuple<Make, Model, Year>> entities, string entityName, string entityTitle = null)
        //{
        //    if (entities == null)
        //    {
        //        return null;
        //    }

        //    return entities.Select(e => new VehicleSeoModel
        //    {
        //        EntityName = entityName,
        //        EntityTitle = string.IsNullOrEmpty(entityTitle) ? entityName : entityTitle,
        //        MakeId = e.Item1.Id,
        //        MakeName = e.Item1.Name,
        //        ModelId = e.Item2.Id,
        //        ModelName = e.Item2.Name,
        //        YearId = e.Item3.Id,
        //        YearName = e.Item3.Id.ToString(),
        //    }).ToList();
        //}

        public static IList<VehicleSeoModel> ToModel(this IList<Make> makes, string entityName, string entityTitle = null)
        {
            if (makes == null)
            {
                return null;
            }

            return makes.Select(m => new VehicleSeoModel
            {
                EntityName = entityName,
                EntityTitle = string.IsNullOrEmpty(entityTitle) ? entityName : entityTitle,
                MakeId = m.Id,
                MakeName = m.Name
            }).ToList();
        }

        public static VehicleSeoModel ToModel(this VehicleUrlRecord entity)
        {
            if (entity == null)
                return null;

            var vehicleService = EngineContext.Current.Resolve<IVehicleService>();
            return new VehicleSeoModel
            {
                Description = entity.Description,
                EntityName = entity.EntityName,
                EntityTitle = entity.EntityName == "Accessories" ? "Parts and Accessories" : entity.EntityName,
                MakeId = entity.MakeId,
                MakeName = vehicleService.GetMake(entity.MakeId).Name,
                ModelId = entity.ModelId,
                ModelName = entity.ModelId.HasValue ? vehicleService.GetModel(entity.ModelId.Value).Name : string.Empty,
                YearId = entity.YearId,
                YearName = entity.YearId?.ToString() ?? string.Empty,
                Id = entity.EntityId
            };
        }

        public static ReturnRequestOrderModel ToModel(this CrmSalesOrder order)
        {
            if (order == null)
                return null;

            var shippingAddress = order.ShippingAddress;
            var billingAddress = order.BillingAddress;
            return new ReturnRequestOrderModel
            {
                CrmOrderId = order.Id,
                OrderNumber = order.Number,
                Channel = order.CrmChannel.Name,
                OrderDate = order.CreatedOn,
                OrderTotal = order.GetOrderChargeAmount(SalesPaymentChargeType.Total),
                FirstName = shippingAddress.FirstName,
                LastName = shippingAddress.FirstName,
                Email = shippingAddress.Email,
                AddressLine1 = shippingAddress.Line1,
                AddressLine2 = shippingAddress.Line2,
                AddressLine3 = null,
                City = shippingAddress.City,
                State = shippingAddress.State?.Name,
                Zip = shippingAddress.Zip,
                Country = shippingAddress.Country.Name,
                ShippingAmount = order.GetOrderChargeAmount(SalesPaymentChargeType.Shipping),
                DiscountAmount = order.GetOrderChargeAmount(SalesPaymentChargeType.Discount),
                Phone = shippingAddress.Phone,
                BillingEmail = billingAddress.Email,
                ChannelId = order.ChannelId,
                TaxAmount = order.GetOrderChargeAmount(SalesPaymentChargeType.Tax),
                OrderSubTotal = order.GetOrderChargeAmount(SalesPaymentChargeType.Subtotal),
                CreditTotal = order.SalesCredits.SelectMany(cr => cr.Charges).Sum(ch => ch.Amount)
            };
        }

        public static OrderItemModel ToModel(this CrmSalesOrderLine crmOrderLine)
        {
            if (crmOrderLine == null)
            {
                return null;
            }

            return new OrderItemModel
            {
                OrderItemId = crmOrderLine.ThubOrderItemId,
                ProductId = crmOrderLine.ProductId.Value,
                ProductName = crmOrderLine.ProductId != 0 ? $"{(crmOrderLine.Product.ProductManufacturers.Any() ? crmOrderLine.Product.ProductManufacturers.FirstOrDefault()?.Manufacturer.Name : string.Empty)} {crmOrderLine.Product.ManufacturerPartNumber}" : crmOrderLine.Description,
                Price = crmOrderLine.UnitPrice,
                Quantity = crmOrderLine.Quantity,
                OrderLineId = crmOrderLine.Id
            };
        }

        public static ReturnRequestSummaryModel ToSummaryModel(this ReturnRequest entity, CrmSalesOrder order)
        {
            if (entity == null || order == null)
            {
                return null;
            }

            return new ReturnRequestSummaryModel
            {
                Id = entity.Id,
                CrmOrderId = entity.OrderId.ToString(),
                CreatedOn = entity.CreatedOn.ToLocalTime(),
                OrderItemsAmount = entity.Items.Sum(i => i.OrderLine.UnitPrice * i.Quantity),
                OrderShippingAmount = order.GetOrderChargeAmount(SalesPaymentChargeType.Shipping),
                OrderDiscountAmount = order.GetOrderChargeAmount(SalesPaymentChargeType.Discount),
                OrderTotalAmount = order.GetOrderChargeAmount(SalesPaymentChargeType.Total),
                ReturnRequestSummaryItems = entity.Items.Select(i => i.ToSummaryModel()).ToList()
            };
        }

        public static ReturnRequestSummaryItemModel ToSummaryModel(this ReturnRequestItem entity)
        {
            if (entity == null)
                return null;

            return new ReturnRequestSummaryItemModel
            {
                Id = entity.Id,
                OrderLineId = entity.LineId,
                OrderItemId = entity.OrderItemId,
                Quantity = entity.Quantity,
                OrderItem = entity.OrderLine.ToModel(),
                ReturnReason = entity.ReturnReason.ToModel(),
                
            };
        }

        public static ReturnReasonModel ToModel(this ReturnReason entity)
        {
            if (entity == null)
                return null;

            return new ReturnReasonModel
            {
                Name = entity.Name,
                FaultType = new FaultTypeModel
                {
                    Id = entity.FaultType.Id,
                    Name = entity.FaultType.Name
                }
            };
        }

        public static SelectListItem ToSelectListItem(this ReturnReason entity, bool isAdmin = false)
        {
            if (entity == null)
            {
                return null;
            }

            var initiationType = entity.InitiationType;
            var initiationTypeName = Enum.GetName(entity.InitiationType.GetType(), entity.InitiationType);
            return new SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString(CultureInfo.InvariantCulture),
                Group = isAdmin ? new SelectListGroup
                {
                    Name = initiationType == InitiationType.Company ? "Delivery issues and intercepts (shipped but not delivered items only)" : $"{initiationTypeName} initiations" 
                } : null
            };
        }

        public static void PrepareCountriesStates(this Models.SimpleCheckout.AddressModel model,
            AddressSettings addressSettings,
            ILocalizationService localizationService = null,
            IStateProvinceService stateProvinceService = null,
            Func<IList<Country>> loadCountries = null)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (addressSettings == null)
            {
                throw new ArgumentNullException(nameof(addressSettings));
            }

            if (addressSettings.CountryEnabled && loadCountries != null)
            {
                if (localizationService == null)
                {
                    throw new ArgumentNullException(nameof(localizationService));
                }

                foreach (var c in loadCountries())
                {
                    model.AvailableCountries.Add(new CustomSelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId,
                        HtmlAttributes = new { data_code = c.TwoLetterIsoCode }
                    });
                }

                if (!model.AvailableCountries.Any(m => m.Selected))
                {
                    model.AvailableCountries.Single(m => m.Value == defaultCountryId.ToString()).Selected = true;
                    model.CountryId = defaultCountryId;
                }

                if (addressSettings.StateProvinceEnabled)
                {
                    if (stateProvinceService == null)
                    {
                        throw new ArgumentNullException(nameof(stateProvinceService));
                    }

                    var states = stateProvinceService
                        .GetStateProvincesByCountryId(model.CountryId ?? 0)
                        .ToList();

                    if (states.Count > 0)
                    {
                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new CustomSelectListItem
                            {
                                Text = s.GetLocalized(x => x.Name),
                                Value = s.Id.ToString(),
                                Selected = s.Id == model.StateProvinceId,
                                HtmlAttributes = new { data_code = s.Abbreviation }
                            });
                        }
                    }
                    else
                    {
                        var anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);
                        model.AvailableStates.Add(new CustomSelectListItem
                        {
                            Text = localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }
                }
            }
        }

        #endregion
    }
}