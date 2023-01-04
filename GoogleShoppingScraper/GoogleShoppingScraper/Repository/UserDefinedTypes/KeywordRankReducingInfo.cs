namespace GoogleShoppingScraper.Repository.UserDefinedTypes
{
    public class KeywordRankReducingInfo
    {
        public int ProductId { get; set; }

        public string SearchTerm { get; set; }

        public bool NoMatchingResults { get; set; }
    }
}
