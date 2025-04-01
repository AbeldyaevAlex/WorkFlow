namespace Asu.Core.Domain.FreshdeskTickets
{
    using System.ComponentModel;

    public static class Extensions
    {
        public static string GetName(this RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Get:
                    return "GET";
                case RequestMethod.Post:
                    return "POST";
                case RequestMethod.Put:
                    return "PUT";
                case RequestMethod.Delete:
                    return "DELETE";
            }

            throw new InvalidEnumArgumentException("method", (int)method, typeof(RequestMethod));
        }
    }
}
