namespace GoogleShoppingScraper.Scraping.ScrapingManager
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    public class Synchronizer
    {
        private readonly object locker = new object();
        private bool bufferLoading;
        private bool bufferFlushing;

        public Synchronizer()
        {
            this.NewTaskEvent = new AutoResetEvent(false);
            this.LoadBufferEvent = new AutoResetEvent(false);
            this.TaskCompletedEvent = new AutoResetEvent(false);
            this.ExitEvent = new ManualResetEvent(false);
            this.AwaitingBuffer = new ConcurrentQueue<Keyword>();
            this.ProcessingBuffer = new List<Keyword>();
            this.CompletedBuffer = new ConcurrentQueue<Keyword>();
            this.BufferLoading = false;
            this.BufferFlushing = false;
        }

        public EventWaitHandle ExitEvent { get; }

        public EventWaitHandle NewTaskEvent { get; }

        public EventWaitHandle LoadBufferEvent { get; }

        public EventWaitHandle TaskCompletedEvent { get; }

        public ConcurrentQueue<Keyword> AwaitingBuffer { get; }

        public ICollection<Keyword> ProcessingBuffer { get; }

        public ConcurrentQueue<Keyword> CompletedBuffer { get; }

        public int LastSavingFailedPercent { get; set; }

        public bool BufferLoading
        {
            get
            {
                lock (this.locker)
                {
                    return this.bufferLoading;
                }
            }
            set
            {
                lock (this.locker)
                {
                    this.bufferLoading = value;
                }
            }
        }

        public bool BufferFlushing
        {
            get
            {
                lock (this.locker)
                {
                    return this.bufferFlushing;
                }
            }
            set
            {
                lock (this.locker)
                {
                    this.bufferFlushing = value;
                }
            }
        }
    }
}
