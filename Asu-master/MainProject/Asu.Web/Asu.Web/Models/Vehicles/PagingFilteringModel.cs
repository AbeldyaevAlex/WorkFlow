using System.Collections.Generic;
using System.Web.Mvc;
using Asu.Framework.UI.Paging;

namespace Asu.Web.Models.Vehicles
{
    public class PagingFilteringModel : BasePageableModel
    {
        public PagingFilteringModel()
        {
            this.AvailableSortOptions = new List<SelectListItem>();
            this.Sort = 0;
        }

        public bool AllowProductSorting { get; set; }
        public IList<SelectListItem> AvailableSortOptions { get; set; }
        public Core.Domain.Vehicles.ProductSortingEnum Sort { get; set; }
    }
}