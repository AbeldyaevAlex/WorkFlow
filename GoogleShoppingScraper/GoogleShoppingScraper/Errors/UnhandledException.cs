namespace GoogleShoppingScraper.Errors
{
    public class UnhandledException : CustomException
    {
        public UnhandledException(string message)
            : base(message)
        {
            this.ErrorType = ErrorType.Unhandled;
        }

        public UnhandledException(string pageText, string message)
            : base(pageText, message)
        {
            this.ErrorType = ErrorType.Unhandled;
        }
    }
}
