namespace Asu.Web.Models.Vehicles
{
    public class ProductAssociatedModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string MPN { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }

        public Catalog.ProductDetailsModel.AddToCartModel AddToCart { get; set; }
    }
}