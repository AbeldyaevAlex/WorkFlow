namespace GoogleShoppingScraper.Request
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    using GoogleShoppingScraper.Misc;
    using GoogleShoppingScraper.Scraping;

    using HtmlAgilityPack;
    using OpenQA.Selenium;
    using Proxy;

    public static class HttpRequest
    {
        private static readonly Random Rnd = new Random();
        private const int Timeout = 30000;
        private static readonly string[] UserAgents = 
        {
            //"Mozilla/5.0 (compatible; MSIE 7.0; Windows NT 6.1)",
            //"Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 6.2)",
            //"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.3)",
            //"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 10.0)",
            //"Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36",
            //"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36",
            //"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36"
        };

        private static HttpWebRequest GetRequest(string url, HttpMethod method, NameValueCollection headers, CookieContainer cookie, IProxy proxy)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Accept = "text/html; charset=UTF-8";
            request.UserAgent = UserAgents[Rnd.Next(UserAgents.Length)];
            request.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;
            request.Method = method.Method;
            request.CookieContainer = cookie;

            if (headers != null && headers.Count > 0)
            {
                request.Headers.Add(headers);
            }

            if (proxy != null)
            {
                request.Proxy = proxy.ToWebProxy();
            }

            return request;
        }
        
        //public static bool Request(string url, HttpMethod method, CookieContainer cookie, NameValueCollection headers, /*Func<HtmlDocument, bool> verifyPage,*/ IProxy proxy, /*out*/ /*HtmlDocument*/ /*document*/ IWebDriver driverWeb)
        //{
            //document = new HtmlDocument();

            //////var request = GetRequest(url, method, headers, cookie, proxy);

            // TODO: review HTTP status code handling logic
            //////var responseTimer = new Stopwatch();
            //var readTimer = new Stopwatch();

            //try
            //{
                //responseTimer.Start();
                //using (var response = (HttpWebResponse)request.GetResponse()) 
                //{
                //    if (response.StatusCode != HttpStatusCode.OK)
                //    {
                //        responseTimer.Stop();
                //        if (proxy != null)
                //        {
                //            ProxyHealthCollector.SetUsed(proxy.Ip, responseTimer.ElapsedMilliseconds);
                //        }

                //        return false;
                //    }

                //    using (var responseStream = response.GetResponseStream())
                //    {
                //        if (responseStream == null)
                //        {
                //            responseTimer.Stop();
                //            if (proxy != null)
                //            {
                //                ProxyHealthCollector.SetUsed(proxy.Ip, responseTimer.ElapsedMilliseconds);
                //            }

                //            return false;
                //        }

                        //using (var readStream = response.CharacterSet == null  ? new StreamReader(responseStream) : new StreamReader(responseStream, Encoding.GetEncoding(response.CharacterSet)))
                        //{
                        //    var responseString = readStream.ReadToEnd();
                        //    responseTimer.Stop();
                        //    document = responseString.BuildHtmlDocument();
                        //    if (proxy != null)
                        //    {
                        //        ProxyHealthCollector.SetUsed(proxy.Ip, responseTimer.ElapsedMilliseconds);

                                //if (Auxiliary.VerifyPage("your search", /*responseString*/  driverWeb) &&
                                //    Auxiliary.VerifyPage("did not match any",/* responseString*/  driverWeb))
                                //{
                                //    ProxyHealthCollector.SetNoMatchingResults(proxy.Ip);
                                //    return false;
                                //}

                                //if (Auxiliary.VerifyPage("the product could not be found", /* responseString*/  driverWeb))
                                //{
                                //    ProxyHealthCollector.SetProductNotFound(proxy.Ip);
                                //    return false;
                                //}

                                //if (Auxiliary.VerifyPage("server error", /* responseString*/  driverWeb) &&
                                //    Auxiliary.VerifyPage("internal server error while processing your request", /* responseString*/  driverWeb))
                                //{
                                //    ProxyHealthCollector.SetServerError(proxy.Ip);
                                //    return false;
                                //}

                //if (verifyPage != null)
                //{
                    //if (!verifyPage.Invoke(document))
                    //{
                    //    ProxyHealthCollector.SetWrongCurrency(proxy.Ip);
                    //    return false;
                    //}
                //}
            //}

            //                return true;
            //            }
                //    }
                //}
            //}
            //catch (WebException ex)
            //{
            //    responseTimer.Stop();
            //    proxy?.SetBan();
            //    if (proxy != null)
            //    {
            //        ProxyHealthCollector.SetUsed(proxy.Ip, responseTimer.ElapsedMilliseconds);
            //        ProxyHealthCollector.SetBanned(proxy.Ip);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    responseTimer.Stop();
            //    if (proxy != null)
            //    {
            //        ProxyHealthCollector.SetUsed(proxy.Ip, responseTimer.ElapsedMilliseconds);
            //    }

            //    throw new Exception(ex.Message);
            //}

            //return false;
        //}
    }
}
