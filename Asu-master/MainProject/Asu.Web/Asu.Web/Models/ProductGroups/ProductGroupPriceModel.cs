namespace Asu.Web.Models.ProductGroups
{
    using Asu.Framework.Mvc;

    public class ProductGroupPriceModel : BaseNopModel
    {
        public decimal MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }
    }
}