namespace GoogleShoppingScraper.Errors
{
    using Scraping;

    public class StopScrapingException : CustomException
    {
        public StopScrapingException(string message, SearchResult searchResult)
            : base(message)
        {
            this.SearchResult = searchResult;
        }

        public StopScrapingException(string pageText, string message, SearchResult searchResult)
            : base(pageText, message)
        {
            this.SearchResult = searchResult;
        }

        public SearchResult SearchResult { get; }
    }
}
