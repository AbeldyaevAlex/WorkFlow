namespace GoogleShoppingScraper.Scraping
{
    using System;
    using System.Web;

    using GoogleShoppingScraper.Errors;
    using GoogleShoppingScraper.Properties;

    using HtmlAgilityPack;

    public static class HtmlAgilityPackExtensions
    {
        public static string GetInnerText(this HtmlNode node)
        {
            return string.IsNullOrEmpty(node?.InnerText) ? string.Empty : HttpUtility.HtmlDecode(ExstractPlainTextFromHtml(node.InnerText));
        }

        public static HtmlDocument BuildHtmlDocument(this string pageContent)
        {
            var document = new HtmlDocument();
            if (string.IsNullOrEmpty(pageContent))
            {
                return document;
            }

            try
            {
                document.LoadHtml(pageContent);
            }
            catch
            {
                return document;
            }

            return document;
        }

        public static string ExstractPlainTextFromHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }

            html = HttpUtility.HtmlDecode(html).Trim();

            while (html.Contains("<!--"))
            {
                int start = html.IndexOf("<!--", StringComparison.Ordinal);
                if (start != -1)
                {
                    int end = html.IndexOf("-->", start, StringComparison.Ordinal);
                    if (end != -1)
                    {
                        end += 3;
                        html = html.Remove(start, end - start);
                    }
                }
            }

            html = html.Replace("\r", " ");
            html = html.Replace("\n", " ");
            html = html.Replace("\t", " ");

            while (html.Contains("  "))
            {
                html = html.Replace("  ", " ");
            }

            return html;
        }

        public static string GetParentAttrValue(this HtmlNode parentNode, string attrName)
        {
            return GetParentAttrValue(parentNode, ".", attrName);
        }

        public static string GetParentAttrValue(this HtmlNode parentNode, string xpathPattern, string attrName)
        {
            if (parentNode == null)
            {
                return string.Empty;
            }

            var node = parentNode.SelectSingleNode(xpathPattern);

            if (!HasAttribute(node, attrName))
            {
                return string.Empty;
            }


            var value = node.Attributes[attrName].Value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = HttpUtility.HtmlDecode(value);
            }


            return value;
        }

        public static bool HasAttribute(this HtmlNode node, string attrName)
        {
            if (node == null || string.IsNullOrEmpty(attrName))
            {
                return false;
            }

            return node.Attributes.Contains(attrName);
        }

        public static bool NextPage(this HtmlDocument document, PageType pageType, out string url)
        {
            url = null;
            var attributeName = "data-reload";
            var xPathPattern = pageType == PageType.Results ? "//span[text() = 'Next']" : "//*[@id='online-pagination']";
            var pagination = document?.DocumentNode?.SelectSingleNode(xPathPattern);
            if (pageType == PageType.Results)
            {
                return pagination?.SelectSingleNode(xPathPattern) != null; 
            }

            HtmlNode button;
            if (pagination == null)
            {
                button = document?.DocumentNode?.SelectSingleNode("//div[contains(@id, 'pagination-button-wrapper')]//button[@data-url]");
                attributeName = "data-url";
                if (button == null)
                {
                    button = document.DocumentNode.SelectSingleNode("//div[@data-base-update-url]");
                    if (button == null)
                    {
                        return false;
                    }

                    attributeName = "data-base-update-url";
                }
            }
            else
            {
                button = pagination.SelectSingleNode(".//a[text() = 'Next']");
                if (button == null)
                {
                    button = pagination.SelectSingleNode(".//div[@data-reload]");
                    if (button == null)
                    {
                        var totalPagesNode = pagination.SelectSingleNode("//div[@id = 'online-pagination']");
                        if (totalPagesNode == null)
                        {
                            throw new Exception("Page pagination");
                        }

                        var totalPages = totalPagesNode.SelectSingleNode(".//*[contains(text(), ' of ')]");
                        if (totalPages != null)
                        {
                            var totalText = totalPages.GetInnerText();
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

            url = button.GetAttributeValue(attributeName, null);
            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidOperationException("Group pagination url parsing");
            }

            var uri = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new Uri(url) : new Uri(string.Concat(Uri.UriSchemeHttps, "://", Settings.Default.GoogleHostAddress, url));
            if (string.IsNullOrEmpty(uri.Query))
            {
                return false;
            }

            url = uri.AbsoluteUri;

            return true;
        }
    }
}
