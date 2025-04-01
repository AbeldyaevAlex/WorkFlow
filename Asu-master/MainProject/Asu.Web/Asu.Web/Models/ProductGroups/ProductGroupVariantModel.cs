namespace Asu.Web.Models.ProductGroups
{
    using Asu.Framework.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ProductGroupVariantModel
    {
        public int GroupId { get; set; }

        public int SelectedVariantId { get; set; }

        public string RedirectUrl { get; set; }

        public IList<CustomSelectListItem> VariantDetails { get; set; }

        public string GroupTypeName { get; set; }

        public ProductGroupVehicleModel Vehicle { get; set; }
    }
}