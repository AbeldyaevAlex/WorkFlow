namespace GoogleShoppingScraper.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RoundQueueProxyManager : RoundConcurrentQueue<IProxy>, IProxyManager
    {
        private readonly List<IProxy> proxies;

        public RoundQueueProxyManager(IEnumerable<IProxy> collection) : base(collection)
        {
            this.proxies = collection.ToList();
        }

        public int Total => this.proxies.Count;

        public int Available => this.proxies.Count(i => !i.IsBanned());

        public int Banned => this.proxies.Count(i => i.IsBanned() && (i.BannedUntilUtc - DateTime.UtcNow).TotalSeconds > i.DelayBetweenUsingSeconds);

        public IProxy GetProxy()
        {
            IProxy proxy;
            do
            {
                proxy = this.Dequeue();
                if (proxy == null)
                {
                    return null;
                }
            }
            while (proxy.IsBanned());

            proxy.SetUsed();
            return proxy;
        }
    }
}