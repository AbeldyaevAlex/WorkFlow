namespace GoogleShoppingScraper.Scraping
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml.XPath;

    using Errors;

    using GoogleShoppingScraper.Misc;

    using HtmlAgilityPack;

    using Properties;

    using Proxy;

    using HttpRequest = Request.HttpRequest;
    using System.Collections.Generic;
    using GoogleShoppingScraper.Repository;
    using System.Text;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome.ChromeDriverExtensions;
    using System.Net;
    using System.Threading;
    using OpenQA.Selenium.Support.UI;

    public class WebCrawler
    {
        private readonly IProxyManager proxyManager;
        private readonly Keyword keyword;
        private readonly int httpRetryAttempts;
        private readonly string[] allowedLocations;
        private readonly string googleHostAddress = Settings.Default.GoogleHostAddress;
        private const string SellerRatingsRegexPattern = @"\([0-9]{1,3}\)|\([0-9]{1,3}\,[0-9]{1,3}\)";
        private const string SellerRatingsPercentRegexPattern = @"\d{1,3}%|\d{1,3}&#37;";
        private const string SellerNoRatingRegexPattern = "No rating";
        private const int PagesLimit = 5;
        private const int PagesLimitSellers = 1;    //2; 
        private IProxy currentProxy;
        private object driver;
        private static readonly Random Rand = new Random();
        public WebCrawler(IProxyManager proxyManager, Keyword keyword, int httpRetryAttempts)
        {
            this.proxyManager = proxyManager;
            this.keyword = keyword;
            this.httpRetryAttempts = httpRetryAttempts;
            this.allowedLocations = File.ReadAllLines(Settings.Default.AllowedLocationsFilePath);
        }

        public void ScrapeData()
        {
            int searchPageNumber = 0, resultOrdinal = 0, itemsPerPage = Settings.Default.ItemsPerPage;
            var pageContent = string.Empty;
            var attempts = int.MaxValue;

            //ChromeOptions options = new ChromeOptions();
            //proxy = new Proxy();
            //proxy.Kind = ProxyKind.Manual;
            //proxy.IsAutoDetect = false;
            //proxy.HttpProxy =
            //proxy.SslProxy = "127.0.0.1:3330";
            //options.Proxy = proxy;
            //options.AddArgument("ignore-certificate-errors");
            //var chromedriver = new ChromeDriver(options);

            //var driver = new ChromeDriver(@"C:\Work\Git\MyProjects", options);

            try
            {
                //HtmlDocument document;
                do
                {
                    var startItemIndex = searchPageNumber * itemsPerPage;
                    var url = BuildTargetUrl(this.keyword.SearchTerm, startItemIndex);


                    //////     var proxy = this.proxyManager.GetProxy();


                    //////     var options = new ChromeOptions();

                    //////   Uri uri = new Uri(url);
                    //////      // Add your HTTP-Proxy
                    //////     options.AddHttpProxy(proxy.ToWebProxy().Address.Host,
                    //////      proxy.ToWebProxy().Address.Port,
                    //////        proxy.ToWebProxy().Credentials.GetCredential(uri, "Basic").UserName,
                    //////     proxy.ToWebProxy().Credentials.GetCredential(uri, "Basic").Password);

                    //var proxySelenium = new OpenQA.Selenium.Proxy();
                    //proxySelenium.Kind = ProxyKind.Manual;    // Manual proxy settings (for httpProxy)
                    //proxySelenium.SocksVersion = 5;
                    //proxySelenium.IsAutoDetect = false;
                    //Uri uri = new Uri(url);
                    //proxySelenium.SocksProxy = proxy.Ip;
                    //proxySelenium.SocksUserName = proxy.ToWebProxy().Credentials.GetCredential(uri, "Basic").UserName;
                    //proxySelenium.SocksPassword = proxy.ToWebProxy().Credentials.GetCredential(uri, "Basic").Password;

                    //options.Proxy = proxySelenium;
                    //////     options.AddArgument("ignore-certificate-errors");   // принятие сертификата веб-сайта при запуске браузера
                    //options.AddArgument("--headless");      // HIDE Chrome Browser
                    //options.addArguments("window-size=1800x900");
                    //////       options.AddArgument("--disable-blink-features");
                    //////        options.AddArgument("--disable-AutomationControl");

                    ////// IWebDriver driver = new ChromeDriver(@"C:\Project\tools\Scrapers\GoogleShoppingScraper_Selenium", options);
                    //////    driver.Manage().Cookies.DeleteAllCookies();   // почистить куки браузера
                    //////      driver.Manage().Window.Maximize();            // разворачивать окно браузера на весь экран
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); //webdriver будет пытаться выполнить команды над контентом в установленный срок (10 секунд)
                    IWebDriver driver = null;
                    try
                    {
                        //////driver.Navigate().GoToUrl(url);               // перейти по указанному адресу
                        //driver.Navigate().Refresh();               // обновить страницу
                        //////}
                        //////finally
                        //////{
                        //////    driver.Quit();
                        //////}
                        ////var document = new HtmlDocument();
                        ////document.LoadHtml(driver.PageSource);
                        //var commonNode = driver.FindElement(By.Id("rso"));

                        //if (commonNode == null)
                        //{
                        //    throw new NoResultsException(pageContent, "Could not find results");
                        //}

                        //var nodes = driver.FindElements(By.ClassName("sh-sr__shop-result-group"));
                        ////var nodes = driver.FindElements(By.ClassName("sh-dlr__thumbnail"));


                        //////////////    if (!this.DownloadPage(url, this.ValidateSearchPrice, ref attempts, out document))

                        ////////if (!this.DownloadPage(url, this.ValidateSearchPrice, ref attempts, out driver))
                        ////////{
                        ////////    try
                        ////////    {
                        ////////        //pageContent = document?.DocumentNode?.OuterHtml ?? string.Empty;
                        ////////        pageContent = driver.FindElement(By.TagName("html")).Text;
                        ////////    }
                        ////////    catch (NullReferenceException)
                        ////////    {
                        ////////        throw new NoResultsException(pageContent, "Could not find results");
                        ////////    }
                        ////////    var nodes = driver.FindElements(By.ClassName("sh-dlr__list-result"));

                        ////////    if (nodes == null || !nodes.Any())
                        ////////    {
                        ////////        pageContent = driver.FindElement(By.TagName("html")).Text;
                        ////////        throw new NoResultsException(pageContent, "No search results on search page", true);
                        ////////    }


                        //////    if (!this.DownloadPage(url, this.ValidateSearchPrice, ref attempts, out document))
                        
                        if (!this.DownloadPage(url, /*this.ValidateSearchPrice,*/ ref attempts, out driver))
                        {
                            try
                            {
                                //pageContent = document?.DocumentNode?.OuterHtml ?? string.Empty;
                                pageContent = driver.FindElement(By.TagName("html"))?.Text;
                            }
                            catch (NullReferenceException)
                            {
                                throw new NoResultsException(pageContent, "Could not find results");
                            }
                        }

                        var nodes = this.GetSearchResultNodes(driver);
                        if (nodes == null || !nodes.Any())
                        {
                            try
                            {
                                pageContent = driver.FindElement(By.TagName("html"))?.Text;
                            }
                            catch { }
                            throw new NoResultsException(pageContent, "No search results on search page", true);

                        }

                        foreach (IWebElement resultNode in nodes)
                            {
                                try
                                {
                                    //var searchResult = this.GetSearchResult(resultNode);
                                    var searchResult = this.GetSearchResult(resultNode, driver);
                                    searchResult.SearchPageRank = ++resultOrdinal;
                                    this.keyword.Results.Add(searchResult);
                                }
                                catch (StopScrapingException)
                                {
                                    resultOrdinal++;
                                }
                            }
                            searchPageNumber++;
                        }
                    ////////}
                    finally
                    {
                        //driver.Quit();
                    }
                //}
         

        }while (/*document.NextPage(PageType.Results, out string uri) &&*/ searchPageNumber < PagesLimit);

            }
            catch (InvalidScrapeValueException ex)
            {
                this.keyword.IsFailed = true;
                if (string.IsNullOrEmpty(ex.PageText))
                {
                    ex.PageText = pageContent;
                }

                throw;
            }
            catch (XPathCustomException ex)
            {
                this.keyword.IsFailed = true;
                if (string.IsNullOrEmpty(ex.PageText))
                {
                    ex.PageText = pageContent;
                }

                throw;
            }
            catch (NoResultsException ex)
            {
                this.keyword.IsFailed = true;
                if (string.IsNullOrEmpty(ex.PageText))
                {
                    ex.PageText = pageContent;
                }

                throw;
            }
            catch (Exception ex)
            {
                this.keyword.IsFailed = true;
                throw new UnhandledException(pageContent, $"{ex.Message}");
            }

        }

        private static string BuildTargetUrl(string keyword, int startItemIndex)
        {
            keyword = string.IsNullOrWhiteSpace(keyword) ? string.Empty : keyword.Trim();

            return string.Format(Settings.Default.SearchTargetUrl, HttpUtility.UrlEncode(keyword), startItemIndex);
        }

        private static string BuildGroupTargetUrl(string url)
        {
            var start = url.IndexOf("?", StringComparison.Ordinal);
            if (start != -1)
            {
                url = url.Insert(start, "/online"); // to show all results inside group
            }

            if (!url.Contains("&os=sellers"))
            {
                url += "&os=sellers";
            }

            return url;
        }


        private /*HtmlNodeCollection*/ IReadOnlyCollection<IWebElement> GetSearchResultNodes(/*HtmlDocument document*/ IWebDriver driver)
        {
            var documentHtml = new HtmlDocument();
            documentHtml.LoadHtml(driver.PageSource);

            //HtmlNodeCollection resultNodes = null;
            IReadOnlyCollection<IWebElement> resultNodes = null;

            //var commonNode = document.DocumentNode.SelectSingleNode("//div[@id='ires']");
            //var commonNode = driver.FindElement(By.XPath("//div[@id='ires']"));
            //////var commonNode = driver.FindElement(By.XPath("//div[@id='rso']"));
            ////////var commonNode = documentHtml.DocumentNode.SelectNodes("//div[@id='rso']");
            ////////if (commonNode != null)
            ////////{
            //resultNodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'product-results')]/div");
            try
            {
                //resultNodes = driver.FindElements(By.ClassName("sh-pr__product-results"));
                //resultNodes = driver.FindElements(By.XPath("//div[contains(@class,'sh-pr__product-results')]/div"));
                resultNodes = driver.FindElements(By.XPath("//div[contains(@class,'sh-dlr__list-result')]"));
                //resultNodes = driver.FindElements(By.CssSelector(".sh-pr__product-results"));
            }
            catch{ }
            if (resultNodes == null || resultNodes.Count==0)
            {
                try
            {
                    //resultNodes = documentHtml.DocumentNode.SelectNodes("//*[contains(@id, 'sellers-cont')]/tr");
                    resultNodes = driver.FindElements(By.XPath("//*[contains(@id, 'sellers-cont')]/tr"));
            }
            catch{ }
                if (resultNodes == null || resultNodes.Count == 0)
                {
                    try
                    {
                        //resultNodes = documentHtml.DocumentNode.SelectNodes("//div[contains(@class, 'list-result')]");
                        resultNodes = driver.FindElements(By.XPath("//div[contains(@class, 'list-result')]"));
                    }
                    catch { }
                    if (resultNodes == null || resultNodes.Count == 0)
                    {
                        try
                        {
                            resultNodes = driver.FindElements(By.CssSelector(".sh-dlr__list-result"));
                        }
                        catch { }
                        if (resultNodes == null || resultNodes.Count == 0)
                        {
                            try
                            {
                                //resultNodes = documentHtml.DocumentNode.SelectNodes(".//*[@class='pslires']");
                                resultNodes = driver.FindElements(By.XPath("//*[@class='pslires']"));
                            }
                            catch { }
                            if (resultNodes == null || resultNodes.Count == 0)
                            {
                                try
                                {
                                    //resultNodes = commonNode.SelectNodes(".//div[string-length(@data-docid) > 0 and not(@data-cid)]");
                                    resultNodes = driver.FindElements(By.XPath("//*[string-length(@data-docid) > 0 and not(@data-cid)]"));
                                }
                                catch { }
                            }
                        }
                    }
                }
                }
            ////////}
            return resultNodes;
        }

        private static Seller ExtractSeller(string url, /*HtmlNode*/ IWebElement resultNode)
        {
            var seller = new Seller
            {
                // Url = url,
                OnGroupPageRank = null,
                SellerName = GetSellerName(resultNode /*, "./div[2]/div[2]"*/)
            };
            ////var documentHtml = new HtmlDocument();
            ////documentHtml.LoadHtml(driver.PageSource);
            string priceTotal = null;
            try
            {
                priceTotal = resultNode.FindElement(By.XPath("//td[4]")).Text;
            }
            catch { }
            if (string.IsNullOrEmpty(priceTotal) || !priceTotal.Contains("$"))
            {
                try
                {
                    priceTotal = resultNode.Text;
                }
                catch { }
            }
            var priceText = GetSearchSellerPriceText(priceTotal);
            seller.Price = ParsePrice(priceText);
            seller.TotalPrice = seller.Price;
            CheckProductDetails(/*priceText*/ priceTotal, seller);

            var detailsStart = priceText.IndexOf(" ", 0, StringComparison.Ordinal);
            if (detailsStart > -1)
            {
                var details = priceText.Substring(detailsStart, priceText.Length - detailsStart).Trim();
                if (!string.IsNullOrEmpty(details))
                {
                    seller.Details = details;
                }
            }

            //////GetRatings(resultNode, seller);

            return seller;
        }

        //private static string GetSearchSellerPriceText(HtmlNode resultNode)
        //{
        //    var priceNode = resultNode.SelectSingleNode("./div[2]/div[1]/b");
        //    if (priceNode == null)
        //    {
        //        priceNode = resultNode.SelectSingleNode("./div[1]/div[2]/div[1]/div[2]/div[1]/.//span/span");
        //        if (priceNode == null)
        //        {
        //            priceNode = resultNode.SelectSingleNode("./div[1]/div[2]/div[1]/div[2]/.//span[contains(text(), '$')]");
        //            if (priceNode == null)
        //            {
        //                throw new XPathCustomException("Price");
        //            }
        //        }
        //    }

        //    return priceNode.GetInnerText().Trim();
        //}

        private static string GetSearchSellerPriceText(/*HtmlNode*/ string result)
        {
            //var priceNode = resultNode.GetInnerText();
            //////IList<IWebElement> elements = resultNode.FindElements(By.ClassName("puehic"));
            ////////*[@id="rso"]/div[2]/div[1]/div[3]/div/div[2]/div/div/div/div/div/div[1]/div[2]/div[2]/div[2]/div[1]/a/div/div[2]/div[2]/span/span[1]/span[1]
            //////if (elements.ToList().Count()>1)
            //////{
            //////    elements = resultNode.FindElements(By.XPath("//*[@*='pspo-fade']//*[@*='price']//span/span[1]/span[1]"));
            //////}
            //////var priceText = elements[0].Text;
            //*[@id="rso"]/div[2]/div[1]/div[3]/div/div[2]/div/div/div/div/div/div[1]/div[2]/div[2]/div[2]/div[1]/a/div/div[2]/div[2]/span/span[1]/span[1]
            //var priceResult = Regex.Match(priceNode, @"\$\d{1,6}\.\d{2}?");
            ////var priceResult = Regex.Match(priceNode, @"\$(\d{1,3}\,)?\d{1,6}\.\d{2}?");
            var priceResult = Regex.Match(result, @"\$(\d{1,3}\,)?\d{1,6}\.\d{2}?");
            if (!priceResult.Success)
            {
                throw new XPathCustomException("Price not found");
            }

            result = priceResult.Value;

            return result;
        }

        private static string GetImageUrl(/*HtmlDocument*/ IWebDriver driver /*document*/, string xPathPattern)
        {
            IWebElement imageUrlNode = null;
            try
            {
                //var imageUrlNode = document.DocumentNode.SelectSingleNode(xPathPattern);
                imageUrlNode = driver.FindElement(By.XPath(xPathPattern));
            }
            catch { }
            if (imageUrlNode == null)
            {
                throw new XPathCustomException("Image url");
            }

            //var imageUrl = imageUrlNode.GetParentAttrValue("src");
            var imageUrl = imageUrlNode.GetAttribute("src");
            if (imageUrl.StartsWith("//"))
            {
                imageUrl = HttpUtility.UrlDecode(string.Concat(Uri.UriSchemeHttp, ":", imageUrl));
            }

            return imageUrl;
        }

        private static string GetSpecificationValue(HtmlNodeCollection specificationsNodes, string name, string additionalPattern)
        {
            var nodeType = specificationsNodes.All(i => i.Name == "div") ? "span" : "td";

            var pattern = $"./{nodeType}[contains(text(), '{name}')]/following-sibling::{nodeType}";
            var node = specificationsNodes.Select(i => i.SelectSingleNode(pattern)).FirstOrDefault(i => i != null);
            if (node == null)
            {
                node = specificationsNodes.FirstOrDefault()?.SelectSingleNode(additionalPattern);
            }

            return node?.GetInnerText();
        }

        private static Seller GetSeller(/*HtmlNode*/ IWebElement sellerNode)
        {
            var seller = new Seller();
            {
                // Url = GetSellerUrl(sellerNode, ".//span[@class='seller-name' or @class='os-seller-name-primary']/a"),
            };

            var sellerName = GetSellerName(sellerNode/*, ".//span[@class='seller-name' or @class='os-seller-name-primary']/a"*/);
            if (string.IsNullOrWhiteSpace(sellerName))
            {
                return null;
            }

            seller.SellerName = sellerName;
            seller.TotalPrice = GetTotalPrice(sellerNode, ".//td[@class='total-col' or @class='os-total-col']");
            //seller.TaxShippingPrice = GetTaxShippingPrice(sellerNode, ".//*[@class='os-total-description']");
            seller.ShippingPrice = GetShippingPrice(sellerNode, ".//*[not(name()='script') and @class='os-total-description']");
            seller.TaxPrice = GetTaxPrice(sellerNode, ".//*[not(name()='script') and  @class='os-total-description']");
            seller.Details = GetDetails(sellerNode, ".//td[@class='os-details-col']");
            seller.Price = GetGroupSellerPrice(sellerNode, ".//span[@class='base-price' or @class='os-base_price']");

            //////GetRatings(sellerNode, seller);
            CheckProductDetails(seller.Details, seller);
            if (seller.TotalPrice == 0)
            {
                seller.TotalPrice = seller.Price + seller.ShippingPrice + seller.TaxPrice ?? 0;
            }

            return seller;
        }

        private static string GetSellerName(/*HtmlNode*/ IWebElement sellerNode/*, string xPathPattern*/)
        {
            if (sellerNode == null)
            {
                throw new ArgumentNullException(nameof(sellerNode));
            }
            IWebElement sellerNameNode = null;
            try
            {
                sellerNameNode = sellerNode.FindElement(By.XPath("./td/div/a"));
            }
            catch { }
            if (sellerNameNode == null)
            {
                try
                {
                    sellerNameNode = sellerNode.FindElement(By.XPath("//*[@class='sh-osd__offer-row']//td/div/a"));
                }
                catch { }
            }
            //var sellerNameNode = sellerNode.SelectNodes(".//*[contains(text(), 'Show all')]")?.FirstOrDefault();
            //if (sellerNameNode != null)
            //{
            //    return null;
            //}

            //sellerNameNode = sellerNode.SelectSingleNode(xPathPattern);
            //if (sellerNameNode == null)
            //{
            //    sellerNameNode = sellerNode.SelectSingleNode("./div[1]/div[2]/div[1]/div[2]/div[1]");
            //    if (sellerNameNode == null)
            //    {
            //        sellerNameNode = sellerNode.SelectSingleNode("./td[1]/div[1]/a/span");
            //        if (sellerNameNode == null)
            //        {
            //            sellerNameNode = sellerNode.SelectSingleNode("./td[1]/div[2]/a/span");

            if (sellerNameNode == null)
            {
                throw new XPathCustomException("Seller name");
            }
            //        }
            //    }
            //}

            var sellerName = sellerNameNode.Text;   //.GetInnerText();
            if (string.IsNullOrWhiteSpace(sellerName))
            {
                throw new InvalidScrapeValueException("Cannot parse seller name value");
            }

            sellerName = new Regex(@"(\$\d+\,\d{0,10}\.\d{1,2})|(\$\d+\.\d{0,5})|(\$\d+\,\d{0,10})|(\$\d+\d{0,10})|((^\.)|(\d+\%.*$))|(\(\£\d+\.\d{0,5}\))|(\£\d+\d{0,5})|(\(\€\d+\.\d{0,5}\))|(\€\d+\d{0,5})")
                        .Replace(sellerName, string.Empty); // (\$\d+\.\d{1,2})|(\. )

            if (sellerName.IndexOf(".") == 0)
            {
                sellerName = new Regex(@"(^\.)").Replace(sellerName, string.Empty);
            }
            return sellerName.Replace("from ", string.Empty).Trim();
        }

        //private static string GetSellerUrl(HtmlNode sellerNode, string xPathPattern)
        //{
        //    var sellerUrlNode = sellerNode.SelectSingleNode(xPathPattern);
        //    if (sellerUrlNode == null)
        //    {
        //        throw new XPathCustomException("Seller url");
        //    }

        //    var sellerUrl = sellerUrlNode.GetParentAttrValue("href");

        //    return !string.IsNullOrEmpty(sellerUrl) && HttpUtility.UrlDecode(sellerUrl).StartsWith(Uri.UriSchemeHttp) ? HttpUtility.UrlDecode(sellerUrl) : string.Concat(Uri.UriSchemeHttp, Settings.Default.GoogleHostAddress, sellerUrl);
        //}

        private static int GetSellerRatings(string source, out decimal avgRating)
        {
            string value = null;
            var ratings = 0;
            var regex = new Regex(SellerNoRatingRegexPattern);
            avgRating = decimal.Zero;
            var match = regex.Match(source);
            if (match.Success)
            {
                return ratings;
            }

            regex = new Regex(SellerRatingsPercentRegexPattern);
            match = regex.Match(source);
            if (match.Success)
            {
                value = match.Value;
                if (match.Value.Contains("%"))
                {
                    if (!decimal.TryParse(value.Replace("positive", string.Empty).Trim('%', ' '), out avgRating))
                    {
                        throw new InvalidCastException($"Couldn't convert average rating {value} from {source}");
                    }

                    avgRating = (decimal)Math.Round((double)avgRating * 0.05, 1);
                }
            }

            regex = new Regex(SellerRatingsRegexPattern);
            match = regex.Match(source);
            if (match.Success)
            {
                value = match.Value;
                if (!int.TryParse(value.Replace(",", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Trim(), out ratings))
                {
                    throw new InvalidCastException($"Couldn't convert ratings {value} from {source}");
                }
            }

            return ratings;
        }

        private static void GetRatings(/*HtmlNode*/ IWebElement sellerNode, Seller seller)
        {
            if (sellerNode == null)
            {
                throw new ArgumentNullException(nameof(sellerNode));
            }

            decimal avgRating;
            string ratingsAmountText;
            /*HtmlNode*/
            IWebElement ratingsAmountNode = null;

            var sellerContent = sellerNode.Text;            //.GetInnerText();
            var match = new Regex(SellerRatingsRegexPattern).Match(sellerContent);
            if (match.Success)
            {
                seller.Ratings = GetSellerRatings(match.Value, out avgRating);
                return;
            }
            bool ratingsNode = false;
            try
            {
                //var ratingsNode = sellerNode.SelectSingleNode(".//td[@class='os-rating-col']");
                ////// var ratingsNode = sellerNode.FindElement(By.XPath(".//td[@class='os-rating-col']"));
                ratingsNode = sellerContent.Contains("os-rating-col");
            }
            catch { }
            if (!ratingsNode)
            {
                try
                {
                    //ratingsAmountNode = sellerNode.SelectSingleNode("./td[1]/div[1]/div[1]/a/span");
                    ratingsAmountNode = (IWebElement)sellerNode.FindElements(By.XPath("./td[1]/div[1]/div[1]/a/span"));
                }
                catch { }
                //if (ratingsNode != null)
                //{
                //ratingsAmountNode = ratingsNode.SelectSingleNode(".//*[contains(@class, 'shop__secondary')]");

                //}

                if (ratingsAmountNode == null)
                {
                    seller.Ratings = 0;
                    seller.AverageRating = 0;

                    return;
                }
            }

            ratingsAmountText = ratingsNode.ToString();  //.Text;     // .GetInnerText();
            if (string.IsNullOrWhiteSpace(ratingsAmountText))
            {
                ratingsAmountText = ratingsAmountNode.Text;    //.GetInnerText();
            }

            if (string.IsNullOrWhiteSpace(ratingsAmountText))
            {
                throw new InvalidScrapeValueException("No ratings amount text");
            }

            ratingsAmountText = ratingsAmountText.ToLower().Trim();

            seller.Ratings = GetSellerRatings(ratingsAmountText, out avgRating);
            if (seller.Ratings.HasValue || seller.AverageRating.HasValue)
            {
                return;
            }

            seller.Ratings = GetRatingsAmount(ratingsAmountText);
            //////////if (ratingsNode == null)
            //////////{
            //////////    //ratingsNode = sellerNode.SelectSingleNode("./td[1]/div[1]/div[1]/span[1]/span");
            //////////    ratingsNode = sellerNode.FindElement(By.XPath("./td[1]/div[1]/div[1]/span[1]/span"));
            //////////}

            //////////seller.AverageRating = GetAverageRating(ratingsNode, ratingsAmountText, seller.Ratings > 0);
        }

        private static int GetRatingsAmount(string ratingsAmountText)
        {
            if (ratingsAmountText.IndexOf("no rating", StringComparison.InvariantCulture) > -1)
            {
                return 0;
            }

            var occurences = new[] { "seller", "rating", "review", "positive", "s", ".", ",", "(", ")" };
            ratingsAmountText = occurences.Aggregate(ratingsAmountText, (current, occur) => current.Replace(occur, string.Empty)).Trim();
            if (string.IsNullOrEmpty(ratingsAmountText))
            {
                return 0;
            }

            int rating;
            if (!int.TryParse(ratingsAmountText, out rating))
            {
                throw new InvalidScrapeValueException("Invalid ratings amount text");
            }

            return rating;
        }

        private static decimal GetAverageRating(/*HtmlNode*/ IWebElement ratingsNode, string ratingsAmountText, bool hasRatingsAmount)
        {
            if (ratingsNode == null)
            {
                throw new NullReferenceException();
            }

            //var ratings = ratingsNode.GetInnerText();
            var ratings = ratingsNode.Text;
            double avgRating;
            IWebElement avgRatingNode = null;
            if (ratings.Contains("%"))
            {
                if (!double.TryParse(ratings.Trim('%', ' '), out avgRating))
                {
                    throw new InvalidCastException("Couldn't convert average rating");
                }

                return (decimal)Math.Round(avgRating * 0.05, 1);
            }
            try
            {
                //var avgRatingNode = ratingsNode.SelectSingleNode(".//span/div[@role='img' and @aria-label]");
                avgRatingNode = ratingsNode.FindElement(By.XPath(".//span/div[@role='img' and @aria-label]"));
            }
            catch { }
            if (avgRatingNode != null)
            {
                //return GetStarredAverageRating(avgRatingNode);
                return GetStarredAverageRating(avgRatingNode);
            }
            try
            {
                //avgRatingNode = ratingsNode.SelectSingleNode(".//span[contains(text(), ' positive')]");
                avgRatingNode = ratingsNode.FindElement(By.XPath(".//span[contains(text(), ' positive')]"));
            }
            catch { }
            if (avgRatingNode != null)
            {
                return GetPercentageAverageRating(avgRatingNode);
            }

            if (ratingsAmountText == "no rating" || ratingsAmountText == "seller rating" || hasRatingsAmount)
            {
                return 0;
            }

            throw new XPathCustomException("No average rating node");
        }

        private static decimal GetStarredAverageRating(/*HtmlNode*/ IWebElement avgRatingNode)
        {
            //var averageRatingText = avgRatingNode.GetAttributeValue("aria-label", string.Empty).Trim();
            var averageRatingText = avgRatingNode.GetAttribute("aria-label").Trim();
            decimal avgRating;
            if (!decimal.TryParse(averageRatingText, NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out avgRating))
            {
                var index = averageRatingText.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase);
                if (index < 1)
                {
                    throw new InvalidScrapeValueException("Invalid average rating text (star)");
                }

                averageRatingText = averageRatingText.Substring(0, index);
                if (!decimal.TryParse(averageRatingText, NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out avgRating))
                {
                    throw new InvalidScrapeValueException("Invalid average rating text (star)");
                }
            }

            return avgRating;
        }

        private static decimal GetPercentageAverageRating(/*HtmlNode*/ IWebElement avgRatingNode)
        {
            //var averageRatingText = avgRatingNode.GetInnerText().Trim();
            var averageRatingText = avgRatingNode.Text.Trim();
            averageRatingText = averageRatingText.Replace("\x2714", string.Empty).Replace("% positive", string.Empty).Trim(); // replace (✔) symbol

            decimal avgRating;
            if (!decimal.TryParse(averageRatingText, NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out avgRating))
            {
                throw new InvalidScrapeValueException("Invalid average rating text (percentage)");
            }

            return 5 * (avgRating / 100);
        }

        private static decimal ParsePrice(string price)
        {
            if (string.IsNullOrWhiteSpace(price))
            {
                throw new InvalidScrapeValueException("Price is missing");
            }

            if (!price.Contains("$") && !price.ToLower().Contains("us"))
            {
                throw new InvalidScrapeValueException("Price doesn't contain currency symbol ($)");
            }

            price = price.ToLower()
                    .Replace("$", string.Empty)
                    .Replace("refurbished", string.Empty)
                    .Replace("used", string.Empty)
                    .Replace("refilled", string.Empty)
                    .Replace("a", string.Empty)
                    .Replace("usd", string.Empty).Trim();

            var occurences = new[] { "+ tx", "D", "(", "gdp" };
            foreach (var occurence in occurences)
            {
                var boundIndex = price.IndexOf(occurence, StringComparison.InvariantCulture);
                if (boundIndex > -1)
                {
                    price = price.Substring(0, boundIndex - 1);
                    break;
                }
            }

            if (price.Contains("."))
            {
                price = price.Replace(",", string.Empty);
            }
            else if (price.Contains(","))
            {
                price = price.Replace(",", ".");
            }

            price = price.Replace("us", string.Empty);

            price = price.Replace("minimum order", string.Empty);

            decimal value;
            if (!decimal.TryParse(price.Trim(), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
            {
                throw new InvalidCastException($"Cannot converse price text {price} to decimal");
            }

            return value;
        }

        private static void CheckProductDetails(string details, Seller seller)
        {
            if (details == null)
            {
                return;
            }

            details = details.ToLower();
            seller.IsFreeShipping = details.Contains("free shipping") || (details.Contains("free delivery") && (!details.Contains("spend"))) ? (bool?)true : null;
            seller.IsTaxFree = details.Contains("no tax") ? (bool?)true : null;
            seller.IsRefurbished = details.Contains("refurbished") ? (bool?)true : null;
            seller.IsUsed = details.Contains("used") ? (bool?)true : null;
            seller.IsRefilled = details.Contains("refilled") ? (bool?)true : null;
        }

        private static decimal GetTotalPrice(/*HtmlNode*/ IWebElement sellerNode, string xPathPattern)
        {
            //var totalPriceNode = sellerNode.SelectSingleNode(xPathPattern);
            IWebElement totalPriceNode = null;
            try
            {
                totalPriceNode = sellerNode.FindElement(By.XPath(xPathPattern));
            }
            catch { }
            if (totalPriceNode == null)
            {
                try
                {
                    //totalPriceNode = sellerNode.SelectSingleNode("./td[4]//div[contains(@class, 'total-price')]");
                    totalPriceNode = sellerNode.FindElement(By.XPath("./td[4]"));
                }
                catch { }
                if (totalPriceNode == null)
                {
                    throw new XPathCustomException("Total price");
                }
            }

            //////var totalPrice = totalPriceNode.GetInnerText();
            var totalPrice = totalPriceNode.Text;
            var value = decimal.Zero;     //0.0m;
            if (string.IsNullOrEmpty(totalPrice))
            {
                return value;
            }

            totalPrice = totalPrice.ToUpper()
                        .Replace("US", string.Empty)
                        .Replace("$", string.Empty)
                        .Replace("A", string.Empty)
                        .Replace("USD", string.Empty)
                        .Replace("D", string.Empty)
                        .Replace("gdp", string.Empty)
                        .Trim();
            if (totalPrice.Contains("."))
            {
                totalPrice = totalPrice.Replace(",", string.Empty);
            }
            else if (totalPrice.Contains(","))
            {
                totalPrice = totalPrice.Replace(",", ".");
            }

            if (totalPrice.IndexOf("ca", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                totalPrice = totalPrice.ToLower().Replace("ca", string.Empty);
            }

            if (!decimal.TryParse(totalPrice.Trim(), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
            {
                throw new InvalidCastException($"Cannot converse price text {totalPrice} to decimal");
            }

            return value;
        }

        private static decimal GetGroupSellerPrice(/*HtmlNode*/ IWebElement sellerNode, string xPathPattern)
        {
            if (sellerNode == null)
            {
                throw new ArgumentNullException(nameof(sellerNode));
            }
            IWebElement priceNode = null;
            try
            {
                //var priceNode = sellerNode.SelectSingleNode(xPathPattern);
                priceNode = sellerNode.FindElement(By.XPath(xPathPattern));
            }
            catch { }
            if (priceNode == null)
            {
                try
                {
                    //priceNode = sellerNode.SelectSingleNode(".//td[@class='os-price-col']/span[1]");
                    priceNode = sellerNode.FindElement(By.XPath(".//td[@class='os-price-col']/span[1]"));
                }
                catch { }
                if (priceNode == null)
                {
                    try
                    {
                        //priceNode = sellerNode.SelectSingleNode("./td[3]");
                        priceNode = sellerNode.FindElement(By.XPath("./td[3]"));
                    }
                    catch { }
                    if (priceNode == null)
                    {
                        throw new XPathCustomException("Group Seller Price");
                    }
                }
            }

            //var price = priceNode.GetInnerText();
            var price = priceNode.Text;

            return ParsePrice(price);
        }

        private static decimal? GetGroupPrice(/*HtmlDocument*/ IWebElement document)
        {
            var price = GetGroupPriceText(document);
            if (string.IsNullOrEmpty(price))
            {
                throw new InvalidScrapeValueException("Price text is not correct");
            }

            return ParsePrice(price);
        }

        private static string GetGroupPriceText(/*HtmlDocument*/ IWebElement document)
        {
            IWebElement priceNode = null;
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            try
            {
                //priceNode = document.FindElement(By.XPath("//*[@id='sh-osd__online-sellers-cont']/tr[1]/td[4]/div/div[1]"));
                priceNode = document.FindElement(By.XPath("//*[@id='sh-osd__online-sellers-cont']/tr[1]/td[4]/div/div[1]"));
            }
            catch { }
            //var priceNode = document.DocumentNode.SelectSingleNode(".//div[@data-internal-precise]/following-sibling::div[2]//div[2]//span[1]");
            ////var priceNode = document.FirstOrDefault().FindElements(By.XPath(".//td[4]"));  
            ////if (priceNode == null)
            ////{
            ////    //priceNode = document.DocumentNode.SelectSingleNode("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]/td[3]");
            ////    priceNode = document.DocumentNode.SelectSingleNode("//div[@id='summary-prices']//span[@class='price']"); 

            if (priceNode == null)
            {
                throw new XPathCustomException("Group price");
            }
            ////}

            //var result = priceNode.GetInnerText();
            var result = priceNode.Text;
            result = Regex.Match(result, @"\$(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;

            return result;
        }

        ////private static decimal? GetTaxShippingPrice(/*HtmlNode*/ IWebElement sellerNode, string xPathPattern)
        ////{
        ////    string taxShippingPrice;
        ////    decimal value;
        ////    decimal? taxShippingPriceValue = 0.0m;

        ////    //var taxShippingPriceNode = sellerNode.SelectSingleNode(xPathPattern);
        ////    var taxShippingPriceNode = sellerNode.FindElements(By.XPath(xPathPattern));
        ////    if (taxShippingPriceNode == null)
        ////    {
        ////        //taxShippingPriceNode = sellerNode.SelectSingleNode("./td[4]//table/tr/td[text() = 'Tax']/following-sibling::td");
        ////        taxShippingPriceNode = sellerNode.FindElements(By.XPath("./td[4]//table/tr/td[text() = 'Tax']/following-sibling::td"));
        ////        if (taxShippingPriceNode != null)
        ////        {
        ////            //taxShippingPrice = taxShippingPriceNode.GetInnerText();
        ////            taxShippingPrice = taxShippingPriceNode.ToString();
        ////            if (!string.IsNullOrEmpty(taxShippingPrice))
        ////            {
        ////                if (!taxShippingPrice.Contains("See checkout") && !taxShippingPrice.Contains("See website"))
        ////                {
        ////                    if (!decimal.TryParse(taxShippingPrice.Trim(' ', '$'), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
        ////                    {
        ////                        throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/ sellerNode.Text, $"Cannot converse TaxShipping tax price text '{taxShippingPrice}' to decimal");
        ////                    }

        ////                    taxShippingPriceValue = value;
        ////                }
        ////            }
        ////        }

        ////        //taxShippingPriceNode = sellerNode.SelectSingleNode("./td[4]//table/tr/td[text() = 'Shipping' or text() = 'Delivery']/following-sibling::td");
        ////        taxShippingPriceNode = sellerNode.FindElements(By.XPath("./td[4]//table/tr/td[text() = 'Shipping' or text() = 'Delivery']/following-sibling::td"));
        ////        if (taxShippingPriceNode == null)
        ////        {
        ////            throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/ sellerNode.Text, "Tax/shipping shipping price");
        ////        }

        ////        //taxShippingPrice = taxShippingPriceNode.GetInnerText();
        ////        taxShippingPrice = taxShippingPriceNode.ToString();
        ////        if (!string.IsNullOrEmpty(taxShippingPrice) && !taxShippingPrice.Contains("See website"))
        ////        {
        ////            if (!decimal.TryParse(taxShippingPrice.Trim(' ', '$'), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
        ////            {
        ////                throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/ sellerNode.Text, $"Cannot converse TaxShipping shipping price text '{taxShippingPrice}' to decimal");
        ////            }

        ////            taxShippingPriceValue += value;
        ////        }

        ////        return taxShippingPriceValue;
        ////    }

        ////    //taxShippingPrice = taxShippingPriceNode.GetInnerText().Replace("US", string.Empty).ToLower(CultureInfo.InvariantCulture);
        ////    taxShippingPrice = taxShippingPriceNode.ToString().Replace("US", string.Empty).ToLower(CultureInfo.InvariantCulture);
        ////    if (string.IsNullOrEmpty(taxShippingPrice))
        ////    {
        ////        return null;
        ////    }

        ////    var occurences = new[] { "shipping", "tax", "free", "no", "and", "+", ":", "\r", "\n", "\t", ". ", "delivery" };
        ////    foreach (var substring in occurences)
        ////    {
        ////        taxShippingPrice = taxShippingPrice.Replace(substring, " ");
        ////    }

        ////    while (taxShippingPrice.Contains("  "))
        ////    {
        ////        taxShippingPrice = taxShippingPrice.Replace("  ", " ");
        ////    }

        ////    if (string.IsNullOrEmpty(taxShippingPrice))
        ////    {
        ////        throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/ sellerNode.Text, "Tax/Shipping price node was found, but the value cannot be parsed");
        ////    }

        ////    // +$0.94 tax, $9.99 shipping
        ////    if (taxShippingPrice.Count(i => i == '$') == 1)
        ////    {
        ////        int start = taxShippingPrice.IndexOf("$", StringComparison.Ordinal);
        ////        if (start != -1)
        ////        {
        ////            start++;
        ////            taxShippingPrice = taxShippingPrice.Substring(start, taxShippingPrice.Length - start);
        ////            if (taxShippingPrice.Contains("."))
        ////            {
        ////                taxShippingPrice = taxShippingPrice.Replace(",", string.Empty);
        ////            }
        ////            else if (taxShippingPrice.Contains(","))
        ////            {
        ////                taxShippingPrice = taxShippingPrice.Replace(",", ".");
        ////            }

        ////            if (!decimal.TryParse(taxShippingPrice.Trim(), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
        ////            {
        ////                throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/ sellerNode.Text, $"Cannot converse tax shipping price text '{taxShippingPrice}' to decimal");
        ////            }

        ////            taxShippingPriceValue = value;
        ////        }
        ////    }

        ////    // "+$0.94 tax and $9.99 shipping"
        ////    if (taxShippingPrice.Count(i => i == '$') == 2)
        ////    {
        ////        taxShippingPrice = taxShippingPrice.Replace("$", string.Empty);
        ////        while (taxShippingPrice.Contains("  "))
        ////        {
        ////            taxShippingPrice = taxShippingPrice.Replace("  ", " ");
        ////        }

        ////        var parts = taxShippingPrice.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        ////        if (parts.Length == 2)
        ////        {
        ////            if (!decimal.TryParse(parts.First().Trim(), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
        ////            {
        ////                throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/ sellerNode.Text, $"Cannot converse tax shipping price text '{parts.First()}' to decimal");
        ////            }

        ////            taxShippingPriceValue += value;

        ////            if (!decimal.TryParse(parts[1].Trim(), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
        ////            {
        ////                throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/ sellerNode.Text, $"Cannot converse tax shipping price text '{parts[1]}' to decimal");
        ////            }

        ////            taxShippingPriceValue += value;
        ////        }
        ////    }

        ////    return taxShippingPriceValue;
        ////}

        private static decimal? GetTaxPrice(/*HtmlNode*/ IWebElement sellerNode, string xPathPattern)
        {
            string taxPrice;
            decimal value;
            decimal? taxPriceValue = decimal.Zero;      //0.0m;
            IWebElement taxPriceNode =null;
            try
            {
                taxPriceNode = sellerNode.FindElement(By.XPath(xPathPattern));
            }
            catch { }
            if (taxPriceNode == null)
            {
                try
                {
                    //taxShippingPriceNode = sellerNode.SelectSingleNode("./td[4]//table/tr/td[text() = 'Tax']/following-sibling::td");
                    taxPriceNode = sellerNode.FindElement(By.XPath("./td[4]//table//tr[3]/td[2]"));
                }
                catch { }
                if (taxPriceNode != null)
                {
                    taxPrice = taxPriceNode?.Text;    /*GetInnerText()*/
                    if (taxPrice.Contains("USD") || taxPrice.Contains("€"))
                    {
                        taxPrice = "$" + Regex.Match(taxPrice, @"(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                    }
                    else
                    {
                        taxPrice = Regex.Match(taxPrice, @"\$(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                    }

                    if (!string.IsNullOrEmpty(taxPrice))
                    {
                        if (!taxPrice.Contains("See checkout") && !taxPrice.Contains("See website"))
                        {
                            if (!decimal.TryParse(taxPrice.Trim(' ', '$'), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
                            {
                                throw new InvalidScrapeValueException(sellerNode.GetAttribute("innerHtml")/*.OuterHtml*/, $"Cannot converse Tax price text '{taxPrice}' to decimal");
                            }

                            taxPriceValue = value;
                        }
                    }
                }

                return taxPriceValue;
            }

            taxPrice = taxPriceNode?.Text   /*GetInnerText()*/.Replace("US", string.Empty).ToLower(CultureInfo.InvariantCulture);
            if (string.IsNullOrEmpty(taxPrice))
            {
                return null;
            }

            //var occurences = new[] { "shipping", "tax", "free", "no", "and", "+", ":", "\r", "\n", "\t", ". ", "delivery" };
            var occurences = new[] { "tax", "no", "and", "+", ":", "\r", "\n", "\t", ". " };
            foreach (var substring in occurences)
            {
                taxPrice = taxPrice.Replace(substring, " ");
            }

            while (taxPrice.Contains("  "))
            {
                taxPrice = taxPrice.Replace("  ", " ");
            }

            if (string.IsNullOrEmpty(taxPrice))
            {
                throw new InvalidScrapeValueException(sellerNode.GetAttribute("innerHtml")/*.OuterHtml*/, "Tax price node was found, but the value cannot be parsed");
            }

            // +$0.94 tax, $9.99 shipping
            if (taxPrice.Count(i => i == '$') == 1)
            {
                int start = taxPrice.IndexOf("$", StringComparison.Ordinal);
                if (start != -1)
                {
                    start++;
                    taxPrice = taxPrice.Substring(start, taxPrice.Length - start);
                    if (taxPrice.Contains("."))
                    {
                        taxPrice = taxPrice.Replace(",", string.Empty);
                    }
                    else if (taxPrice.Contains(","))
                    {
                        taxPrice = taxPrice.Replace(",", ".");
                    }

                    if (taxPrice.Contains("USD") || taxPrice.Contains("€"))
                    {
                        taxPrice = "$" + Regex.Match(taxPrice, @"(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                    }
                    else
                    {
                        taxPrice = Regex.Match(taxPrice, @"\$(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                    }

                    if (!decimal.TryParse(taxPrice.Trim(), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
                    {
                        throw new InvalidScrapeValueException(sellerNode.GetAttribute("innerHtml")/*.OuterHtml*/, $"Cannot converse tax price text '{taxPrice}' to decimal");
                    }

                    taxPriceValue = value;
                }
            }

            return taxPriceValue;
        }

        private static decimal? GetShippingPrice(/*HtmlNode*/ IWebElement sellerNode, string xPathPattern)
        {
            string ShippingPrice;
            decimal value;
            decimal? ShippingPriceValue = decimal.Zero;    //0.0m;

            IWebElement ShippingPriceNode = null;
            try
            {
                //var ShippingPriceNode = sellerNode.SelectSingleNode(xPathPattern);
                ShippingPriceNode = sellerNode.FindElement(By.XPath(xPathPattern));
            }
            catch { }
            if (ShippingPriceNode == null)
            {
                try
                {
                    //ShippingPriceNode = sellerNode.FindElement(By.XPath("./td[4]//table/tr/td[not(name()='script') and (text() = 'Shipping' or text() = 'Delivery')]/following-sibling::td[not(name()='script') and (contains(text(),'$') or contains(text(),'USD')) and not(contains(text(),' OFF'))]"));
                    ShippingPriceNode = sellerNode.FindElement(By.XPath("./td[4]//table//*//td[text() = 'Shipping' or text() = 'Delivery']/following-sibling::td[(contains(text(),'$') or contains(text(),'USD')) and not(contains(text(),' OFF'))]"));
                }
                catch { }
                if (ShippingPriceNode == null)
                {
                    try
                    {
                        ShippingPriceNode = sellerNode.FindElement(By.XPath("./td[4]//table//*//td[(contains(text(),'$') or contains(text(),'USD')) and not(contains(text(),' OFF'))]"));
                    }
                    catch { }
                    if (ShippingPriceNode == null)
                    {
                        try
                        {
                            ShippingPriceNode = sellerNode.FindElement(By.XPath("./*//td[2]"));
                        }
                        catch { }
                        if (ShippingPriceNode == null)
                        {

                            throw new InvalidScrapeValueException(/*sellerNode.OuterHtml*/sellerNode.GetAttribute("innerHTML"), "Shipping price");
                        }
                    }
                }
                if (ShippingPriceNode?.Text   /*GetInnerText()*/ == "Item price")
                {
                    try
                    {
                        //ShippingPriceNode = sellerNode.SelectSingleNode("//div[not(name()='script') and (contains(text(),'spend') or" +
                        //    " contains(text(),'shipping') or contains(text(),'delivery')) and  contains(text(),'$')]");
                        ShippingPriceNode = sellerNode.FindElement(By.XPath("//div[not(name()='script') and (contains(text(),'spend') or" +
                            " contains(text(),'shipping') or contains(text(),'delivery')) and  contains(text(),'$')]"));
                    }
                    catch { }
                }
                if (ShippingPriceNode == null)
                {
                    ShippingPrice = "$0.00";
                }
                ShippingPrice = ShippingPriceNode?.Text/*GetInnerText()*/.Replace("Spend ", string.Empty).Replace(" for free delivery", string.Empty).Replace("delivery", string.Empty);

                if (ShippingPrice.Contains("USD") || ShippingPrice.Contains("€"))
                {
                    ShippingPrice = "$" + Regex.Match(ShippingPrice, @"(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                }
                else
                {
                    ShippingPrice = Regex.Match(ShippingPrice, @"\$(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                }

                if (!string.IsNullOrEmpty(ShippingPrice) && !ShippingPrice.Contains("See website"))
                {
                    if (!decimal.TryParse(ShippingPrice.Trim(' ', '$'), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
                    {
                        throw new InvalidScrapeValueException(sellerNode.GetAttribute("innerHtml")/*.OuterHtml*/, $"Cannot converse Shipping price text '{ShippingPrice}' to decimal");
                    }

                    ShippingPriceValue += value;
                }

                return ShippingPriceValue;
            }

            ShippingPrice = ShippingPriceNode?.Text/*GetInnerText()*/.Replace("US", string.Empty).ToLower(CultureInfo.InvariantCulture);
            if (string.IsNullOrEmpty(ShippingPrice))
            {
                return null;
            }

            var occurences = new[] { "shipping", "free", "no", "and", "+", ":", "\r", "\n", "\t", ". ", "delivery" };
            foreach (var substring in occurences)
            {
                ShippingPrice = ShippingPrice.Replace(substring, " ");
            }

            while (ShippingPrice.Contains("  "))
            {
                ShippingPrice = ShippingPrice.Replace("  ", " ");
            }

            if (string.IsNullOrEmpty(ShippingPrice))
            {
                throw new InvalidScrapeValueException(sellerNode.GetAttribute("innerHtml")/*.OuterHtml*/, "Shipping price node was found, but the value cannot be parsed");
            }

            // +$0.94 tax, $9.99 shipping
            if (ShippingPrice.Count(i => i == '$') == 1)
            {
                int start = ShippingPrice.IndexOf("$", StringComparison.Ordinal);
                if (start != -1)
                {
                    start++;
                    ShippingPrice = ShippingPrice.Substring(start, ShippingPrice.Length - start);
                    if (ShippingPrice.Contains("."))
                    {
                        ShippingPrice = ShippingPrice.Replace(",", string.Empty);
                    }
                    else if (ShippingPrice.Contains(","))
                    {
                        ShippingPrice = ShippingPrice.Replace(",", ".");
                    }

                    if (ShippingPrice.Contains("USD") || ShippingPrice.Contains("€"))
                    {
                        ShippingPrice = "$" + Regex.Match(ShippingPrice, @"(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                    }
                    else
                    {
                        ShippingPrice = Regex.Match(ShippingPrice, @"\$(\d{1,3}\,)?\d{1,6}\.\d{2}?").Value;
                    }


                    if (!decimal.TryParse(ShippingPrice.Trim(), NumberStyles.Float | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out value))
                    {
                        throw new InvalidScrapeValueException(sellerNode.GetAttribute("innerHtml")/*.OuterHtml*/, $"Cannot converse shipping price text '{ShippingPrice}' to decimal");
                    }

                    ShippingPriceValue = value;
                }
            }

            return ShippingPriceValue;
        }

        private static string GetDetails(/*HtmlNode*/ IWebElement sellerNode, string xPathPattern)
        {
            IWebElement detailsNode = null;
            try
            {
                //var detailsNode = sellerNode.SelectSingleNode(xPathPattern);
                detailsNode = sellerNode.FindElement(By.XPath(xPathPattern));
            }
            catch { }
            if (detailsNode == null)
            {
                try
                {
                    //detailsNode = sellerNode.SelectSingleNode("./td[2]/div[1]");
                    detailsNode = sellerNode.FindElement(By.XPath("./td[2]/div[1]"));
                }
                catch { }
                if (detailsNode == null)
                {
                    throw new XPathCustomException("Details node not found");
                }
            }

            //var details = detailsNode.GetInnerText()?.Replace("+", string.Empty).Replace("Â", string.Empty).Replace("·", string.Empty).Trim();
            var details = detailsNode.Text?.Replace("+", string.Empty).Replace("Â", string.Empty).Replace("·", string.Empty).Trim();

            if (!string.IsNullOrWhiteSpace(details))
            {
                if (details.Contains("Google"))
                {
                    try
                    {
                        //details = sellerNode.SelectSingleNode("./td[2]/div[1]/div[1]/span[1]/span[1]/a")
                        //.GetInnerText()?.Replace("+", string.Empty).Replace("Â", string.Empty).Replace("·", string.Empty).Trim();
                        details = sellerNode.FindElement(By.XPath("./td[2]/div[1]/div[1]/span[1]/span[1]/a"))
                            .Text?.Replace("+", string.Empty).Replace("Â", string.Empty).Replace("·", string.Empty).Trim();
                    }
                    catch { }
                }

                return details;
            }

            try
            {
                //detailsNode = sellerNode.SelectSingleNode("//*[@class='os-total-description']");
                detailsNode = sellerNode.FindElement(By.XPath("//*[@class='os-total-description']"));
            }
            catch { }
            if (detailsNode == null)
            {
                try
                {
                    //detailsNode = sellerNode.SelectSingleNode("//td[2]/div");
                    detailsNode = sellerNode.FindElement(By.XPath("//td[2]/div"));
                }
                catch { }
                if (detailsNode == null)
                {
                    throw new XPathCustomException("TaxShipping for details node not found");
                }
            }

            //details = detailsNode.GetInnerText()?.Replace("+", string.Empty).Replace("Â", string.Empty).Replace("·", string.Empty).Trim();
            details = detailsNode.Text?.Replace("+", string.Empty).Replace("Â", string.Empty).Replace("·", string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(details))
            {
                return details;
            }

            details = details.Replace("+", string.Empty).Replace("Â", string.Empty).Replace("·", string.Empty).Trim();
            if (details.Contains(" and "))
            {
                details = details.Replace(" and ", ",");
            }

            while (details.Contains("$"))
            {
                var i = details.IndexOf("$", StringComparison.Ordinal);
                details = details.Remove(i, details.IndexOf(" ", i, StringComparison.Ordinal) - i).Trim();
            }

            return details;
        }

        private static string ToUrlString(string url, string hostAddress)
        {
            url = Uri.IsWellFormedUriString(url, UriKind.Absolute)
                          ? new Uri(url).AbsoluteUri
                          : new Uri(string.Concat(Uri.UriSchemeHttps, "://", hostAddress, url)).AbsoluteUri;

            return url;
        }

        private SearchResult GetSearchResult(/*HtmlNode*/ IWebElement resultNode, IWebDriver driver)
        {
            var searchResult = new SearchResult();


            //IList<IWebElement> elements = resultNode.FindElements(By.ClassName("sh-dlr__thumbnail"));
            //IList<IWebElement> TagA = resultNode.FindElements(By.TagName("img"));
            //foreach (IWebElement element2 in TagA)
            //{
            //    var resultIMG = element2.GetAttribute("src");
            //    Console.WriteLine(resultIMG);
            //}



            //var resnodes = resultNode.FindElements(By.ClassName("sh-dlr__thumbnail"));
            //IWebElement elem = resnodes.FirstOrDefault();
            IList<IWebElement> elemIMG = null;
            try
            {
                elemIMG = resultNode.FindElements(By.TagName("img"));
            }
            catch{ }
            try
            {
                searchResult.ImageUrl = elemIMG.FirstOrDefault().GetAttribute("src");
            }
            catch{ }

            try
            {
                //var resnodes = resultNode.FindElements(By.XPath("//div[contains(@ClassName, 'sh-dlr__content')]"));
                var resnodes = resultNode.FindElements(By.ClassName("sh-dlr__thumbnail"));
            }
            catch{ }
            //var resnodes = resultNode.FindElements(By.ClassName("sh-sr__shop-result-group"));
            //IWebElement elem1 = resnodes.FirstOrDefault();
            string resultURL = null;
            try
            {
                IList<IWebElement> TagA2 = resultNode.FindElements(By.TagName("a"));
                IWebElement element = TagA2.FirstOrDefault();
                resultURL = element.GetAttribute("href");
            }
            catch{ }

            //var groupYes = resultNode.FindElements(By.XPath("./div[contains(text(), 'Compare prices from')]")).Count();
            //if (groupYes > 0)
            //{
            //    var groupRef = resultNode.FindElements(By.XPath("//div[contains(text(), 'Compare prices from')]//parent::a")).FirstOrDefault().GetAttribute("href");
            //}

            if (resultURL == null)
            {
                throw new XPathCustomException("Product result hyperlink");
            }

            //var linkNode = resultNode.SelectSingleNode(".//h3[@class='r']/a");
            //if (linkNode == null)
            //{
            //    linkNode = resultNode.SelectSingleNode(".//div[1]/div[2]/div[1]/div[1]/a");
            //    if (linkNode == null)
            //    {
            //        linkNode = resultNode.SelectSingleNode(".//div[1]/div[1]/a");
            //    }
            //        if (linkNode == null)
            //        {
            //            throw new XPathCustomException("Product result hyperlink");
            //        }
            //}

            //var resultUrl = linkNode.GetParentAttrValue("href");

            ////searchResult.Title = linkNode.GetInnerText();

            searchResult.Title = driver.Title.Replace(" - Google Shopping", "");

            //searchResult.ImageUrl = GetImageUrl(document, ".//div[@class='psliimg']/a/img");
            ////searchResult.ImageUrl = GetImageUrl(driver, ".//div[@class='psliimg']/a/img");

            if (!resultURL.Contains("/shopping/product/"))
            {
                searchResult.IsGroup = false;
                searchResult.GroupPrice = null;
                searchResult.Sellers.Add(ExtractSeller(resultURL, resultNode));
            }
            else
            {
                searchResult.IsGroup = true;
                searchResult.MatchingGroup = false;
                searchResult.GroupId = this.GetGroupId(resultURL);
                if (this.keyword.Matched)
                {
                    throw new StopScrapingException("Group matched", searchResult);
                }

                //this.ScrapeGroup(resultUrl, searchResult);
                this.ScrapeGroup1(searchResult, driver, resultURL);
            }

            //const int pricePrecision = 12;
            //var invalidSellers = searchResult.Sellers.Where(m => m.Price.ToString().Length > pricePrecision + 1).ToList();
            //foreach (var seller in invalidSellers)
            //{
            //    searchResult.Sellers.Remove(seller);
            //}

            return searchResult;
        }

        private static bool ValidateSellersPage(/*HtmlDocument document*/ IWebDriver driver)
        {
            //var sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'os-sellers-table')]//tr[contains(@class, 'os-row')]");
            var sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'os-sellers-table')]//tr[contains(@class, 'os-row')]"));
            if (sellerNodes == null)
            {
                //sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'os-sellers-table')]//tr");
                sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'os-sellers-table')]//tr"));
                if (sellerNodes == null)
                {
                    //sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]");
                    sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]"));
                }
            }

            if (sellerNodes != null && sellerNodes.Count > 0)
            {
                return true;
            }

            return false;
        }

        private /*HtmlNodeCollection*/ IReadOnlyCollection<IWebElement> GetSellerNodesBuyOnGoogle(/*HtmlDocument document*/ IWebDriver driver)
        {
            ////////string noMatchingStores = null;
            ////////try
            ////////{
            ////////    //var noMatchingStores = document.DocumentNode.SelectSingleNode("//div[contains(@id, 'online-sellers-grid-wrapper')]")?.GetInnerText();
            ////////    noMatchingStores = driver.FindElement(By.XPath("//div[contains(@class, 'sh-osd__pb-grid-wrapper')]")).Text;
            ////////}
            ////////catch { }
            ////////if (!string.IsNullOrEmpty(noMatchingStores) && noMatchingStores.Contains("No matching"))
            ////////{
            ////////    return null;
            ////////}
            IReadOnlyCollection<IWebElement> sellerNodes = null;
            try
            {
                //var sellerNodes = document.DocumentNode.SelectNodes("//section[@id='transactable']//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]");
                sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]"));
            }
            catch { }
            if (sellerNodes != null && sellerNodes.Count > 0)
            {
                return sellerNodes;
            }

            return null;
        }

        private /*HtmlNodeCollection */ IReadOnlyCollection<IWebElement> GetSellerNodes(HtmlDocument document, IWebDriver driver)
        {

            //////var noMatchingStores = document.DocumentNode.SelectSingleNode("//div[contains(@id, 'online-sellers-grid-wrapper')]")?.GetInnerText();
            //////////var noMatchingStores = document.FirstOrDefault().FindElements(By.XPath("//div[contains(@id, 'online-sellers-grid-wrapper')]")).Count();
            //////if (!string.IsNullOrEmpty(noMatchingStores) && noMatchingStores.Contains("No matching"))
            //////{
            //////    return null;
            //////}
            IReadOnlyCollection<IWebElement> sellerNodes = null;
            try
            {

                //var sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'os-sellers-table')]//tr[contains(@class, 'os-row')]");
                sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]"));
                //var sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]");
            }
            catch { }
                if (sellerNodes == null)
            {
                try
                {
                    //sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'os-sellers-table')]//tr");
                    sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'os-sellers-table')]//tr"));
                    //sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'os-sellers-table')]//tr");
                }
                catch { }
                if (sellerNodes == null)
                {
                    try
                    {
                        //sellerNodes = document.DocumentNode.SelectNodes("//section[@id='online']//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]");
                        sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'os-sellers-table')]//tr[contains(@class, 'os-row')]"));
                        //sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'os-sellers-table')]//tr[contains(@class, 'os-row')]");
                    }
                    catch { }
                    if (sellerNodes == null)
                    {
                        try
                        {
                            //sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]");
                            sellerNodes = driver.FindElements(By.XPath("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]"));
                            //sellerNodes = document.DocumentNode.SelectNodes("//table[contains(@id, 'online-sellers-grid')]//tr[contains(@class, 'offer-row')]");
                        }
                        catch { }
                    }
                }
            }

            if (sellerNodes != null && sellerNodes.Count > 0)
            {
                return sellerNodes;
            }
            IReadOnlyCollection<IWebElement> displayedNoMatchingStoresNode = null;
            try
            {
                //var displayedNoMatchingStoresNode = document.DocumentNode.SelectSingleNode("//div[@id='os-nosellers' and not(contains(@style, 'display:none'))]");
                displayedNoMatchingStoresNode = driver.FindElements(By.XPath("//div[@id='os-nosellers' and not(contains(@style, 'display:none'))]"));
                //var displayedNoMatchingStoresNode = document.DocumentNode.SelectSingleNode("//div[@id='os-nosellers' and not(contains(@style, 'display:none'))]");
            }
            catch { }
            if (displayedNoMatchingStoresNode != null)
            {
                return null;
            }
            IReadOnlyCollection<IWebElement> productNotFoundNode = null;
            try
            {
                //var productNotFoundNode = document.DocumentNode.SelectSingleNode("//div[@class='product-not-found']");
                productNotFoundNode = driver.FindElements(By.XPath("//div[@class='product-not-found']"));
                //var productNotFoundNode = document.DocumentNode.SelectSingleNode("//div[@class='product-not-found']");
            }
            catch { }
            if (productNotFoundNode != null)
            {
                return null;
            }
            try
            {
                //if (IsLetterWithDiacritics(document.DocumentNode.SelectSingleNode("//div[contains(@id, 'online-sellers-grid-wrapper')]")?.GetInnerText()))
                //if (IsLetterWithDiacritics(document.FirstOrDefault().FindElements(By.XPath("//div[contains(@id, 'online-sellers-grid-wrapper')]")).ToString()));
                if (IsLetterWithDiacritics(driver.FindElement(By.XPath("//div[contains(@id, 'online-sellers-grid-wrapper')]")).ToString()));
                {
                    var proxyIp = this.currentProxy.Ip;
                    File.AppendAllText("Proxies.txt", proxyIp + Environment.NewLine);
                }
            }
            catch { }
            //throw new NoResultsException(document.DocumentNode.OuterHtml, "Group sellers are not found", true);
            ////////var documHTML = new HtmlDocument();
            ////////HtmlDocument documentHTML = documHTML.LoadHtml(driver.PageSource);
            throw new NoResultsException(document.DocumentNode.OuterHtml, "Group sellers are not found", true);
        }

        private bool IsLetterWithDiacritics(string exampleString)
        {
            var normalizedString = exampleString.Normalize(NormalizationForm.FormD);

            foreach (var item in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(item);

                if (unicodeCategory == UnicodeCategory.NonSpacingMark)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetGroupId(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                url = $"https://www.google.com{url}";
            }

            var groupId = new Uri(url)
                .Segments
                .FirstOrDefault(m => Regex.IsMatch(m, @"^\d+\/$"))
                ?.Replace("/", string.Empty);

            if (string.IsNullOrEmpty(groupId))
            {
                groupId = Regex.Match(url, @"\d+(\/|\?)").Value?.Replace("?", string.Empty);
            }

            if (string.IsNullOrEmpty(groupId))
            {
                throw new Exception("Cannot parse Gooogle Id");
            }

            return groupId;
        }

        private bool GroupMatched(List<Seller> sellers, bool mpnMatched)
        {
            return mpnMatched && sellers.Any(s => s.SellerName.IndexOf("Autoplicity.com", StringComparison.InvariantCultureIgnoreCase) > -1);
        }

        private void ScrapeGroup(string url, SearchResult searchResult, IWebDriver driver)
        {
            bool mpnMatched = false;
            string pageContent = null;
            string nextPageUrl;
            //HtmlDocument document = new HtmlDocument();
            short onGroupPageRank = 0;
            var attempts = int.MaxValue;
            int sellersPageNumber = 0;
            try
            {
                do
                {
                    url = $"https://{this.googleHostAddress}/shopping/product/{searchResult.GroupId}/online?q={HttpUtility.UrlEncode(keyword.SearchTerm)}&prds=start:{onGroupPageRank}";

                    //////if (!this.DownloadPage(url, this.ValidateGroupPrice, ref attempts, out document))
                    //////{
                    //////    throw new CustomException("Group page loading failed");
                    //////}
                    //////////////////// !!!!!!!!!!!!!!!!!
                    //////    if (!this.DownloadPage(url, this.ValidateSearchPrice, ref attempts, out document))

                    if (!this.DownloadPage(url, /*this.ValidateSearchPrice,*/ ref attempts, out driver))
                    {
                        try
                        {
                            //pageContent = document?.DocumentNode?.OuterHtml ?? string.Empty;
                            pageContent = driver.FindElement(By.TagName("html"))?.Text ?? string.Empty;
                        }
                        catch (NullReferenceException)
                        {
                            throw new NoResultsException(pageContent, "Could not find results");
                        }
                        var nodes = driver.FindElements(By.ClassName("sh-dlr__list-result"));

                        if (nodes == null || !nodes.Any())
                        {
                            pageContent = driver.FindElement(By.TagName("html"))?.Text;
                            throw new NoResultsException(pageContent, "No search results on search page", true);
                        }
                        ////////////////////
                        //pageContent = document.DocumentNode?.OuterHtml ?? string.Empty;
                        pageContent = driver.FindElement(By.TagName("html"))?.Text ?? string.Empty;
                        //var sellerNodes = this.GetSellerNodes(document);

                        var documentHtml = new HtmlDocument();
                        documentHtml.LoadHtml(driver.PageSource);

                        var sellerNodes = GetSellerNodes(documentHtml, driver);
                        if (sellerNodes == null)
                        {
                            return;
                        }

                        ////////if (!string.IsNullOrEmpty(keyword.SearchTerm))
                        ////////{
                        ////////    searchResult.GroupId = this.GetGroupId(url);
                        ////////    searchResult.GroupPrice = GetGroupPrice(/*document*/driver.FindElement(By.TagName("html")));
                        ////////}

                        ////////    //var noSellersNode = document.DocumentNode.SelectSingleNode("//div[contains(@id, 'no-sellers') or contains(@id, 'os-nosellers')]");
                        ////////    var noSellersNode = driver.FindElement(By.Id("no-sellers"));
                        ////////    if (noSellersNode == null)
                        ////////    {
                        ////////      noSellersNode = driver.FindElement(By.Id("os-nosellers"));
                        ////////    }
                        ////////if (noSellersNode == null || (noSellersNode != null && (noSellersNode.GetAttribute("style")?.Contains("display:none") ?? false)))
                        ////////{
                        ////////        //pageContent = document.DocumentNode?.OuterHtml ?? string.Empty;
                        ////////        pageContent = driver.FindElement(By.TagName("html")).Text ?? string.Empty;
                        ////////        sellerNodes = GetSellerNodes(/*document*/ driver.FindElement(By.TagName("html")) , driver);
                        ////////}
                        ////////else
                        ////////{
                        ////////    return;
                        ////////}

                        foreach (var sellerNode in sellerNodes)
                        {
                            if (!(sellerNode.Text.Contains("Vendido por") || sellerNode.Text.Contains("Sold by") ||
     (sellerNode.Text.Contains(" stock ") && sellerNode.Text.Contains("Today:")) ||
      sellerNode.Text.Contains("Closed today") || sellerNode.Text.Contains("No matching stores") ||
      sellerNode.Text.Contains("No results") || sellerNode.Text.Contains("product could not be found")) &&
    ((sellerNode.Text.Contains("$") || (sellerNode.Text.Contains("USD") && !sellerNode.Text.Contains(" OFF")))
      && !(sellerNode.Text.Contains("NZ$") || sellerNode.Text.Contains("CA$") || sellerNode.Text.Contains("A$")
      || sellerNode.Text.Contains("hk$") || sellerNode.Text.Contains("£") || //!sellerNode.InnerText.Contains("Â")
         sellerNode.Text.Contains("ZAR") || sellerNode.Text.Contains("·") ||
         sellerNode.Text.Contains("CHF") || sellerNode.Text.Contains("AED") ||
         sellerNode.Text.Contains("₫") || sellerNode.Text.Contains("IDR") || sellerNode.Text.Contains("SGD"))))
                            {

                                var seller = GetSeller((IWebElement)sellerNode);
                                if (seller == null)
                                {
                                    ++onGroupPageRank;
                                    continue;
                                }

                                seller.OnGroupPageRank = ++onGroupPageRank;
                                searchResult.Sellers.Add(seller);
                            }
                        }
                    }
                    sellersPageNumber++;
                } while (driver.NextPageSelenium(PageType.Group, out nextPageUrl) && sellersPageNumber < PagesLimitSellers) ;     //  document.NextPage(PageType.Group, out nextPageUrl));

                    //////if (!string.IsNullOrEmpty(keyword.SearchTerm))
                    //////{
                    //////    var specs = this.GetGoogleSpecifications(url, /*document*/ driver, searchResult);

                    //////    mpnMatched = this.IsMpnMatched(searchResult.GoogleBrand, searchResult.GooglePartNumber);
                    //////}
                }                
            catch (InvalidScrapeValueException ex)
            {
                ex.PageText = pageContent;
                throw;
            }
            catch (NoResultsException ex)
            {
                ex.PageText = pageContent;
                throw;
            }
            catch (XPathCustomException ex)
            {
                ex.PageText = pageContent;
                throw;
            }
            catch (XPathException ex)
            {
                throw new XPathCustomException(pageContent, ex.Message);
            }
            catch (StopScrapingException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnhandledException(pageContent, ex.Message);
            }

            searchResult.MatchingGroup = this.GroupMatched(searchResult.Sellers, mpnMatched);
        }

        private void ScrapeGroup1(SearchResult searchResult, IWebDriver driver, string resultUrl)
        {
            bool mpnMatched = false;
            string pageContent = null;
            string nextPageUrl;
            int onGroupPageRank = 10;
            short sellerOnGroupPageRank = 0;
            HtmlDocument document = new HtmlDocument();
            //IEnumerable<HtmlNode> allSellerNodes = null;
            IReadOnlyCollection<IWebElement> allSellerNodes = null;
            var attempts = this.httpRetryAttempts;

            try
            {

                var url = $"https://{this.googleHostAddress}/shopping/product/{searchResult.GroupId}?q={HttpUtility.UrlEncode(keyword.SearchTerm)}";

                //////driver.Navigate().GoToUrl(url);

                //////    if (!this.DownloadPage(url, this.ValidateSearchPrice, ref attempts, out document))
                ////// !!!!!!!!!!!
                if (!this.DownloadPage(url, /*this.ValidateSearchPrice,*/ ref attempts, out driver))
                {
                    throw new CustomException("Group page loading failed");
                }


                //if (!this.DownloadPage(url, this.ValidateGroupPrice, ref attempts, out document))
                //{
                //    throw new CustomException("Group page loading failed");
                //}
                IReadOnlyCollection<IWebElement> TableProduct = null;
                try
                {
                    //pageContent = document.DocumentNode?.OuterHtml ?? string.Empty;
                    //////var TableProduct = driver.FindElements(By.Id("sh-osd__online-sellers-cont"));
                    TableProduct = driver.FindElements(By.ClassName("sh-osd__offer-row"));
                }
                catch { }
                //////searchResult.GroupPrice = GetGroupPrice(document);
                searchResult.GroupPrice = GetGroupPrice(TableProduct.FirstOrDefault());


                //////var noSellersNode = document.DocumentNode.SelectSingleNode("//div[contains(@id, 'no-sellers') or contains(@id, 'os-nosellers')]");
                ////////////var noSellersNode = driver.FindElement(By.XPath("//div[contains(@id, 'no-sellers') or contains(@id, 'os-nosellers')]"));
                //////////////if (noSellersNode != null && (noSellersNode.GetAttributeValue("style", null)?.Contains("display:none") ?? false))
                ////////////if (noSellersNode != null && (noSellersNode.GetAttribute("style")?.Contains("display:none") ?? false))
                ////////////{
                ////////////    return;
                ////////////}
                var documentHtml = new HtmlDocument();
                documentHtml.LoadHtml(driver.PageSource);
                ////var sellerNodes = this.GetSellerNodes(document, driver);
                var sellerNodes = this.GetSellerNodes(documentHtml, driver);

                if (sellerNodes == null)
                {
                    return;
                }
                var sellerNodesByOnGoogle = this.GetSellerNodesBuyOnGoogle(/*documentHtml*/ driver);
                //var sellerNodesByOnGoogle = this.GetSellerNodesBuyOnGoogle(TableProduct);

                //////allSellerNodes = (sellerNodesByOnGoogle != null ?
                //////    (IReadOnlyCollection<IWebElement>)sellerNodes.AsEnumerable().Concat((IReadOnlyCollection<IWebElement>)sellerNodesByOnGoogle.AsEnumerable()) :
                //////    (IReadOnlyCollection<IWebElement>)sellerNodes.AsEnumerable());
                allSellerNodes = (sellerNodesByOnGoogle != null ? sellerNodesByOnGoogle : (IReadOnlyCollection<IWebElement>)sellerNodes.AsEnumerable());

                var specs = this.GetGoogleSpecifications(resultUrl, /*document*/ driver, searchResult);

                mpnMatched = this.IsMpnMatched(searchResult.GoogleBrand, searchResult.GooglePartNumber);

                //HtmlDocument sellersDocument = new HtmlDocument();

                do
                {
                    //var isSellersEnd = document.DocumentNode.SelectSingleNode("//a[contains(text(),'Compare prices from')]");

                    if (sellerNodes.Count() < 10)
                    {
                        break;
                    }

                    url = $"https://{this.googleHostAddress}/shopping/product/{searchResult.GroupId}/online?q={HttpUtility.UrlEncode(keyword.SearchTerm)}&prds=start:{onGroupPageRank}";

                    if (!this.DownloadPage(url, /*ValidateSellersPage,*/ ref attempts, out /*sellersDocument*/driver))
                    {
                        throw new CustomException("Group page loading failed");
                    }
                    ///////////////
                    /////////////TableProduct = driver.FindElements(By.ClassName("sh-osd__offer-row"));
                    ///
                    documentHtml = new HtmlDocument();
                    documentHtml.LoadHtml(driver.PageSource);
                    ///////////////
                    var newSellerNodes = this.GetSellerNodes(/*sellersDocument*/ documentHtml, driver);

                    if (newSellerNodes == null)
                    {
                        throw new CustomException("SellerNodes on Sellers Group Page not found");
                    }

                    onGroupPageRank = onGroupPageRank + newSellerNodes.Count();

                    //////allSellerNodes = (IReadOnlyCollection<IWebElement>)allSellerNodes.Concat((IReadOnlyCollection<IWebElement>)newSellerNodes.AsEnumerable());
                    allSellerNodes = newSellerNodes != null ? newSellerNodes : allSellerNodes;
                }
                while (/*sellersDocument*/documentHtml.NextPage(PageType.Group, out nextPageUrl));    // driver.NextPageSelenium(PageType.Group, out nextPageUrl));

                foreach (var sellerNode in allSellerNodes)
                {
                    if (!(sellerNode.Text.Contains("Vendido por") || sellerNode.Text.Contains("Sold by") ||
                         (sellerNode.Text.Contains(" stock ") && sellerNode.Text.Contains("Today:")) ||
                          sellerNode.Text.Contains("Closed today") || sellerNode.Text.Contains("No matching stores") ||
                          sellerNode.Text.Contains("No results") || sellerNode.Text.Contains("product could not be found")) &&
                        ((sellerNode.Text.Contains("$") || (sellerNode.Text.Contains("USD") && !sellerNode.Text.Contains(" OFF")))
                          && !(sellerNode.Text.Contains("NZ$") || sellerNode.Text.Contains("CA$") || sellerNode.Text.Contains("A$")
                          || sellerNode.Text.Contains("hk$") || sellerNode.Text.Contains("£") || //!sellerNode.InnerText.Contains("Â")
                             sellerNode.Text.Contains("ZAR") || sellerNode.Text.Contains("·") ||
                             sellerNode.Text.Contains("CHF") || sellerNode.Text.Contains("AED") ||
                             sellerNode.Text.Contains("₫") || sellerNode.Text.Contains("IDR") || sellerNode.Text.Contains("SGD"))))
                    {
                        var seller = GetSeller(sellerNode);
                        if (seller == null)
                        {
                            ++sellerOnGroupPageRank;
                            continue;
                        }

                        seller.OnGroupPageRank = ++sellerOnGroupPageRank;
                        searchResult.Sellers.Add(seller);
                    }
                }
            }
            catch (InvalidScrapeValueException ex)
            {
                ex.PageText = pageContent;
                throw;
            }
            catch (NoResultsException ex)
            {
                ex.PageText = pageContent;
                throw;
            }
            catch (XPathCustomException ex)
            {
                ex.PageText = pageContent;
                throw;
            }
            catch (XPathException ex)
            {
                throw new XPathCustomException(pageContent, ex.Message);
            }
            catch (StopScrapingException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnhandledException(pageContent, ex.Message);
            }

            searchResult.MatchingGroup = this.GroupMatched(searchResult.Sellers, mpnMatched);
        }

        private void SetSpecifications(/*HtmlDocument document*/ IWebDriver webElement, SearchResult result)
        {
            try
            {
                //result.GoogleBrand = document.DocumentNode.SelectSingleNode("//*[text() = 'Brand']/following-sibling::*")?.GetInnerText();
                result.GoogleBrand = webElement.FindElement(By.XPath("//*[text() = 'Brand']/following-sibling::*"))?.Text;
            }
            catch { }
            try
            {
                //result.GooglePartNumber = document.DocumentNode.SelectSingleNode("//*[contains(text(), 'Part Number')]/following-sibling::*")?.GetInnerText();
                result.GooglePartNumber = webElement.FindElement(By.XPath("//*[contains(text(), 'Part Number')]/following-sibling::*"))?.Text;
            }
            catch { }
            try
            {
                //result.GoogleGtin = document.DocumentNode.SelectSingleNode("//*[contains(text(), 'GTIN')]/following-sibling::*")?.GetInnerText();
                result.GoogleGtin = webElement.FindElement(By.XPath("//*[contains(text(), 'GTIN')]/following-sibling::*"))?.Text;
            }
            catch { }
        }

        private void SetSpecificationsDownloaded(/*HtmlDocument document*/ IWebDriver webElement, SearchResult result)
        {
            IList<IWebElement> specs = null;
            try
            {
                //var specs = document.DocumentNode.SelectNodes("//table/tr/child::*[text() = 'Universal Product Identifiers']/parent::tr/following-sibling::tr");
                specs = webElement.FindElements(By.XPath("//table/tr/child::*[text() = 'Universal Product Identifiers']/parent::tr/following-sibling::tr"));
            }
            catch { }
            //if (specs == null && document.DocumentNode.OuterHtml.Contains("Universal Product Identifiers"))
            if (specs == null && webElement.FindElements(By.XPath("//table/*[Contains(text(),'Universal Product Identifiers')]")).FirstOrDefault().Text.Length > 0)
            {
                try
                {
                    //specs = document.DocumentNode.SelectNodes("//*/child::*[text() = 'Universal Product Identifiers']/following-sibling::*");
                    specs = webElement.FindElements(By.XPath("//*/child::*[text() = 'Universal Product Identifiers']/following-sibling::*"));
                }
                catch { }
                if (specs == null)
                {
                    var document = new HtmlDocument();
                    document.LoadHtml(webElement.PageSource);

                    //throw new NoResultsException(document.DocumentNode.OuterHtml, "Specifications not found", true);
                    throw new NoResultsException(document.Text, "Specifications not found", true);
                }
            }

            GetSpecificationsFromDownloaded(specs, result);
        }

        private void GetSpecificationsFromDownloaded(/*HtmlNodeCollection*/ IList<IWebElement> nodes, SearchResult result)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var value = nodes[i].Text;                    //.GetInnerText();
                if (value.Contains("Brand"))
                {
                    result.GoogleBrand = value?.Replace("Brand", string.Empty)?.Trim();
                }

                if (value.Contains("Part Number"))
                {
                    result.GooglePartNumber = value?.Replace("Part Numbers", string.Empty)?.Replace("Part Number", string.Empty)?.Trim();
                }

                if (value.Contains("GTIN"))
                {
                    result.GoogleGtin = value?.Replace("GTIN", string.Empty)?.Trim();
                }
            }
        }

        private static bool SpecificationsExist(/*HtmlDocument document*/ IWebDriver driver)
        {
            var documentHtml = new HtmlDocument();
            documentHtml.LoadHtml(driver.PageSource);
            return documentHtml.DocumentNode.OuterHtml.Contains("Universal Product Identifiers");
        }

        private bool GetGoogleSpecifications(string url, /*HtmlDocument document*/ IWebDriver driver, SearchResult result)
        {
            //SetSpecifications(document, result);
            SetSpecifications(driver, result);

            //////if (string.IsNullOrEmpty(result.GooglePartNumber))
            //////{
            //////    url = $"https://{googleHostAddress}/shopping/product/{result.GroupId}/specs?q={HttpUtility.UrlEncode(keyword.SearchTerm)}";
            //////    var attempts = this.httpRetryAttempts;
            //////    var proxy = this.proxyManager.GetProxy();
            //////    if (this.DownloadPage(url, /*(d) => { return ValidateEncoding(d); },*/ ref attempts, out driver))
            //////    {
            //////        if (SpecificationsExist(driver))
            //////        {
            //////            SetSpecificationsDownloaded(driver, result);
            //////        }
            //////    }
            //////}

            if (!string.IsNullOrEmpty(result.GoogleBrand))
            {
                var brand = Mapping.Brands.FirstOrDefault(m => m.GoogleName == result.GoogleBrand);

                if (brand != null)
                {
                    result.GoogleBrand = brand.Name;
                }
            }

            return true;
        }

        private bool IsMpnMatched(string brand, string mpn)
        {
            if (string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(mpn))
            {
                return false;
            }

            var keywordNormalized = new string(Array.FindAll(this.keyword.SearchTerm.ToCharArray(), char.IsLetterOrDigit)).ToLower();
            var mpnArray = mpn.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return mpnArray.Any(mpnItem => string.Equals(keywordNormalized, new string(Array.FindAll($"{brand.ToLower()}{mpnItem}".ToCharArray(), char.IsLetterOrDigit))));
        }

        private bool DownloadPage(string url, /*Func</*HtmlDocument*/ /*IWebDriver, bool> verifyPage,*/ ref int retryAttempts, out /*HtmlDocument document*/ IWebDriver driver)
        {
            bool success = true;
            var document = new HtmlDocument();

            //driver = new ChromeDriver(@"C:\Project\tools\Scrapers\GoogleShoppingScraper_Selenium");

            do
            {
                //////var proxy = this.proxyManager.GetProxy();
                //////if (this.currentProxy == null || success == false)
                //////{
                //if (success == false)
                //{
                //    driver.Quit;
                //}
                    var proxy = this.proxyManager.GetProxy();
                    this.currentProxy = proxy;
                    //if (success == false)
                    //{
                    //    Thread.Sleep(Rand.Next(1000, 5000));
                    //}
                    var options = new ChromeOptions();

                    Uri uri = new Uri(url);
                    // Add your HTTP-Proxy
                    options.AddHttpProxy(proxy.ToWebProxy().Address.Host,
                          proxy.ToWebProxy().Address.Port,
                          proxy.ToWebProxy().Credentials.GetCredential(uri, "Basic").UserName,
                          proxy.ToWebProxy().Credentials.GetCredential(uri, "Basic").Password);

                    options.AddArgument("ignore-certificate-errors");   // принятие сертификата веб-сайта при запуске браузера
                    //options.AddArgument("--headless");      // HIDE Chrome Browser
                    //options.addArguments("window-size=1800x900");
                    options.AddArgument("--disable-blink-features");
                    options.AddArgument("--disable-AutomationControl");
                    //options.AddArgument("excludeSwitches", "enable-automation");

                    /*IWebDriver */
                    driver = new ChromeDriver(@"C:\Project\tools\Scrapers\GoogleShoppingScraper_Selenium", options);
                    driver.Manage().Cookies.DeleteAllCookies();   // почистить куки браузера
                    driver.Manage().Window.Maximize();            // разворачивать окно браузера на весь экран
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);  //(120); //webdriver будет пытаться выполнить команды над контентом в установленный срок (10 секунд)

                ////////////////WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                ////////////////wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

                //////}
                //////driver.Navigate().Refresh();               // обновить страницу
                driver.Navigate().GoToUrl(url);               // перейти по указанному адресу
                                                              //driver.Navigate().Refresh();               // обновить страницу
                                                              //////}
                                                              //////finally
                                                              //////{
                                                              //////    driver.Quit();
                                                              //////}
                                                              //driver.Navigate().Refresh();
                                                              //Thread.Sleep(Rand.Next(1000, 5000));
                                                              //Thread.Sleep(10000);

                //////////var xxx = driver.FindElement(By.CssSelector(".sh-dlr__list-result"));
                ///////////////////IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector("#rso")));
                //////////var yyy = driver.FindElement(By.CssSelector("#rso")).GetAttribute("innerText");
                /////////////////IWebElement firstResult7 = wait.Until(e => e.FindElement(By.CssSelector("span[style='text-overflow:ellipsis']")));
                //////////string nnn = null;
                //////////try
                //////////{
                //////////    nnn = driver.FindElement(By.CssSelector("span[style='text-overflow:ellipsis']")).GetAttribute("innerText");
                //////////}
                //////////catch { }
                ////////////////////var locat = driver.FindElement(By.CssSelector("div[class^='sh-dr__g']")).Text;
                /////////////
                ////////////IWebElement firstResult2 = wait.Until(e => e.FindElement(By.XPath("//*/div[@title]")));
                ////////////var firstResult555 = firstResult2?.Text;

                
                    var documentHtml = new HtmlDocument();
                documentHtml.LoadHtml(driver.PageSource);

                //success = HttpRequest.Request(url, HttpMethod.Get, null, null, /*verifyPage,*/ proxy, /*out*/ /*document*/ driver) && /*document?.DocumentNode?.OuterHtml*/ document.Text != null;
                if (Auxiliary.VerifyPageSelenium("your search", driver) &&
                    Auxiliary.VerifyPageSelenium("did not match any", driver))
                {
                    ProxyHealthCollector.SetNoMatchingResults(proxy.Ip);
                    //return false;
                    success = false;
                }

                if (Auxiliary.VerifyPageSelenium("the product could not be found", driver))
                {
                    ProxyHealthCollector.SetProductNotFound(proxy.Ip);
                    //return false;
                    success = false;
                }

                if (Auxiliary.VerifyPageSelenium("server error", driver) &&
                    Auxiliary.VerifyPageSelenium("internal server error while processing your request", driver))
                {
                    ProxyHealthCollector.SetServerError(proxy.Ip);
                    //return false;
                    success = false;
                }
                ////////
                //var documentHtml = new HtmlDocument();
                //documentHtml.LoadHtml(driver.PageSource);

                if (Auxiliary.VerifyPageSelenium("your search", driver) &&
                    Auxiliary.VerifyPageSelenium("did not match any", driver))
                {
                    this.keyword.NoMatchingResults = true;
                    throw new NoResultsException(documentHtml.DocumentNode.OuterHtml, "This page did not match any documents");
                }

                if (Auxiliary.VerifyPageSelenium("server error", driver) &&
                    Auxiliary.VerifyPageSelenium("internal server error while processing your request", driver))
                {
                    this.keyword.ServerError = true;
                    throw new NoResultsException(documentHtml.DocumentNode.OuterHtml, "Internal server error");
                }

            if (documentHtml?.DocumentNode == null)
            {
                throw new NoResultsException("Document not loaded");
                }

                success = this.ValidateSearchPrice(driver);
                if (success == false)
                {
                    continue;
                }

                var pageContent = documentHtml.DocumentNode?.OuterHtml ?? string.Empty;

            var resultNodes = this.GetSearchResultNodes(driver);
            if (resultNodes == null || !resultNodes.Any())
            {
                throw new NoResultsException(pageContent, "No search results on search page", true);
            }
            var StringResponse = documentHtml.DocumentNode.SelectSingleNode("//body//*[not(name()='script') and " +
                    " (contains(@id,'online-sellers-grid') or " +
                    "contains(@class,'card-section') or " +
                    "contains(@class,'product-result') or " +
                    "contains(@ids,'search') or " +
                    "contains(@data-sh-or,'price'))]");
            if (StringResponse != null)
            {
                if (StringResponse.InnerText.Contains("Closed today") ||
                    StringResponse.InnerText.Contains("Google Shopping:") ||
                    StringResponse.InnerText.Contains("No matching stores") ||
                    StringResponse.InnerText.Contains("no matching stores") ||
                    StringResponse.InnerText.Contains("No results") ||
                    StringResponse.InnerText.Contains("no results") ||
                    StringResponse.InnerText.Contains("did not match any"))      //||
                                                                                 //!(StringResponse.InnerText.Contains("$") || StringResponse.InnerText.Contains("USD")))
                {
                    ProxyHealthCollector.SetNoMatchingResults(proxy.Ip);
                        //return false;
                        success = false;
                        continue;
                    }
                if ((StringResponse.InnerText.Contains("NZ$") || StringResponse.InnerText.Contains("CA$") ||
                    StringResponse.InnerText.Contains("A$") || StringResponse.InnerText.Contains("hk$") ||
                    StringResponse.InnerText.Contains("£") || StringResponse.InnerText.Contains("ZAR") ||
                    StringResponse.InnerText.Contains("₫") || StringResponse.InnerText.Contains("IDR") ||
                    (StringResponse.InnerText.Contains(" stock ") && StringResponse.InnerText.Contains("Today:"))) &&
                    StringResponse.SelectSingleNode("//td[4]") == null)
                {
                    ProxyHealthCollector.SetNoMatchingResults(proxy.Ip);
                        //return false;
                        success = false;
                        continue;
                    }

            }
            else
            {
                if (pageContent.Contains("Closed today") ||
                    pageContent.Contains("Google Shopping:") ||
                    pageContent.Contains("No matching stores") ||
                    pageContent.Contains("no matching stores") ||
                    pageContent.Contains("No results") ||
                    pageContent.Contains("no results") ||
                    pageContent.Contains("did not match any")) //||
                                                                  //!(responseString.Contains("$") || responseString.Contains("USD")))
                {
                    ProxyHealthCollector.SetNoMatchingResults(proxy.Ip);
                        //return false;
                        success = false;
                        continue;
                    }
                if ((pageContent.Contains("NZ$") || pageContent.Contains("CA$") ||
                    pageContent.Contains("A$") || pageContent.Contains("hk$") ||
                    pageContent.Contains("£") || pageContent.Contains("ZAR") ||
                    pageContent.Contains("₫") || pageContent.Contains("IDR") ||
                    (pageContent.Contains(" stock ") && pageContent.Contains("Today:"))) &&
                    document.DocumentNode.SelectSingleNode("//td[4]") == null)
                {
                    ProxyHealthCollector.SetNoMatchingResults(proxy.Ip);
                        //return false;
                        success = false;
                        continue;
                    }

            }

            if (pageContent.Contains("The product could not be found") ||
                pageContent.Contains("No se pudo encontrar el producto"))
            {
                ProxyHealthCollector.SetProductNotFound(proxy.Ip);
                    //return false;
                    success = false;
                    continue;
                }

            /////////////////////
            retryAttempts--;
                //if (success = false)
                //{
                //    driver.Quit;
                //}
            } while (!success && retryAttempts > 0);

            return success;
        }

        private static bool ValidateEncoding(/*HtmlDocument*/ IWebDriver /*document*/ driver)
        {
            //if (document.DocumentNode.OuterHtml.Contains("个") || document.DocumentNode?.SelectSingleNode("html")?.GetAttributeValue("lang", null) == "zh-HK")
            if (driver.FindElement(By.TagName("html")).Text.Contains("个") || driver.FindElement(By.TagName("html"))?.GetAttribute("lang") == "zh-HK")
            {
                return false;
            }

            return true;
        }

        private bool ValidateSearchPrice(/*HtmlDocument document*/ IWebDriver driver)
        {
            string priceText = null;
            if (!ValidateEncoding(driver))
            {
                return false;
            }
            try
            {
                //////var documentHtml = new HtmlDocument();
                //////documentHtml.LoadHtml(driver.PageSource);

                //var location = documentHtml.DocumentNode.SelectSingleNode("//*/div[@title]")?.InnerText;
                string location = null;
                try
                {
                    location = driver.FindElement(By.XPath("//div[@title]"))?.Text;
                }
                catch { }
                if (location == null || string.IsNullOrEmpty(location) ||
                    location?.Substring(0, 1) == "&")
                {
                    try
                    {
                        location = driver.FindElement(By.XPath("//div[@ClassName^='sh-dr__g']"))?.Text;
                    }
                    catch { }
                    try
                    {
                        //location = documentHtml.DocumentNode.SelectSingleNode("//*[@id='hnnlTc']/div[1]/p/span[2]")?.InnerText;
                        location = driver.FindElement(By.XPath("//*[@id='hnnlTc']/div[1]/p/span[2]"))?.Text;
                    }
                    catch { }
                    if (location == null || string.IsNullOrEmpty(location) ||
                        location?.Substring(0, 1) == "&")
                    {
                        try
                        {
                            //location = documentHtml.DocumentNode.SelectSingleNode("//*[@*='fbar']/div[1]/div/span")?.InnerText;
                            location = driver.FindElement(By.XPath("//*[@*='fbar']/div[1]/div/span"))?.Text;
                        }
                        catch { }
                        if (location == null || string.IsNullOrEmpty(location) ||
                            location?.Substring(0, 1) == "&")
                        {
                            try
                            {
                                //location = documentHtml.DocumentNode.SelectSingleNode("//*[contains(text(), 'Location:')]/following-sibling::div")?.InnerText;
                                location = driver.FindElement(By.XPath("//*[contains(text(), 'Location:')]/following-sibling::div"))?.Text;
                            }
                            catch { }
                            if (location == null || string.IsNullOrEmpty(location) ||
                                location?.Substring(0, 1) == "&")
                            {
                                try
                                {
                                    //location = documentHtml.DocumentNode.SelectSingleNode("//*[@*='rcnt']/div[3]/div/div/div[1]//span[2]")?.InnerText;
                                    location = driver.FindElement(By.XPath("//*[@*='rcnt']/div[3]/div/div/div[1]//span[2]"))?.Text;
                                }
                                catch { }
                                if (location == null || string.IsNullOrEmpty(location) ||
                                    location?.Substring(0, 1) == "&")
                                {
                                    try
                                    {
                                        //location = documentHtml.DocumentNode.SelectSingleNode("//*/div[1]/p/span[2]")?.InnerText;
                                        location = driver.FindElement(By.XPath("//*/div[1]/p/span[2]"))?.Text;
                                    }
                                    catch { }
                                    if (location == null || string.IsNullOrEmpty(location) ||
                                        location?.Substring(0, 1) == "&")
                                    {
                                        try
                                        {
                                            //location = documentHtml.DocumentNode.SelectSingleNode("//*/div[3]/div/div/div[1]/div/div/div/div/span[2]")?.InnerText;
                                            location = driver.FindElement(By.XPath("//*/div[3]/div/div/div[1]/div/div/div/div/span[2]"))?.Text;
                                        }
                                        catch { }
                                        if (location == null || string.IsNullOrEmpty(location) ||
                                            location?.Substring(0, 1) == "&")
                                        {
                                            try
                                            {
                                                //location = documentHtml.DocumentNode.SelectSingleNode("//*[contains(text(), 'Your location')]")?.InnerText;
                                                location = driver.FindElement(By.XPath("//*[contains(text(), 'Your location')]"))?.Text;
                                            }
                                            catch { }
                                            if (location == null || string.IsNullOrEmpty(location) ||
                                                location?.Substring(0, 1) == "&")
                                            {
                                                try
                                                {
                                                    //location = documentHtml.DocumentNode.SelectSingleNode("//*/c-wiz[2]/div/div/div[1]/div/div/div/div[2]/footer/div[1]/div/span[2]")?.InnerText;
                                                    location = driver.FindElement(By.XPath("//*/c-wiz[2]/div/div/div[1]/div/div/div/div[2]/footer/div[1]/div/span[2]"))?.Text;
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //var location = locationNode?.Text;
                if (string.IsNullOrWhiteSpace(location) || this.allowedLocations.All(l => !location.Contains(l)))
                {
                    return false;
                }

                var nodes = this.GetSearchResultNodes(driver);
                if (nodes == null || !nodes.Any())
                {
                    return false;        //return true;
                }
                string priceTotal = null;
                try
                {
                    priceTotal = nodes.FirstOrDefault().FindElement(By.ClassName("puehic"))?.Text;
                    priceText = GetSearchSellerPriceText(priceTotal);
                }
                catch { }
                if (priceText == null || !priceTotal.Contains("$"))
                {
                    try
                    {
                        priceTotal = nodes.FirstOrDefault().FindElement(By.XPath("./td[4]"))?.Text;
                        priceText = GetSearchSellerPriceText(priceTotal);
                    }
                    catch { }
                }
                if (priceText == null || !priceTotal.Contains("$"))
                { 
                    try
                    {
                        priceTotal = nodes.FirstOrDefault().Text;
                        priceText = GetSearchSellerPriceText(priceTotal);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            if (string.IsNullOrEmpty(priceText))
            {
                return false;
            }

            if (priceText.IndexOf("ca$", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("hk$", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("NZ$", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("A$", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("CA$", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("£", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("ZAR", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("CHF", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("AED", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("SGD", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("₫", StringComparison.CurrentCultureIgnoreCase) > -1
                || priceText.IndexOf("IDR", StringComparison.CurrentCultureIgnoreCase) > -1)

            /*&& priceText.IndexOf("us", StringComparison.InvariantCultureIgnoreCase) < 0*/
            {
                return false;
            }

            return true;
        }

        private bool ValidateGroupPrice(/*HtmlDocument document*/ IWebDriver driver)
        {
            if (!ValidateEncoding(driver))
            {
                return false;
            }

            //var noSellersNode = document.DocumentNode.SelectSingleNode("//div[@id = 'no-sellers']");
            //if (noSellersNode != null)
            //{
            //    return !noSellersNode.GetAttributeValue("style", null)?.Contains("display:none") ?? false;
            //}
            IWebElement locationNode = null;
            IWebElement locationNodeNot = null;
            try
            {
                locationNode = driver.FindElement(By.XPath("//*(contains(text(), 'Your location') or contains(text(),'United States'))]"));
            }
            catch { }
            if (locationNode == null)
            {
                try
                {
                    locationNode = driver.FindElement(By.XPath("//*[(Contains(text(),'Closed today') or Contains(text(),' stock ') and Contains(text(),'Today:')) or Contains(text(),'No matching stores'))]"));
                }
                catch { }
                if (locationNode == null)
                {
                    try
                    {
                        locationNode = driver.FindElement(By.XPath("//c-wiz[2]/div/div/div[1]/div/div/div/div[2]/footer/div[1]/div/span[2]"));
                    }
                    catch { }
                    if (locationNode == null)
                    {
                        try
                        {
                            locationNode = driver.FindElement(By.XPath("//div[1]/p/span[2]"));
                        }
                        catch { }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    locationNodeNot = driver.FindElement(By.XPath("//*" +
                        " [contains(text(),'Closed today') or (contains(text(),' stock ') and contains(text(),'Today:')) or" +
                        " contains(text(),'No matching stores'))]"));
                }
                catch { }
                if (locationNodeNot != null)
                {
                    return false;
                }

            }
            var location = locationNode?.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(location) && this.allowedLocations.All(l => !location.Contains(l)))
            {
                return false;
            }
            //******************
            string locationTd = null;
            string locationDivSpan = null;
            try
            {
                locationTd = driver.FindElement(By.XPath("//td[3]"))?.Text;
            }
            catch { }
            try
            {
                locationDivSpan = driver.FindElement(By.XPath("//section[contains(@name,'GLOBAL__sellers')]//div|span[(contains(text(),'$')" +
                    " or (contains(text(),'USD') and not(contains(text(),' OFF'))))" +
                         " and not(contains(text(),' OFF')" +
                                " or contains(text(),'CA$')" +
                                " or contains(text(),'NZ$')" +
                                " or contains(text(),'A$')" +
                                " or contains(text(),'hk$')" +
                                " or contains(text(),'($')" +
                                " or contains(text(),'CHF')" +
                                " or contains(text(),'AED')" +
                                " or contains(text(),'SGD')" +
                                " or contains(text(),'₫')" +
                                " or contains(text(),'IDR')" +
                                " or contains(text(),'ZAR'))]"))?.Text;
                // " or contains(text(),'Â') or contains(text(),'·')" 
            }
            catch { }
            if ((locationTd == null && locationDivSpan == null) ||
                (locationTd != null && (/*locationTd.Contains("delivery") ||*/
                locationTd.Contains("Vendido por") || locationTd.Contains("Sold by") ||
                (locationTd.Contains(" stock ") && locationTd.Contains("Today:")) ||
                locationTd.Contains("Closed today") || locationTd.Contains("No matching stores") ||
                locationTd.Contains("no matching stores") ||
                locationTd.Contains("No results") || locationTd.Contains("no results") ||
                locationTd.Contains("product could not be found") ||
                locationTd.Contains("($") ||
                locationTd.Contains("₫") || locationTd.Contains("IDR") ||
                locationTd.Contains("CA$") || locationTd.Contains("NZ$") || locationTd.Contains("A$") ||
                locationTd.Contains("hk$") || //locationTd.Contains("Â") || locationTd.Contains("·") ||
                locationTd.Contains("ZAR") || locationTd.Contains("CHF") || locationTd.Contains("AED"))) ||
                (locationDivSpan != null && (locationDivSpan.Contains("delivery") ||
                locationDivSpan.Contains("Vendido por") || locationDivSpan.Contains("Sold by") ||
                (locationDivSpan.Contains(" stock ") && locationDivSpan.Contains("Today:")) ||
                locationDivSpan.Contains("Closed today") || locationDivSpan.Contains("No matching stores") ||
                locationDivSpan.Contains("no matching stores") ||
                locationDivSpan.Contains("No results") || locationDivSpan.Contains("no results") ||
                locationDivSpan.Contains("product could not be found") ||
                locationDivSpan.Contains("($") ||
                locationDivSpan.Contains("₫") || locationDivSpan.Contains("IDR") ||
                locationDivSpan.Contains("CA$") || locationDivSpan.Contains("NZ$") || locationDivSpan.Contains("A$") ||
                locationDivSpan.Contains("hk$") || //locationDivSpan.Contains("Â") || locationDivSpan.Contains("·") ||
                locationDivSpan.Contains("ZAR") || locationDivSpan.Contains("CHF") || locationDivSpan.Contains("AED") ||
                locationDivSpan.Contains("SGD"))))
            {
                try
                {
                    locationDivSpan = driver.FindElement(By.XPath("//*[contains(*,'Sold by')]//div|span[(contains(text(),'$')" +
                      " or (contains(text(),'USD') and not(contains(text(),' OFF'))))" +
                      " and not(contains(text(),' OFF')" +
                      " or contains(text(),'CA$')" +
                      " or contains(text(),'NZ$')" +
                      " or contains(text(),'A$')" +
                      " or contains(text(),'hk$')" +
                      " or contains(text(),'($')" +
                      " or contains(text(),'CHF')" +
                      " or contains(text(),'AED')" +
                      " or contains(text(),'SGD')" +
                      " or contains(text(),'₫')" +
                      " or contains(text(),'IDR')" +
                      " or contains(text(),'ZAR')))]"))?.Text;
                }
                catch { }
                if (locationDivSpan == null ||
                   (locationDivSpan != null && (/*locationDivSpan.Contains("delivery") ||*/
                   locationDivSpan.Contains("Vendido por") ||
                   (locationDivSpan.Contains(" stock ") && locationDivSpan.Contains("Today:")) ||
                   locationDivSpan.Contains("Closed today") || locationDivSpan.Contains("No matching stores") ||
                   locationDivSpan.Contains("no matching stores") ||
                   locationDivSpan.Contains("No results") || locationDivSpan.Contains("no results") ||
                   locationDivSpan.Contains("product could not be found") ||
                   locationDivSpan.Contains("($") || locationDivSpan.Contains("₫") || locationDivSpan.Contains("IDR") ||
                   locationDivSpan.Contains("CA$") || locationDivSpan.Contains("NZ$") || locationDivSpan.Contains("A$") ||
                   locationDivSpan.Contains("hk$") || locationDivSpan.Contains("SGD") ||
                   locationDivSpan.Contains("ZAR") || locationDivSpan.Contains("CHF") || locationDivSpan.Contains("AED"))))
                {
                    return false;
                }
            }
            //******************
            string priceText;
            try
            {
                priceText = GetGroupPriceText(driver.FindElement(By.TagName("html")))?.ToLower();
            }
            catch (Exception ex)
            {
                return true;
            }

            if (string.IsNullOrEmpty(priceText))
            {
                return true;
            }

            //if (priceText.Contains("hk") || priceText.Contains("ca") || !priceText.Contains("$") && !priceText.Contains("us"))
            if ((priceText.Contains("hk$") || priceText.Contains("ca$") || priceText.Contains("NZ$") ||
                 priceText.Contains("A$") || priceText.Contains("CA$") ||
                 priceText.Contains("£") || priceText.Contains("ZAR") || priceText.Contains("CHF") || priceText.Contains("AED") ||
                 priceText.Contains("SGD") || priceText.Contains("₫") || priceText.Contains("IDR"))
                && !priceText.Contains("us") && !priceText.Contains("$"))
            {
                return false;
            }

            return true;
        }
    }
}
