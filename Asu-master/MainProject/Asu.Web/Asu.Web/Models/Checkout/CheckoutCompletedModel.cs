using Asu.Framework.Mvc;

namespace Asu.Web.Models.Checkout
{
    public partial class CheckoutCompletedModel : BaseNopModel
    {
        public int OrderId { get; set; }
        public bool OnePageCheckoutEnabled { get; set; }
        public bool IsClubMember { get; set; }
    }
}