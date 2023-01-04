using GoogleShoppingScraper.Properties;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace GoogleShoppingScraper.Scraping
{
    public static class SeleniumMethods
    {
        //////public static IList<IWebElement> GetSearchResultNodes(IWebElement commonNode)
        //////{
        //////    return commonNode.FindElements(By.ClassName("sh-sr__shop-result-group"));
        //////}

        public static bool NextPageSelenium(this IWebDriver driver, PageType pageType, out string url)
        {
            url = null;
            var attributeName = "data-reload";
            var xPathPattern = pageType == PageType.Results ? "//span[text() = 'Next']" : "//*[@id='online-pagination']";
            //var pagination = document?.DocumentNode?.SelectSingleNode(xPathPattern);
            var pagination = driver?.FindElement(By.XPath(xPathPattern));  
            if (pageType == PageType.Results)
            {
                //return pagination?.SelectSingleNode(xPathPattern) != null;
                return pagination?.FindElement(By.XPath(xPathPattern)) != null;
            }

            IWebElement button;
            if (pagination == null)
            {
                //button = document?.DocumentNode?.SelectSingleNode("//div[contains(@id, 'pagination-button-wrapper')]//button[@data-url]");
                button = driver?.FindElement(By.XPath("//div[contains(@id, 'pagination-button-wrapper')]//button[@data-url]"));
                attributeName = "data-url";
                if (button == null)
                {
                    //button = document.DocumentNode.SelectSingleNode("//div[@data-base-update-url]");
                    button = driver?.FindElement(By.XPath("//div[contains('//div[@data-base-update-url]')]"));
                    if (button == null)
                    {
                        return false;
                    }

                    attributeName = "data-base-update-url";
                }
            }
            else
            {
                //button = pagination.SelectSingleNode(".//a[text() = 'Next']");
                button = pagination.FindElement(By.XPath(".//a[text() = 'Next']"));
                if (button == null)
                {
                    //button = pagination.SelectSingleNode(".//div[@data-reload]");
                    button = pagination.FindElement(By.XPath(".//div[@data-reload]"));
                    if (button == null)
                    {
                        //var totalPagesNode = pagination.SelectSingleNode("//div[@id = 'online-pagination']");
                        var totalPagesNode = pagination.FindElement(By.XPath("//div[@id = 'online-pagination']"));
                        if (totalPagesNode == null)
                        {
                            throw new Exception("Page pagination");
                        }

                        //var totalPages = totalPagesNode.SelectSingleNode(".//*[contains(text(), ' of ')]");
                        var totalPages = totalPagesNode.FindElement(By.XPath(".//*[contains(text(), ' of ')]"));
                        if (totalPages != null)
                        {
                            //var totalText = totalPages.GetInnerText();
                            var totalText = totalPages.Text;
                            var index = totalText.LastIndexOf(" ", StringComparison.OrdinalIgnoreCase);
                            if (index < 0)
                            {
                                throw new Exception("Page pagination");
                            }

                            var value = totalText.Substring(index + 1);
                            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out int pages))
                            {
                                throw new Exception("Page pagination");
                            }

                            if (pages > 25)
                            {
                                throw new Exception("Page pagination");
                            }

                            return false;
                        }
                    }
                }
            }

            //url = button.GetAttributeValue(attributeName, null);
            url = button.GetAttribute(attributeName);
            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidOperationException("Group pagination url parsing");
            }

            //var uri = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new Uri(url) : new Uri(string.Concat(Uri.UriSchemeHttps, "://", Settings.Default.GoogleHostAddress, url));
            var uri = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new Uri(url) : new Uri(string.Concat(Uri.UriSchemeHttps, "://",
                      Settings.Default.GoogleHostAddress, url));
            if (string.IsNullOrEmpty(uri.Query))
            {
                return false;
            }

            url = uri.AbsoluteUri;

            return true;
        }
    }
}
