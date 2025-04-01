using Asu.Core.Domain.Tax;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Common
{
    public partial class TaxTypeSelectorModel : BaseNopModel
    {
        public TaxDisplayType CurrentTaxType { get; set; }
    }
}