namespace GoogleShoppingScraper.Proxy
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    public class RoundConcurrentQueue<T> : ConcurrentQueue<T>
    {
        public RoundConcurrentQueue(IEnumerable<T> collection)
            : base(collection)
        {
        }

        protected T Dequeue()
        {
            T obj;
            while (!this.TryDequeue(out obj))
            {
                Thread.Sleep(5);
            }

            this.Enqueue(obj);
            return obj;
        }
    }
}
