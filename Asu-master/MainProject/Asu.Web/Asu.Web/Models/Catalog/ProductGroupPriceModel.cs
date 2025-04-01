namespace Asu.Web.Models.Catalog
{
    using Asu.Framework.Mvc;

    public class ProductGroupPriceModel : BaseNopModel
    {
        public decimal MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }
    }
}