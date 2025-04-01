using Asu.Framework.Mvc;

namespace Asu.Web.Models.Common
{
    public partial class AdminHeaderLinksModel : BaseNopModel
    {
        public string ImpersonatedCustomerEmailUsername { get; set; }
        public bool IsCustomerImpersonated { get; set; }
        public bool DisplayAdminLink { get; set; }

        public bool DisplayServerDetails { get; set; }
    }
}