namespace GoogleShoppingScraper.Scraping.ScrapingManager
{
    using System.Threading;

    using Errors;

    using Proxy;

    public class ScrapingManager
    {
        private readonly Synchronizer synchronizer;
        private readonly TaskConsumer taskConsumer;
        private readonly TaskProducer taskProducer;
        private readonly BufferLoadingConsumer bufferLoadingConsumer;
        private readonly BufferFlushingConsumer bufferFlushingConsumer;
        private Thread taskProducerThread;
        private Thread taskConsumerThread;
        private Thread bufferLoadingThread;
        private Thread bufferFlushingThread;

        public ScrapingManager(IProxyManager proxyManager, Logger logger, Settings settings)
        {
            this.synchronizer = new Synchronizer();
            this.taskConsumer = new TaskConsumer(proxyManager, this.synchronizer, logger, settings);
            this.taskProducer = new TaskProducer(this.synchronizer, settings);
            this.bufferLoadingConsumer = new BufferLoadingConsumer(this.synchronizer, logger, settings);
            this.bufferFlushingConsumer = new BufferFlushingConsumer(this.synchronizer, logger, settings);
        }

        public int TotalProcessed => this.taskConsumer.TotalConsumed;

        public int BufferedToHandle => this.synchronizer.AwaitingBuffer.Count;

        public int BufferedToSave => this.synchronizer.CompletedBuffer.Count;

        public int Processing => this.synchronizer.ProcessingBuffer.Count;

        public int TotalSaved => this.bufferFlushingConsumer.TotalSaved;

        public int TotalFailed => this.bufferFlushingConsumer.TotalFailed;

        public bool BufferLoading => this.synchronizer.BufferLoading;

        public bool BufferFlushing => this.synchronizer.BufferFlushing;

        public EventWaitHandle ExitEvent => this.synchronizer.ExitEvent;

        public void Start()
        {
            this.taskProducerThread = new Thread(this.taskProducer.Run);
            this.taskConsumerThread = new Thread(this.taskConsumer.Run);
            this.bufferLoadingThread = new Thread(this.bufferLoadingConsumer.Run);
            this.bufferFlushingThread = new Thread(this.bufferFlushingConsumer.Run);
            this.taskProducerThread.Start();
            this.taskConsumerThread.Start();
            this.bufferLoadingThread.Start();
            this.bufferFlushingThread.Start();
        }

        public void Stop()
        {
            this.synchronizer.ExitEvent.Set();

            this.taskProducerThread.Join();
            this.taskConsumerThread.Join();
            this.bufferLoadingThread.Join();
            this.bufferFlushingThread.Join();
        }
    }
}
