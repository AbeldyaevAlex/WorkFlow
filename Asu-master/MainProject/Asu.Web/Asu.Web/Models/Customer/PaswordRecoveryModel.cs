using System.Web.Mvc;
using FluentValidation.Attributes;
using Asu.Framework;
using Asu.Framework.Mvc;
using Asu.Web.Validators.Customer;

namespace Asu.Web.Models.Customer
{
    [Validator(typeof(PasswordRecoveryValidator))]
    public partial class PasswordRecoveryModel : BaseNopModel
    {
        [AllowHtml]
        [NopResourceDisplayName("Account.PasswordRecovery.Email")]
        public string Email { get; set; }
        public string Result { get; set; }
        public bool IsEmailSent { get; set; }
    }
}