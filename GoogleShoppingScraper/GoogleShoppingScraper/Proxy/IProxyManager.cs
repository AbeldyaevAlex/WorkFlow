namespace GoogleShoppingScraper.Proxy
{
    public interface IProxyManager
    {
        int Total { get; }

        int Available { get; }

        int Banned { get; }

        IProxy GetProxy();
    }
}
