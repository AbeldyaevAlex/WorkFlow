namespace GoogleShoppingScraper.Errors
{
    public class InvalidScrapeValueException : CustomException
    {
        public InvalidScrapeValueException(string message)
            : base(message)
        {
            this.ErrorType = ErrorType.InvalidValue;
        }

        public InvalidScrapeValueException(string pageText, string message)
            : base(pageText, message)
        {
            this.ErrorType = ErrorType.InvalidValue;
        }
    }
}
