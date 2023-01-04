namespace GoogleShoppingScraper.Scraping.ScrapingManager
{
    public class Settings
    {
        public Settings(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.MaxDegreeOfTasks = 1;
            this.KeywordsLoadingBufferSize = 1000;
            this.BufferLoadingBoundaryPercent = 30;
            this.KeywordsFlushingBufferSize = 100;
            this.HttpRetryAttempts = 5;
            this.SqlRetryAttempts = 3;
        }

        public int MaxDegreeOfTasks { get; set; }

        public int KeywordsLoadingBufferSize { get; set; }

        public int BufferLoadingBoundaryPercent { get; set; }

        public int KeywordsFlushingBufferSize { get; set; }

        public int HttpRetryAttempts { get; set; }

        public int SqlRetryAttempts { get; set; }

        public string ConnectionString { get; set; }
    }
}
