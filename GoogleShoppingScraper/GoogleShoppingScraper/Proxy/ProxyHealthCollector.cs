namespace GoogleShoppingScraper.Proxy
{
    using System.Collections.Concurrent;
    using System.Linq;

    using Properties;

    using Repository;

    using Scraping;

    public static class ProxyHealthCollector
    {
        private static readonly ConcurrentDictionary<string, ProxyHealth> Proxies = new ConcurrentDictionary<string, ProxyHealth>();

        public static void SetUsed(string ip, long requestTimeoutMilliseconds)
        {
            GetOrAdd(ip).SetUsed(requestTimeoutMilliseconds);
        }

        public static void SetBanned(string ip)
        {
            GetOrAdd(ip).SetBanned();
        }

        public static void SetWrongCurrency(string ip)
        {
            GetOrAdd(ip).SetWrongCurrency();
        }

        public static void SetNoMatchingResults(string ip)
        {
            GetOrAdd(ip).SetNoMatchingResults();
        }

        public static void SetServerError(string ip)
        {
            GetOrAdd(ip).SetServerError();
        }

        public static void SetProductNotFound(string ip)
        {
            GetOrAdd(ip).SetProductNotFound();
        }

        public static bool FlushHealthStatistic()
        {
            var proxies = Proxies.Values.Select(i => i.GetCopyAndReset());
            using (var context = new DatabaseContext(Settings.Default.ScraperConnectionString))
            {
                string errorMessage;
                return context.SaveProxiesStatistic(proxies.ToTableType().ToDataTable(), out errorMessage, Settings.Default.SqlRetryAttempts);
            }
        }

        private static ProxyHealth GetOrAdd(string ip)
        {
            return Proxies.GetOrAdd(ip, new ProxyHealth(ip));
        }
    }
}
