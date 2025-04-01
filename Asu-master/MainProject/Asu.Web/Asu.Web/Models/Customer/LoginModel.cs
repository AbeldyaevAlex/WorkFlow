using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Asu.Framework;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Customer
{
    using Customization;

    public partial class LoginModel : BaseNopModel
    {
        public LoginModel()
        {
         this.CheckOrderModel = new CheckOrderModel();
        }

        public bool CheckoutAsGuest { get; set; }

        [NopResourceDisplayName("Account.Login.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }
        [NopResourceDisplayName("Account.Login.Fields.UserName")]
        [AllowHtml]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [NopResourceDisplayName("Account.Login.Fields.Password")]
        [AllowHtml]
        public string Password { get; set; }

        [NopResourceDisplayName("Account.Login.Fields.RememberMe")]
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }

        // WC.
        public CheckOrderModel CheckOrderModel { get; set; }
    }
}