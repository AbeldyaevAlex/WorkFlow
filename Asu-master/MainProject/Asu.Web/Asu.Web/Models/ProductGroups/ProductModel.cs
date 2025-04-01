using System.Collections.Generic;
using System.Web.Mvc;

namespace Asu.Web.Models.ProductGroups
{
    public class ProductModel
    {
        public int Id { get; set; }

        public int SelectedQuantity { get; set; }

        public bool IsShippingFromManufacturer { get; set; }

        public int StockQuantity { get; set; }

        public bool IsFreeShipping { get; set; }

        public string Mpn { get; set; }

        public int UpdatedShoppingCartItemId { get; set; }

        public bool IsShipEnabled { get; set; }

        public int? AvgShipLeadTime { get; set; }
    }
}