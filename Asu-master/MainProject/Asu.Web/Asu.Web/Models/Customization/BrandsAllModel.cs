using Asu.Framework.Mvc;
using Asu.Web.Models.Catalog;
using System.Collections.Generic;

namespace Asu.Web.Models.Customization
{
    public partial class BrandsAllModel : BaseNopModel
    {
        public BrandsAllModel()
        {
            this.Manufacturers = new List<CustomManufacturerModel>();
        }
        public List<string> Alfabet { get; set; }
        public List<CustomManufacturerModel> Manufacturers { get; set; }
        public bool IsSingleSymbolPage { get; set; }
        public string CurrentSymbol { get; set; }
    }
}