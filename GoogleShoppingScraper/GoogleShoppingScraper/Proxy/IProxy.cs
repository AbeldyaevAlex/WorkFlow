namespace GoogleShoppingScraper.Proxy
{
    using System;
    using System.Net;

    public interface IProxy
    {
        string Ip { get; }

        DateTime BannedUntilUtc { get; }

        int DelayBetweenUsingSeconds { get; }

        bool IsBanned();

        void SetBan();

        void SetUsed();

        WebProxy ToWebProxy();
    }
}
