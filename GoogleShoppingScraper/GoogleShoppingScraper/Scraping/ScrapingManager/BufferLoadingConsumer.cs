namespace GoogleShoppingScraper.Scraping.ScrapingManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Errors;

    using Repository;

    public class BufferLoadingConsumer
    {
        private readonly Synchronizer synchronizer;
        private readonly WaitHandle[] eventArray;
        private readonly Logger logger;
        private readonly Settings settings;

        public BufferLoadingConsumer(Synchronizer synchronizer, Logger logger, Settings settings)
        {
            this.synchronizer = synchronizer;
            this.logger = logger;
            this.settings = settings;
            this.eventArray = new WaitHandle[2];
            this.eventArray[0] = this.synchronizer.ExitEvent;
            this.eventArray[1] = this.synchronizer.LoadBufferEvent;
        }

        public void Run()
        {
            while (WaitHandle.WaitAny(this.eventArray) != 0)
            {
                if (this.synchronizer.BufferFlushing)
                {
                    Thread.Sleep(10);
                    continue;
                }

                this.synchronizer.BufferLoading = true;

                #region ForDebug

                /*this.synchronizer.AwaitingBuffer.Enqueue(new Keyword { SearchTerm = "Dorman Steel Wheel 15\" Honda Accord 03-07 939-147" });
                this.synchronizer.BufferLoading = false;
                return;*/

                #endregion

                var keywords = this.LoadKeywords();
                foreach (var keyword in keywords)
                {
                    this.synchronizer.AwaitingBuffer.Enqueue(keyword);
                }

                this.synchronizer.BufferLoading = false;
            }
        }

        private IEnumerable<Keyword> LoadKeywords()
        {
            try
            {
                List<int> processingBuffer;
                lock (((ICollection)this.synchronizer.ProcessingBuffer).SyncRoot)
                {
                    processingBuffer = this.synchronizer.ProcessingBuffer.Select(i => i.ProductId).ToList();
                }

                var bufferedKeywords = processingBuffer
                    .Union(from i in this.synchronizer.CompletedBuffer select i.ProductId)
                    .Union(from i in this.synchronizer.AwaitingBuffer select i.ProductId)
                    .Select(i => i)
                    .Distinct().ToArray();

                using (var context = new DatabaseContext(this.settings.ConnectionString))
                {
                    string errorMessage;
                    var results = context.LoadKeywords(bufferedKeywords.ToDataTable(), this.settings.KeywordsLoadingBufferSize, this.synchronizer.LastSavingFailedPercent, out errorMessage, this.settings.SqlRetryAttempts);
                    if (results == null)
                    {
                        this.logger.SendMail($"Error when load keywords. Message: {errorMessage}");
                        return new List<Keyword>();
                    }

                    return results.Select(i => new Keyword { ProductId = i.ProductId, SearchTerm = i.Keywords }).AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                this.logger.SendMail($"{ex.Message}\r\n{ex.StackTrace}");
                return new Keyword[0];
            }
        }
    }
}
