namespace Asu.Web.Models.Catalog
{
    public class CoverkingPostback
    {
        public string SessionId { get; set; }

        public CoverkingItem[] Items { get; set; }

        public class CoverkingItem
        {
            public string ItemId { get; set; }

            public string Description { get; set; }

            public string Sku { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }

            public string ProductCode { get; set; }

            public string MaterialCategory { get; set; }

            public string UniqueSku { get; set; }
        }
    }
}