namespace GoogleShoppingScraper.Errors
{
    public class NoResultsException : CustomException
    {
        public NoResultsException(string message, bool saveResults = false)
            : base(message)
        {
            this.ErrorType = ErrorType.NoResults;
            this.SaveToFile = saveResults;
        }

        public NoResultsException(string pageText, string message, bool saveResults = false)
            : base(pageText, message)
        {
            this.ErrorType = ErrorType.NoResults;
            this.SaveToFile = saveResults;
        }
    }
}