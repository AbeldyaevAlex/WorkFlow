namespace GoogleShoppingScraper.Scraping.ScrapingManager
{
    using System.Collections;
    using System.Threading;

    public class TaskProducer
    {
        private readonly Synchronizer synchronizer;
        private readonly Settings settings;

        public TaskProducer(Synchronizer synchronizer, Settings settings)
        {
            this.synchronizer = synchronizer;
            this.settings = settings;
            this.TotalProduced = 0;
        }

        public int TotalProduced { get; private set; }

        public void Run()
        {
            while (!this.synchronizer.ExitEvent.WaitOne(0, false))
            {
                Thread.Sleep(500);

                if (!this.synchronizer.BufferLoading && !this.synchronizer.BufferFlushing && this.synchronizer.AwaitingBuffer.Count <= this.settings.KeywordsLoadingBufferSize * (this.settings.BufferLoadingBoundaryPercent / 100f))
                {
                    this.synchronizer.LoadBufferEvent.Set();
                }

                lock (((ICollection)this.synchronizer.ProcessingBuffer).SyncRoot)
                {
                    while (!this.synchronizer.AwaitingBuffer.IsEmpty && this.synchronizer.ProcessingBuffer.Count < this.settings.MaxDegreeOfTasks)
                    {
                        Keyword keyword;
                        while (!this.synchronizer.AwaitingBuffer.TryDequeue(out keyword))
                        {
                            Thread.Sleep(10);
                        }

                        this.synchronizer.ProcessingBuffer.Add(keyword);
                        this.synchronizer.NewTaskEvent.Set();
                        this.TotalProduced++;
                    }
                }
            }
        }
    }
}
