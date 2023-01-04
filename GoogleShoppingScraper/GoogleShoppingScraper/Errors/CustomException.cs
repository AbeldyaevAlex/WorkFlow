namespace GoogleShoppingScraper.Errors
{
    using System;

    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
            this.SaveToFile = true;
        }

        public CustomException(string pageText, string message) : base(message)
        {
            this.PageText = pageText;
            this.SaveToFile = !string.IsNullOrEmpty(pageText);
        }

        public string PageText { get; set; }

        public ErrorType ErrorType { get; protected set; }

        public bool SaveToFile { get; protected set; }
    }
}
