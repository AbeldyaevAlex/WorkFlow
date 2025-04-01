using Asu.Framework.Mvc;

namespace Asu.Web.Models.Catalog
{
    public partial class SearchBoxModel : BaseNopModel
    {
        public bool AutoCompleteEnabled { get; set; }
        public bool ShowProductImagesInSearchAutoComplete { get; set; }
        public int SearchTermMinimumLength { get; set; }
    }
}