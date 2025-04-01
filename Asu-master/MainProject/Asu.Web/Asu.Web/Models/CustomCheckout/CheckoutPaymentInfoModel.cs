using System.Collections.Generic;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Checkout
{
    public partial class CheckoutPaymentInfoModel : BaseNopModel
    {
        public IList<string> Warnings { get; set; }
    }
}