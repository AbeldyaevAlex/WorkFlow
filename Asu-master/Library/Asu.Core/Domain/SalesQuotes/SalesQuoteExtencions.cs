namespace Asu.Core.Domain.SalesQuotes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public static class SalesQuoteExtencions
    {
        public static string ComputeHash(this SalesQuote quote)
        {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes($"{quote.Id}-{quote.Email}")).ToSeparatedList(i => i.ToString("X"), string.Empty);
        }

        private static string ToSeparatedList<T>(this IEnumerable<T> collection, Func<T, string> selector, string separator, string empty = "")
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            collection = collection.ToArray();
            return collection.Any() ? collection.Select(selector).Aggregate((a, i) => $"{a}{separator}{i}") : empty;
        }
    }
}
