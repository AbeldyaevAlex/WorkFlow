using Asu.Framework.Mvc;

namespace Asu.Web.Models.Customization
{
    public partial class AmazonCheckoutCompletedModel : BaseNopModel
    {
        public int OrderId { get; set; }
        public string AmazonOrderReferenceId { get; set; }
        public string ErrorMessage { get; set; }
    }
}