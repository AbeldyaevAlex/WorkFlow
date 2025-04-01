namespace Asu.Web.Models.Customization
{
    using System.ComponentModel;

    using FluentValidation.Attributes;

    using Asu.Framework.Mvc;
    using Asu.Web.Validators.Customization;

    [Validator(typeof(CheckOrderModelValidator))]
    public class CheckOrderModel : BaseNopModel
    {
        [DisplayName(@"Order number")]
        public string OrderNumber { get; set; }

        [DisplayName(@"ZIP code")]
        public string ZipCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}