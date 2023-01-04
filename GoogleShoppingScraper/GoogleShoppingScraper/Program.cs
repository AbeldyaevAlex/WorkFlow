namespace GoogleShoppingScraper
{
    using System;
    using System.Linq;
    using System.Threading;

    using Errors;
    using OpenQA.Selenium;
    using Proxy;
    using Repository;
    using Scraping;
    using Scraping.ScrapingManager;

    using Settings = Properties.Settings;

    public class Program
    {
        private static readonly DateTime Started = DateTime.UtcNow;
        private static ScrapingManager manager;
        private static IProxyManager proxyManager;
        private static Logger logger;
        public IWebDriver driver;
        public static void Main(string[] args)
        {
            logger = new Logger(Settings.Default.HtmlPagesPath, Settings.Default.MailTo, Settings.Default.MailSubject);
            InitProxies();
            try
            {
                //var settings = new Scraping.ScrapingManager.Settings(Settings.Default.ScraperConnectionString)
                //{
                //    KeywordsLoadingBufferSize = Settings.Default.KeywordsLoadingBufferSize,
                //    BufferLoadingBoundaryPercent = Settings.Default.BufferLoadingBoundaryPercent,
                //    KeywordsFlushingBufferSize = Settings.Default.KeywordsFlushingBufferSize,
                //    MaxDegreeOfTasks = Settings.Default.MaxDegreeOfTasks,
                //    HttpRetryAttempts = Settings.Default.HttpRetryAttempts,
                //    SqlRetryAttempts = Settings.Default.SqlRetryAttempts
                //};

                //var keyword = new Keyword();
                //keyword.SearchTerm = "Pro Comp K1175T";
                //var scrape = new WebCrawler(proxyManager, keyword, Settings.Default.HttpRetryAttempts);
                //scrape.ScrapeData();
                //return;

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

                var settings = new Scraping.ScrapingManager.Settings(Settings.Default.ScraperConnectionString)
                {
                    KeywordsLoadingBufferSize = Settings.Default.KeywordsLoadingBufferSize,
                    BufferLoadingBoundaryPercent = Settings.Default.BufferLoadingBoundaryPercent,
                    KeywordsFlushingBufferSize = Settings.Default.KeywordsFlushingBufferSize,
                    MaxDegreeOfTasks = Settings.Default.MaxDegreeOfTasks,
                    HttpRetryAttempts = Settings.Default.HttpRetryAttempts,
                    SqlRetryAttempts = Settings.Default.SqlRetryAttempts
                };

                //var keyword = new Keyword();
                //keyword.SearchTerm = "Pro Comp K1175T";
                //var scrape = new WebCrawler(proxyManager, keyword, Settings.Default.HttpRetryAttempts);
                //scrape.ScrapeData();
                //return;

                var scraperManager = new ScrapingManager(proxyManager, logger, settings);
                manager = scraperManager;
                scraperManager.Start();
                var statisticsThread = new Thread(ShowStatistics);
                statisticsThread.Start();

                do
                {
                    var key = Console.ReadKey().KeyChar;
                    if (key == 'q' || key == 'Q')
                    {
                        break;
                    }
                }
                while (true);

                scraperManager.Stop();
                statisticsThread.Join();
            }
            catch (Exception ex)
            {
                logger.SendMail($"{ex.Message}\r\n{ex.StackTrace}");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        private static void ShowStatistics()
        {
            var reportSent = true;
            while (!manager.ExitEvent.WaitOne(0, false))
            {
                Console.Clear();
                Console.ResetColor();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Elapsed: \t\t{(DateTime.UtcNow - Started).ToReadableString()}\r\n");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Buffer to handle: \t{manager.BufferedToHandle}\t({Settings.Default.KeywordsLoadingBufferSize})");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Buffer to save: \t{manager.BufferedToSave}\t({Settings.Default.KeywordsFlushingBufferSize})");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Processing: \t\t{manager.Processing}\t({Settings.Default.MaxDegreeOfTasks})");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Processed: \t\t{manager.TotalProcessed}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Total saved: \t\t");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{manager.TotalSaved}\r\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Total failed: \t\t");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{manager.TotalFailed}\r\n");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Proxies: \t\t{proxyManager.Total} / ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{proxyManager.Available}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" / ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{proxyManager.Banned}\r\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Speed: \t\t\t{(int)(manager.TotalProcessed / (DateTime.UtcNow - Started).TotalMinutes)} sku/min");
                Console.WriteLine();

                if (manager.BufferLoading)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Keywords loading...");
                }

                if (manager.BufferFlushing)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Keywords saving...");
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press 'Q' key to quit...");

                if (!reportSent && DateTime.UtcNow.Minute % 10 == 0 && (DateTime.UtcNow - Started).TotalMinutes > 5)
                {
                    /*if (manager.BufferedToSave > Settings.Default.KeywordsFlushingBufferSize * 2)
                    {
                        logger.SendMail("Too many results in the buffer to save.");
                    }

                    if (manager.BufferedToHandle == 0)
                    {
                        logger.SendMail("No keywords in the buffer to handle.");
                    }

                    if (manager.Processing == 0)
                    {
                        logger.SendMail("No keywords in the processing queue.");
                    }*/

                    reportSent = true;
                }

                if (DateTime.UtcNow.Minute % 10 != 0)
                {
                    reportSent = false;
                }

                Thread.Sleep(2000);
            }
        }

        private static void InitProxies()
        {
            using (var context = new DatabaseContext(Settings.Default.ScraperConnectionString))
            {
                var proxies = from p in context.Proxies
                              where p.IsActive
                              select new Proxy.Proxy(p.Ip, p.Port, p.UserName, p.Password, Settings.Default.ProxyDelayBetweenRequestsSeconds, Settings.Default.ProxyBanSeconds);

                proxyManager = new RoundQueueProxyManager(proxies.Shuffle().ToArray());
            }
        }
    }
}
