//using Nop.Web.Framework;
using Asu.Framework.Mvc;
//using System.Web.Mvc;

namespace Asu.Web.Models.Catalog
{
    public partial class BackInStockSubscribeModel : BaseNopModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSeName { get; set; }

        public bool IsCurrentCustomerRegistered { get; set; }
        public bool SubscriptionAllowed { get; set; }
        public bool AlreadySubscribed { get; set; }

        public int MaximumBackInStockSubscriptions { get; set; }
        public int CurrentNumberOfBackInStockSubscriptions { get; set; }

        //[NopResourceDisplayName("Account.Fields.Email")]
        //[AllowHtml]
        //public string Email { get; set; }
    }
}