namespace GoogleShoppingScraper.Repository.UserDefinedTypes
{
    using System;

    public class SearchResult
    {
        public string SearchTerm { get; set; }

        public string SellerName { get; set; }

        public string Title { get; set; }

        public string SellerUrl { get; set; }

        public string ImageUrl { get; set; }

        public string GooglePartNumber { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        //public decimal? TaxShippingPrice { get; set; }
        public decimal? TaxPrice { get; set; }
        public decimal? ShippingPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal? GroupPrice { get; set; }

        public short? OnGroupPageRank { get; set; }

        public bool IsInGroup { get; set; }

        public bool? IsFreeShipping { get; set; }

        public bool? IsUsed { get; set; }

        public bool? IsRefurbished { get; set; }

        public bool? IsRefilled { get; set; }

        public bool? IsTaxFree { get; set; }

        public decimal? AvgRating { get; set; }

        public int? Ratings { get; set; }

        public string Details { get; set; }

        public int OnSearchPageRank { get; set; }

        public string GoogleBrand { get; set; }

        public string GoogleGtin { get; set; }

        public bool? MatchingGroup { get; set; }

        public string GroupId { get; set; }

        public DateTime ScrapedDate { get; set; }
    }
}
