namespace GoogleShoppingScraper.Misc
{
    using OpenQA.Selenium;
    using System;

    public static class Auxiliary
    {
        public static bool VerifyPage(string substring, string content)
        {
            return content.IndexOf(substring, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        public static bool VerifyPageSelenium(string substring, IWebDriver content)
        {
            return content.FindElement(By.TagName("html")).Text.IndexOf(substring, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

    }
}
