namespace GoogleShoppingScraper.Scraping
{
    public class Seller
    {
        public string SellerName { get; set; }

        public string Url { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public bool? IsUsed { get; set; }

        public bool? IsRefurbished { get; set; }

        public bool? IsRefilled { get; set; }

        public int? Ratings { get; set; }

        //public decimal? TaxShippingPrice { get; set; }

        public decimal? TaxPrice { get; set; }

        public decimal? ShippingPrice { get; set; }

        public string Details { get; set; }

        public decimal? AverageRating { get; set; }

        public bool? IsFreeShipping { get; set; }

        public short? OnGroupPageRank { get; set; }

        public bool? IsTaxFree { get; set; }
    }
}