namespace GoogleShoppingScraper.Errors
{
    public class XPathCustomException : CustomException
    {
        public XPathCustomException(string nodeType)
            : base($"XPath pattern is incorrect. {nodeType}")
        {
            this.ErrorType = ErrorType.XPath;
        }

        public XPathCustomException(string pageText, string nodeType)
            : base(pageText, nodeType.StartsWith("XPath pattern is incorrect.") ? nodeType : $"XPath pattern is incorrect. {nodeType}")
        {
            this.ErrorType = ErrorType.XPath;
        }
    }
}
