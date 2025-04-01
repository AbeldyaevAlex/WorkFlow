using System.Collections.Generic;
using Asu.Framework.Mvc;
using Asu.Web.Models.Vehicles;

namespace Asu.Web.Models.Catalog
{
    public partial class CompareProductsModel : BaseNopEntityModel
    {
        public CompareProductsModel()
        {
            Products = new List<CustomProductOverviewModel>();
        }
        public IList<CustomProductOverviewModel> Products { get; set; }

        public bool IncludeShortDescriptionInCompareProducts { get; set; }
        public bool IncludeFullDescriptionInCompareProducts { get; set; }
    }
}