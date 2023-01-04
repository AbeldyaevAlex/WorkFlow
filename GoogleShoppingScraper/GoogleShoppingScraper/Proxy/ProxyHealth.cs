namespace GoogleShoppingScraper.Proxy
{
    using System;

    public class ProxyHealth
    {
        private readonly object locker = new object();

        public ProxyHealth(string ip)
        {
            this.Ip = ip;
            this.Used = 0;
            this.Banned = 0;
            this.WrongCurrency = 0;
            this.SpeedSumMilliseconds = 0;
            this.NoMatchingResults = 0;
            this.ServerError = 0;
            this.ProductNotFound = 0;
            this.LastUsed = null;
        }

        public string Ip { get; }

        public int Used { get; private set; }

        public int Banned { get; private set; }

        public int WrongCurrency { get; private set; }

        public int NoMatchingResults { get; private set; }

        public long SpeedSumMilliseconds { get; private set; }

        public int ServerError { get; private set; }

        public int ProductNotFound { get; private set; }

        public DateTime? LastUsed { get; set; }

        public void SetUsed(long requestTimeoutMilliseconds)
        {
            lock (this.locker)
            {
                this.LastUsed = DateTime.UtcNow;
                this.Used++;
                this.SpeedSumMilliseconds += requestTimeoutMilliseconds;
            }
        }

        public void SetBanned()
        {
            lock (this.locker)
            {
                this.Banned++;
            }
        }

        public void SetWrongCurrency()
        {
            lock (this.locker)
            {
                this.WrongCurrency++;
            }
        }

        public void SetNoMatchingResults()
        {
            lock (this.locker)
            {
                this.NoMatchingResults++;
            }
        }

        public void SetServerError()
        {
            lock (this.locker)
            {
                this.ServerError++;
            }
        }

        public void SetProductNotFound()
        {
            lock (this.locker)
            {
                this.ProductNotFound++;
            }
        }

        public ProxyHealth GetCopyAndReset()
        {
            lock (this.locker)
            {
                var copy = new ProxyHealth(this.Ip)
                {
                    Used = this.Used,
                    Banned = this.Banned,
                    WrongCurrency = this.WrongCurrency,
                    NoMatchingResults = this.NoMatchingResults,
                    SpeedSumMilliseconds = this.SpeedSumMilliseconds,
                    LastUsed = this.LastUsed,
                    ServerError = this.ServerError,
                    ProductNotFound = this.ProductNotFound
                };

                this.Used = 0;
                this.Banned = 0;
                this.WrongCurrency = 0;
                this.NoMatchingResults = 0;
                this.SpeedSumMilliseconds = 0;
                this.ServerError = 0;
                this.ProductNotFound = 0;

                return copy;
            }
        }
    }
}
