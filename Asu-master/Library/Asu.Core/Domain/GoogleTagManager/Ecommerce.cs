using System.Collections.Generic;
using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class Ecommerce
    {
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("impressions")]
        public List<Impression> Impressions { get; set; }
    }
}
