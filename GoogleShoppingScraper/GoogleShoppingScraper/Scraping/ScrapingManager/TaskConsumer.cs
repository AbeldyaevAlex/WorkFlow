namespace GoogleShoppingScraper.Scraping.ScrapingManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Errors;
    using GoogleShoppingScraper.Misc;
    using Proxy;

    public class TaskConsumer
    {
        private readonly Synchronizer synchronizer;
        private readonly WaitHandle[] eventArray;
        private readonly List<Task> tasks;
        private readonly IProxyManager proxyManager;
        private readonly Logger logger;
        private readonly Settings settings;

        public TaskConsumer(IProxyManager proxyManager, Synchronizer synchronizer, Logger logger, Settings settings)
        {
            this.tasks = new List<Task>();
            this.synchronizer = synchronizer;
            this.eventArray = new WaitHandle[2];
            this.eventArray[0] = this.synchronizer.ExitEvent;
            this.eventArray[1] = this.synchronizer.NewTaskEvent;
            this.proxyManager = proxyManager;
            this.logger = logger;
            this.settings = settings;
            this.TotalConsumed = 0;
        }

        public int TotalConsumed { get; private set; }

        public void Run()
        {
            while (WaitHandle.WaitAny(this.eventArray) != 0)
            {
                lock (((ICollection)this.synchronizer.ProcessingBuffer).SyncRoot)
                {
                    if (this.synchronizer.ProcessingBuffer.All(i => i.Processing))
                    {
                        continue;
                    }

                    var keywords = this.synchronizer.ProcessingBuffer.Where(i => !i.Processing).ToList();
                    lock (((ICollection)this.tasks).SyncRoot)
                    {
                        foreach (var keyword in keywords)
                        {
                            keyword.Processing = true;
                            var task = Task.Factory.StartNew(() => this.Scrape(keyword), TaskCreationOptions.LongRunning);
                            task.ContinueWith(this.Dequeue);
                            this.tasks.Add(task);
                        }
                    }
                }
            }
        }

        private Keyword Scrape(Keyword keyword)
        {
            try
            {
                var webCrawler = new WebCrawler(this.proxyManager, keyword, this.settings.HttpRetryAttempts);
                webCrawler.ScrapeData();
            }
            catch (CustomException ex)
            {
                this.logger.AddLog(keyword, ex);
            }
            catch (Exception ex)
            {
                this.logger.AddLog(keyword, ex);
            }

            return keyword;
        }

        private void Dequeue(Task<Keyword> task)
        {
            task.Result.Completed = true;
            lock (((ICollection)this.synchronizer.ProcessingBuffer).SyncRoot)
            {
                this.synchronizer.ProcessingBuffer.Remove(task.Result);
                task.Result.Processing = false;
            }

            this.synchronizer.CompletedBuffer.Enqueue(task.Result);
            this.synchronizer.TaskCompletedEvent.Set();

            lock (((ICollection)this.tasks).SyncRoot)
            {
                this.TotalConsumed++;
                this.tasks.Remove(task);
            }
        }
    }
}
