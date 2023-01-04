namespace GoogleShoppingScraper.Proxy
{
    using System;
    using System.Net;

    public class Proxy : IProxy
    {
        public Proxy(string ip, int port, string userName, string password, int delayBetweenUsingSeconds, int banSeconds)
        {
            this.Ip = ip;
            this.Port = port;
            this.UserName = userName;
            this.Password = password;
            this.BanSeconds = banSeconds;
            this.BannedUntilUtc = DateTime.UtcNow;
            this.DelayBetweenUsingSeconds = delayBetweenUsingSeconds;
        }

        public string Ip { get; }

        public int Port { get; }

        public string UserName { get; }

        public string Password { get; }

        public DateTime BannedUntilUtc { get; private set; }

        public int DelayBetweenUsingSeconds { get; }

        private int BanSeconds { get; }

        public bool IsBanned()
        {
            return this.BannedUntilUtc > DateTime.UtcNow;
        }

        public void SetBan()
        {
            this.BannedUntilUtc = DateTime.UtcNow.AddSeconds(this.BanSeconds);
        }

        public void SetUsed()
        {
            this.BannedUntilUtc = DateTime.UtcNow.AddSeconds(this.DelayBetweenUsingSeconds);
        }

        public WebProxy ToWebProxy()
        {
            var webProxy = new WebProxy(this.Ip, this.Port);
            if (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password))
            {
                webProxy.Credentials = new NetworkCredential(this.UserName, this.Password);
            }

            return webProxy;
        }
    }
}
