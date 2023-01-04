namespace GoogleShoppingScraper.Repository.UserDefinedTypes
{
    using System;

    public class Proxy
    {
        public string Ip { get; set; }

        public int Used { get; set; }

        public int Banned { get; set; }

        public int WrongCurrency { get; set; }

        public int NoMatchingResults { get; set; }

        public int ServerError { get; set; }

        public int ProductNotFound { get; set; }

        public long SpeedSumMilliseconds { get; set; }

        public DateTime? LastUsed { get; set; }
    }
}
