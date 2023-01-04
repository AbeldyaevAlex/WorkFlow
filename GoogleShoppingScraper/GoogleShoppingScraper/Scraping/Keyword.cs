namespace GoogleShoppingScraper.Scraping
{
    using System.Collections.Generic;
    using System.Linq;

    public class Keyword
    {
        public Keyword()
        {
            this.Results = new List<SearchResult>();
        }

        public string SearchTerm { get; set; }

        public int ProductId { get; set; }

        public List<SearchResult> Results { get; set; }

        public bool Completed { get; set; }

        public bool IsFailed { get; set; }

        public bool Processing { get; set; }

        public bool NoMatchingResults { get; set; }

        public bool ServerError { get; set; }

        public bool Matched => this.Results.Any(r => r.MatchingGroup.HasValue && r.MatchingGroup.Value) || this.Results.Where(r => r.IsGroup).Count() >= 5; 
    }
}