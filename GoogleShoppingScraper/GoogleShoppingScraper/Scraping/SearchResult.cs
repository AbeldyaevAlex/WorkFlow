namespace GoogleShoppingScraper.Scraping
{
    using System.Collections.Generic;

    public class SearchResult
    {
        public SearchResult()
        {
            this.Sellers = new List<Seller>();
        }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int SearchPageRank { get; set; }

        public bool IsGroup { get; set; }

        public string GoogleBrand { get; set; }

        public string GoogleGtin { get; set; }

        public string GooglePartNumber { get; set; }

        public decimal? GroupPrice { get; set; }

        public bool? MatchingGroup { get; set; }

        public List<Seller> Sellers { get; }

        public string GroupId { get; set; }
    }
}